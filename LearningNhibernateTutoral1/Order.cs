using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningNhibernateTutoral1
{
    public class Order
    {
        public virtual Guid Id { get; set; }
        public virtual DateTime Ordered { get; set; }
        public virtual DateTime? Shipped { get; set; }
        public virtual Location ShipTo { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
