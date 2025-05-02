
namespace Rental.Domain.Exceptions
{
    public class BadRequestException(string resourceType) : Exception($"{resourceType}")
    {
    }
}
