using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace LabProject.Labs.Lab2
{
    partial class Lab2Control
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dataGridViewAB;
        private System.Windows.Forms.DataGridView dataGridViewX;
        private System.Windows.Forms.NumericUpDown numericUpDownN;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importFromExcelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToExcelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem solveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gaussToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem jordanGaussToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cramerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generateRandomToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            dataGridViewAB = new DataGridView();
            dataGridViewX = new DataGridView();
            numericUpDownN = new NumericUpDown();
            label1 = new Label();
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            importFromExcelToolStripMenuItem = new ToolStripMenuItem();
            exportToExcelToolStripMenuItem = new ToolStripMenuItem();
            exitToolStripMenuItem = new ToolStripMenuItem();
            solveToolStripMenuItem = new ToolStripMenuItem();
            gaussToolStripMenuItem = new ToolStripMenuItem();
            jordanGaussToolStripMenuItem = new ToolStripMenuItem();
            cramerToolStripMenuItem = new ToolStripMenuItem();
            generateToolStripMenuItem = new ToolStripMenuItem();
            generateRandomToolStripMenuItem = new ToolStripMenuItem();
            clearToolStripMenuItem = new ToolStripMenuItem();
            statusStrip1 = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            toolStripProgressBar1 = new ToolStripProgressBar();
            ((System.ComponentModel.ISupportInitialize)dataGridViewAB).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewX).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownN).BeginInit();
            menuStrip1.SuspendLayout();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // dataGridViewAB
            // 
            dataGridViewAB.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridViewAB.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewAB.Location = new Point(12, 77);
            dataGridViewAB.Name = "dataGridViewAB";
            dataGridViewAB.RowHeadersWidth = 51;
            dataGridViewAB.Size = new Size(1161, 450);
            dataGridViewAB.TabIndex = 0;
            // 
            // dataGridViewX
            // 
            dataGridViewX.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            dataGridViewX.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewX.Location = new Point(1179, 77);
            dataGridViewX.Name = "dataGridViewX";
            dataGridViewX.RowHeadersWidth = 51;
            dataGridViewX.Size = new Size(183, 450);
            dataGridViewX.TabIndex = 1;
            // 
            // numericUpDownN
            // 
            numericUpDownN.Location = new Point(207, 40);
            numericUpDownN.Maximum = new decimal(new int[] { 50, 0, 0, 0 });
            numericUpDownN.Minimum = new decimal(new int[] { 2, 0, 0, 0 });
            numericUpDownN.Name = "numericUpDownN";
            numericUpDownN.Size = new Size(60, 27);
            numericUpDownN.TabIndex = 2;
            numericUpDownN.Value = new decimal(new int[] { 3, 0, 0, 0 });
            numericUpDownN.ValueChanged += numericUpDownN_ValueChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 42);
            label1.Name = "label1";
            label1.Size = new Size(189, 20);
            label1.TabIndex = 3;
            label1.Text = "Размер матрицы N (2-50):";
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, solveToolStripMenuItem, generateToolStripMenuItem, clearToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1374, 28);
            menuStrip1.TabIndex = 4;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { importFromExcelToolStripMenuItem, exportToExcelToolStripMenuItem, exitToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(59, 24);
            fileToolStripMenuItem.Text = "Файл";
            // 
            // importFromExcelToolStripMenuItem
            // 
            importFromExcelToolStripMenuItem.Name = "importFromExcelToolStripMenuItem";
            importFromExcelToolStripMenuItem.Size = new Size(224, 26);
            importFromExcelToolStripMenuItem.Text = "Импорт из Excel";
            importFromExcelToolStripMenuItem.Click += ImportFromExcelToolStripMenuItem_Click;
            // 
            // exportToExcelToolStripMenuItem
            // 
            exportToExcelToolStripMenuItem.Name = "exportToExcelToolStripMenuItem";
            exportToExcelToolStripMenuItem.Size = new Size(224, 26);
            exportToExcelToolStripMenuItem.Text = "Экспорт в Excel";
            exportToExcelToolStripMenuItem.Click += ExportToExcelToolStripMenuItem_Click;
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(224, 26);
            exitToolStripMenuItem.Text = "Выход";
            exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
            // 
            // solveToolStripMenuItem
            // 
            solveToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { gaussToolStripMenuItem, jordanGaussToolStripMenuItem, cramerToolStripMenuItem });
            solveToolStripMenuItem.Name = "solveToolStripMenuItem";
            solveToolStripMenuItem.Size = new Size(85, 24);
            solveToolStripMenuItem.Text = "Решение";
            // 
            // gaussToolStripMenuItem
            // 
            gaussToolStripMenuItem.Name = "gaussToolStripMenuItem";
            gaussToolStripMenuItem.Size = new Size(253, 26);
            gaussToolStripMenuItem.Text = "Метод Гаусса";
            gaussToolStripMenuItem.Click += GaussToolStripMenuItem_Click;
            // 
            // jordanGaussToolStripMenuItem
            // 
            jordanGaussToolStripMenuItem.Name = "jordanGaussToolStripMenuItem";
            jordanGaussToolStripMenuItem.Size = new Size(253, 26);
            jordanGaussToolStripMenuItem.Text = "Метод Жардана-Гаусса";
            jordanGaussToolStripMenuItem.Click += JordanGaussToolStripMenuItem_Click;
            // 
            // cramerToolStripMenuItem
            // 
            cramerToolStripMenuItem.Name = "cramerToolStripMenuItem";
            cramerToolStripMenuItem.Size = new Size(253, 26);
            cramerToolStripMenuItem.Text = "Метод Крамера";
            cramerToolStripMenuItem.Click += CramerToolStripMenuItem_Click;
            // 
            // generateToolStripMenuItem
            // 
            generateToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { generateRandomToolStripMenuItem });
            generateToolStripMenuItem.Name = "generateToolStripMenuItem";
            generateToolStripMenuItem.Size = new Size(98, 24);
            generateToolStripMenuItem.Text = "Генерация";
            // 
            // generateRandomToolStripMenuItem
            // 
            generateRandomToolStripMenuItem.Name = "generateRandomToolStripMenuItem";
            generateRandomToolStripMenuItem.Size = new Size(226, 26);
            generateRandomToolStripMenuItem.Text = "Случайные данные";
            generateRandomToolStripMenuItem.Click += GenerateRandomToolStripMenuItem_Click;
            // 
            // clearToolStripMenuItem
            // 
            clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            clearToolStripMenuItem.Size = new Size(87, 24);
            clearToolStripMenuItem.Text = "Очистить";
            clearToolStripMenuItem.Click += ClearToolStripMenuItem_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(20, 20);
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1, toolStripProgressBar1 });
            statusStrip1.Location = new Point(0, 534);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1374, 26);
            statusStrip1.TabIndex = 5;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(57, 20);
            toolStripStatusLabel1.Text = "Готово";
            // 
            // toolStripProgressBar1
            // 
            toolStripProgressBar1.Name = "toolStripProgressBar1";
            toolStripProgressBar1.Size = new Size(100, 18);
            toolStripProgressBar1.Visible = false;
            // 
            // Lab2Control
            // 
            Controls.Add(statusStrip1);
            Controls.Add(label1);
            Controls.Add(numericUpDownN);
            Controls.Add(dataGridViewX);
            Controls.Add(dataGridViewAB);
            Controls.Add(menuStrip1);
            Name = "Lab2Control";
            Size = new Size(1374, 560);
            Load += Lab2Control_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridViewAB).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewX).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownN).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
