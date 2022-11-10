using EasyWatermark.Storage;

namespace EasyWatermark.App.Model
{
    public class AppConfigManager : DataManager<AppConfig>
    {
        public override AppConfig Default => AppConfig.Default;
        private static AppConfigManager _instance;
        public static AppConfigManager Instance => _instance ?? (_instance = new AppConfigManager());

        private AppConfigManager() : base(typeof(AppConfigManager))
        {

        }
    }
}
