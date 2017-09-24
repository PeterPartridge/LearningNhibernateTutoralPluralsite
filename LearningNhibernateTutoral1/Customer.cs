using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningNhibernateTutoral1
{
    public class Customer
    {
        public Customer()
        {
            Orders = new HashSet<Order>();
        }

        public virtual int Id { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual Location Locations { get; set; }
        public virtual ISet<Order> Orders { get; set; }
    }

    public class Location 
    {
        // this data is held in the same table as Customer.

        public virtual string Street { get; set; }
        public virtual string City { get; set; }
        public virtual string Province { get; set; }
        public virtual string Country { get; set; }
    
    
    }

}
