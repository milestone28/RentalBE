

namespace Rental.Application.Common
{
    public class PageResult<T>
    {
        public PageResult(IEnumerable<T> _items, int _totalCount, int _pageSize , int _pageNumber)
        {
            Data = _items;
            TotalCount = _totalCount;
            TotalPage = (int)Math.Ceiling((double)_totalCount / _pageSize); //math.ceil is used to round up the total page count (2.8) => 3
            PageFrom = _pageSize * (_pageNumber - 1) + 1; // The first item on the page
            PageTo = _pageSize * _pageNumber; // The last item on the page
        }

        public IEnumerable<T> Data { get; private set; }
        public int PageFrom { get; private set; }
        public int PageTo { get; private set; }
     
        public int TotalPage { get; private set; }
        public int TotalCount { get; private set; }
    }

}
