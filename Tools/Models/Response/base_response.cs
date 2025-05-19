namespace GTETools.Models.Response
{
    public class base_response
    {
        private int _result_code;
        private string _result_msg;

        public string result_msg
        {
            get { return _result_msg; }
            set { _result_msg = value; }
        }

        public int result_code
        {
            get { return _result_code; }
            set { _result_code = value; }
        }
    }

    public class with_req_no : base_response
    {
        private string _request_num;

        public string request_num
        {
            get { return _request_num; }
            set { _request_num = value; }
        }
    }
}
