

using FluentValidation;
using Rental.Application.Users.Querries;
using Rental.Domain.Entities;

namespace Rental.Application.Users.Queries
{
    public class GetUserQueriesValidation : AbstractValidator<GetUserQueries>
    {
        private int[] _allowPageSize = [5, 10, 20, 30, 50, 100];
        private string[] _allowSortBy = [nameof(User.is_owner), nameof(User.is_user), nameof(User.user_id).ToLower()];
        public GetUserQueriesValidation()
        {
            RuleFor(x => x.page_number)
                .GreaterThan(0)
                .WithMessage("Page Number must be greater than 0.");
            RuleFor(x => x.page_size)
                .Must(x => _allowPageSize.Contains(x))
                .WithMessage($"Page size must be one of the following values: {string.Join(", ", _allowPageSize)}");
            RuleFor(x => x.sort_by)
               .Must(value => _allowSortBy.Any(allowed => allowed.Equals(value, StringComparison.OrdinalIgnoreCase))).When(q => q.sort_by != null)
               .WithMessage($"Sort by must be one of the following values: {string.Join(", ", _allowSortBy)}");
        }
    }
}
