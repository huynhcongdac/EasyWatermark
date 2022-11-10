using System.Drawing;

namespace EasyWatermark.App.Model
{
    public class AppConfig
    {
        public string ImageFolder { get; set; }
        public string WatermakFileName { get; set; }
        public string OutputFolder { get; set; }
        public Size ImageOriginalSize { get; set; }
        public Rectangle WatermarkArea { get; set; }

        public static AppConfig Default
        {
            get
            {
                return new AppConfig()
                {
                    OutputFolder = "",
                    ImageFolder = "",
                    WatermakFileName = "",
                    ImageOriginalSize = Size.Empty,
                    WatermarkArea = Rectangle.Empty
                };
            }
        }
    }
}
