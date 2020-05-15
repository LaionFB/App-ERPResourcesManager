using Notes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using ZXing.Mobile;

namespace ERPResourcesManager
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            CheckIfLoggedIn();
        }
        private async void CheckIfLoggedIn()
        {
            var tokens = await App.Database.GetAuthTokensAsync();
            if(tokens.Count > 0)
                await Navigation.PushAsync(new Home());
        }
        private void Button_Clicked(object sender, EventArgs e)
        {
            Login();
        }
        async void Login()
        {
            try
            {
                var token = Services.HttpService.Login(username.Text, password.Text);
                var authToken = new AuthToken() { Token = token };
                await App.Database.SaveAuthTokenAsync(authToken);
                await Navigation.PushAsync(new Home());
            }
            catch (Exception e)
            {
                await DisplayAlert("Erro", e.Message, "OK");
            }
        }
    }
}
