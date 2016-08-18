
namespace SeleniumNUnitSimple
{
    class SearchText
    {
        public enum SearchType { Full, Wildcard };

        public int searchId;
        public string fullSearch;
        public string wildCardSearch;

        public SearchText(int id, string fs, string ws)
        {
            searchId = id;
            fullSearch = fs;
            wildCardSearch = ws;
        }

        public string getTextFor(SearchType type)
        {
            string txt = wildCardSearch;
            if (type == SearchType.Full)
            {
                txt = fullSearch;
            }
            return txt;
        }
    }
}
