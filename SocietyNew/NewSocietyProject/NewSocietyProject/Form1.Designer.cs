namespace NewSocietyProject
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.createWorldToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.population10ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.popylation50ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.population100ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.actionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startMovingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopMovingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pauseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.drawAreaPB = new System.Windows.Forms.PictureBox();
            this.speedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.slowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.drawAreaPB)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createWorldToolStripMenuItem,
            this.actionsToolStripMenuItem,
            this.speedToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1022, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // createWorldToolStripMenuItem
            // 
            this.createWorldToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.population10ToolStripMenuItem,
            this.popylation50ToolStripMenuItem,
            this.population100ToolStripMenuItem});
            this.createWorldToolStripMenuItem.Name = "createWorldToolStripMenuItem";
            this.createWorldToolStripMenuItem.Size = new System.Drawing.Size(88, 20);
            this.createWorldToolStripMenuItem.Text = "Create World";
            // 
            // population10ToolStripMenuItem
            // 
            this.population10ToolStripMenuItem.Name = "population10ToolStripMenuItem";
            this.population10ToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.population10ToolStripMenuItem.Text = "Population = 10";
            this.population10ToolStripMenuItem.Click += new System.EventHandler(this.population10ToolStripMenuItem_Click);
            // 
            // popylation50ToolStripMenuItem
            // 
            this.popylation50ToolStripMenuItem.Name = "popylation50ToolStripMenuItem";
            this.popylation50ToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.popylation50ToolStripMenuItem.Text = "Population = 50";
            this.popylation50ToolStripMenuItem.Click += new System.EventHandler(this.popylation50ToolStripMenuItem_Click);
            // 
            // population100ToolStripMenuItem
            // 
            this.population100ToolStripMenuItem.Name = "population100ToolStripMenuItem";
            this.population100ToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.population100ToolStripMenuItem.Text = "Population = 100";
            this.population100ToolStripMenuItem.Click += new System.EventHandler(this.population100ToolStripMenuItem_Click);
            // 
            // actionsToolStripMenuItem
            // 
            this.actionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startMovingToolStripMenuItem,
            this.stopMovingToolStripMenuItem,
            this.pauseToolStripMenuItem});
            this.actionsToolStripMenuItem.Name = "actionsToolStripMenuItem";
            this.actionsToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.actionsToolStripMenuItem.Text = "Actions";
            // 
            // startMovingToolStripMenuItem
            // 
            this.startMovingToolStripMenuItem.Name = "startMovingToolStripMenuItem";
            this.startMovingToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.startMovingToolStripMenuItem.Text = "Start moving!";
            this.startMovingToolStripMenuItem.Click += new System.EventHandler(this.startMovingToolStripMenuItem_Click);
            // 
            // stopMovingToolStripMenuItem
            // 
            this.stopMovingToolStripMenuItem.Name = "stopMovingToolStripMenuItem";
            this.stopMovingToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.stopMovingToolStripMenuItem.Text = "Stop moving!";
            this.stopMovingToolStripMenuItem.Click += new System.EventHandler(this.stopMovingToolStripMenuItem_Click);
            // 
            // pauseToolStripMenuItem
            // 
            this.pauseToolStripMenuItem.Name = "pauseToolStripMenuItem";
            this.pauseToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.pauseToolStripMenuItem.Text = "Pause";
            this.pauseToolStripMenuItem.Click += new System.EventHandler(this.pauseToolStripMenuItem_Click);
            // 
            // drawAreaPB
            // 
            this.drawAreaPB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.drawAreaPB.Location = new System.Drawing.Point(0, 24);
            this.drawAreaPB.Name = "drawAreaPB";
            this.drawAreaPB.Size = new System.Drawing.Size(1022, 379);
            this.drawAreaPB.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.drawAreaPB.TabIndex = 5;
            this.drawAreaPB.TabStop = false;
            this.drawAreaPB.SizeChanged += new System.EventHandler(this.drawAreaPB_SizeChanged);
            // 
            // speedToolStripMenuItem
            // 
            this.speedToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2,
            this.slowToolStripMenuItem});
            this.speedToolStripMenuItem.Name = "speedToolStripMenuItem";
            this.speedToolStripMenuItem.Size = new System.Drawing.Size(51, 20);
            this.speedToolStripMenuItem.Text = "Speed";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItem2.Text = "Faster";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // slowToolStripMenuItem
            // 
            this.slowToolStripMenuItem.Name = "slowToolStripMenuItem";
            this.slowToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.slowToolStripMenuItem.Text = "Slowly";
            this.slowToolStripMenuItem.Click += new System.EventHandler(this.slowToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1022, 403);
            this.Controls.Add(this.drawAreaPB);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Society Project";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.drawAreaPB)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem createWorldToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem population10ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem popylation50ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem population100ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem actionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startMovingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopMovingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pauseToolStripMenuItem;
        private System.Windows.Forms.PictureBox drawAreaPB;
        private System.Windows.Forms.ToolStripMenuItem speedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem slowToolStripMenuItem;
    }
}

