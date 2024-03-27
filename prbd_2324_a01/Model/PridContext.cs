﻿using Azure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Logging;
using PRBD_Framework;
using System.Configuration;
using System.Data;

namespace prbd_2324_a01.Model;

public class PridContext : DbContextBase
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        base.OnConfiguring(optionsBuilder);

        /*
         * SQLite
         */

        // var connectionString = ConfigurationManager.ConnectionStrings["SqliteConnectionString"].ConnectionString;
        // optionsBuilder.UseSqlite(connectionString);

        /*
         * SQL Server
         */

        var connectionString = ConfigurationManager.ConnectionStrings["MsSqlConnectionString"].ConnectionString;
        optionsBuilder.UseSqlServer(connectionString);

        ConfigureOptions(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Subscription>().HasKey(k => k.tricount);
        modelBuilder.Entity<Subscription>().HasKey(k => k.user);

        SeedData(modelBuilder);
    }

    private static void ConfigureOptions(DbContextOptionsBuilder optionsBuilder) {
        optionsBuilder.UseLazyLoadingProxies()
            .LogTo(Console.WriteLine, LogLevel.Information) // permet de visualiser les requêtes SQL générées par LINQ
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors() // attention : ralentit les requêtes
            ;
    }

    private void SeedData(ModelBuilder modelBuilder) {
        //Ajout des utilisateurs
        modelBuilder.Entity<User>().HasData(
                new User("boverhaegen@epfc.eu", "3D4AEC0A9B43782133B8120B2FDD8C6104ABB513FE0CDCD0D1D4D791AA42E338:C217604FDAEA7291C7BA5D1D525815E4:100000:SHA256", "Boris", 0),
                new User("bepenelle@epfc.eu", "9E58D87797C6795D294E6762B6C05116D075BC18445AD4078C25674809DB57EF:C91E0B85B7264877C0424D52494D6296:100000:SHA256", "Benoît", 0),
                new User("xapigeolet@epfc.eu", "5B979AB86EC73B0996F439D0BC3947ECCFA0A41310C77533EA36CB409DBB1243:0CF43009110DE4B4AA6D4E749F622755:100000:SHA256", "Xavier", 0),
                new User("mamichel@epfc.eu", "955F147CE3473774E35EE58F4233AA84AE9118C6ECD4699DD788B8D588238034:5514D1DD0A97E9BA7FE4C0B5A4E89351:100000:SHA256", "Marc", 0),
                new User("admin@epfc.eu", "C9949A02A5DFBE50F1DA289DC162E3C97443AB09CE6F6EB1FD0C9D51B5241BBD:5533437973C5BC6459DB687CA5BDE76C:100000:SHA256", "Administrator", 1)
            );

        //Ajout des tricounts
        modelBuilder.Entity<Tricount>().HasData(
            new Tricount {
                Id = 1, Title = "Gers 2023", Description = null, Creator = 1,
                CreatedAt = DateTime.Parse("2023/10/10 18:42:24")
            },
            new Tricount {
                Id = 2, Title = "Resto badminton", Description = null, Creator = 1,
                CreatedAt = DateTime.Parse("2023/10/10 19:25:10")
            },
            new Tricount {
                Id = 4, Title = "Vacances", Description = "A la mer du nord", Creator = 1,
                CreatedAt = DateTime.Parse("2023/10/10 19:31:09")
            },
            new Tricount {
                Id = 5, Title = "Grosse virée", Description = "A Torremolinos", Creator = 2,
                CreatedAt = DateTime.Parse("2023/08/15 10:00:00")
            },
            new Tricount {
                Id = 6, Title = "Torhout Werchter", Description = "Memorabile", Creator = 3,
                CreatedAt = DateTime.Parse("2023/06/02 18:30:12")
            }
            );

        //Ajout des subscriptions
        modelBuilder.Entity<Subscription>().HasData(
            new Subscription(1, 1),
            new Subscription(1, 2),
            new Subscription(1, 4),
            new Subscription(1, 6),
            new Subscription(2, 2),
            new Subscription(2, 4),
            new Subscription(2, 5),
            new Subscription(2, 6),
            new Subscription(3, 4),
            new Subscription(3, 5),
            new Subscription(3, 6),
            new Subscription(4, 4),
            new Subscription(4, 5),
            new Subscription(4, 6)
            );

        //Ajout des repartiotions
        modelBuilder.Entity<Subscription>().HasData(
            new Repartition(1, 1, 1),
            new Repartition(1, 2, 1),
            new Repartition(2, 1, 1),
            new Repartition(2, 2, 1),
            new Repartition(3, 1, 2),
            new Repartition(3, 2, 1),
            new Repartition(3, 3, 1),
            new Repartition(4, 1, 1),
            new Repartition(4, 2, 2),
            new Repartition(4, 3, 3),
            new Repartition(5, 1, 2),
            new Repartition(5, 2, 1),
            new Repartition(5, 3, 1),
            new Repartition(6, 1, 1),
            new Repartition(6, 3, 1),
            new Repartition(7, 2, 1),
            new Repartition(7, 3, 2),
            new Repartition(7, 4, 3),
            new Repartition(8, 3, 2),
            new Repartition(8, 4, 1),
            new Repartition(9, 2, 1),
            new Repartition(9, 4, 5),
            new Repartition(10, 1, 1),
            new Repartition(10, 3, 1),
            new Repartition(11, 2, 2),
            new Repartition(11, 4, 2)
            );

    }

    
    // Création des tables
    public DbSet<User> Users => Set<User>();
    public DbSet<Tricount> Tricounts => Set<Tricount>();
    public DbSet<Subscription> Subscriptions => Set<Subscription>();
    public DbSet<Operation> Operations => Set<Operation>();

    public DbSet<Repartition> Repartitions => Set<Repartition>();
}