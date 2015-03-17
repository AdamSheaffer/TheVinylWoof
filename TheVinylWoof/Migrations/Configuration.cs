namespace VinylWoof.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using TheVinylWoof.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<TheVinylWoof.Data.VinylWoofContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(TheVinylWoof.Data.VinylWoofContext context)
        {
            context.Albums.AddOrUpdate(i => i.Title,
                new Album
                {
                    Title = "Abbey Road",
                    Artist = "The Beatles",
                    Genre = "Classic Rock",
                    Condition = "Good",
                    Cost = 3,
                    UserId = 1,
                    IsSold = false,
                    Description = "Great album"
                },

                 new Album
                 {
                     Title = "Sticky Fingers",
                     Artist = "The Rolling Stones",
                     Genre = "Classic Rock",
                     Condition = "Good",
                     Cost = 3,
                     UserId = 1,
                     IsSold = false,
                     Description = "Great album"
                 },

                 new Album
                 {
                     Title = "Yeezus",
                     Artist = "Kayne West",
                     Genre = "Hip-Hop",
                     Condition = "Excellent",
                     Cost = 3,
                     UserId = 2,
                     IsSold = false,
                     Description = "Great album"
                 },

               new Album
               {
                   Title = "Locked Down",
                   Artist = "Dr. John",
                   Genre = "Blues",
                   Condition = "Good",
                   Cost = 4,
                   UserId = 2,
                   IsSold = false,
                   Description = "Great album"
               }
            );

            context.Users.AddOrUpdate(i => i.Username,
               new User
               {
                   Username = "Adam",
                   Bio = "Here's Adam's Story",
                   Id = 1,
                   Credits = 5,
                   Rating = 4
               },
               new User
               {
                   Username = "Bob",
                   Bio = "Here's Bob's Story",
                   Id = 2,
                   Credits = 7,
                   Rating = 4
               }
            );
        }
    }
}