using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
    namespace BookMyShowWebApplicationModal
    {
        public class PaginationDto<T>
        {
            public IList<T> Items { get; }
            public int PageIndex { get; }
            public int TotalPages { get; }
            public bool HasPreviousPage => PageIndex > 1;
            public bool HasNextPage => PageIndex < TotalPages;

            public PaginationDto(IList<T> items, int pageIndex, int totalPages)
            {
                Items = items ?? throw new ArgumentNullException(nameof(items));
                PageIndex = pageIndex;
                TotalPages = totalPages;
            }
        }
    }


