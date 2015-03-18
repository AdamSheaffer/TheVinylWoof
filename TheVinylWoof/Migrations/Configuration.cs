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
                    UserId = "2d427ab5-04d1-41a4-8354-255cedc78bd8",
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
                     UserId = "2d427ab5-04d1-41a4-8354-255cedc78bd8",
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
                     UserId = "85bddf62-9670-4aa9-8d0b-fd94ff3a271f",
                     IsSold = false,
                     Description = "Great album",
                     CoverLocation = "http://ecx.images-amazon.com/images/I/71Ubu92jsAL._SL1500_.jpg"
                 },

               new Album
               {
                   Title = "Locked Down",
                   Artist = "Dr. John",
                   Genre = "Blues",
                   Condition = "Good",
                   Cost = 4,
                   UserId = "85bddf62-9670-4aa9-8d0b-fd94ff3a271f",
                   IsSold = false,
                   CoverLocation = "http://ecx.images-amazon.com/images/I/816Af3gozOL._SL1425_.jpg",
                   Description = "Great album"
               }
            );
        }
    }
}