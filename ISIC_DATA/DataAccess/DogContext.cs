﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using ISIC_DATA.Models;

namespace ISIC_DATA.DataAccess
{
    public class DogContext : DbContext
    {
        public DbSet<Dog> Dog { get; set; }
        public DbSet<Color> Color { get; set; }
        public DbSet<Litter> Litter { get; set; }

        public DbSet<Country> Country { get; set; }
        public DbSet<Person> Person { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<NewsArticle> NewsArticle  {get; set;}

        public DbSet<Users> Users { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)  
        {
            modelBuilder.Entity<Litter>()
                        .HasRequired(f => f.Father)
                        .WithMany(d => d.Father)
                        .HasForeignKey(f => f.FatherId)
                        .WillCascadeOnDelete(false);

            modelBuilder.Entity<Litter>()
                        .HasRequired(f => f.Mother)
                        .WithMany(d => d.Mother)
                        .HasForeignKey(f => f.MotherId)
                        .WillCascadeOnDelete(false);

            modelBuilder.Entity<Dog>()
                        .HasOptional(d => d.BornInCountry)
                        .WithMany(c => c.BornInCountry)
                        .HasForeignKey(d => d.BornInCountryId)
                        .WillCascadeOnDelete(false);

            modelBuilder.Entity<Dog>()
                        .HasOptional(d => d.LivesInCountry)
                        .WithMany(c => c.LivesInCountry)
                        .HasForeignKey(d => d.LivesInCountryId)
                        .WillCascadeOnDelete(false);
            
            base.OnModelCreating(modelBuilder);         //Removing pluralisation, needed for the custom Users. 
            //modelBuilder.Conventions.Remove<System.Data.Entity.ModelConfiguration.Conventions.PluralizingTableNameConvention>();

        }


  
    }

}

