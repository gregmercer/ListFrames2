﻿using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using DataTemplates.ViewModels;
using DataTemplates.Pages;

[assembly:XamlCompilation (XamlCompilationOptions.Compile)]
namespace DataTemplates
{
	public class App : Application
	{
        public static RoomsViewModel RoomsViewModel { get; set; }

		public App ()
		{
            RoomsViewModel = new RoomsViewModel();
            RoomsViewModel.NumberSlots = 5;

            MainPage = new NavigationPage(new HomePageCS());
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}

