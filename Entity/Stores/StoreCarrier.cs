using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Entity.Stores
{
    public class StoreCarrier
    {
        public int StoreId { get; set; }
        public Store Store { get; set; }
        public int CarrierId { get; set; }
        public Carrier Carrier { get; set; }
    }
}
