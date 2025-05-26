using System.ComponentModel.DataAnnotations;

namespace Tools.Models.Response
{
    public class list_response : base_response
    {
        private int _page_no;
        private int _total_page_count;
        private int _no_of_records;
        private int _page_size;

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter valid integer value.")]
        public int page_no
        {
            get { return _page_no; }
            set { _page_no = value; }
        }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter valid integer value.")]
        public int total_page_count
        {
            get { return _total_page_count; }
            set { _total_page_count = value; }
        }

        [Required]
        [Range(1, 1000, ErrorMessage = "Please enter valid integer value.")]
        public int no_of_records
        {
            get { return _no_of_records; }
            set { _no_of_records = value; }
        }

        [Required]
        [Range(1, 1000, ErrorMessage = "Please enter valid integer value.")]
        public int page_size
        {
            get { return _page_size; }
            set { _page_size = value; }
        }
    }
}
