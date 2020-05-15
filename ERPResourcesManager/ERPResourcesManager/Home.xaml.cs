using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Mobile;

namespace ERPResourcesManager
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Home : ContentPage
	{
		public Home ()
		{
			InitializeComponent ();
            NavigationPage.SetHasNavigationBar(this, false);
        }
        private void Button_Clicked(object sender, EventArgs e)
        {
            Search();
        }
        private async void Button2_Clicked(object sender, EventArgs e)
        {
            try
            {
                var scanner = new ZXing.Mobile.MobileBarcodeScanner();

                var result = await scanner.Scan();

                if (result != null)
                    await Navigation.PushAsync(new Details(result.Text));

            } catch(Exception err)
            {
                await DisplayAlert("Erro", err.Message, "OK");
            }
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