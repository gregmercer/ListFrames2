using System;
using System.Collections.Generic;

using Platform = Xamarin.Forms.PlatformConfiguration;

using Xamarin.Forms;

using DataTemplates.Views;

namespace DataTemplates.Pages
{
    public class FramesPageCS : ContentPage
    {
        ListView listView = new ListView() { };

        public FramesPageCS()
        {
            Title = "Frames";
            Icon = "csharp.png";

            CreateContent();
        }

        public Command AddTimeSlot {
            get {
                return new Command (() => {
                    App.RoomsViewModel.NumberSlots = 6;
                    App.RoomsViewModel.GetRoomsCommand.Execute(null);
                });
            }
        }

        private void CreateContent()
        {
            // Rooms DataTemplate

            var roomsDataTemplate = new DataTemplate(() => {

                var stackLayout = new StackLayout();

                var indexLabel = new Label { IsVisible = false };
                var nameLabel = new Label { FontAttributes = FontAttributes.Bold };

                indexLabel.SetBinding(Label.TextProperty, "Index");
                nameLabel.SetBinding(Label.TextProperty, "Name");

                stackLayout.Children.Add(indexLabel);
                stackLayout.Children.Add(nameLabel);                  

                var timeSlotsLayout = new TimeSlotsWrapLayout();
                timeSlotsLayout.Padding = 20;
                timeSlotsLayout.WidthRequest = 300;
                timeSlotsLayout.HorizontalOptions = LayoutOptions.Start;
                timeSlotsLayout.SetBinding(TimeSlotsWrapLayout.TimeSlotsSourceProperty, "TimeSlots");

                stackLayout.Children.Add(timeSlotsLayout);

                var viewCell = new ViewCell
                {
                    View = stackLayout
                };

                return viewCell;

            });

            // Rooms ListView

            //listView = new ListView()
            listView = new ListView //(ListViewCachingStrategy.RecycleElement)
            { 
                ItemTemplate = roomsDataTemplate, 
                HasUnevenRows = true,
                SeparatorColor = Color.Transparent,
            };
            listView.HasUnevenRows = true;

            listView.Margin = new Thickness(10);
            listView.SetBinding(ListView.ItemsSourceProperty, "Rooms", BindingMode.Default);

            Content = new StackLayout
            {
                Padding = new Thickness(0, 0, 0, 0),
                WidthRequest = 300,
                Children = {
                    listView,
                }
            };

            BindingContext = App.RoomsViewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            App.RoomsViewModel.GetRoomsCommand.Execute(null);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }
    }
}