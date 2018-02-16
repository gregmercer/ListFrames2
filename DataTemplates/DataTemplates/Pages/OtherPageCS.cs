using System;

using Xamarin.Forms;

namespace DataTemplates.Pages
{
    public class OtherPageCS : ContentPage
    {
        ListView listView = new ListView(ListViewCachingStrategy.RecycleElement) { };

        public OtherPageCS()
        {
            Title = "Other Page";
            Icon = "csharp.png";

            Label labelView = new Label() { Text = "Hi There." };

            Content = new StackLayout
            {
                Padding = new Thickness(0, 0, 0, 0),
                WidthRequest = 300,
                Children = {
                    labelView
                }
            };
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }
    }
}