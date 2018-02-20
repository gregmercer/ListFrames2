using System;
using System.Collections.Generic;
using System.Windows.Input;

using Xamarin.Forms;

using DataTemplates.Model;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using DataTemplates.Views;

namespace DataTemplates.ViewModels
{
    public class RoomsViewModel : SimpleViewModel
    {
        public ICommand GetRoomsCommand { get; private set; }
        public ICommand ClearChildrenCommand { get; private set; }

        public RoomsViewModel()
        {
            GetRoomsCommand = new Command(async () => await GetRooms());
        }

        // Commands

        protected async Task GetRooms()
        {
            Rooms = new ObservableCollection<RoomViewModel>() 
            {
                new RoomViewModel() { Index = 1, Name = "Room 1", TimeSlots = new ObservableCollection<TimeSlotViewModel>()
                    {   new TimeSlotViewModel() { StartTime = "1:01a aa" },
                        new TimeSlotViewModel() { StartTime = "2:01a bb" },
                        new TimeSlotViewModel() { StartTime = "3:01a cc" },
                        new TimeSlotViewModel() { StartTime = "4:01a dd" },
                        new TimeSlotViewModel() { StartTime = "5:01a ee" },
                        new TimeSlotViewModel() { StartTime = "6:01a ff" },
                        new TimeSlotViewModel() { StartTime = "7:01a gg" },
                    } },

                new RoomViewModel() { Index = 2, Name = "Room 2", TimeSlots = new ObservableCollection<TimeSlotViewModel>()
                    {   new TimeSlotViewModel() { StartTime = "1:02a aa" },
                        new TimeSlotViewModel() { StartTime = "2:02a bb" },
                        new TimeSlotViewModel() { StartTime = "3:02a cc" },
                        new TimeSlotViewModel() { StartTime = "4:02a dd" },
                        new TimeSlotViewModel() { StartTime = "5:02a ee" },
                        new TimeSlotViewModel() { StartTime = "6:02a ff" },
                        new TimeSlotViewModel() { StartTime = "7:02a gg" },
                    } },
                
                new RoomViewModel() { Index = 3, Name = "Room 3", TimeSlots = new ObservableCollection<TimeSlotViewModel>()
                    {   new TimeSlotViewModel() { StartTime = "1:03a aa" },
                        new TimeSlotViewModel() { StartTime = "2:03a bb" },
                        new TimeSlotViewModel() { StartTime = "3:03a cc" },
                        new TimeSlotViewModel() { StartTime = "4:03a dd" },
                        new TimeSlotViewModel() { StartTime = "5:03a ee" },
                        new TimeSlotViewModel() { StartTime = "6:03a ff" },
                        new TimeSlotViewModel() { StartTime = "7:03a gg" },
                        new TimeSlotViewModel() { StartTime = "8:03a hh" },
                    } },
                
                new RoomViewModel() { Index = 4, Name = "Room 4", TimeSlots = new ObservableCollection<TimeSlotViewModel>()
                    {   new TimeSlotViewModel() { StartTime = "1:04a aa" },
                        new TimeSlotViewModel() { StartTime = "2:04a bb" },
                        new TimeSlotViewModel() { StartTime = "3:04a cc" },
                        new TimeSlotViewModel() { StartTime = "4:04a dd" },
                        new TimeSlotViewModel() { StartTime = "5:04a ee" },
                        new TimeSlotViewModel() { StartTime = "6:04a ff" },
                        new TimeSlotViewModel() { StartTime = "7:04a gg" },
                        new TimeSlotViewModel() { StartTime = "8:04a hh" },
                    } },
                
                new RoomViewModel() { Index = 5, Name = "Room 5", TimeSlots = new ObservableCollection<TimeSlotViewModel>()
                    {   new TimeSlotViewModel() { StartTime = "1:05a aa" },
                        new TimeSlotViewModel() { StartTime = "2:05a bb" },
                        new TimeSlotViewModel() { StartTime = "3:05a cc" },
                        new TimeSlotViewModel() { StartTime = "4:05a dd" },
                        new TimeSlotViewModel() { StartTime = "5:05a ee" },
                        new TimeSlotViewModel() { StartTime = "6:05a ff" },
                        new TimeSlotViewModel() { StartTime = "7:05a gg" },
                        new TimeSlotViewModel() { StartTime = "8:05a hh" },
                        new TimeSlotViewModel() { StartTime = "9:05a ii" },
                        new TimeSlotViewModel() { StartTime = "10:05a jj" },
                    } },
                
                new RoomViewModel() { Index = 6, Name = "Room 6", TimeSlots = new ObservableCollection<TimeSlotViewModel>()
                    {   new TimeSlotViewModel() { StartTime = "1:06a aa" },
                        new TimeSlotViewModel() { StartTime = "2:06a bb" },
                        new TimeSlotViewModel() { StartTime = "3:06a cc" },
                        new TimeSlotViewModel() { StartTime = "4:06a dd" },
                        new TimeSlotViewModel() { StartTime = "5:06a ee" },
                        new TimeSlotViewModel() { StartTime = "6:06a ff" },
                        new TimeSlotViewModel() { StartTime = "7:06a gg" },
                        new TimeSlotViewModel() { StartTime = "8:06a hh" },
                        new TimeSlotViewModel() { StartTime = "9:06a ii" },
                        new TimeSlotViewModel() { StartTime = "10:06a jj" },
                    } },
            };
        }

        // Properties

        public int NumberSlots { set; get; }

        private ObservableCollection<RoomViewModel> rooms;
        public ObservableCollection<RoomViewModel> Rooms
        {
            get
            {
                return this.rooms;
            }
            set
            {
                rooms = value;
                RaisePropertyChanged();
            }
        }
    }
}
