﻿using Book_Model.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Book_DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
         
        public DbSet<Category> Categories { get; set; }  
        public DbSet<CoverType> CoverTypes { get; set; }  
        public DbSet<Product> Products { get; set; }  
        public DbSet<Company> Companies { get; set; }  
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }  
        public DbSet<OrderHeader> OrderHeaders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
    }
}