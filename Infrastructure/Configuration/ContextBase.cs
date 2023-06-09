﻿using Entities.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Configuration
{
    public class ContextBase : IdentityDbContext<User>//*
    {
        public ContextBase(DbContextOptions<ContextBase> options) : base(options) { }

        public DbSet<Product> Product { get; set; }
       
        public DbSet<UserPurchase> UserPurchase { get; set; }

        public DbSet<User> User { get; set; } //*

       
        protected override void OnModelCreating(ModelBuilder builder) //*
        {
            //*INFORMA AO ASPNET CORE QUEM É A CHAVE PRIMARIA
            //...UTILIZANDO MINHA CALSSE DE USUARIO CUSTOMIZADA.
            builder.Entity<User>().ToTable("AspNetUsers").HasKey(t=>t.Id);

            base.OnModelCreating(builder);
        }

        //*CONFIGURACAO DA STRING DE CONEXAO.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) //*
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
