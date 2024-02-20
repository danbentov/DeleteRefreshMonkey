using DeleeRefreshMonkey.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DeleeRefreshMonkey.Services;
using Microsoft.Maui.Controls;

namespace DeleeRefreshMonkey.ViewModels
{
    public class MonkeyViewModel:ViewModelBase
    {
        private ObservableCollection<Monkey> monkeys;
        public ObservableCollection<Monkey> Monkeys
        {
            get
            {
                return this.monkeys;
            }
            set
            {
                this.monkeys = value;
                OnPropertyChanged();
            }
        }

        private MonkeyService monkeysService;
        public MonkeyViewModel(MonkeyService service)
        {
            this.monkeysService = service;
            Monkeys = new ObservableCollection<Monkey>();
            IsRefreshing = false;
            ReadMonkeys();
            FillLocations();
            SelectedLocation = MonkeyLocations.First();
        }

        private async void ReadMonkeys()
        {
            List<Monkey> list = await monkeysService.GetMonkeys();
            this.Monkeys = new ObservableCollection<Monkey>(list);
        }

        public ICommand DeleteCommand => new Command<Monkey>(RemoveMonkey);

        void RemoveMonkey(Monkey st)
        {
            if (Monkeys.Contains(st))
            {
                Monkeys.Remove(st);
            }
        }

   
        #region Refresh View
        public ICommand RefreshCommand => new Command(Refresh);
        private async void Refresh()
        {

            OnPickerChanged();

            IsRefreshing = false;
        }

        private bool isRefreshing;
        public bool IsRefreshing
        {
            get
            {
                return this.isRefreshing;
            }
            set
            {
                this.isRefreshing = value;
                OnPropertyChanged();
            }
        }
        #endregion


        private object selectedMonkey;
        public object SelectedMonkey
        {
            get
            {
                return this.selectedMonkey;
            }
            set
            {
                this.selectedMonkey = value;
                OnPropertyChanged();
            }
        }

        public ICommand SingleSelectCommand => new Command(OnSingleSelectMonkey);

        async void OnSingleSelectMonkey()
        {
            if (SelectedMonkey != null)
            {
                var navParam = new Dictionary<string, object>()
            {
                { "selectedMonkey",SelectedMonkey}
            };
                //Add goto here to show details
                await Shell.Current.GoToAsync("monkeyDetails", navParam);

                SelectedMonkey = null;
            }
        }

        private ObservableCollection<string> monkeyLocations;
        public ObservableCollection<string> MonkeyLocations
        {
            get
            {
                return this.monkeyLocations;
            }
            set
            {
                this.monkeyLocations = value;
                OnPropertyChanged();
            }
        }

        private string selectedLocation;
        public string SelectedLocation
        {
            get
            {
                return this.selectedLocation;
            }
            set
            {
                this.selectedLocation = value;
                OnPickerChanged();
                OnPropertyChanged();
            }
        }

        private void OnPickerChanged()
        {
            ReadMonkeys();
            if (SelectedLocation != null)
            {
                if (SelectedLocation != "All Monkeys")
                {
                    List<Monkey> tobeRemoved = Monkeys.Where(s => s.Location != SelectedLocation).ToList();
                    foreach (Monkey m in tobeRemoved)
                    {
                        Monkeys.Remove(m);
                    }
                }
            }
        }

        private void FillLocations()
        {
            ObservableCollection<string> mLocation = new ObservableCollection<string>();
            mLocation.Add("All Monkeys");
            mLocation.Add("Africa & Asia");
            mLocation.Add("Central & South America");
            mLocation.Add("Central and East Africa");
            mLocation.Add("Brazil");
            mLocation.Add("South America");
            mLocation.Add("Japan");
            mLocation.Add("Southern Cameroon, Gabon, Equatorial Guinea, and Congo");
            mLocation.Add("Borneo");
            mLocation.Add("Vietnam, Laos");
            mLocation.Add("Vietnam");
            mLocation.Add("China");
            mLocation.Add("Indonesia");
            mLocation.Add("Sri Lanka");
            mLocation.Add("Ethiopia");
            this.MonkeyLocations = mLocation;
        }
    }
}
