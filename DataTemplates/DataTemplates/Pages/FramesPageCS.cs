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

                var grid = new Grid();
                CompressedLayout.SetIsHeadless(grid, true);

                grid.RowDefinitions.Add(new RowDefinition { Height = 20 });

                grid.RowSpacing = 0;
                grid.ColumnSpacing = 10;

                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(0.5, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(0.2, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(0.3, GridUnitType.Star) });

                var indexLabel = new Label { IsVisible = false };
                var nameLabel = new Label { FontAttributes = FontAttributes.Bold };

                indexLabel.SetBinding(Label.TextProperty, "Index");
                nameLabel.SetBinding(Label.TextProperty, "Name");

                grid.Children.Add(indexLabel, 0, 1, 0, 1);
                grid.Children.Add(nameLabel, 0, 1, 0, 1);                  

                var timeSlotsLayout = new TimeSlotsWrapLayout();
                timeSlotsLayout.Padding = 20;
                timeSlotsLayout.WidthRequest = 300;
                timeSlotsLayout.HorizontalOptions = LayoutOptions.Start;
                timeSlotsLayout.SetBinding(TimeSlotsWrapLayout.TimeSlotsSourceProperty, "TimeSlots");
                grid.Children.Add(timeSlotsLayout, 0, 3, 1, 2);

                return new ViewCell
                {
                    View = grid
                };

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

            //CreateContent();
            App.RoomsViewModel.GetRoomsCommand.Execute(null);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            //App.RoomsViewModel.ClearChildrenCommand.Execute(null);
            //Content = null;
        }
    }
}