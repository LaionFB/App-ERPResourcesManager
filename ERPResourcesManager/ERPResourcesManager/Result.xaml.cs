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
	public partial class Result : ContentPage
	{
		public Result ()
		{
			InitializeComponent ();

            var template = new DataTemplate(typeof(TextCell));
            template.SetValue(TextCell.TextColorProperty, Color.Black);
            template.SetBinding(TextCell.TextProperty, ".");
            lv1.ItemTemplate = template;

            List<String> itens = new List<String>()
            {
                "Parafuso M90", "Parafuso M95", "Parafuso M100"
            };
            lv1.ItemsSource = itens;
        }
        private void Button_Clicked(object sender, EventArgs e)
        {
            Details();
        }
        async void Details()
        {
            await Navigation.PushAsync(new Details());
        }

        async void Logout()
        {
            await Application.Current.MainPage.Navigation.PopToRootAsync();
        }
        private void Button_Clicked_1(object sender, EventArgs e)
        {
            Logout();
        }
    }
}