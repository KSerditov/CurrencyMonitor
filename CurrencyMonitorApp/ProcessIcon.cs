using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CurrencyMonitor.Properties;
using System.Windows.Threading;

namespace CurrencyMonitor
{
    class ProcessIcon : IDisposable
    {
        NotifyIcon _ni;
        OnlineMonitor _data;
        List<DataItem> _dataItems;
            
        public ProcessIcon(List<DataItem> dataItems)
        {
           _ni = new NotifyIcon();
           _data = new OnlineMonitor(_ni, dataItems);
           _dataItems = dataItems;
        }

        public void Display()
        {
            _ni.MouseClick += ni_MouseClick;
            _ni.MouseUp += ni_MouseUp;
            _ni.Icon = Resources.money;
            _ni.Visible = true;
            _ni.ShowBalloonTip(1500, null, "Обновляю курс", ToolTipIcon.Info);
        }

        private void ni_MouseUp(object sender, MouseEventArgs e)
        {
            MethodInfo mi = typeof(NotifyIcon).GetMethod("ShowContextMenu", BindingFlags.Instance | BindingFlags.NonPublic);
            mi.Invoke(_ni, null);
        }

        private void ni_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _ni.ContextMenuStrip = new DataListMenu(_data, _dataItems).Create();
            }
            if (e.Button == MouseButtons.Right)
            {
                _ni.ContextMenuStrip = new ContextMenus().Create();
            }
            _ni.ContextMenuStrip.Show(Cursor.Position);
        }

        public void Dispose()
        {
            _ni.Dispose();
        }
    }
}
