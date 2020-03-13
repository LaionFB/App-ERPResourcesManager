using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ERPResourcesManager
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Home : ContentPage
	{
		public Home ()
		{
			InitializeComponent ();
        }
        private void Button_Clicked(object sender, EventArgs e)
        {
            Search();
        }
        async void Search()
        {
            await Navigation.PushAsync(new Search());
        }

        async void Logout()
        {
            var tokens = await App.Database.GetAuthTokensAsync();
            foreach(var token in tokens)
                await App.Database.DeleteAuthTokenAsync(token);
            await Application.Current.MainPage.Navigation.PopToRootAsync();
        }
        private void Button_Clicked_1(object sender, EventArgs e)
        {
            Logout();
        }
    }
}