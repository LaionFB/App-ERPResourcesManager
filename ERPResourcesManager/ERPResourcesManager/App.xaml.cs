using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AuthTokens.Data;
using System.IO;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace ERPResourcesManager
{
    public partial class App : Application
    {
        static AuthTokenDatabase database;

        public static AuthTokenDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new AuthTokenDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Notes.db3"));
                }
                return database;
            }
        }

        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
