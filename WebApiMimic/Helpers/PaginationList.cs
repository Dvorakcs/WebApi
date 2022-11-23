using System.Collections.Generic;

namespace WebApiMimic.Helpers
{
    public class PaginationList<T>: List<T>
    {
        public Paginacao Paginacao { get; set; }
    }
}
