using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabProject.Shell
{
    partial class MainForm
    {
        private Panel panelHost;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem labsToolStripMenuItem;
        private ToolStripMenuItem lab1ToolStripMenuItem;
        private ToolStripMenuItem lab2ToolStripMenuItem;

        private void InitializeComponent()
        {
            menuStrip1 = new MenuStrip();
            labsToolStripMenuItem = new ToolStripMenuItem();
            lab1ToolStripMenuItem = new ToolStripMenuItem();
            lab2ToolStripMenuItem = new ToolStripMenuItem();

            labsToolStripMenuItem.Text = "Лабораторные";
            lab1ToolStripMenuItem.Text = "Лабораторная №1 (Метод дихотомии)";
            lab2ToolStripMenuItem.Text = "Лабораторная №2 (Методы решения СЛАУ)";

            lab1ToolStripMenuItem.Click += lab1ToolStripMenuItem_Click;
            lab2ToolStripMenuItem.Click += lab2ToolStripMenuItem_Click;

            labsToolStripMenuItem.DropDownItems.AddRange(
                new ToolStripItem[] { lab1ToolStripMenuItem, lab2ToolStripMenuItem });

            menuStrip1.Items.Add(labsToolStripMenuItem);

            panelHost = new Panel();
            panelHost.Dock = DockStyle.Fill;

            Controls.Add(panelHost);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
        }
    }
}
