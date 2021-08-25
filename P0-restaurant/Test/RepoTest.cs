using Xunit;
using Microsoft.EntityFrameworkCore;
using Entity = DL.Entities;
using Models;
using DL;
using System.Collections.Generic;
using System;

namespace UserTest;

public class RepoTest
{
    private readonly DbContextOptions<Entity.P0_RestaurantRContext> options;

    public RepoTest()
    {
        options = new DbContextOptionsBuilder<Entity.P0_RestaurantRContext>().UseSqlite("Filename=Test.db").Options;
        Seed();
    }
    
    [Fact]
    public void AddAReviewShouldAddAReview()
    {
        using (var testcontext = new Entity.P0_RestaurantRContext(options))
        {
            IReviewRepo _repo = new ReviewRepo(testcontext);

            //Act
            _repo.AddReview(
                new Models.Review {
                    Id = 1,
                    Comment = "great",
                    Time = DateTime.Now,
                    Rating = "4"

                }
            );
        }
        using (var assertContext = new Entity.P0_RestaurantRContext(options))
        {
            Entity.Review review = assertContext.Reviews.FirstOrDefault(review => review.Id == 1);

            
            Assert.NotNull(review);
            Assert.Equal("great", review.Comment);
            //Assert.Equal(DateTime.Now, review.Time);
            Assert.Equal("4", review.Rating);
        }
    }

    [Fact]
    public void AddAUserShouldAddAUser()
    {
        using (var testcontext = new Entity.P0_RestaurantRContext(options))
        {
            ICustomerRepo _repo = new CustomerRepo(testcontext);

            //Act
            _repo.AddAUser(
                new Models.Customer {
                    Id = 1,
                    FirstName = "Emma",
                    LastName = "Lee",
                    UserName = "el12",
                    Email = "emmaw@gmail.com"
                }
            );
        }
        using (var assertContext = new Entity.P0_RestaurantRContext(options))
        {
            Entity.Customer customer = assertContext.Customers.FirstOrDefault(customer => customer.Id == 1);

            
            Assert.NotNull(customer);
            Assert.Equal(1, customer.Id);
            Assert.Equal("Emma", customer.FirstName);
            Assert.Equal("Lee", customer.LastName);
            Assert.Equal("emmaw@gmail.com", customer.Email);
        }
    }

    [Fact]
    public void GetAllRestaurantsShouldGetAllRestaurants()
    {
        using (var context = new Entity.P0_RestaurantRContext(options))
        {
            IRestaurantRepo _repo = new RestaurantRepo(context);
            var restaurants = _repo.GetAllRestaurants();

            Assert.Equal(2, restaurants.Count);
        }
    }

    private void Seed()
    {
        using(var context = new Entity.P0_RestaurantRContext(options))
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            context.Restaurants.AddRange(
                new Entity.Restaurant{
                    Id = 1,
                    Name = "JimmyJohn",
                    Location = "Cherry road",
                    Contact = "123-456",
                    Zipcode = "49544"
                    },
                new Entity.Restaurant{
                    Id = 2,
                    Name = "Burger King",
                    Location = "Division road",
                    Contact = "789-456",
                    Zipcode = "49504"
                }
                    
           );
           context.SaveChanges();
        }
    }
}      