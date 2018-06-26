namespace SimpleMvc.Framework.ViewEngine.Generics
{
    using Interfaces.Generics;
    using System;

    public class ActionResult<T> : IActionResult<T>
    {
        public ActionResult(string viewFullQualifedName, T model)
        {
            this.Action = (IRenderable<T>)Activator
                .CreateInstance(Type.GetType(viewFullQualifedName));

            this.Action.Model = model;
        }

        public string Invoke()
        {
            return this.Action.Render();
        }

        public IRenderable<T> Action { get; set; }
    }
}