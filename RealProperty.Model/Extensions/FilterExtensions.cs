using RealProperty.Data.Entities;
using RealProperty.Model.Common;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace RealProperty.Model.Extensions
{
    public static class FilterExtensions
    {
        public static SearchResult<T> BaseFilter<T>(this IQueryable<T> query, BaseFilter filter)
        {
            var totalCount = query.Count();

            // Ordering
            if (!string.IsNullOrEmpty(filter.OrderBy))
            {
                query = query.OrderBy(filter.OrderBy);
            }

            // Pagination
            if (filter.Skip.HasValue)
                query = query.Skip(filter.Skip.Value);

            if (filter.Take.HasValue)
                query = query.Take(filter.Take.Value);

            return new SearchResult<T>
            {
                Data = query.ToList(),
                TotalCount = totalCount
            };
        }

        public static IQueryable<User> FilterUsers(this IQueryable<User> query, BaseFilter filter)
        {
            if (!string.IsNullOrEmpty(filter.Search))
            {
                query = query.Where(x => x.UserName.Contains(filter.GetTrimSearch()) || x.Email.Contains(filter.GetTrimSearch()));
            }

            return query;
        }
    }
}
