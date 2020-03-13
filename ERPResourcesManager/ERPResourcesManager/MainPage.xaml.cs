using Notes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

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
            var token = GenRandomString(32);//simula geração de um token no backend
            var authToken = new AuthToken() { Token = token };
            await App.Database.SaveAuthTokenAsync(authToken);
            await Navigation.PushAsync(new Home());
        }

        private static string GenRandomString(int size)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, size)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            return result;
        }
    }
}
