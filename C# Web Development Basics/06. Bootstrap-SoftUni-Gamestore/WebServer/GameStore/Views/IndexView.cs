namespace HTTPServer.GameStore.Views
{
    using Server.Contracts;

    public class IndexView : IView
    {
        private readonly string htmlFile;

        public IndexView(string htmlFile)
        {
            this.htmlFile = htmlFile;
        }

        public string View()
        {
            return this.htmlFile;
        }
    }
}