using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Core.Spac
{
    public class CatalogSpecParms
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? Sort { get; set; }
        public string? Search { get; set; }
        public string? BrandId { get; set; }
        public string? TypeId { get; set; }
    }
}
