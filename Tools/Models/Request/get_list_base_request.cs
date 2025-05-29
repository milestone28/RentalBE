
using System.ComponentModel.DataAnnotations;
using static Tools.Models.sort_direction;


namespace Tools.Models.Request
{
    public class get_list_base_request : get_base_request
    {
        private string? _search;
        private string? _sort_by;
        [MaxLength(255)]
        public string search
        {
            get { return _search; }
            set { _search = value; }
        }
        [MaxLength(255)]
        public string sort_by
        {
            get { return _sort_by; }
            set { _sort_by = value; }
        }
        public SortDirection sort_direction { get; set; }
    }

    public class get_base_request
    {
        private int _page_no;
        private int _page_size;

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter valid integer value.")]
        public int page_no
        {
            get { return _page_no; }
            set { _page_no = value; }
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
