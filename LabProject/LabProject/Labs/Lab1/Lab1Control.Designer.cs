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
            ChartArea chartArea1 = new ChartArea();
            Series series1 = new Series();
            Series series2 = new Series();

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
            ((System.ComponentModel.ISupportInitialize)chart).BeginInit();
            menuStrip1.SuspendLayout();
            SuspendLayout();

            // txtA
            txtA.Location = new System.Drawing.Point(57, 30);
            txtA.Name = "txtA";
            txtA.Size = new System.Drawing.Size(120, 27);
            txtA.TabIndex = 0;

            // txtB
            txtB.Location = new System.Drawing.Point(247, 30);
            txtB.Name = "txtB";
            txtB.Size = new System.Drawing.Size(120, 27);
            txtB.TabIndex = 1;

            // txtE
            txtE.Location = new System.Drawing.Point(437, 30);
            txtE.Name = "txtE";
            txtE.Size = new System.Drawing.Size(120, 27);
            txtE.TabIndex = 2;

            // txtFunc
            txtFunc.Location = new System.Drawing.Point(57, 70);
            txtFunc.Name = "txtFunc";
            txtFunc.Size = new System.Drawing.Size(300, 27);
            txtFunc.TabIndex = 3;

            // lblA
            lblA.AutoSize = true;
            lblA.Location = new System.Drawing.Point(17, 33);
            lblA.Name = "lblA";
            lblA.Size = new System.Drawing.Size(20, 20);
            lblA.TabIndex = 4;
            lblA.Text = "a:";

            // lblB
            lblB.AutoSize = true;
            lblB.Location = new System.Drawing.Point(212, 33);
            lblB.Name = "lblB";
            lblB.Size = new System.Drawing.Size(21, 20);
            lblB.TabIndex = 5;
            lblB.Text = "b:";

            // lblE
            lblE.AutoSize = true;
            lblE.Location = new System.Drawing.Point(402, 33);
            lblE.Name = "lblE";
            lblE.Size = new System.Drawing.Size(20, 20);
            lblE.TabIndex = 6;
            lblE.Text = "e:";

            // lblFunc
            lblFunc.AutoSize = true;
            lblFunc.Location = new System.Drawing.Point(17, 73);
            lblFunc.Name = "lblFunc";
            lblFunc.Size = new System.Drawing.Size(34, 20);
            lblFunc.TabIndex = 7;
            lblFunc.Text = "f(x):";

            // lblResult
            lblResult.AutoSize = true;
            lblResult.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            lblResult.Location = new System.Drawing.Point(20, 100);
            lblResult.Name = "lblResult";
            lblResult.Size = new System.Drawing.Size(93, 23);
            lblResult.TabIndex = 10;
            lblResult.Text = "Результат:";

            // chart
            chartArea1.Name = "ChartArea1";
            chart.ChartAreas.Add(chartArea1);
            chart.Location = new System.Drawing.Point(20, 130);
            chart.Name = "chart";
            chart.Palette = ChartColorPalette.SeaGreen;
            series1.BorderWidth = 2;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = SeriesChartType.Line;
            series1.Name = "f";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = SeriesChartType.Point;
            series2.MarkerSize = 10;
            series2.MarkerStyle = MarkerStyle.Circle;
            series2.Name = "min";
            chart.Series.Add(series1);
            chart.Series.Add(series2);
            chart.Size = new System.Drawing.Size(740, 400);
            chart.TabIndex = 11;

            // menuStrip1
            menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] {
            рассчитатьToolStripMenuItem,
            очиститьToolStripMenuItem});
            menuStrip1.Location = new System.Drawing.Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new System.Drawing.Size(800, 28);
            menuStrip1.TabIndex = 12;
            menuStrip1.Text = "menuStrip1";

            // рассчитатьToolStripMenuItem
            рассчитатьToolStripMenuItem.Name = "рассчитатьToolStripMenuItem";
            рассчитатьToolStripMenuItem.Size = new System.Drawing.Size(98, 24);
            рассчитатьToolStripMenuItem.Text = "Рассчитать";

            // очиститьToolStripMenuItem
            очиститьToolStripMenuItem.Name = "очиститьToolStripMenuItem";
            очиститьToolStripMenuItem.Size = new System.Drawing.Size(87, 24);
            очиститьToolStripMenuItem.Text = "Очистить";

            // Lab1Control
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
            Size = new System.Drawing.Size(800, 550);
            ((System.ComponentModel.ISupportInitialize)chart).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}