﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ERPResourcesManager
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Search : ContentPage
	{
		public Search ()
		{
			InitializeComponent ();
        }
        private void Button_Clicked(object sender, EventArgs e)
        {
            Result();
        }
        async void Result()
        {
            await Navigation.PushAsync(new Result());
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