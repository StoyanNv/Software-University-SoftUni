namespace SimpleMvc.Framework.Interfaces.Generics
{
    public interface IActionResult<T> : IInvocable
    {
        IRenderable<T> Action { get; set; }
    }
}