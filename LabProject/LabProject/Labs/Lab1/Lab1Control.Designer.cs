using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml.Linq;

namespace LabProject.Labs
{
    partial class Lab1Control
    {
        private System.ComponentModel.IContainer components = null;
        private TextBox txtA;
        private TextBox txtB;
        private TextBox txtE;
        private TextBox txtFunc;
        private Label lblA;
        private Label lblB;
        private Label lblE;
        private Label lblFunc;
        private Label lblResult;
        private Chart chart;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem рассчитатьToolStripMenuItem;
        private ToolStripMenuItem очиститьToolStripMenuItem;

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
            ChartArea chartArea2 = new ChartArea();
            Series series3 = new Series();
            Series series4 = new Series();
            txtA = new TextBox();
            txtB = new TextBox();
            txtE = new TextBox();
            txtFunc = new TextBox();
            lblA = new Label();
            lblB = new Label();
            lblE = new Label();
            lblFunc = new Label();
            lblResult = new Label();
            chart = new Chart();
            menuStrip1 = new MenuStrip();
            рассчитатьToolStripMenuItem = new ToolStripMenuItem();
            очиститьToolStripMenuItem = new ToolStripMenuItem();
            aboutMenuItem1 = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)chart).BeginInit();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // txtA
            // 
            txtA.Location = new Point(57, 30);
            txtA.Name = "txtA";
            txtA.Size = new Size(120, 27);
            txtA.TabIndex = 0;
            // 
            // txtB
            // 
            txtB.Location = new Point(247, 30);
            txtB.Name = "txtB";
            txtB.Size = new Size(120, 27);
            txtB.TabIndex = 1;
            // 
            // txtE
            // 
            txtE.Location = new Point(437, 30);
            txtE.Name = "txtE";
            txtE.Size = new Size(120, 27);
            txtE.TabIndex = 2;
            // 
            // txtFunc
            // 
            txtFunc.Location = new Point(57, 70);
            txtFunc.Name = "txtFunc";
            txtFunc.Size = new Size(300, 27);
            txtFunc.TabIndex = 3;
            // 
            // lblA
            // 
            lblA.AutoSize = true;
            lblA.Location = new Point(17, 33);
            lblA.Name = "lblA";
            lblA.Size = new Size(20, 20);
            lblA.TabIndex = 4;
            lblA.Text = "a:";
            // 
            // lblB
            // 
            lblB.AutoSize = true;
            lblB.Location = new Point(212, 33);
            lblB.Name = "lblB";
            lblB.Size = new Size(21, 20);
            lblB.TabIndex = 5;
            lblB.Text = "b:";
            // 
            // lblE
            // 
            lblE.AutoSize = true;
            lblE.Location = new Point(402, 33);
            lblE.Name = "lblE";
            lblE.Size = new Size(20, 20);
            lblE.TabIndex = 6;
            lblE.Text = "e:";
            // 
            // lblFunc
            // 
            lblFunc.AutoSize = true;
            lblFunc.Location = new Point(17, 73);
            lblFunc.Name = "lblFunc";
            lblFunc.Size = new Size(34, 20);
            lblFunc.TabIndex = 7;
            lblFunc.Text = "f(x):";
            // 
            // lblResult
            // 
            lblResult.AutoSize = true;
            lblResult.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            lblResult.Location = new Point(20, 100);
            lblResult.Name = "lblResult";
            lblResult.Size = new Size(93, 23);
            lblResult.TabIndex = 10;
            lblResult.Text = "Результат:";
            // 
            // chart
            // 
            chartArea2.Name = "ChartArea1";
            chart.ChartAreas.Add(chartArea2);
            chart.Location = new Point(20, 130);
            chart.Name = "chart";
            chart.Palette = ChartColorPalette.SeaGreen;
            series3.BorderWidth = 2;
            series3.ChartArea = "ChartArea1";
            series3.ChartType = SeriesChartType.Line;
            series3.Name = "f";
            series4.ChartArea = "ChartArea1";
            series4.ChartType = SeriesChartType.Point;
            series4.MarkerSize = 10;
            series4.MarkerStyle = MarkerStyle.Circle;
            series4.Name = "min";
            chart.Series.Add(series3);
            chart.Series.Add(series4);
            chart.Size = new Size(740, 400);
            chart.TabIndex = 11;
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { рассчитатьToolStripMenuItem, очиститьToolStripMenuItem, aboutMenuItem1 });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(800, 28);
            menuStrip1.TabIndex = 12;
            menuStrip1.Text = "menuStrip1";
            // 
            // рассчитатьToolStripMenuItem
            // 
            рассчитатьToolStripMenuItem.Name = "рассчитатьToolStripMenuItem";
            рассчитатьToolStripMenuItem.Size = new Size(98, 24);
            рассчитатьToolStripMenuItem.Text = "Рассчитать";
            // 
            // очиститьToolStripMenuItem
            // 
            очиститьToolStripMenuItem.Name = "очиститьToolStripMenuItem";
            очиститьToolStripMenuItem.Size = new Size(87, 24);
            очиститьToolStripMenuItem.Text = "Очистить";
            // 
            // aboutMenuItem1
            // 
            aboutMenuItem1.Name = "aboutMenuItem1";
            aboutMenuItem1.Size = new Size(81, 24);
            aboutMenuItem1.Text = "Справка";
            aboutMenuItem1.Click += aboutMenuItem1_Click;
            // 
            // Lab1Control
            // 
            Controls.Add(chart);
            Controls.Add(lblResult);
            Controls.Add(lblFunc);
            Controls.Add(lblE);
            Controls.Add(lblB);
            Controls.Add(lblA);
            Controls.Add(txtFunc);
            Controls.Add(txtE);
            Controls.Add(txtB);
            Controls.Add(txtA);
            Controls.Add(menuStrip1);
            Name = "Lab1Control";
            Size = new Size(800, 550);
            ((System.ComponentModel.ISupportInitialize)chart).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }
        private ToolStripMenuItem aboutMenuItem1;
    }
}