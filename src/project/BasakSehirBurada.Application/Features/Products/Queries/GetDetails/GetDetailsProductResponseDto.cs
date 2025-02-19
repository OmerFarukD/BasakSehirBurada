using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasakSehirBurada.Application.Features.Products.Queries.GetDetails
{
   public class GetDetailsProductResponseDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }

        public int Stock { get; set; }

        public string CategoryName { get; set; }
    }
}
