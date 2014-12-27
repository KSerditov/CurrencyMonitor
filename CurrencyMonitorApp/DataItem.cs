using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyMonitor
{
    class DataItem
    {
        private string _name;
        private string _link;
        private string _rate;
        private string _change;
        private string _dailyChange;

        public string Rate
        {
            get { return _rate; }
            set { _rate = value; }
        }

        public string Change
        {
            get { return _change; }
            set { _change = value; }
        }

        public string DailyChange
        {
            get { return _dailyChange; }
            set { _dailyChange = value; }
        }

        public string Link
        {
            get { return _link; }
        }

        public string Name
        {
            get { return _name; }
        }

        public DataItem(string Name, string Link)
        {
            _name = Name;
            _link = Link;
        }

    }
}
