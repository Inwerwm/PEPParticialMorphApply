namespace ParticialMorphApply
{
    partial class CtrlForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.treeViewMorph = new System.Windows.Forms.TreeView();
            this.trackBarMorphRatio = new System.Windows.Forms.TrackBar();
            this.textBoxMorphRatio = new System.Windows.Forms.TextBox();
            this.buttonRun = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarMorphRatio)).BeginInit();
            this.SuspendLayout();
            // 
            // treeViewMorph
            // 
            this.treeViewMorph.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeViewMorph.Location = new System.Drawing.Point(0, 0);
            this.treeViewMorph.Margin = new System.Windows.Forms.Padding(5);
            this.treeViewMorph.Name = "treeViewMorph";
            this.treeViewMorph.Size = new System.Drawing.Size(300, 661);
            this.treeViewMorph.TabIndex = 0;
            this.treeViewMorph.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewMorph_AfterSelect);
            // 
            // trackBarMorphRatio
            // 
            this.trackBarMorphRatio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBarMorphRatio.LargeChange = 50;
            this.trackBarMorphRatio.Location = new System.Drawing.Point(300, 33);
            this.trackBarMorphRatio.Maximum = 1000;
            this.trackBarMorphRatio.Name = "trackBarMorphRatio";
            this.trackBarMorphRatio.Size = new System.Drawing.Size(200, 45);
            this.trackBarMorphRatio.SmallChange = 10;
            this.trackBarMorphRatio.TabIndex = 1;
            this.trackBarMorphRatio.TickFrequency = 100;
            this.trackBarMorphRatio.Scroll += new System.EventHandler(this.trackBarMorphRatio_Scroll);
            // 
            // textBoxMorphRatio
            // 
            this.textBoxMorphRatio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxMorphRatio.Location = new System.Drawing.Point(300, 0);
            this.textBoxMorphRatio.Name = "textBoxMorphRatio";
            this.textBoxMorphRatio.Size = new System.Drawing.Size(200, 27);
            this.textBoxMorphRatio.TabIndex = 2;
            this.textBoxMorphRatio.Text = "0";
            this.textBoxMorphRatio.TextChanged += new System.EventHandler(this.textBoxMorphRatio_TextChanged);
            // 
            // buttonRun
            // 
            this.buttonRun.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRun.Location = new System.Drawing.Point(300, 611);
            this.buttonRun.Margin = new System.Windows.Forms.Padding(0);
            this.buttonRun.Name = "buttonRun";
            this.buttonRun.Size = new System.Drawing.Size(200, 50);
            this.buttonRun.TabIndex = 3;
            this.buttonRun.Text = "選択頂点に適用";
            this.buttonRun.UseVisualStyleBackColor = true;
            // 
            // CtrlForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 661);
            this.Controls.Add(this.buttonRun);
            this.Controls.Add(this.textBoxMorphRatio);
            this.Controls.Add(this.trackBarMorphRatio);
            this.Controls.Add(this.treeViewMorph);
            this.Font = new System.Drawing.Font("Yu Gothic UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MinimumSize = new System.Drawing.Size(516, 155);
            this.Name = "CtrlForm";
            this.Text = "CtrlForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CtrlForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.trackBarMorphRatio)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeViewMorph;
        private System.Windows.Forms.TrackBar trackBarMorphRatio;
        private System.Windows.Forms.TextBox textBoxMorphRatio;
        private System.Windows.Forms.Button buttonRun;
    }
}