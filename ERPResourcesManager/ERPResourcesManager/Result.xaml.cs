using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ERPResourcesManager
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Result : ContentPage
	{
        private List<dynamic> list = new List<dynamic>();
        private ObservableCollection<string> names = new ObservableCollection<string>();
        public Result (string cod, string name, string position)
		{
			InitializeComponent ();

            var template = new DataTemplate(typeof(TextCell));
            template.SetValue(TextCell.TextColorProperty, Color.Black);
            template.SetBinding(TextCell.TextProperty, ".");
            lv1.ItemTemplate = template;
            
            try
            {
                Services.HttpService.SearchAsync(cod, name, position).ContinueWith(x => {
                    list = x.Result;
                    foreach (var item in list)
                        names.Add(item["name"].ToString());
                });
            }
            catch (Exception e)
            {
                DisplayAlert("Erro", e.Message, "OK");
            }
            lv1.ItemsSource = names;
        }
        private void Button_Clicked(object sender, EventArgs e)
        {
            var index = names.IndexOf(lv1.SelectedItem.ToString());
            var id = list[index]["id"];
            Details((int)id);
        }
        async void Details(int id)
        {
            await Navigation.PushAsync(new Details(id));
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
    }
}