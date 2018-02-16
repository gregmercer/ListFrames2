using System;
using Xamarin.Forms;

using DataTemplates.Views;

namespace DataTemplates.ViewModels
{
    public class TimeSlotViewModel : SimpleViewModel
    {
        public TimeSlotViewModel()
        {
        }

        public RoomViewModel RoomViewModel { get; set; }

        public Frame TimeSlotFrame { get; set; }
        public Label TimeSlotLabel { get; set; }
        public TapGestureRecognizer FrameTap { get; set; }
        public TimeSlotsWrapLayout SlotsWrapLayout { get; set; }

        Boolean isVisible = false;
        public Boolean IsVisible {
            get { return this.isVisible; }
            set
            {
                if (this.isVisible == value) {
                    return;
                }

                this.isVisible = value;
            }
        }

        string startTime = "";
        public string StartTime {
            get { return this.startTime; }
            set
            {
                if (this.startTime == value) {
                    return;
                }

                this.startTime = value;
            }
        }
    }
}
