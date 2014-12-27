using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CurrencyMonitor
{
    class ContextMenus
    {
        ContextMenuStrip menu;
        ToolStripMenuItem item;

        public ContextMenus()
        {
            menu = new ContextMenuStrip();
            menu.RenderMode = ToolStripRenderMode.Professional;
        }

        public ContextMenuStrip Create()
        {
            item = new ToolStripMenuItem();
            item.Text = "Выход";
            item.Click += item_Click;
            menu.Items.Add(item);

            return menu;
        }

        void item_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

    }
}
