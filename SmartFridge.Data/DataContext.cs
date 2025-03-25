using Microsoft.EntityFrameworkCore;
using SmartFridge.Core.Model;
using System;
using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;

namespace SmartFridge.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Category>? Categories { get; set; }
        public DbSet<Fridge>? Fridges { get; set; }
        public DbSet<Product>? Products { get; set; }
        public DbSet<Recipe>? Recipes { get; set; }
        public DbSet<Note>? Notes { get; set; }


    }
}
