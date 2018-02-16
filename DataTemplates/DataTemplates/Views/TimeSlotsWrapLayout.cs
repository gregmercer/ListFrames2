using System;
using System.Collections.Generic;

using Xamarin.Forms;

using DataTemplates.Views;
using DataTemplates.ViewModels;
using System.Linq;

namespace DataTemplates.Views
{
    /// <summary>
    /// Layout which performs wrapping on the boundaries.
    /// Base Code -> https://forums.xamarin.com/discussion/comment/57486/#Comment_57486 
    ///              http://bit.ly/xf-custompanel
    /// </summary>
    public class TimeSlotsWrapLayout : Layout<View>
    {
        public TimeSlotsWrapLayout()
        {
        }

        public static readonly BindableProperty SlotFramingHeightProperty = BindableProperty.Create(
            propertyName: "SlotFrameHeight",
            returnType: typeof(double),
            declaringType: typeof(TimeSlotsWrapLayout),
            defaultValue: 20.0,
            propertyChanged: (bindable, oldvalue, newvalue) =>
        {
            ((TimeSlotsWrapLayout)bindable).InvalidateLayout();
        }
        );

        public double SlotFrameHeight
        {
            set { SetValue(SlotFramingHeightProperty, value); }
            get { return (double)GetValue(SlotFramingHeightProperty); }
        }

        public static readonly BindableProperty TimeSlotsSourceProperty = BindableProperty.Create(
            propertyName: "TimeSlotsSource",
            returnType: typeof(IList<TimeSlotViewModel>),
            declaringType: typeof(TimeSlotsWrapLayout),
            defaultValue: new List<TimeSlotViewModel>(),
            propertyChanged: (bindable, oldvalue, newvalue) =>
        {
            TimeSlotsWrapLayout slotsWrapLayout = bindable as TimeSlotsWrapLayout;

            if (slotsWrapLayout.Children.Count > 0)
            {
                return;
            }

            IList<TimeSlotViewModel> timeSlotViewModels = newvalue as IList<TimeSlotViewModel>;
            foreach (TimeSlotViewModel timeSlotViewModel in timeSlotViewModels)
            {
                //
                // Frame Approach - Begin
                //

                timeSlotViewModel.SlotsWrapLayout = slotsWrapLayout;

                var timeSlotLabel = new Label
                {
                    Text = timeSlotViewModel.StartTime,
                    TextColor = Color.Green,
                    BackgroundColor = Color.Transparent,
                    FontSize = 14.0,
                    FontAttributes = FontAttributes.Bold,
                    HorizontalOptions = LayoutOptions.Fill,
                    VerticalOptions = LayoutOptions.Center,
                    VerticalTextAlignment = TextAlignment.Center,
                    HorizontalTextAlignment = TextAlignment.Center,
                };
                timeSlotViewModel.TimeSlotLabel = timeSlotLabel;

                Frame timeSlotFrame = new Frame
                {
                    Content = timeSlotLabel,
                    BindingContext = timeSlotViewModel,
                    //WidthRequest = 40.0,
                    HeightRequest = slotsWrapLayout.SlotFrameHeight,
                    OutlineColor = Color.Green,
                    VerticalOptions = LayoutOptions.Start,
                    HorizontalOptions = LayoutOptions.Fill,
                    BackgroundColor = Color.White,
                    HasShadow = false,
                    Padding = new Thickness(10, 10, 10, 10),
                    Margin = new Thickness(0, 3, 0, 0),
                };
                timeSlotViewModel.TimeSlotFrame = timeSlotFrame;

                timeSlotFrame.BackgroundColor = Color.White;
                timeSlotLabel.TextColor = Color.Green;
                timeSlotFrame.OutlineColor = Color.Green;

                timeSlotFrame.IsVisible = timeSlotViewModel.IsVisible;

                slotsWrapLayout.Children.Add(timeSlotFrame);

                //
                // Frame Approach - End
                //
            }
        }
        );

        /// <summary>
        /// Spacing added between elements
        /// </summary>
        public double Spacing
        {
            get { return (double)3; }
            set {; }
        }

        protected override SizeRequest OnMeasure(double widthConstraint, double heightConstraint)
        {
            if (WidthRequest > 0)
                widthConstraint = Math.Min(widthConstraint, WidthRequest);

            if (HeightRequest > 0)
                heightConstraint = Math.Min(heightConstraint, HeightRequest);

            double internalWidth = double.IsPositiveInfinity(widthConstraint) ? double.PositiveInfinity : Math.Max(0, widthConstraint);
            double internalHeight = double.IsPositiveInfinity(heightConstraint) ? double.PositiveInfinity : Math.Max(0, heightConstraint);

            return VariableMeasureAndLayout(internalWidth, internalHeight);
        }

        protected override void LayoutChildren(double x, double y, double width, double height)
        {
            VariableMeasureAndLayout(width, height, true, x, y);
        }

        private SizeRequest VariableMeasureAndLayout(double widthConstraint, double heightConstraint, bool doLayout = false, double x = 0, double y = 0)
        {
            double totalWidth = 0;
            double totalHeight = 0;
            double rowHeight = 0;
            double rowWidth = 0;
            double minWidth = 0;
            double minHeight = 0;
            double xPos = x;
            double yPos = y;

            var visibleChildren = Children.Where(c => c.IsVisible).Select(c => new
            {
                child = c,
                size = c.Measure(widthConstraint, heightConstraint)
            });

            var nextChildren = visibleChildren.Skip(1).ToList();
            nextChildren.Add(null); //make element count same

            var zipChildren = visibleChildren.Zip(nextChildren, (c, n) => new { current = c, next = n });

            foreach (var childBlock in zipChildren)
            {

                var child = childBlock.current.child;
                var size = childBlock.current.size;
                var itemWidth = size.Request.Width;
                var itemHeight = size.Request.Height;

                rowHeight = Math.Max(rowHeight, itemHeight + Spacing);
                rowWidth += itemWidth + Spacing;

                minHeight = Math.Max(minHeight, itemHeight);
                minWidth = Math.Max(minWidth, itemWidth);

                if (doLayout)
                {
                    var region = new Rectangle(xPos, yPos, itemWidth, itemHeight);
                    LayoutChildIntoBoundingRegion(child, region);
                }

                if (childBlock.next == null)
                {
                    totalHeight += rowHeight;
                    totalWidth = Math.Max(totalWidth, rowWidth);
                    break;
                }

                xPos += itemWidth + Spacing;
                var nextWitdh = childBlock.next.size.Request.Width;

                if (xPos + nextWitdh - x > widthConstraint)
                {
                    xPos = x;
                    yPos += rowHeight;
                    totalHeight += rowHeight;
                    totalWidth = Math.Max(totalWidth, rowWidth);
                    rowHeight = 0;
                    rowWidth = 0;
                }
            }

            totalWidth = Math.Max(totalWidth - Spacing, 0);
            totalHeight = Math.Max(totalHeight - Spacing, 0);

            return new SizeRequest(new Size(totalWidth, totalHeight), new Size(minWidth, minHeight));
        }

        protected override void InvalidateLayout()
        {
            base.InvalidateLayout();
        }

        protected override void OnChildMeasureInvalidated()
        {
            base.OnChildMeasureInvalidated();
        }
    }
}
