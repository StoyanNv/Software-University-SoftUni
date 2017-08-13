namespace _02.Graphic_Editor
{
    public class Program
    {
        public static void Main()
        {
            var shape = new Circle();
            var editor = new GraphicEditor();
            editor.DrawShape(shape);
        }
    }
}