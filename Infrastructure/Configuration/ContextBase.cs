﻿using Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Configuration
{
    public class ContextBase : DbContext
    {
        public ContextBase(DbContextOptions<ContextBase> options) : base(options) { }

        public DbSet<Product> Product { get; set; }


        //*CONFIGURACAO DA STRING DE CONEXAO.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(GetStringConnection(), ServerVersion.Parse("8.0.29"));
                base.OnConfiguring(optionsBuilder);
            }
        }

        private string GetStringConnection()
        {
            return "server=localhost;userid=root;password=123456;database=ecommerceddd";
        }

    }
}