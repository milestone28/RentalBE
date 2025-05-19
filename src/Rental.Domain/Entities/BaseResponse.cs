

namespace Rental.Domain.Entities
{
    public class BaseResponse
    {
        private int _result_code;
        private string _result_msg = "";

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
}
