namespace SimpleMvc.Framework.ViewEngine
{
    using Interfaces;
    using System;

    public class ActionResult : IActionResult
    {
        public ActionResult(string viewFullQualifedName)
        {
            this.Action = (IRenderable)Activator
                .CreateInstance(Type.GetType(viewFullQualifedName));
        }

        public string Invoke()
        {
            return this.Action.Render();
        }

        public IRenderable Action { get; set; }
    }
}