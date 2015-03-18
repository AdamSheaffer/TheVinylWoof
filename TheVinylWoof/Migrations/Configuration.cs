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
                    UserId = "1",
                    IsSold = false,
                    Description = "Great album",
                    CoverLocation = "http://ecx.images-amazon.com/images/I/91uC0QuxoML._SL1500_.jpg"
                },

                 new Album
                 {
                     Title = "Sticky Fingers",
                     Artist = "The Rolling Stones",
                     Genre = "Classic Rock",
                     Condition = "Good",
                     Cost = 3,
                     UserId = "1",
                     IsSold = false,
                     Description = "Great album",
                     CoverLocation = "http://ecx.images-amazon.com/images/I/51481od8IuL.jpg"
                 },

                 new Album
                 {
                     Title = "Yeezus",
                     Artist = "Kayne West",
                     Genre = "Hip-Hop",
                     Condition = "Excellent",
                     Cost = 3,
                     UserId = "2",
                     IsSold = false,
                     Description = "Great album",
                     CoverLocation = "http://images.amazon.com/images/G/01/richmedia/images/cover.gif"
                 },

               new Album
               {
                   Title = "Locked Down",
                   Artist = "Dr. John",
                   Genre = "Blues",
                   Condition = "Good",
                   Cost = 4,
                   UserId = "2",
                   IsSold = false,
                   CoverLocation = "http://ecx.images-amazon.com/images/I/816Af3gozOL._SL1425_.jpg",
                   Description = "Great album"
               }
            );

            context.ProfileUsers.AddOrUpdate(i => i.Email,
               new ApplicationUser
               {
                   Email = "Adam.e.Sheaffer@gmail.com",
                   Bio = "Here's Adam's Story",
                   Id = "1",
                   Credits = 5,
                   Rating = 4
               },
               new ApplicationUser
               {
                   Email = "Bob@bob.com",
                   Bio = "Here's Bob's Story",
                   Id = "2",
                   Credits = 7,
                   Rating = 4
               }
            );
        }
    }
}