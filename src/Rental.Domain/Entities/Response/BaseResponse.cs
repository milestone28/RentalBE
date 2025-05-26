namespace Rental.Domain.Entities.Response
{
    public class BaseResponse
    {
        public int result_code { get; private set; }
        public string? result_msg { get; private set; }
    }
}
