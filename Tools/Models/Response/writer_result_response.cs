namespace GTETools.Models.Response
{
    public class writer_result_response : base_response
    {
        private string _details;
        public string details
        {
            get { return _details; }
            set { _details = value; }
        }
    }
}
