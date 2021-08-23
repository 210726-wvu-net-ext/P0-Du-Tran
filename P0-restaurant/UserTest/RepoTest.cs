using Xunit;
using Microsoft.EntityFrameworkCore;
using Entity = DL.Entities;
using Models;

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
    public void Test1()
    {

    }

    private void Seed()
    {
        using(var context = new Entity.P0_RestaurantRContext(options))
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            context.Reviews.AddRange(
                new Entity.Customer{
                    Id = 1,
                    Comment = "good food"
                    Rating = "5",
                    Reviews = new List<Entity.Review>{
                        new Entity.Review {
                            Id = 1
                            ReviewId = 1,
                            
                        },
                        new Entity.ReviewJoin {
                            Id = 4,
                            Rating = "4"
                        }
                    }
                },

                new Entity.Customer{
                    Id = 2,
                    FirstName = "Joe",
                    LastName = "Black",
                    ReviewJoins = new List<Entity.Review>{
                        new Entity.Review {
                            Id = 2,
                            Rating = "5"
                        },
                        new Entity.Review {
                            Id = 3,
                            Rating = "4.5"
                        }
                    
                    }
                }
           );
    }
}
}      