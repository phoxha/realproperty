using System.Collections.Generic;

namespace RealProperty.Model.Common
{
    public class SearchResult<T>
    {
        public List<T> Data { get; set; } = new List<T>();
        public int TotalCount { get; set; }
    }
}
