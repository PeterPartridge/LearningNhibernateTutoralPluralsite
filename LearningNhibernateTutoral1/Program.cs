using HibernatingRhinos.Profiler.Appender.NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Linq;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LearningNhibernateTutoral1
{
    class Program
    {
        static void Main()
        {
            NHibernateProfiler.Initialize();

            var cfg = new Configuration();
            //last config wins
            cfg.Configure();

            cfg.DataBaseIntegration(x =>
             {
                 //    x.ConnectionString = "Data Source=(localdb);Initial Catalog=LearningNhibernateTutoral1;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False";
                 //    x.Driver<SqlClientDriver>();
                 //    x.Dialect<MsSql2008Dialect>();
                 //    x.LogSqlInConsole = true;
                 x.Timeout = 10;
             });
            // creates database creation script
            cfg.GenerateSchemaCreationScript(Dialect.GetDialect(cfg.Properties));
            SchemaExport export = new SchemaExport(cfg);
            export.Create(true, true);
            //gets an assembaley
            cfg.AddAssembly(Assembly.GetExecutingAssembly());



            cfg.SessionFactory().GenerateStatistics();

            var sessionFactory = cfg.BuildSessionFactory();
            using (var session = sessionFactory.OpenSession())
            {
                using (var tx = session.BeginTransaction())
                {
                    var customer = new Customer
                    {
                        FirstName = "Bob",
                        LastName = "Davies",
                        /* This will write data into the same table*/
                        Locations = new Location
                        {
                            City = "London",
                            Country = "UK",
                            Province = "London",
                            Street = "221B Bakler Street"
                        }
                    };
                    session.SaveOrUpdate(customer);
                    Console.WriteLine("Customer ID is: {0}", customer.Id);
                    tx.Commit();

                }
            }
            using (var session = sessionFactory.OpenSession())
            {
                using (var tx = session.BeginTransaction())
                {

                    // var customers = session.CreateCriteria<Customer>().List<Customer>();
                    var customers = from customer in session.Query<Customer>()
                                    where customer.LastName.Length > 5 && customer.LastName.StartsWith("A")
                                    orderby customer.LastName
                                    select customer;
                    var customerOrders = customers.Fetch(x => x.Orders).ToList();
                    
                    foreach (var customer in customerOrders)
                    {

                        Console.WriteLine(customer);

                        tx.Commit();
                        Console.ReadLine();
                    }


                }

            }
        }
    }
}

