using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Style
{
    class MainPageVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPrpertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
