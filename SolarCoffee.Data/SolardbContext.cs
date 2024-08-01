using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SolarCoffee.Data.Models;

namespace SolarCoffee.Data
{
    public class SolardbContext : IdentityDbContext
    {
        public SolardbContext() { }
        public SolardbContext(DbContextOptions options) : base(options) { }
        public virtual DbSet<Customer> Customers { get; set; } //create table called Customers c:
        public virtual DbSet<CustomerAdresses> CustomerAdresses{ get; set; }
    }
}
