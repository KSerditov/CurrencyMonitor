using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CurrencyMonitor
{
    class DataListMenu : IDisposable
    {
        ContextMenuStrip _menu;
        ToolStripMenuItem _item;
        List<DataItem> _dataItems;
        private OnlineMonitor _data;

        public DataListMenu(OnlineMonitor data, List<DataItem> dataItems)
        {
            _menu = new ContextMenuStrip();
            _menu.RenderMode = ToolStripRenderMode.Professional;
            _data = data;
            _dataItems = dataItems;
        }

        public ContextMenuStrip Create(){

            foreach(DataItem item in _dataItems)
            {
                _item = new ToolStripMenuItem();
                _item.Text = item.Name + " " + item.Rate + " " + item.Change + " " + item.DailyChange;
                _item.Click += (sender, eventArgs) =>
                {
                    Clipboard.SetText(_item.Text + " рублей?? " + _item.Text + " рублей, блеать!1111 (с)");
                };

                _menu.Items.Add(_item);
            }

            return _menu;
        }

        public void Dispose()
        {
            _item.Dispose();
            _menu.Dispose();
        }
    }
}
