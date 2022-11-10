using DevExpress.Utils.Extensions;
using DevExpress.XtraEditors;
using EasyWatermark.App.Model;
using EasyWatermark;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace EasyWatermark.App.View
{
    public partial class FrmSetupWatermark : XtraForm
    {
        private Bitmap _defaultDesign;
        private BaseEdit[] _editors;
        private FrameControl _frame;
        private AppConfigManager _configManager;
        public FrmSetupWatermark()
        {
            InitializeComponent();
            _configManager = AppConfigManager.Instance;
            var config = _configManager.Load();
            _editors = new BaseEdit[]
            {
                nmrW,
                nmrH,
                nmrX,
                nmrY
            };
            var sampleWatermarkPath = Path.Combine(Application.StartupPath, @"data\sample_watermark.png");
            var sampleImagePath = Path.Combine(Application.StartupPath, @"data\sample_image.png");
            if (File.Exists(sampleWatermarkPath))
            {
                _defaultDesign = new Bitmap(Image.FromFile(sampleWatermarkPath));
            }
            _editors.ForEach(editor => editor.EditValueChanged += Editor_EditValueChanged);

            var bmp = AppHelper.GetBitmapFromFile(sampleImagePath);
            picImage.Image = bmp;
            picImage.SizeMode = PictureBoxSizeMode.StretchImage;

            if (!config.WatermarkArea.IsEmpty)
            {
                _cropInfo = new AreaInfo()
                {
                    Area = config.WatermarkArea,
                    OriginalSize = config.ImageOriginalSize
                };
                //_rectangle = _cropInfo.Area;

                _frame = new FrameControl()
                {
                    Size = new Size(_cropInfo.Area.Width, _cropInfo.Area.Height),
                    Location = _cropInfo.Area.Location
                };
                picImage.Controls.Add(_frame);
                picImage.Refresh();
                UpdateRectangle();
            }
            else
            {
                var s = 100;
                _frame = new FrameControl();
                _frame.Size = new Size(s, s);
                _frame.Location = new Point((picImage.Width - s) / 2, (picImage.Height - s) / 2);
                picImage.Controls.Add(_frame);
                _cropInfo = new AreaInfo()
                {
                    Area = new Rectangle(_frame.Location, _frame.Size),
                    OriginalSize = config.ImageOriginalSize
                };
                picImage.Refresh();
                UpdateRectangle();
            }

            _frame.SizeChanged += (a, b) =>
            {
                if (!_frameSizeLocationChangedEnabled) return;
                _cropInfo = new AreaInfo()
                {
                    Area = new Rectangle(_frame.Location, _frame.Size),
                    OriginalSize = config.ImageOriginalSize
                };
                UpdateRectangle();
            };
            _frame.LocationChanged += (a, b) =>
            {
                if (!_frameSizeLocationChangedEnabled) return;
                _cropInfo = new AreaInfo()
                {
                    Area = new Rectangle(_frame.Location, _frame.Size),
                    OriginalSize = config.ImageOriginalSize
                };
                UpdateRectangle();
            };
        }

        private bool _frameSizeLocationChangedEnabled = true;

        private void Editor_EditValueChanged(object sender, EventArgs e)
        {
            if (!_enabledBoundsEditorEditValueChanged) return;

            _frameSizeLocationChangedEnabled = false;
            _cropInfo = new AreaInfo()
            {
                Area = new Rectangle(Convert.ToInt32(nmrX.EditValue),
                Convert.ToInt32(nmrY.EditValue),
                Convert.ToInt32(nmrW.EditValue),
                Convert.ToInt32(nmrH.EditValue)),
                OriginalSize = picImage.Size
            };
            _frame.Location = _cropInfo.Area.Location;
            _frame.Size = _cropInfo.Area.Size;
            picImage.Refresh();
            _frameSizeLocationChangedEnabled = true;
        }

        private void btnSave_Click(object sender, System.EventArgs e)
        {
            if (_cropInfo == null || _cropInfo.Area.IsEmpty)
            {
                XtraMessageBox.Show($"Please setup design area by dragging and moving mouse in the mockup image", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            _configManager.DynamicUpdate(c =>
            {
                c.WatermarkArea = _cropInfo.Area;
                c.ImageOriginalSize = picImage.Size;
            });
            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            var confirm = XtraMessageBox.Show($"Do you want to save changes?",
                Application.ProductName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            switch (confirm)
            {
                case DialogResult.Cancel:
                    return;
                case DialogResult.Yes:
                    btnSave_Click(null, null);
                    return;
                default:
                    break;
            }
            DialogResult = DialogResult.Cancel;
        }

        private AreaInfo _cropInfo;

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;   // WS_EX_COMPOSITED       
                return cp;
            }
        }

        private void picImage_Paint(object sender, PaintEventArgs e)
        {
            if (_cropInfo != null)
            {

                e.Graphics.ExcludeClip(picImage.Controls[0].Bounds);
                using (var b = new SolidBrush(Color.FromArgb(100, Color.Black)))
                    e.Graphics.FillRectangle(b, picImage.ClientRectangle);

                //_frame.BackColor = Color.White;
                _frame.BackgroundImage = _defaultDesign;
                _frame.BackgroundImageLayout = ImageLayout.Stretch;
            }
        }

        private bool _enabledBoundsEditorEditValueChanged = false;
        private void UpdateRectangle()
        {
            _enabledBoundsEditorEditValueChanged = false;
            nmrX.EditValue = _frame.Location.X;
            nmrY.EditValue = _frame.Location.Y;
            nmrW.EditValue = _frame.Size.Width;
            nmrH.EditValue = _frame.Size.Height;
            _enabledBoundsEditorEditValueChanged = true;
        }
    }
}