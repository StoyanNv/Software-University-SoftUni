namespace SimpleMvc.Framework.Routers
{
    using Attributes.Methods;
    using Controllers;
    using Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using WebServer.Contracts;
    using WebServer.Exceptions;
    using WebServer.Http.Contracts;
    using WebServer.Http.Response;
    using HttpStatusCode = WebServer.Enums.HttpStatusCode;

    public class ControllerRouter : IHandleable
    {
        private IDictionary<string, string> getParams;
        private IDictionary<string, string> postParams;
        private string requestMethod;
        private string controllerName;
        private string actionName;
        private object[] methodParams;

        public IHttpResponse Handle(IHttpRequest request)
        {
            this.getParams = new Dictionary<string, string>(request.UrlParameters);
            this.postParams = new Dictionary<string, string>(request.FormData);
            this.requestMethod = request.Method.ToString().ToUpper();
            PrepareControllerAndActionNames(request);
            MethodInfo method = this.GetMethod();
            if (method == null)
            {
                return new NotFoundResponse();
            }

            var parameters = method.GetParameters();
            this.methodParams = new object[parameters.Length];

            int index = 0;

            foreach (ParameterInfo param in parameters)
            {
                if (param.ParameterType.IsPrimitive ||
                    param.ParameterType == typeof(string))
                {
                    object value = this.getParams[param.Name];
                    this.methodParams[index] = Convert.ChangeType(
                        value, param.ParameterType);
                    index++;
                }
                else
                {
                    Type bindingModelType = param.ParameterType;
                    object bindingModel =
                        Activator.CreateInstance(bindingModelType);

                    IEnumerable<PropertyInfo> properties
                        = bindingModelType.GetProperties();

                    foreach (PropertyInfo property in properties)
                    {
                        property.SetValue(
                            bindingModel,
                            Convert.ChangeType(
                                postParams[property.Name],
                                property.PropertyType
                                )
                            );
                    }
                    this.methodParams[index] = Convert.ChangeType(
                        bindingModel,
                        bindingModelType);

                    index++;
                }
            }
            IInvocable actionResult = (IInvocable)this.GetMethod().Invoke(this.GetController(), this.methodParams);
            string content = actionResult.Invoke();

            IHttpResponse response = new ContentResponse(HttpStatusCode.Ok, content);
            return response;
        }

        private MethodInfo GetMethod()
        {
            MethodInfo method = null;

            foreach (MethodInfo methodInfo in this.GetSuitableMethods())
            {
                var attributes = methodInfo
                    .GetCustomAttributes()
                    .Where(a => a is HttpMethodAttribute)
                    .Cast<HttpMethodAttribute>();
                if (!attributes.Any() && this.requestMethod == "GET")
                {
                    return methodInfo;
                }
                foreach (HttpMethodAttribute attribute in attributes)
                {
                    if (attribute.IsValid(this.requestMethod))
                    {
                        return methodInfo;
                    }
                }
            }
            return method;
        }

        private void PrepareControllerAndActionNames(IHttpRequest request)
        {
            var pathParts = request.Path.Split(
                new[] { '/', '?' },
                StringSplitOptions.RemoveEmptyEntries);

            if (pathParts.Length < 2)
            {
                if (request.Path == "/")
                {
                    this.controllerName = "HomeController";
                    this.actionName = "Index";

                    return;
                }
                else
                {
                    BadRequestException.ThrowFromInvalidRequest();
                }
            }
            var cotrollerFirstChar = pathParts[0].ToUpper()[0];
            var actionFirstChar = pathParts[1].ToUpper()[0];
            this.controllerName = $"{cotrollerFirstChar}{pathParts[0].Substring(1)}{MvcContext.Get.ControllersSuffix}";
            this.actionName = actionFirstChar + pathParts[1].Substring(1);
        }

        private IEnumerable<MethodInfo> GetSuitableMethods()
        {
            var controller = this.GetController();
            if (controller == null)
            {
                return new MethodInfo[0];
            }
            return this.GetController()
                .GetType()
                .GetMethods()
                .Where(m => m.Name.ToLower() == this.actionName.ToLower());
        }

        private Controller GetController()
        {
            var controllerFullQualifiedName = string.Format(
                "{0}.{1}.{2}, {0}",
                MvcContext.Get.AssemblyName,
                MvcContext.Get.ControllersFolder,
                this.controllerName);

            Type type = Type.GetType(controllerFullQualifiedName);
            if (type == null)
            {
                return null;
            }
            var controller = (Controller)Activator.CreateInstance(type);
            return controller;
        }
    }
}