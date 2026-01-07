using LabProject.Core;
using LabProject.Labs;
using LabProject.Labs.Lab2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LabProject.Shell
{
    public partial class MainForm : Form
    {
        private readonly Dictionary<int, UserControl> labs = new();
        private ILabModule? currentLab;
        private StatusStrip statusStrip = null!;
        private ToolStripStatusLabel statusLabel = null!;

        public MainForm()
        {
            InitializeComponent();

            // Настройка формы
            StartPosition = FormStartPosition.CenterScreen;
            WindowState = FormWindowState.Maximized;
            MinimumSize = new Size(1000, 700);
            AutoScaleMode = AutoScaleMode.Dpi; // Лучше чем Font для разных разрешений

            // Создание статус-бара
            SetupStatusBar();

            RegisterLabs();
            ShowLab(1); // по умолчанию
        }

        private void SetupStatusBar()
        {
            statusStrip = new StatusStrip();
            statusLabel = new ToolStripStatusLabel { Text = "Готово" };
            statusStrip.Items.Add(statusLabel);
            Controls.Add(statusStrip);
            statusStrip.Dock = DockStyle.Bottom;
        }

        private void RegisterLabs()
        {
            labs.Add(1, new Lab1Control());
            labs.Add(2, new Lab2Control());
            // Добавьте остальные лабораторные работы здесь
        }

        public void AddLab(int number, UserControl control)
        {
            if (control is not ILabModule)
                throw new Exception("Лабораторная должна реализовывать ILabModule");

            control.Dock = DockStyle.Fill;
            labs[number] = control;
        }

        public void SetStatus(string message)
        {
            statusLabel.Text = message;
        }

        private void ShowLab(int number)
        {
            if (!labs.ContainsKey(number))
            {
                MessageBox.Show($"Лабораторная №{number} не подключена");
                return;
            }

            panelHost.Controls.Clear();
            currentLab?.OnHide();

            var labControl = labs[number];
            var labModule = labControl as ILabModule;

            labControl.Dock = DockStyle.Fill; // Критично для масштабирования

            panelHost.Controls.Add(labControl);
            labModule?.OnShow();
            currentLab = labModule;

            Text = labModule?.LabTitle ?? $"Лабораторная работа №{number}";

            // Обновляем статус
            SetStatus($"Активна: {labModule?.LabTitle}");
        }

        // обработчики меню
        private void lab1ToolStripMenuItem_Click(object sender, EventArgs e) => ShowLab(1);
        private void lab2ToolStripMenuItem_Click(object sender, EventArgs e) => ShowLab(2);
    }
}