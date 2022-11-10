using System.Collections.Generic;

namespace EasyWatermark.Storage
{
    public class DictionaryDataMemorizer : DataMemorizer<Dictionary<string, object>>
    {
        public DictionaryDataMemorizer(string fileName) : base(fileName)
        {

        }

        public DictionaryDataMemorizer(IDataManager<Dictionary<string, object>> dataManager) : base(dataManager)
        {

        }
    }
}
