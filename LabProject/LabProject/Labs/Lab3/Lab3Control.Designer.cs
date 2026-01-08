namespace LabProject.Labs
{
    partial class Lab3Control : System.Windows.Forms.UserControl
    {
        protected System.Windows.Forms.TextBox txtA;
        protected System.Windows.Forms.TextBox txtB;
        protected System.Windows.Forms.TextBox txtEpsilon;
        protected System.Windows.Forms.TextBox txtFunction;
        protected System.Windows.Forms.TextBox txtResult;
        protected System.Windows.Forms.Label lblA;
        protected System.Windows.Forms.Label lblB;
        protected System.Windows.Forms.Label lblEpsilon;
        protected System.Windows.Forms.Label lblFunction;
        protected System.Windows.Forms.Label lblResult;
        protected System.Windows.Forms.Label lblMode;
        protected System.Windows.Forms.PictureBox graphPictureBox;
        protected System.Windows.Forms.RadioButton radioMin;
        protected System.Windows.Forms.RadioButton radioMax;
        protected System.Windows.Forms.GroupBox groupBoxMode;
        protected System.Windows.Forms.MenuStrip menuStrip;
        protected System.Windows.Forms.ToolStripMenuItem calculateMenuItem;
        protected System.Windows.Forms.ToolStripMenuItem clearMenuItem;
        protected System.Windows.Forms.ToolStripMenuItem aboutMenuItem3;
        private System.ComponentModel.IContainer components = null;

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
            menuStrip = new MenuStrip();
            calculateMenuItem = new ToolStripMenuItem();
            clearMenuItem = new ToolStripMenuItem();
            aboutMenuItem3 = new ToolStripMenuItem();
            txtA = new TextBox();
            txtB = new TextBox();
            txtEpsilon = new TextBox();
            txtFunction = new TextBox();
            txtResult = new TextBox();
            lblA = new Label();
            lblB = new Label();
            lblEpsilon = new Label();
            lblFunction = new Label();
            lblResult = new Label();
            lblMode = new Label();
            graphPictureBox = new PictureBox();
            radioMin = new RadioButton();
            radioMax = new RadioButton();
            groupBoxMode = new GroupBox();
            inputPanel = new Panel();
            menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)graphPictureBox).BeginInit();
            groupBoxMode.SuspendLayout();
            inputPanel.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip
            // 
            menuStrip.ImageScalingSize = new Size(20, 20);
            menuStrip.Items.AddRange(new ToolStripItem[] { calculateMenuItem, clearMenuItem, aboutMenuItem3 });
            menuStrip.Location = new Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.Padding = new Padding(8, 3, 0, 3);
            menuStrip.Size = new Size(1200, 30);
            menuStrip.TabIndex = 0;
            menuStrip.Text = "menuStrip1";
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
            // aboutMenuItem3
            // 
            aboutMenuItem3.Name = "aboutMenuItem3";
            aboutMenuItem3.Size = new Size(81, 24);
            aboutMenuItem3.Text = "Справка";
            // 
            // txtA
            // 
            txtA.Location = new Point(175, 5);
            txtA.Margin = new Padding(4, 5, 4, 5);
            txtA.Name = "txtA";
            txtA.Size = new Size(132, 27);
            txtA.TabIndex = 1;
            // 
            // txtB
            // 
            txtB.Location = new Point(175, 40);
            txtB.Margin = new Padding(4, 5, 4, 5);
            txtB.Name = "txtB";
            txtB.Size = new Size(132, 27);
            txtB.TabIndex = 2;
            // 
            // txtEpsilon
            // 
            txtEpsilon.Location = new Point(175, 77);
            txtEpsilon.Margin = new Padding(4, 5, 4, 5);
            txtEpsilon.Name = "txtEpsilon";
            txtEpsilon.Size = new Size(132, 27);
            txtEpsilon.TabIndex = 3;
            // 
            // txtFunction
            // 
            txtFunction.Location = new Point(110, 114);
            txtFunction.Margin = new Padding(4, 5, 4, 5);
            txtFunction.Name = "txtFunction";
            txtFunction.Size = new Size(265, 27);
            txtFunction.TabIndex = 4;
            // 
            // txtResult
            // 
            txtResult.Location = new Point(27, 446);
            txtResult.Margin = new Padding(4, 5, 4, 5);
            txtResult.Multiline = true;
            txtResult.Name = "txtResult";
            txtResult.ReadOnly = true;
            txtResult.Size = new Size(372, 152);
            txtResult.TabIndex = 5;
            // 
            // lblA
            // 
            lblA.AutoSize = true;
            lblA.Location = new Point(4, 5);
            lblA.Margin = new Padding(4, 0, 4, 0);
            lblA.Name = "lblA";
            lblA.Size = new Size(163, 20);
            lblA.TabIndex = 6;
            lblA.Text = "Начало интервала (a):";
            // 
            // lblB
            // 
            lblB.AutoSize = true;
            lblB.Location = new Point(11, 43);
            lblB.Margin = new Padding(4, 0, 4, 0);
            lblB.Name = "lblB";
            lblB.Size = new Size(156, 20);
            lblB.TabIndex = 7;
            lblB.Text = "Конец интервала (b):";
            // 
            // lblEpsilon
            // 
            lblEpsilon.AutoSize = true;
            lblEpsilon.Location = new Point(40, 80);
            lblEpsilon.Margin = new Padding(4, 0, 4, 0);
            lblEpsilon.Name = "lblEpsilon";
            lblEpsilon.Size = new Size(97, 20);
            lblEpsilon.TabIndex = 8;
            lblEpsilon.Text = "Точность (ε):";
            // 
            // lblFunction
            // 
            lblFunction.AutoSize = true;
            lblFunction.Location = new Point(4, 111);
            lblFunction.Margin = new Padding(4, 0, 4, 0);
            lblFunction.Name = "lblFunction";
            lblFunction.Size = new Size(98, 20);
            lblFunction.TabIndex = 9;
            lblFunction.Text = "Функция f(x):";
            // 
            // lblResult
            // 
            lblResult.AutoSize = true;
            lblResult.Location = new Point(27, 415);
            lblResult.Margin = new Padding(4, 0, 4, 0);
            lblResult.Name = "lblResult";
            lblResult.Size = new Size(78, 20);
            lblResult.TabIndex = 10;
            lblResult.Text = "Результат:";
            // 
            // lblMode
            // 
            lblMode.AutoSize = true;
            lblMode.Location = new Point(12, 158);
            lblMode.Margin = new Padding(4, 0, 4, 0);
            lblMode.Name = "lblMode";
            lblMode.Size = new Size(112, 20);
            lblMode.TabIndex = 11;
            lblMode.Text = "Режим поиска:";
            // 
            // graphPictureBox
            // 
            graphPictureBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            graphPictureBox.BackColor = Color.White;
            graphPictureBox.Location = new Point(467, 30);
            graphPictureBox.Margin = new Padding(4, 5, 4, 5);
            graphPictureBox.Name = "graphPictureBox";
            graphPictureBox.Size = new Size(716, 875);
            graphPictureBox.SizeMode = PictureBoxSizeMode.AutoSize;
            graphPictureBox.TabIndex = 12;
            graphPictureBox.TabStop = false;
            // 
            // radioMin
            // 
            radioMin.AutoSize = true;
            radioMin.Checked = true;
            radioMin.Location = new Point(13, 15);
            radioMin.Margin = new Padding(4, 5, 4, 5);
            radioMin.Name = "radioMin";
            radioMin.Size = new Size(99, 24);
            radioMin.TabIndex = 14;
            radioMin.TabStop = true;
            radioMin.Text = "Минимум";
            radioMin.UseVisualStyleBackColor = true;
            // 
            // radioMax
            // 
            radioMax.AutoSize = true;
            radioMax.Location = new Point(13, 49);
            radioMax.Margin = new Padding(4, 5, 4, 5);
            radioMax.Name = "radioMax";
            radioMax.Size = new Size(103, 24);
            radioMax.TabIndex = 15;
            radioMax.Text = "Максимум";
            radioMax.UseVisualStyleBackColor = true;
            // 
            // groupBoxMode
            // 
            groupBoxMode.Controls.Add(radioMin);
            groupBoxMode.Controls.Add(radioMax);
            groupBoxMode.Location = new Point(175, 158);
            groupBoxMode.Margin = new Padding(4, 5, 4, 5);
            groupBoxMode.Name = "groupBoxMode";
            groupBoxMode.Padding = new Padding(4, 5, 4, 5);
            groupBoxMode.Size = new Size(133, 92);
            groupBoxMode.TabIndex = 16;
            groupBoxMode.TabStop = false;
            // 
            // inputPanel
            // 
            inputPanel.BorderStyle = BorderStyle.FixedSingle;
            inputPanel.Controls.Add(groupBoxMode);
            inputPanel.Controls.Add(txtA);
            inputPanel.Controls.Add(lblA);
            inputPanel.Controls.Add(lblMode);
            inputPanel.Controls.Add(txtB);
            inputPanel.Controls.Add(lblB);
            inputPanel.Controls.Add(lblFunction);
            inputPanel.Controls.Add(txtFunction);
            inputPanel.Controls.Add(txtEpsilon);
            inputPanel.Controls.Add(lblEpsilon);
            inputPanel.Location = new Point(27, 31);
            inputPanel.Margin = new Padding(4, 5, 4, 5);
            inputPanel.Name = "inputPanel";
            inputPanel.Size = new Size(399, 368);
            inputPanel.TabIndex = 13;
            // 
            // Lab3Control
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(graphPictureBox);
            Controls.Add(lblResult);
            Controls.Add(txtResult);
            Controls.Add(menuStrip);
            Controls.Add(inputPanel);
            Margin = new Padding(4, 5, 4, 5);
            Name = "Lab3Control";
            Size = new Size(1200, 923);
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)graphPictureBox).EndInit();
            groupBoxMode.ResumeLayout(false);
            groupBoxMode.PerformLayout();
            inputPanel.ResumeLayout(false);
            inputPanel.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }
        protected Panel inputPanel;
    }
}