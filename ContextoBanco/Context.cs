using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using tech_test_payment_api.Models;

namespace tech_test_payment_api.ContextoBanco
{
    public class Context : DbContext
    {
         public Context(DbContextOptions<Context> options) : base(options)
        {

        }

       protected override void OnModelCreating(ModelBuilder builder)
        {
                
        }
        public DbSet<Pedido> Pedidos {get; set;}
        public DbSet<Item> Itens {get; set;}
        public DbSet<Vendedor> Vendedores {get; set;}
    }
}