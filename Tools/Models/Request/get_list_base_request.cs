using System;
using System.ComponentModel.DataAnnotations;

namespace GTETools.Models.Request
{
    public class get_list_base_request : get_base_request
    {
        private string _search;

        [MaxLength(255)]
        public string search
        {
            get { return _search; }
            set { _search = value; }
        }
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
