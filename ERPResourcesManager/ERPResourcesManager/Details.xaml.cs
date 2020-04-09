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
	public partial class Details : ContentPage
	{
        private dynamic product;
		public Details (int id)
		{
			InitializeComponent();
            Services.HttpService.GetByIdAsync(id).ContinueWith(x => {
                product = x.Result;
                name.Text = x.Result["name"].ToString();
                description.Text = x.Result["desc"].ToString();
                position.Text = x.Result["position"].ToString();
                qtd.Text = ((int)x.Result["qtd"]).ToString();
            });
        }

        async void Logout()
        {
            var tokens = await App.Database.GetAuthTokensAsync();
            foreach (var token in tokens)
                await App.Database.DeleteAuthTokenAsync(token);
            await Application.Current.MainPage.Navigation.PopToRootAsync();
        }
        private void Button_Clicked_1(object sender, EventArgs e)
        {
            Logout();
        }
        private async void Button_Clicked_2(object sender, EventArgs e)
        {
            product["position"] = position.Text;
            product["qtd"] = Convert.ToInt32(qtd.Text);

            try
            {
                await Services.HttpService.Save(product);
                await DisplayAlert("Sucesso", "Item salvo com sucesso", "OK");
            }
            catch(Exception ex)
            {
                await DisplayAlert("Erro", ex.Message, "OK");
            }
        }
    }
}