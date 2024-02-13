using DeleeRefreshMonkey.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeleeRefreshMonkey.ViewModels
{
    [QueryProperty(nameof(SelectedMonkey), "selectedMonkey")]
    public class MonkeyDetailsViewModel : ViewModelBase
    {
        private Monkey selectedMonkey;
        public Monkey SelectedMonkey
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
    }
}
