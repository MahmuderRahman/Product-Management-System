using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Model
{
    class Product
    {
        private string query = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

        public int Id { get; set; }
        public string ProductCode { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
    }
}
