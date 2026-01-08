using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace LabProject.Labs
{
    partial class Lab4Control
    {
        private IContainer components = null;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem calculateMenuItem;
        private ToolStripMenuItem clearMenuItem;
        private ToolStripMenuItem generateDataMenuItem;
        private ToolStripMenuItem aboutMenuItem4;
        private GroupBox groupBox1;
        private Label label1;
        private NumericUpDown numericUpDownSize;
        private CheckBox checkBoxAscending;
        private CheckBox checkBoxDescending;
        private GroupBox groupBox2;
        private CheckBox checkBoxBogo;
        private CheckBox checkBoxQuick;
        private CheckBox checkBoxShaker;
        private CheckBox checkBoxInsertion;
        private CheckBox checkBoxBubble;
        private GroupBox groupBox3;
        private TextBox txtResults;
        private Chart chartSorting;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private ToolStripProgressBar toolStripProgressBar1;

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
            ChartArea chartArea1 = new ChartArea();
            Legend legend1 = new Legend();
            Series series1 = new Series();
            menuStrip1 = new MenuStrip();
            calculateMenuItem = new ToolStripMenuItem();
            clearMenuItem = new ToolStripMenuItem();
            generateDataMenuItem = new ToolStripMenuItem();
            aboutMenuItem4 = new ToolStripMenuItem();
            groupBox1 = new GroupBox();
            checkBoxDescending = new CheckBox();
            checkBoxAscending = new CheckBox();
            numericUpDownSize = new NumericUpDown();
            label1 = new Label();
            groupBox2 = new GroupBox();
            checkBoxBogo = new CheckBox();
            checkBoxQuick = new CheckBox();
            checkBoxShaker = new CheckBox();
            checkBoxInsertion = new CheckBox();
            checkBoxBubble = new CheckBox();
            groupBox3 = new GroupBox();
            txtResults = new TextBox();
            chartSorting = new Chart();
            statusStrip1 = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            toolStripProgressBar1 = new ToolStripProgressBar();
            menuStrip1.SuspendLayout();
            groupBox1.SuspendLayout();
            ((ISupportInitialize)numericUpDownSize).BeginInit();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            ((ISupportInitialize)chartSorting).BeginInit();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { calculateMenuItem, clearMenuItem, generateDataMenuItem, aboutMenuItem4 });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(8, 3, 0, 3);
            menuStrip1.Size = new Size(1200, 30);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // calculateMenuItem
            // 
            calculateMenuItem.Name = "calculateMenuItem";
            calculateMenuItem.Size = new Size(98, 24);
            calculateMenuItem.Text = "Рассчитать";
            // 
            // clearMenuItem
            // 
            clearMenuItem.Name = "clearMenuItem";
            clearMenuItem.Size = new Size(87, 24);
            clearMenuItem.Text = "Очистить";
            // 
            // generateDataMenuItem
            // 
            generateDataMenuItem.Name = "generateDataMenuItem";
            generateDataMenuItem.Size = new Size(186, 24);
            generateDataMenuItem.Text = "Сгенерировать данные";
            // 
            // aboutMenuItem4
            // 
            aboutMenuItem4.Name = "aboutMenuItem4";
            aboutMenuItem4.Size = new Size(81, 24);
            aboutMenuItem4.Text = "Справка";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(checkBoxDescending);
            groupBox1.Controls.Add(checkBoxAscending);
            groupBox1.Controls.Add(numericUpDownSize);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(16, 46);
            groupBox1.Margin = new Padding(4, 5, 4, 5);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(4, 5, 4, 5);
            groupBox1.Size = new Size(400, 154);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Параметры";
            // 
            // checkBoxDescending
            // 
            checkBoxDescending.AutoSize = true;
            checkBoxDescending.Location = new Point(200, 100);
            checkBoxDescending.Margin = new Padding(4, 5, 4, 5);
            checkBoxDescending.Name = "checkBoxDescending";
            checkBoxDescending.Size = new Size(128, 24);
            checkBoxDescending.TabIndex = 3;
            checkBoxDescending.Text = "По убыванию";
            checkBoxDescending.UseVisualStyleBackColor = true;
            checkBoxDescending.CheckedChanged += checkBoxDescending_CheckedChanged;
            // 
            // checkBoxAscending
            // 
            checkBoxAscending.AutoSize = true;
            checkBoxAscending.Checked = true;
            checkBoxAscending.CheckState = CheckState.Checked;
            checkBoxAscending.Location = new Point(13, 100);
            checkBoxAscending.Margin = new Padding(4, 5, 4, 5);
            checkBoxAscending.Name = "checkBoxAscending";
            checkBoxAscending.Size = new Size(147, 24);
            checkBoxAscending.TabIndex = 2;
            checkBoxAscending.Text = "По возрастанию";
            checkBoxAscending.UseVisualStyleBackColor = true;
            checkBoxAscending.CheckedChanged += checkBoxAscending_CheckedChanged;
            // 
            // numericUpDownSize
            // 
            numericUpDownSize.Location = new Point(133, 46);
            numericUpDownSize.Margin = new Padding(4, 5, 4, 5);
            numericUpDownSize.Minimum = new decimal(new int[] { 10, 0, 0, 0 });
            numericUpDownSize.Name = "numericUpDownSize";
            numericUpDownSize.Size = new Size(160, 27);
            numericUpDownSize.TabIndex = 1;
            numericUpDownSize.Value = new decimal(new int[] { 20, 0, 0, 0 });
            numericUpDownSize.ValueChanged += numericUpDownSize_ValueChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(13, 49);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(125, 20);
            label1.TabIndex = 0;
            label1.Text = "Размер массива:";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(checkBoxBogo);
            groupBox2.Controls.Add(checkBoxQuick);
            groupBox2.Controls.Add(checkBoxShaker);
            groupBox2.Controls.Add(checkBoxInsertion);
            groupBox2.Controls.Add(checkBoxBubble);
            groupBox2.Location = new Point(16, 209);
            groupBox2.Margin = new Padding(4, 5, 4, 5);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(4, 5, 4, 5);
            groupBox2.Size = new Size(400, 231);
            groupBox2.TabIndex = 2;
            groupBox2.TabStop = false;
            groupBox2.Text = "Алгоритмы сортировки";
            // 
            // checkBoxBogo
            // 
            checkBoxBogo.AutoSize = true;
            checkBoxBogo.Location = new Point(13, 192);
            checkBoxBogo.Margin = new Padding(4, 5, 4, 5);
            checkBoxBogo.Name = "checkBoxBogo";
            checkBoxBogo.Size = new Size(72, 24);
            checkBoxBogo.TabIndex = 4;
            checkBoxBogo.Text = "BOGO";
            checkBoxBogo.UseVisualStyleBackColor = true;
            // 
            // checkBoxQuick
            // 
            checkBoxQuick.AutoSize = true;
            checkBoxQuick.Location = new Point(13, 154);
            checkBoxQuick.Margin = new Padding(4, 5, 4, 5);
            checkBoxQuick.Name = "checkBoxQuick";
            checkBoxQuick.Size = new Size(174, 24);
            checkBoxQuick.TabIndex = 3;
            checkBoxQuick.Text = "Быстрая сортировка";
            checkBoxQuick.UseVisualStyleBackColor = true;
            // 
            // checkBoxShaker
            // 
            checkBoxShaker.AutoSize = true;
            checkBoxShaker.Location = new Point(13, 115);
            checkBoxShaker.Margin = new Padding(4, 5, 4, 5);
            checkBoxShaker.Name = "checkBoxShaker";
            checkBoxShaker.Size = new Size(111, 24);
            checkBoxShaker.TabIndex = 2;
            checkBoxShaker.Text = "Шейкерная";
            checkBoxShaker.UseVisualStyleBackColor = true;
            // 
            // checkBoxInsertion
            // 
            checkBoxInsertion.AutoSize = true;
            checkBoxInsertion.Location = new Point(13, 77);
            checkBoxInsertion.Margin = new Padding(4, 5, 4, 5);
            checkBoxInsertion.Name = "checkBoxInsertion";
            checkBoxInsertion.Size = new Size(190, 24);
            checkBoxInsertion.TabIndex = 1;
            checkBoxInsertion.Text = "Сортировка вставками";
            checkBoxInsertion.UseVisualStyleBackColor = true;
            // 
            // checkBoxBubble
            // 
            checkBoxBubble.AutoSize = true;
            checkBoxBubble.Checked = true;
            checkBoxBubble.CheckState = CheckState.Checked;
            checkBoxBubble.Location = new Point(13, 38);
            checkBoxBubble.Margin = new Padding(4, 5, 4, 5);
            checkBoxBubble.Name = "checkBoxBubble";
            checkBoxBubble.Size = new Size(124, 24);
            checkBoxBubble.TabIndex = 0;
            checkBoxBubble.Text = "Пузырьковая";
            checkBoxBubble.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            groupBox3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            groupBox3.AutoSize = true;
            groupBox3.Controls.Add(txtResults);
            groupBox3.Location = new Point(16, 449);
            groupBox3.Margin = new Padding(4, 5, 4, 5);
            groupBox3.Name = "groupBox3";
            groupBox3.Padding = new Padding(4, 5, 4, 5);
            groupBox3.Size = new Size(400, 151);
            groupBox3.TabIndex = 3;
            groupBox3.TabStop = false;
            groupBox3.Text = "Результаты:";
            // 
            // txtResults
            // 
            txtResults.Dock = DockStyle.Fill;
            txtResults.Location = new Point(4, 25);
            txtResults.Margin = new Padding(4, 5, 4, 5);
            txtResults.Multiline = true;
            txtResults.Name = "txtResults";
            txtResults.ReadOnly = true;
            txtResults.ScrollBars = ScrollBars.Vertical;
            txtResults.Size = new Size(392, 121);
            txtResults.TabIndex = 0;
            // 
            // chartSorting
            // 
            chartSorting.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            chartSorting.BorderlineColor = Color.Black;
            chartSorting.BorderlineDashStyle = ChartDashStyle.Solid;
            chartArea1.Name = "ChartArea1";
            chartSorting.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            chartSorting.Legends.Add(legend1);
            chartSorting.Location = new Point(440, 46);
            chartSorting.Margin = new Padding(4, 5, 4, 5);
            chartSorting.Name = "chartSorting";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            chartSorting.Series.Add(series1);
            chartSorting.Size = new Size(756, 554);
            chartSorting.TabIndex = 4;
            chartSorting.Text = "chartSorting";
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(20, 20);
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1, toolStripProgressBar1 });
            statusStrip1.Location = new Point(0, 605);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Padding = new Padding(1, 0, 19, 0);
            statusStrip1.Size = new Size(1200, 26);
            statusStrip1.TabIndex = 5;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(121, 20);
            toolStripStatusLabel1.Text = "Готово к работе";
            // 
            // toolStripProgressBar1
            // 
            toolStripProgressBar1.Name = "toolStripProgressBar1";
            toolStripProgressBar1.Size = new Size(133, 28);
            toolStripProgressBar1.Visible = false;
            // 
            // Lab4Control
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(statusStrip1);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(menuStrip1);
            Controls.Add(chartSorting);
            Margin = new Padding(4, 5, 4, 5);
            Name = "Lab4Control";
            Size = new Size(1200, 631);
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((ISupportInitialize)numericUpDownSize).EndInit();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            ((ISupportInitialize)chartSorting).EndInit();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}