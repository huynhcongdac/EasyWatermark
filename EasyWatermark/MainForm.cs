using DevExpress.LookAndFeel;
using DevExpress.Utils.Extensions;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Popup;
using EasyWatermark.App.Model;
using EasyWatermark.App.View;
using EasyWatermark.App.ViewModel;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EasyWatermark
{
    public partial class MainForm : XtraForm
    {
        public const string ProcessingText = "Processing";
        public const string CompleteText = "Complete";
        public const string ErrorText = "Error";
        private AppConfigManager _configManager;
        private BaseEdit[] _editors;
        public MainForm()
        {
            InitializeComponent();
            _editors = new BaseEdit[]
            {
                txtImageFolder,
                txtWatermarkImage,
                txtOutputFolder
            };
            _configManager = AppConfigManager.Instance;
            var config = _configManager.Load();
            txtImageFolder.Text = config.ImageFolder;
            txtWatermarkImage.Text = config.WatermakFileName;
            txtOutputFolder.Text = config.OutputFolder;
            gridControl1.DataSource = new List<DesignViewModel>();
            gridView1.ConfigureGlobalSettings();
            gridView1.OptionsView.ShowGroupPanel = false;
            gridView1.CustomDrawCell += GridView1_CustomDrawCell;
            OnImageFolderChanged();
            ConfigureDragDropBehaviors();
            _editors.ForEach(editor => editor.EditValueChanged += Editor_EditValueChanged);
        }

        private void GridView1_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column.FieldName == nameof(DesignViewModel.Status))
            {
                var cellValue = e.CellValue?.ToString() ?? "";
                switch (cellValue)
                {
                    case ProcessingText:
                        e.Appearance.ForeColor = Color.Blue;
                        break;
                    case CompleteText:
                        e.Appearance.ForeColor = Color.Green;
                        break;
                    case ErrorText:
                        e.Appearance.ForeColor = Color.Red;
                        break;
                }
            }
        }

        private void Editor_EditValueChanged(object sender, EventArgs e)
        {
            _configManager.DynamicUpdate(c =>
            {
                c.ImageFolder = txtImageFolder.Text;
                c.WatermakFileName = txtWatermarkImage.Text;
                c.OutputFolder = txtOutputFolder.Text;
            });
            OnImageFolderChanged();
        }

        private void OnImageFolderChanged()
        {
            if (!Directory.Exists(txtImageFolder.Text)) return;
            var dir = new DirectoryInfo(txtImageFolder.Text);
            var imgFiles = dir.GetFiles();
            var vmList = new List<DesignViewModel>();
            foreach (var file in imgFiles)
            {
                var vm = new DesignViewModel()
                {
                    Name = file.Name,
                    Status = ""
                };
                vmList.Add(vm);
            }
            lblStatus.Caption = $"{imgFiles.Length} image(s) found";
            gridControl1.DataSource = vmList;
        }

        private void lblVisitWeb_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://easycrawler.net");
        }

        private void lblVisitFacebook_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://www.facebook.com/easycrawler");
        }

        private bool _isRunning;
        private bool _isStoppingRequested;
        private async void btnStartStop_Click(object sender, EventArgs e)
        {
            if (!_isRunning)
            {
                _isRunning = true;
                _isStoppingRequested = false;

                var imgFolder = txtImageFolder.Text;
                var outputFolder = txtOutputFolder.Text;
                var watermarkFile = txtWatermarkImage.Text;

                if (!Directory.Exists(imgFolder))
                {
                    XtraMessageBox.Show($"Image folder does not exist",
                        Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (!File.Exists(watermarkFile))
                {
                    XtraMessageBox.Show($"Watermark file does not exist",
                        Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (!Directory.Exists(outputFolder))
                {
                    XtraMessageBox.Show($"Output folder does not exist",
                        Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                var config = _configManager.Load();
                if (config.WatermarkArea.IsEmpty)
                {
                    XtraMessageBox.Show($"You did not setup watermark area. Please setup first",
                        Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnSetupWatermark_Click(null, null);
                    return;
                }

                Bitmap watermark = null;
                try
                {
                    watermark = AppHelper.GetBitmapFromFile(watermarkFile);
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show($"Error loading watermark file:{Environment.NewLine}" +
                        $"{ex.Message}",
                        Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var imgFiles = new DirectoryInfo(imgFolder).GetFiles();
                var count = 0;
                var areaInfo = new AreaInfo()
                {
                    Area = config.WatermarkArea,
                    OriginalSize = config.ImageOriginalSize
                };
                OnStarted();
                foreach (var imgFile in imgFiles)
                {
                    if (_isStoppingRequested) break;
                    count++;
                    Exception ex = null;
                    var items = gridControl1.DataSource as List<DesignViewModel>;
                    var target = items.FirstOrDefault(i => i.Name == imgFile.Name);
                    if(target != null)
                    {
                        target.Status = ProcessingText;
                        gridControl1.RefreshDataSource();
                    }
                    await Task.Run(async () =>
                    {
                        try
                        {
                            var img = AppHelper.GetBitmapFromFile(imgFile.FullName);
                            var watermarkedImg = await AppHelper.AddWatermarkAsync(img, watermark, areaInfo);
                            var outputFile = Path.Combine(outputFolder, $"watermarked-{imgFile.Name}");
                            var format = imgFile.Extension.ToLower().EndsWith("png") ? ImageFormat.Png : ImageFormat.Jpeg;
                            watermarkedImg.Save(outputFile, format);
                        }
                        catch (Exception exception)
                        {
                            ex = exception;
                        }
                    });
                    var status = ex == null ? CompleteText : ErrorText;
                    if (target != null)
                    {
                        target.Status = status;
                        gridControl1.RefreshDataSource();
                    }

                    lblStatus.Caption = $"Processing {count}/{imgFiles.Length}";
                }
                OnStopped();
                XtraMessageBox.Show($"Complete", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (Directory.Exists(outputFolder))
                {
                    Process.Start(outputFolder);
                }
            }
            else
            {
                _isStoppingRequested = true;
                btnStartStop.Text = "STOPPING...";
                btnStartStop.Enabled = false;
            }
        }

        private void OnStarted()
        {
            btnStartStop.Appearance.BackColor = DXSkinColors.FillColors.Danger;
            btnStartStop.Text = "STOP";
            txtImageFolder.Enabled = btnBrowseImageFolder.Enabled = txtWatermarkImage.Enabled = btnSelectWatermarkFile.Enabled = btnSelectWatermarkFile.Enabled = btnSetupWatermark.Enabled = txtOutputFolder.Enabled = btnBrowseOutputFolder.Enabled = false;
            lblStatus.Caption = "Processing...";
        }

        private void OnStopped()
        {
            btnStartStop.Appearance.BackColor = DXSkinColors.FillColors.Question;
            btnStartStop.Text = "START";
            btnStartStop.Enabled = true;
            txtImageFolder.Enabled = btnBrowseImageFolder.Enabled = txtWatermarkImage.Enabled = btnSelectWatermarkFile.Enabled = btnSelectWatermarkFile.Enabled = btnSetupWatermark.Enabled = txtOutputFolder.Enabled = btnBrowseOutputFolder.Enabled = true;
        }

        private void btnSetupWatermark_Click(object sender, EventArgs e)
        {
            using (var frm = new FrmSetupWatermark())
            {
                frm.ShowDialog();
            }
        }

        private void btnBrowseImageFolder_Click(object sender, EventArgs e)
        {
            using (var fbd = new CommonOpenFileDialog())
            {
                fbd.IsFolderPicker = true;
                if (fbd.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    txtImageFolder.Text = fbd.FileName;
                }
            }
        }

        private void btnSelectWatermarkFile_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.Title = "Select image file";
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    txtWatermarkImage.Text = ofd.FileName;
                }
            }
        }

        private void btnBrowseOutputFolder_Click(object sender, EventArgs e)
        {
            using (var fbd = new CommonOpenFileDialog())
            {
                fbd.IsFolderPicker = true;
                if (fbd.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    txtOutputFolder.Text = fbd.FileName;
                }
            }
        }

        private void ConfigureDragDropBehaviors()
        {
            DragEventHandler action = (a, e) =>
            {
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                {
                    e.Effect = DragDropEffects.Copy;
                }
                else
                {
                    e.Effect = DragDropEffects.None;
                }
            };
            txtImageFolder.DragEnter += action;
            txtWatermarkImage.DragEnter += action;

            txtImageFolder.DragDrop += (a, e) =>
            {
                var folders = (string[])e.Data.GetData(DataFormats.FileDrop, false);
                if (folders.Length == 0) return;
                var folder = folders.FirstOrDefault();
                txtImageFolder.Text = folder;
                OnImageFolderChanged();
            };
            txtWatermarkImage.DragDrop += (a, e) =>
            {
                var files = (string[])e.Data.GetData(DataFormats.FileDrop, false);
                if (files.Length == 0) return;
                var file = files.FirstOrDefault();
                txtWatermarkImage.Text = file;
            };
        }
    }
}
