using System.Windows;
using Raven.Client.Embedded;

namespace View
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static EmbeddableDocumentStore DocumentStore = new EmbeddableDocumentStore()
            {
                DataDirectory = "Data",
                UseEmbeddedHttpServer = true
            };

        public App()
        {
            DocumentStore.Initialize();
        }
    }
}
