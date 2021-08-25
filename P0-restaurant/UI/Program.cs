using UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using DL.Entities;
using BL;
using DL;
using System.IO;
using Models;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();
string connectionString = configuration.GetConnectionString("reviewappdb");

DbContextOptions<P0_RestaurantRContext> options = new DbContextOptionsBuilder<P0_RestaurantRContext>()
    .UseSqlServer(connectionString)
    .Options;

var context = new P0_RestaurantRContext(options);

IMenu menu = new MainMenu(new CustomerBL(new CustomerRepo(context)), new RestaurantBL(new RestaurantRepo(context)), new ReviewBL(new ReviewRepo(context)), new ReviewJoinBL(new ReviewJoinRepo(context)));
menu.Start();



