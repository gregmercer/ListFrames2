using System;
using System.Collections.Generic;
using System.Windows.Input;

using Xamarin.Forms;

using DataTemplates.Model;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace DataTemplates.ViewModels
{
    public enum BookRoomResults
    {
        Failed,
        Success,
    }

    public class RoomsViewModel : SimpleViewModel
    {
        public ICommand GetRoomsCommand { get; private set; }
        public ICommand ClearChildrenCommand { get; private set; }

        public RoomsViewModel()
        {
            GetRoomsCommand = new Command(async () => await GetRooms());
            ClearChildrenCommand = new Command(async () => await ClearChildren());
        }

        // Commands

        protected async Task ClearChildren()
        {
            if (Rooms == null)
            {
                return;
            }

            foreach(RoomViewModel rvm in Rooms)
            {
                foreach(TimeSlotViewModel tvm in rvm.TimeSlots)
                {
                    if (tvm.TimeSlotFrame != null)
                    {
                        tvm.SlotsWrapLayout.Children.Remove(tvm.TimeSlotFrame);
                        tvm.TimeSlotFrame.GestureRecognizers.Remove(tvm.FrameTap);
                        tvm.TimeSlotFrame = null;
                        Debug.WriteLine(@"Removing {0} for Room: {1}", tvm.TimeSlotLabel.Text, tvm.RoomViewModel.Name);
                    }
                }
                rvm.TimeSlots.Clear();
            }

            Rooms.Clear();
        }

        protected async Task GetRooms()
        {
            ObservableCollection<RoomViewModel> roomsList = new ObservableCollection<RoomViewModel>{};

            //if (Rooms != null)
            //{
                //Rooms.Clear();
            //}
            await ClearChildren();

            int numSlots = GetRandomNumber(20, 40);

            List<String> slotTextList = RandomWordSet(numSlots);

            Debug.WriteLine("numSlots = " + numSlots);

            for (int index = 0; index < 10; index++)
            {
                List<TimeSlotViewModel> tsvmList = new List<TimeSlotViewModel>();

                RoomViewModel rvm = new RoomViewModel() { Index = index, Name = "Room " + index, TimeSlots = tsvmList };

                Debug.WriteLine("Room = " + rvm.Name + " numSlots = " + numSlots + " words = " + slotTextList.ToString());

                List<Boolean> bitList = RandomBitSet(numSlots);

                var timeSlots = new List<string>();
                for (var slotIndex = 0; slotIndex < numSlots; slotIndex++)
                {
                    var slotText = slotTextList.ElementAt(slotIndex);
                    var slotIsVisible = bitList.ElementAt(slotIndex);

                    TimeSlotViewModel tsm = new TimeSlotViewModel() { StartTime = slotText, RoomViewModel = rvm, IsVisible = slotIsVisible };
                    tsvmList.Add(tsm);
                }

                roomsList.Add(rvm);
            }

            Rooms = roomsList;
        }

        private static List<Boolean> RandomBitSet(int setSize)
        {
            List<Boolean> bitList = new List<Boolean>();
            for (var index = 0; index < setSize; index++)
            {
                Boolean bit = true;
                var ranNumber = GetRandomNumber(0, 2);
                //Debug.WriteLine("in RandomBitSet ranNumber = " + ranNumber);
                if (ranNumber == 0)
                {
                    bit = false;
                }
                //Debug.WriteLine("in RandomBitSet bit = " + bit);
                bitList.Add(bit);
            }
            return bitList;
        }

        private static List<String> RandomWordSet(int setSize)
        {
            List<String> wordList = new List<String>();
            for (var index = 0; index < setSize; index++)
            {
                var slotText = RandomString(GetRandomNumber(5, 15));
                wordList.Add(slotText);
            }

            return wordList;
        }

        private static readonly Random getrandom = new Random();
        private static readonly object syncLock = new object();

        //Function to get random number
        public static int GetRandomNumber(int min, int max)
        {
            lock(syncLock)  // synchronize
            {
                return getrandom .Next(min, max);
            }
        }

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                                      .Select(s => s[random.Next(s.Length)]).ToArray());
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
