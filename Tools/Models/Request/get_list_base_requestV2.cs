

namespace Tools.Models.Request
{
        public class get_list_base_requestV2<T>
        {
            public get_list_base_requestV2(IEnumerable<T> _items, int _totalCount, int _pageSize, int _pageNumber)
            {
            data = _items;
            total_count = _totalCount;
            total_page = (int)Math.Ceiling((double)_totalCount / _pageSize); //math.ceil is used to round up the total page count (2.8) => 3
            page_from = _pageSize * (_pageNumber - 1) + 1; // The first item on the page
            page_to = _pageSize * _pageNumber; // The last item on the page
            }

            public IEnumerable<T> data { get; private set; }
            public int page_from { get; private set; }
            public int page_to { get; private set; }

            public int total_page { get; private set; }
            public int total_count { get; private set; }
        }
}
