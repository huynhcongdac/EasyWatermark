using DevExpress.XtraGrid.Views.Grid;
using EasyWatermark.App.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Threading.Tasks;

namespace EasyWatermark
{
    public static class AppHelper
    {
        public static Bitmap GetBitmapFromFile(string fileName)
        {
            Bitmap img;
            using (var bmpTemp = new Bitmap(fileName))
            {
                img = new Bitmap(bmpTemp);
            }
            return img;
        }
        public static void ConfigureGlobalSettings(this GridView gridView)
        {
            gridView.OptionsBehavior.Editable = false;
            gridView.OptionsSelection.EnableAppearanceFocusedCell = false;
            gridView.OptionsSelection.MultiSelect = true;
            gridView.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect;
        }

        public static IEnumerable<T> GetSelectedItems<T>(this GridView gv) where T : class
        {
            var selectedRowHandles = gv.GetSelectedRows();
            for (int i = 0; i < selectedRowHandles.Length; i++)
            {
                var selectedItem = gv.GetRow(selectedRowHandles[i]) as T;
                yield return selectedItem;
            }
        }

        public static readonly object SynchronizeObject = new object();
        public static Rectangle GetRectangle(AreaInfo cropInfo, Size targetSize)
        {
            int x = cropInfo.Area.X;
            int y = cropInfo.Area.Y;
            int width = cropInfo.Area.Width;
            int height = cropInfo.Area.Height;

            var viewSize = new Size(cropInfo.OriginalSize.Width, cropInfo.OriginalSize.Height);
            var tileWith = (double)targetSize.Width / viewSize.Width;
            var tileHeight = (double)targetSize.Height / viewSize.Height;

            x = (int)(x * tileWith);
            y = (int)(y * tileHeight);
            width = (int)(width * tileWith);
            height = (int)(height * tileHeight);

            Rectangle rect = new Rectangle(x, y, width, height);

            return rect;
        }

        public static async Task<Bitmap> AddWatermarkAsync(Image img, Image design, AreaInfo areaInfo)
        {
            await Task.Delay(10);
            Bitmap mockupImg;
            lock (SynchronizeObject)
            {
                mockupImg = new Bitmap(img);
            }

            var tempWaterMark = new Bitmap(design);

            var temp = new Bitmap(mockupImg.Width, mockupImg.Height);

            using (Graphics g = Graphics.FromImage(temp))
            {
                g.DrawImage(mockupImg, 0, 0, mockupImg.Width, mockupImg.Height);

                tempWaterMark.SetResolution(g.DpiX, g.DpiY);

                var rectangle = areaInfo == null ? Rectangle.Empty : GetRectangle(areaInfo, mockupImg.Size);

                var tenPercentWidth = (int)(mockupImg.Width * 0.1);

                int x = (mockupImg.Width - tempWaterMark.Width) - tenPercentWidth;
                int y = (mockupImg.Height - tempWaterMark.Height) / 10 * 9;
                var w = tempWaterMark.Width;
                var h = tempWaterMark.Height;
                if (rectangle != Rectangle.Empty)
                {
                    x = rectangle.X;
                    y = rectangle.Y;
                    w = rectangle.Width;
                    h = rectangle.Height;
                }

                if (rectangle.Width > tempWaterMark.Width && rectangle.Height < tempWaterMark.Height)
                {
                    var scalePercent = (double)rectangle.Height / tempWaterMark.Height;
                    w = Convert.ToInt32(tempWaterMark.Width * scalePercent);
                    var xOffset = (rectangle.Width - w) / 2;
                    x += xOffset;
                    h = rectangle.Height;
                }
                else if (rectangle.Width < tempWaterMark.Width && rectangle.Height > tempWaterMark.Height)
                {
                    var scalePercent = (double)rectangle.Width / tempWaterMark.Width;
                    h = Convert.ToInt32(tempWaterMark.Height * scalePercent);
                    var yOffset = (rectangle.Height - h) / 2;
                    y += yOffset;
                    w = rectangle.Width;
                }
                else if (rectangle.Height > tempWaterMark.Height && rectangle.Width > tempWaterMark.Width)
                {
                    var yOffset = (rectangle.Height - tempWaterMark.Height) / 2;
                    y += yOffset;
                    h = tempWaterMark.Height;

                    var xOffset = (rectangle.Width - tempWaterMark.Width) / 2;
                    x += xOffset;
                    w = tempWaterMark.Width;
                }
                g.DrawImage(tempWaterMark, x, y, w, h);
            }

            tempWaterMark.Dispose();

            return temp;
        }
    }
}
