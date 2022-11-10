namespace EasyWatermark.App.View
{
    partial class FrmSetupWatermark
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
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.nmrY = new DevExpress.XtraEditors.SpinEdit();
            this.nmrX = new DevExpress.XtraEditors.SpinEdit();
            this.nmrH = new DevExpress.XtraEditors.SpinEdit();
            this.nmrW = new DevExpress.XtraEditors.SpinEdit();
            this.picImage = new System.Windows.Forms.PictureBox();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmrY.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmrX.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmrH.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmrW.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btnCancel);
            this.layoutControl1.Controls.Add(this.simpleButton1);
            this.layoutControl1.Controls.Add(this.nmrY);
            this.layoutControl1.Controls.Add(this.nmrX);
            this.layoutControl1.Controls.Add(this.nmrH);
            this.layoutControl1.Controls.Add(this.nmrW);
            this.layoutControl1.Controls.Add(this.picImage);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(390, 478);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // btnCancel
            // 
            this.btnCancel.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Danger;
            this.btnCancel.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Appearance.Options.UseBackColor = true;
            this.btnCancel.Appearance.Options.UseFont = true;
            this.btnCancel.Location = new System.Drawing.Point(198, 434);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(176, 28);
            this.btnCancel.StyleController = this.layoutControl1;
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Success;
            this.simpleButton1.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simpleButton1.Appearance.Options.UseBackColor = true;
            this.simpleButton1.Appearance.Options.UseFont = true;
            this.simpleButton1.Location = new System.Drawing.Point(16, 434);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(176, 28);
            this.simpleButton1.StyleController = this.layoutControl1;
            this.simpleButton1.TabIndex = 9;
            this.simpleButton1.Text = "Save";
            this.simpleButton1.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // nmrY
            // 
            this.nmrY.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nmrY.Location = new System.Drawing.Point(255, 368);
            this.nmrY.Name = "nmrY";
            this.nmrY.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.nmrY.Properties.MaskSettings.Set("mask", "n0");
            this.nmrY.Size = new System.Drawing.Size(119, 30);
            this.nmrY.StyleController = this.layoutControl1;
            this.nmrY.TabIndex = 8;
            // 
            // nmrX
            // 
            this.nmrX.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nmrX.Location = new System.Drawing.Point(73, 368);
            this.nmrX.Name = "nmrX";
            this.nmrX.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.nmrX.Properties.MaskSettings.Set("mask", "n0");
            this.nmrX.Size = new System.Drawing.Size(119, 30);
            this.nmrX.StyleController = this.layoutControl1;
            this.nmrX.TabIndex = 7;
            // 
            // nmrH
            // 
            this.nmrH.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nmrH.Location = new System.Drawing.Point(255, 332);
            this.nmrH.Name = "nmrH";
            this.nmrH.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.nmrH.Properties.MaskSettings.Set("mask", "n0");
            this.nmrH.Size = new System.Drawing.Size(119, 30);
            this.nmrH.StyleController = this.layoutControl1;
            this.nmrH.TabIndex = 6;
            // 
            // nmrW
            // 
            this.nmrW.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nmrW.Location = new System.Drawing.Point(73, 332);
            this.nmrW.Name = "nmrW";
            this.nmrW.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.nmrW.Properties.MaskSettings.Set("mask", "n0");
            this.nmrW.Size = new System.Drawing.Size(119, 30);
            this.nmrW.StyleController = this.layoutControl1;
            this.nmrW.TabIndex = 5;
            // 
            // picImage
            // 
            this.picImage.Location = new System.Drawing.Point(16, 16);
            this.picImage.Name = "picImage";
            this.picImage.Size = new System.Drawing.Size(358, 310);
            this.picImage.TabIndex = 4;
            this.picImage.TabStop = false;
            this.picImage.Paint += new System.Windows.Forms.PaintEventHandler(this.picImage_Paint);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem6,
            this.emptySpaceItem1,
            this.layoutControlItem7});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(390, 478);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.picImage;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(364, 316);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.nmrW;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 316);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(182, 36);
            this.layoutControlItem2.Text = "Width:";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(41, 16);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.nmrH;
            this.layoutControlItem3.Location = new System.Drawing.Point(182, 316);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(182, 36);
            this.layoutControlItem3.Text = "Height:";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(41, 16);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.nmrX;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 352);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(182, 36);
            this.layoutControlItem4.Text = "X:";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(41, 16);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.nmrY;
            this.layoutControlItem5.Location = new System.Drawing.Point(182, 352);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(182, 36);
            this.layoutControlItem5.Text = "Y:";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(41, 16);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.simpleButton1;
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 418);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(182, 34);
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 388);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(364, 30);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.btnCancel;
            this.layoutControlItem7.Location = new System.Drawing.Point(182, 418);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(182, 34);
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextVisible = false;
            // 
            // FrmSetupWatermark
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(390, 478);
            this.Controls.Add(this.layoutControl1);
            this.Name = "FrmSetupWatermark";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Watermark Settings";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nmrY.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmrX.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmrH.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmrW.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private System.Windows.Forms.PictureBox picImage;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.SpinEdit nmrH;
        private DevExpress.XtraEditors.SpinEdit nmrW;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraEditors.SpinEdit nmrX;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraEditors.SpinEdit nmrY;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
    }
}