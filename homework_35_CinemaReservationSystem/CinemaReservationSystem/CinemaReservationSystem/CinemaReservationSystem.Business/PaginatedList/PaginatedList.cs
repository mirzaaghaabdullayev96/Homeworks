using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaReservationSystem.Business.PaginatedList
{
    public class PaginatedList<T> : List<T>
    {
        public PaginatedList(List<T> datas, int count, int page, int pageSize)
        {
            this.AddRange(datas);
            ActivePage = page;
            TotalPage = (int)Math.Ceiling(count / (double)pageSize);
            TotalDataCount = count;
        }

        public int ActivePage { get; set; }
        public int TotalPage { get; set; }
        public int TotalDataCount { get; set; }

        public bool Next { get => ActivePage != TotalPage; }
        public bool Previous { get => ActivePage > 1; }

        public static PaginatedList<T> Create(IQueryable<T> query, int page, int pageSize)
        {
            return new PaginatedList<T>(query.Skip((page - 1) * pageSize).Take(pageSize).ToList(), query.Count(), page, pageSize);
        }

    }

}
