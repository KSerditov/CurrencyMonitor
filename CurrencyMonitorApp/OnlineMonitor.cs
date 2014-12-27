using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Threading;

namespace CurrencyMonitor
{
    class OnlineMonitor : IDisposable
    {
        NotifyIcon _ni;
        Task _task;
        Dispatcher _dispatcher;
        List<DataItem> _dataItems;
        bool isInitialized = false;

        public OnlineMonitor(NotifyIcon notifyIcon, List<DataItem> dataItems)
        {
            _dispatcher = Dispatcher.CurrentDispatcher;
            _ni = notifyIcon;
            _dataItems = dataItems;
            _task = new Task(() => StreamData());
            _task.Start();
        }

        private void StreamData()
        {
            for (int i = 0; i <= int.MaxValue; i++)
            {

                    try
                    {
                        WebClient web = new WebClient();
                        foreach (DataItem item in _dataItems)
                        {

                            var byteContent = web.DownloadData(item.Link);
                            string content = Encoding.UTF8.GetString(byteContent, 0, byteContent.Length);
                            string rate = Regex.Match(content, string.Format("<span id=\"issuer-profile-informer-last\">(.+?)</span>")).Groups[1].Value;

                            string lastChange = Regex.Match(content, string.Format("<span id=\"issuer-profile-informer-change\" class=\"(.+?)\">(.+?)</span>")).Groups[2].Value;
                            string dailyChange = Regex.Match(content, string.Format("<span id=\"issuer-profile-informer-pchange\" class=\"(.+?)\">(.+?)</span>")).Groups[2].Value;

                            item.Rate = rate;
                            item.Change = lastChange;
                            item.DailyChange = dailyChange;

                        }

                        if (!isInitialized)
                        {
                            _dispatcher.Invoke(new Action(() => _ni.ShowBalloonTip(1500, null, "Курс получен", ToolTipIcon.Info)));
                            isInitialized = true;
                        }
                    } catch (Exception e)
                    {
                        _dispatcher.Invoke(new Action(() => _ni.ShowBalloonTip(1500, null, e.Message, ToolTipIcon.Info)));
                    }

                Thread.Sleep(5000);
            }
            
        }

        public void Dispose()
        {
            _task.Dispose();
        }
    }
}
