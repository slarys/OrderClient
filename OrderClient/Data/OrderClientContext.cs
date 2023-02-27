using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OrderClient.Models.Clients;
using OrderClient.Models.Orders;

namespace OrderClient.Data
{
    public class OrderClientContext : DbContext
    {
        public OrderClientContext (DbContextOptions<OrderClientContext> options)
            : base(options)
        {
        }

        public DbSet<OrderClient.Models.Clients.Client> Client { get; set; } = default!;

        public DbSet<OrderClient.Models.Orders.Order> Order { get; set; } = default!;
        
    }
}
