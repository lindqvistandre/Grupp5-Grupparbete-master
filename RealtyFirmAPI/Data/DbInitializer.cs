using RealtyFirmAPI.Models;
using System;
using System.Linq;

namespace RealtyFirmAPI.Data
{
    public static class DbInitializer
    {
        public static void Initialize(RealtyDbContext _context)
        {
            _context.Database.EnsureCreated();

            if (!_context.Bidders.Any())
            {
                Bidder[] bidders = new Bidder[]
                {
                    new Bidder {Bidder_Id = Guid.NewGuid(), First_Name = "Lars", Last_Name = "Andersson", Email = "lars@andersson.test", Phone_Number = "07777777771", /*Address = "Gatuvägen 1", Postal_Area = "Dennastad", Postal_Code = "999 99", Password = "abc123", Google_Id = "n/a"*/},
                    new Bidder {Bidder_Id = Guid.NewGuid(), First_Name = "Jan", Last_Name = "Nilsson", Email = "jan@nilsson.test", Phone_Number = "07777777772", /*Address = "Gatuvägen 2", Postal_Area = "Dennastad", Postal_Code = "323 23", Password = "abc123", Google_Id = "n/a"*/},
                    new Bidder {Bidder_Id = Guid.NewGuid(), First_Name = "Ove", Last_Name = "Persson", Email = "ove@persson.test", Phone_Number = "07777777773", /*Address = "Gatuvägen 3", Postal_Area = "Annanstad", Postal_Code = "481 88", Password = "abc123", Google_Id = "n/a"*/},
                    new Bidder {Bidder_Id = Guid.NewGuid(), First_Name = "Per", Last_Name = "Larsson", Email = "per@larsson.test", Phone_Number = "07777777774", /*Address = "Gatuvägen 4", Postal_Area = "Annanstad", Postal_Code = "481 88", Password = "abc123", Google_Id = "n/a"*/}
                };
                foreach (Bidder b in bidders)
                {
                    _context.Bidders.Add(b);
                }
                _context.SaveChanges();
            }
            if (!_context.Brokers.Any())
            {
                Broker[] brokers = new Broker[]
                {
                    new Broker {Broker_Id = Guid.NewGuid(), First_Name = "Mikkel", Last_Name = "Jensen", Email = "mikkel@foretag.test", Phone_Number = "07777777717", Address = "Gatuvägen 5", Postal_Area = "Ingenstad", Postal_Code ="480 11", Password = "abc123", Google_Id = "n/a"},
                    new Broker {Broker_Id = Guid.NewGuid(), First_Name = "Ole", Last_Name = "Abendsen", Email = "Ole@foretag.test", Phone_Number = "07777777717", Address = "Gatuvägen 6", Postal_Area = "Storstad", Postal_Code ="491 22", Password = "abc123", Google_Id = "n/a"},
                    new Broker {Broker_Id = Guid.NewGuid(), First_Name = "Anders", Last_Name = "Fransen", Email = "Anders@foretag.test", Phone_Number = "07777777717", Address = "Gatuvägen 7", Postal_Area = "Lillestad", Postal_Code ="994 12", Password = "abc123", Google_Id = "n/a"},
                    new Broker {Broker_Id = Guid.NewGuid(), First_Name = "Nils", Last_Name = "Bigum", Email = "Nils@foretag.test", Phone_Number = "07777777717", Address = "Gatuvägen 8", Postal_Area = "Annanstad", Postal_Code ="325 44", Password = "abc123", Google_Id = "n/a"},
                    new Broker {Broker_Id = Guid.Parse("4f99bad0-0cc2-4bbc-b9c5-610ebb24464f"), Address = "hemma hos mig själv.", Postal_Area = "Hässelby", Postal_Code ="165 55", Phone_Number ="07777777755", First_Name= "Erik", Last_Name = "Sundberg", Email = "erik.sundberg@student.kyh.se", Google_Id="113209907415962471726", Password="FwM6/1aUmeGJXkNYdDOwqYtMY6f0bGI5"}
                };

                foreach (Broker b in brokers)
                {
                    _context.Brokers.Add(b);
                }
                _context.SaveChanges();
            }
            if (!_context.Listings.Any())
            {
                Listing[] listings = new Listing[]
                {
                    new Listing {Listing_Id = Guid.NewGuid(), Listing_Type = "Lägenhet", Address = "Miljonprogramsgatan 2", Postal_Code ="990 77", Postal_Area = "Annanstad", Longitude= 18.0676, Latitude=59.3275, Description = "Enrummare", Room_Count = 1, Listing_Price = 1000000, Year_Built=1900, Floor_Area = 49, Nonusable_Floor_Area = 5, Lot_Area = 0, Form_Of_Lease = "Bostadsrätt", Broker_Id=_context.Brokers.ToArray()[1].Broker_Id},
                    new Listing {Listing_Id = Guid.NewGuid(), Listing_Type = "Hus", Address = "Husvägen 3", Postal_Code="320 55", Postal_Area = "Lillestad", Longitude= 17.8868, Latitude=59.3217, Description = "Gammalt hus", Room_Count = 5, Listing_Price = 2000000, Year_Built=1910, Floor_Area = 125, Nonusable_Floor_Area = 20, Lot_Area = 40, Form_Of_Lease = "Friköpt", Broker_Id=_context.Brokers.ToArray()[0].Broker_Id},
                    new Listing {Listing_Id = Guid.NewGuid(), Listing_Type = "Lägenhet", Address = "Centralgatan 5", Postal_Code="480 66", Postal_Area = "Storstad", Longitude= 18.0391, Latitude=59.3311, Description = "Femrummare", Room_Count = 5, Listing_Price = 5000000, Year_Built=1940, Floor_Area = 110, Nonusable_Floor_Area = 15, Lot_Area = 0, Form_Of_Lease = "Bostadsrätt", Broker_Id=_context.Brokers.ToArray()[2].Broker_Id}

                };
                foreach (Listing l in listings)
                {
                    _context.Listings.Add(l);
                }

                _context.SaveChanges();
            }
            if (!_context.Images.Any())
            {
                Image[] images = new Image[]
                {
                    new Image {Image_Id = Guid.NewGuid(), Image_url = "https://cdn.pixabay.com/photo/2015/07/08/10/29/appartment-building-835817_960_720.jpg", Listing_Id = _context.Listings.ToArray()[0].Listing_Id },
                    new Image {Image_Id = Guid.NewGuid(), Image_url = "https://cdn.pixabay.com/photo/2014/12/27/14/37/living-room-581073_960_720.jpg", Listing_Id = _context.Listings.ToArray()[0].Listing_Id },
                    new Image {Image_Id = Guid.NewGuid(), Image_url = "https://cdn.pixabay.com/photo/2016/11/18/17/46/house-1836070_960_720.jpg", Listing_Id = _context.Listings.ToArray()[1].Listing_Id },
                    new Image {Image_Id = Guid.NewGuid(), Image_url = "https://cdn.pixabay.com/photo/2017/08/27/10/16/interior-2685521_960_720.jpg", Listing_Id = _context.Listings.ToArray()[1].Listing_Id },
                    new Image {Image_Id = Guid.NewGuid(), Image_url = "https://cdn.pixabay.com/photo/2013/09/24/12/08/apartment-185779_960_720.jpg", Listing_Id = _context.Listings.ToArray()[2].Listing_Id },
                    new Image {Image_Id = Guid.NewGuid(), Image_url = "https://cdn.pixabay.com/photo/2018/04/08/18/14/skyscraper-3302027_960_720.jpg", Listing_Id = _context.Listings.ToArray()[2].Listing_Id }
                };

                foreach (Image i in images)
                {
                    _context.Images.Add(i);
                }

                _context.SaveChanges();
            }
            if (!_context.ListingBidders.Any())
            {
                ListingBidder[] listingBidders = new ListingBidder[]
                {
                    new ListingBidder {Listing_Id = _context.Listings.ToArray()[0].Listing_Id, Bidder_Id = _context.Bidders.ToArray()[0].Bidder_Id, Bid = 100 },
                    new ListingBidder {Listing_Id = _context.Listings.ToArray()[0].Listing_Id, Bidder_Id = _context.Bidders.ToArray()[1].Bidder_Id, Bid = 200 },
                    new ListingBidder {Listing_Id = _context.Listings.ToArray()[0].Listing_Id, Bidder_Id = _context.Bidders.ToArray()[2].Bidder_Id, Bid = 300 },
                    new ListingBidder {Listing_Id = _context.Listings.ToArray()[1].Listing_Id, Bidder_Id = _context.Bidders.ToArray()[1].Bidder_Id, Bid = 400 },
                    new ListingBidder {Listing_Id = _context.Listings.ToArray()[2].Listing_Id, Bidder_Id = _context.Bidders.ToArray()[3].Bidder_Id, Bid = 500 }
                };
                foreach (ListingBidder lb in listingBidders)
                {
                    _context.ListingBidders.Add(lb);
                }
                _context.SaveChanges();
            }
        }

    }
}
