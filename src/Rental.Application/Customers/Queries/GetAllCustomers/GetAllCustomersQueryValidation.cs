

using FluentValidation;
using Rental.Application.Customers.DTOs;

namespace Rental.Application.Customers.Queries.GetAllCustomers
{
    public class GetAllCustomersQueryValidation : AbstractValidator<GetAllCustomersQuery>
    {
        private int[] _allowPageSize = [5, 10, 20,30,50,100];
        private string[] _allowSortBy = [nameof(CustomersDtos.Name), nameof(CustomersDtos.DateCreated)];
        public GetAllCustomersQueryValidation()
        {
            RuleFor(x => x.PageNumber)
                .GreaterThan(0)
                .WithMessage("Page Number must be greater than 0.");
            RuleFor(x => x.PageSize)
                .Must(x => _allowPageSize.Contains(x))
                .WithMessage($"Page size must be one of the following values: {string.Join(", ", _allowPageSize)}");
            RuleFor(x => x.SortBy)
               .Must(value => _allowSortBy.Any(allowed => allowed.Equals(value, StringComparison.OrdinalIgnoreCase))).When(q => q.SortBy != null)
               .WithMessage($"Sort by must be one of the following values: {string.Join(", ", _allowSortBy)}");
        }
    }
}
