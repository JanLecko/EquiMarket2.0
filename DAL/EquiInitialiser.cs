using EquiMarket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EquiMarket.DAL
{
    public class EquiInitialiser : System.Data.Entity.DropCreateDatabaseAlways<EquiContext> 
        //System.Data.Entity.DropCreateDatabaseIfModelChanges<EquiContext>
    {

        protected override void Seed(EquiContext context)
        {
            try
            {

                var Breeds = new List<Breed>
                {
                    new Breed{ ID = 1, TitleEN = "American Paint", TitleSK = "American Paint"},
                    new Breed{ ID = 2, TitleEN = "American Quarter", TitleSK = "American Quarter"},
                    new Breed{ ID = 4, TitleEN = "Appaloosa", TitleSK = "Appaloosa"},
                    new Breed{ ID = 5, TitleEN = "Arabian", TitleSK = "Arabian"},
                    new Breed{ ID = 6, TitleEN = "Clydesdale", TitleSK = "Clydesdale"},
                    new Breed{ ID = 7, TitleEN = "Gelderlander", TitleSK = "Gelderlander"},
                    new Breed{ ID = 8, TitleEN = "Shetland Pony", TitleSK = "Shetland Pony"},
                    new Breed{ ID = 9, TitleEN = "Thoroughbred", TitleSK = "Thoroughbred"}
                };

                Breeds.ForEach(b => context.Breeds.Add(b));
                context.SaveChanges();

                var Horses = new List<Horse>
                {
                    new Horse { ID = 1, BreedID = 1,  Name = "Old Sorrel", FathersName = "Hickory Bill", MothersName = "Dr. Rose TB Mare", 
                        BirthDate = DateTime.Parse("2005-09-01"), KVH = 170, Price = 60000, Sex = Models.Sex.Stallion,
                        Description=@"Old Sorrel was foaled in 1915 and was sold that same year to the King Ranch of Texas. 
                                    He proved himself worth breeding through ranch work on the ranch, before being used 
                                    as the foundation of the King Ranch Quarter Horse linebreeding program" },
                
                    new Horse { ID = 2, BreedID = 2, Name="Poco Lena", FathersName = "Poco Bueno", MothersName = "Sheilwin", 
                        BirthDate = DateTime.Parse("2001-09-01"), KVH = 165, Price = 50000, Sex = Models.Sex.Mare,
                        Description=@"Poco Lena earned her AQHA Championship, a Performance Register of Merit, a Superior Cutting Horse award and a 
                                    Superior Halter Horse award. She was also the AQHA High Point Cutting Horse in 1959, 1960, and 1961." },

                    new Horse { ID = 3, BreedID = 2, Name = "JOKER B", FathersName = "Red Dog", MothersName = "Blue Vitrol", 
                        BirthDate = DateTime.Parse("2000-09-01"), KVH = 165, Price = 50000, Sex = Models.Sex.Mare,
                        Description=@"Joker B was the unexpected result of 52 years of planned breeding 
                                    by Jack and Dan Casement at their ranch in northern Colorado." }                                                                           

                };

                Horses.ForEach(h => context.Horses.Add(h));
                context.SaveChanges();

                //var Images = new List<Image>
                //{
                //    new Image { ID = 1, HorseID = 1, FileName = "Old_Sorrel_1.jpg" },
                //    new Image { ID = 2, HorseID = 1, FileName = "Old_Sorrel_2.jpg" },
                //    new Image { ID = 3, HorseID = 2, FileName = "Poco_Lana.jpg" },
                //    new Image { ID = 4, HorseID = 3, FileName = "Joker_B.jpg" }
                //};

                //Images.ForEach(i => context.Images.Add(i));
                //context.SaveChanges();
            }
            catch (Exception ex)
            {
                //TODO: Log this
            }


        }
    }
}