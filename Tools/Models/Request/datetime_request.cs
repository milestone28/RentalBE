using System;

namespace Tools.Models.Request
{
    public class datetime_request : get_list_base_request
    {
        private DateTime? _date_from;
        private DateTime? _date_to;

        public DateTime? date_from
        {
            get { return _date_from; }
            set { _date_from = value; }
        }

        public DateTime? date_to
        {
            get { return _date_to; }
            set { _date_to = value; }
        }
    }
}
