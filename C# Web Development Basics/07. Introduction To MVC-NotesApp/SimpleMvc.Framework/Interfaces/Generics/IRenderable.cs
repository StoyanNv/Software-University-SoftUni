namespace SimpleMvc.Framework.Interfaces.Generics
{
    public interface IRenderable<T> : IRenderable
    {
        T Model { get; set; }
    }
}