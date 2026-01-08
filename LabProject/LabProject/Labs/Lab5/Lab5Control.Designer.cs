using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace LabProject.Labs
{
    partial class Lab5Control : UserControl
    {
        private IContainer components = null;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem calculateMenuItem;
        private ToolStripMenuItem clearMenuItem;
        private ToolStripMenuItem generateDataMenuItem;
        private GroupBox groupBox1;
        private TextBox txtFunction;
        private Label label1;
        private Label label2;
        private TextBox txtA;
        private TextBox txtB;
        private Label label3;
        private TextBox txtEpsilon;
        private GroupBox groupBox2;
        private CheckBox chkRectangles;
        private CheckBox chkTrapezoidal;
        private CheckBox chkSimpson;
        private GroupBox groupBox3;
        private TextBox txtResults;
        private Chart chartMain;
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
            groupBox1 = new GroupBox();
            txtFunction = new TextBox();
            label1 = new Label();
            label2 = new Label();
            txtA = new TextBox();
            txtB = new TextBox();
            label3 = new Label();
            txtEpsilon = new TextBox();
            groupBox2 = new GroupBox();
            chkSimpson = new CheckBox();
            chkTrapezoidal = new CheckBox();
            chkRectangles = new CheckBox();
            groupBox3 = new GroupBox();
            txtResults = new TextBox();
            chartMain = new Chart();
            statusStrip1 = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            toolStripProgressBar1 = new ToolStripProgressBar();
            справкаToolStripMenuItem = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            ((ISupportInitialize)chartMain).BeginInit();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { calculateMenuItem, clearMenuItem, generateDataMenuItem, справкаToolStripMenuItem });
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
            // groupBox1
            // 
            groupBox1.Controls.Add(txtFunction);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(txtA);
            groupBox1.Controls.Add(txtB);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(txtEpsilon);
            groupBox1.Location = new Point(16, 46);
            groupBox1.Margin = new Padding(4, 5, 4, 5);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(4, 5, 4, 5);
            groupBox1.Size = new Size(400, 185);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Ввод данных";
            // 
            // txtFunction
            // 
            txtFunction.Location = new Point(93, 33);
            txtFunction.Margin = new Padding(4, 5, 4, 5);
            txtFunction.Name = "txtFunction";
            txtFunction.Size = new Size(285, 27);
            txtFunction.TabIndex = 0;
            txtFunction.Text = "x^2";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(13, 36);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(72, 20);
            label1.TabIndex = 0;
            label1.Text = "Функция:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(13, 85);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(74, 20);
            label2.TabIndex = 2;
            label2.Text = "Границы:";
            // 
            // txtA
            // 
            txtA.Location = new Point(93, 80);
            txtA.Margin = new Padding(4, 5, 4, 5);
            txtA.Name = "txtA";
            txtA.Size = new Size(79, 27);
            txtA.TabIndex = 1;
            txtA.Text = "0";
            // 
            // txtB
            // 
            txtB.Location = new Point(200, 80);
            txtB.Margin = new Padding(4, 5, 4, 5);
            txtB.Name = "txtB";
            txtB.Size = new Size(79, 27);
            txtB.TabIndex = 2;
            txtB.Text = "2";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(13, 131);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(76, 20);
            label3.TabIndex = 6;
            label3.Text = "Точность:";
            // 
            // txtEpsilon
            // 
            txtEpsilon.Location = new Point(93, 126);
            txtEpsilon.Margin = new Padding(4, 5, 4, 5);
            txtEpsilon.Name = "txtEpsilon";
            txtEpsilon.Size = new Size(132, 27);
            txtEpsilon.TabIndex = 3;
            txtEpsilon.Text = "0.0001";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(chkSimpson);
            groupBox2.Controls.Add(chkTrapezoidal);
            groupBox2.Controls.Add(chkRectangles);
            groupBox2.Location = new Point(16, 240);
            groupBox2.Margin = new Padding(4, 5, 4, 5);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(4, 5, 4, 5);
            groupBox2.Size = new Size(400, 154);
            groupBox2.TabIndex = 2;
            groupBox2.TabStop = false;
            groupBox2.Text = "Выберите методы:";
            // 
            // chkSimpson
            // 
            chkSimpson.AutoSize = true;
            chkSimpson.Checked = true;
            chkSimpson.CheckState = CheckState.Checked;
            chkSimpson.Location = new Point(13, 115);
            chkSimpson.Margin = new Padding(4, 5, 4, 5);
            chkSimpson.Name = "chkSimpson";
            chkSimpson.Size = new Size(150, 24);
            chkSimpson.TabIndex = 2;
            chkSimpson.Text = "Метод Симпсона";
            chkSimpson.UseVisualStyleBackColor = true;
            // 
            // chkTrapezoidal
            // 
            chkTrapezoidal.AutoSize = true;
            chkTrapezoidal.Checked = true;
            chkTrapezoidal.CheckState = CheckState.Checked;
            chkTrapezoidal.Location = new Point(13, 85);
            chkTrapezoidal.Margin = new Padding(4, 5, 4, 5);
            chkTrapezoidal.Name = "chkTrapezoidal";
            chkTrapezoidal.Size = new Size(146, 24);
            chkTrapezoidal.TabIndex = 1;
            chkTrapezoidal.Text = "Метод трапеций";
            chkTrapezoidal.UseVisualStyleBackColor = true;
            // 
            // chkRectangles
            // 
            chkRectangles.AutoSize = true;
            chkRectangles.Checked = true;
            chkRectangles.CheckState = CheckState.Checked;
            chkRectangles.Location = new Point(13, 54);
            chkRectangles.Margin = new Padding(4, 5, 4, 5);
            chkRectangles.Name = "chkRectangles";
            chkRectangles.Size = new Size(205, 24);
            chkRectangles.TabIndex = 0;
            chkRectangles.Text = "Метод прямоугольников";
            chkRectangles.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            groupBox3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            groupBox3.AutoSize = true;
            groupBox3.Controls.Add(txtResults);
            groupBox3.Location = new Point(0, 417);
            groupBox3.Margin = new Padding(4, 5, 4, 5);
            groupBox3.Name = "groupBox3";
            groupBox3.Padding = new Padding(4, 5, 4, 5);
            groupBox3.Size = new Size(416, 183);
            groupBox3.TabIndex = 3;
            groupBox3.TabStop = false;
            groupBox3.Text = "Результаты:";
            // 
            // txtResults
            // 
            txtResults.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtResults.Location = new Point(8, 30);
            txtResults.Margin = new Padding(4, 5, 4, 5);
            txtResults.Multiline = true;
            txtResults.Name = "txtResults";
            txtResults.ReadOnly = true;
            txtResults.ScrollBars = ScrollBars.Vertical;
            txtResults.Size = new Size(400, 143);
            txtResults.TabIndex = 0;
            // 
            // chartMain
            // 
            chartMain.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            chartMain.BorderlineColor = Color.Black;
            chartMain.BorderlineDashStyle = ChartDashStyle.Solid;
            chartArea1.Name = "ChartArea1";
            chartMain.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            chartMain.Legends.Add(legend1);
            chartMain.Location = new Point(424, 35);
            chartMain.Margin = new Padding(4, 5, 4, 5);
            chartMain.Name = "chartMain";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            chartMain.Series.Add(series1);
            chartMain.Size = new Size(772, 565);
            chartMain.TabIndex = 4;
            chartMain.Text = "chartMain";
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
            // справкаToolStripMenuItem
            // 
            справкаToolStripMenuItem.Name = "справкаToolStripMenuItem";
            справкаToolStripMenuItem.Size = new Size(81, 24);
            справкаToolStripMenuItem.Text = "Справка";
            справкаToolStripMenuItem.Click += справкаToolStripMenuItem_Click;
            // 
            // Lab5Control
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(statusStrip1);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(menuStrip1);
            Controls.Add(chartMain);
            Margin = new Padding(4, 5, 4, 5);
            Name = "Lab5Control";
            Size = new Size(1200, 631);
            Load += Lab5Control_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            ((ISupportInitialize)chartMain).EndInit();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        private void Lab5Control_Load(object sender, EventArgs e)
        {
            InitializeChart();
        }

        private void InitializeChart()
        {
            chartMain.Series.Clear();
            chartMain.ChartAreas[0].AxisX.Title = "x";
            chartMain.ChartAreas[0].AxisY.Title = "f(x)";
            chartMain.ChartAreas[0].AxisX.Minimum = 0;
            chartMain.ChartAreas[0].AxisX.Maximum = 2;
            chartMain.ChartAreas[0].AxisY.Minimum = 0;
            chartMain.ChartAreas[0].AxisY.Maximum = 5;
        }
        private ToolStripMenuItem справкаToolStripMenuItem;
    }
}