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
                    Description = "Early first pressing of the Beatles penultimate album and last recording session Abbey Road. It is in good condition and sounds great on the stereo. This is a first pressing as it has the record label variant and the album cover commonly called the sewer cover. Great to have in any record collection. Email me if you have any questions",
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
                     Description = "Great album but it's time for someone else to enjoy this. Vinyl in good condition - has a couple of faint scratches on side 1.",
                     CoverLocation = "http://ecx.images-amazon.com/images/I/51481od8IuL.jpg"
                 },

                 new Album
                 {
                     Title = "Yeezus",
                     Artist = "Kanye West",
                     Genre = "Hip-Hop",
                     Condition = "Excellent",
                     Cost = 3,
                     UserId = "85bddf62-9670-4aa9-8d0b-fd94ff3a271f",
                     IsSold = false,
                     Description = "Great album but it's time for someone else to enjoy this. Vinyl in good condition - has a couple of faint scratches on side 1.",
                     CoverLocation = "http://ecx.images-amazon.com/images/I/71Ubu92jsAL._SL1500_.jpg"
                 },

               new Album
               {
                   Title = "Locked Down",
                   Artist = "Dr. John",
                   Genre = "Blues",
                   Condition = "Excellent",
                   Cost = 4,
                   UserId = "85bddf62-9670-4aa9-8d0b-fd94ff3a271f",
                   IsSold = false,
                   CoverLocation = "http://ecx.images-amazon.com/images/I/816Af3gozOL._SL1425_.jpg",
                   Description = "Brand new vinyl and still sealed. Someone gave this to me and I'm just not a big Dr. John fan."
               }
            );
        }
    }
}