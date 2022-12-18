using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestApi123.Models;

namespace TestApi123.model
{
    public partial class ResponceCustomer
    {
        public ResponceCustomer(Customers customers)
        {
            id = customers.id;
            Name = customers.Name;
            PhoneNumber = customers.PhoneNumber;
            idRoles = customers.idRoles;
            Salary = customers.Salary;
        }
        public int id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public int idRoles { get; set; }
        public decimal Salary { get; set; }
    }
}