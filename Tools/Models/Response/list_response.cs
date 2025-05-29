using System.ComponentModel.DataAnnotations;

namespace Tools.Models.Response
{
    public class list_response : base_response
    {
        private int _page_to;
        private int _page_size;
        private int _page_no;
        private int _page_from;
        private int _total_page_count;
        private int _no_of_records;

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter valid integer value.")]
        public int page_no { 
            get {return _page_no; } 
            set { _page_no = value; }
        }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter valid integer value.")]
        public int total_page_count 
        { 
            get{return _total_page_count; } 
            set{ _total_page_count = value; }
        }

        [Required]
        [Range(1, 5000, ErrorMessage = "Please enter valid integer value.")]
        public int no_of_records { 
            get { return _no_of_records; }
            set { _no_of_records = value; }
        }


        [Required]
        [Range(1, 1000, ErrorMessage = "Please enter valid integer value.")]
        public int page_size {
            get { return _page_size; }
            set { _page_size = value; } 
        }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter valid integer value.")]
        public int page_from { 
            get{return _page_from; } 
            set{ _page_from = value; }
        }
        
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter valid integer value.")]
        public int page_to {
            get { return _page_to; } 
            set { _page_to = value; }
        }
    }
}


//total_page = (int)Math.Ceiling((double)_totalCount / _pageSize); //math.ceil is used to round up the total page count (2.8) => 3
//page_from = _pageSize * (_pageNumber - 1) + 1; // The first item on the page
//page_to = _pageSize * _pageNumber; // The last item on the page