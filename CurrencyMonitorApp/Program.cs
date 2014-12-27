using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace CurrencyMonitor
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            List<DataItem> dataItems = new List<DataItem>();

            XmlTextReader reader = new XmlTextReader("configuration.xml");
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    if (reader.Name == "currency")
                    {
                        dataItems.Add(new DataItem(reader.GetAttribute("name"), reader.GetAttribute("link")));
                    }
                }
            }

            using (ProcessIcon pi = new ProcessIcon(dataItems))
            {
                pi.Display();
                Application.Run();
            }
        }
    }
}
