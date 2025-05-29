namespace Tools.Models.Response
{
    public class base_response
    {
        public string? result_msg { get; set; }
        public int result_code { get; set; }
    }

    public class with_req_no : base_response
    {
        public string? request_num { get; set; }
    }
}
