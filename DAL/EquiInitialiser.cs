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
                    new Breed{ ID = 9, TitleEN = "Thoroughbred", TitleSK = "Thoroughbred"},
                    new Breed{ ID = 10, TitleEN = "Dutch Warmblood (KWPN)", TitleSK = "Dutch Warmblood (KWPN)"},
                    new Breed{ ID = 11, TitleEN = "Czech Warmblood", TitleSK = "Czech Warmblood"},
                    new Breed{ ID = 12, TitleEN = "Slovakian Warmblood", TitleSK = "Slovakian Warmblood"},
                    new Breed{ ID = 13, TitleEN = "Lipizzan", TitleSK = "Lipizzan"},
                    new Breed{ ID = 14, TitleEN = "Oldenburg", TitleSK = "Oldenburg"},
                    new Breed{ ID = 15, TitleEN = "Furioso", TitleSK = "Furioso"},
                    new Breed{ ID = 16, TitleEN = "Andalusian (PRE)", TitleSK = "Andalusian (PRE)"}
                };

                Breeds.ForEach(b => context.Breeds.Add(b));
                context.SaveChanges();

                var Horses = new List<Horse>
                {
                    new Horse { ID = 1, BreedID = 1,  Name = "Old Sorrel", FathersName = "Hickory Bill", MothersName = "Dr. Rose TB Mare", Created = DateTime.Now,
                        BirthDate = DateTime.Parse("2005-09-01"), KVH = 170, Price = 60000, Sex = Models.Sex.Stallion,
                        Description=@"Old Sorrel was foaled in 1915 and was sold that same year to the King Ranch of Texas. 
He proved himself worth breeding through ranch work on the ranch, before being used as the foundation of the King Ranch Quarter Horse linebreeding program" },
                
                    new Horse { ID = 2, BreedID = 2, Name="Poco Lena", FathersName = "Poco Bueno", MothersName = "Sheilwin", Created = DateTime.Now,
                        BirthDate = DateTime.Parse("2001-09-01"), KVH = 165, Price = 50000, Sex = Models.Sex.Mare,
                        Description=@"Poco Lena earned her AQHA Championship, a Performance Register of Merit, 
a Superior Cutting Horse award and a Superior Halter Horse award. She was also the AQHA High Point Cutting Horse in 1959, 1960, and 1961." },

                    new Horse { ID = 3, BreedID = 2, Name = "JOKER B", FathersName = "Red Dog", MothersName = "Blue Vitrol", Created = DateTime.Now,
                        BirthDate = DateTime.Parse("2000-09-01"), KVH = 165, Price = 50000, Sex = Models.Sex.Mare,
                        Description=@"Joker B was the unexpected result of 52 years of planned breeding by Jack and Dan Casement at their ranch in northern Colorado." },

                    new Horse { ID = 4, BreedID = 5, Name = "Marengo", Created = DateTime.Now,
                        BirthDate = DateTime.Parse("1793-09-01"), KVH = 165, Price = 500000, Sex = Models.Sex.Stallion,
                        Description=@"Marengo was wounded eight times in his career. 
As one of 52 horses in Napoleon's personal stud, Marengo fled with these horses when it was raided by Russians in 1812, 
surviving the retreat from Moscow; however, the stallion was captured in 1815 at the Battle of Waterloo by William Henry Francis Petre, 11th Baron Petre" },

                    new Horse { ID = 3, BreedID = 2, Name = "Magnum Psyche", FathersName = "Padrons Psyche", MothersName = "Fancy Miracle", Created = DateTime.Now,
                        BirthDate = DateTime.Parse("1995-09-01"), KVH = 165, Price = 50000, Sex = Models.Sex.Mare,
                        Description=@"At the heart of humankind’s romance with the horse is the image of a glorious Arabian stallion, 
a creature of indescribable beauty that represents our most deeply cherished freedoms. Magnum Psyche epitomizes this vision. 
An undeniable gift from God, a paradox in the most elemental sense, Magnum mirrors the love and the liberty, the strength and the gentleness toward which we travel on this road of life." },

                    new Horse { ID = 3, BreedID = 2, Name = "Baske Afire", Created = DateTime.Now,
                        BirthDate = DateTime.Parse("2001-09-01"), KVH = 165, Price = 70000, Sex = Models.Sex.Stallion,
                        Description=@"Baske Afire’s pedigree is rich in national champions. His sire and dam, 
as well as his grandsire and grandam, were national champions and producers of national champions. 
Most of his champions at the major shows now are divided equally between halter and performance, 
with nearly all of his performance champions in the English and country English pleasure divisions. 
In 2008, he was Top Five on 13 of the 18 AHT charts for U.S. Nationals." }


                };

                Horses.ForEach(h => context.Horses.Add(h));
                context.SaveChanges();

                var Images = new List<Image>
                {
                    new Image { ID = 1, HorseID = 1, FileName = "Old_Sorrel_1.jpg" },
                    new Image { ID = 2, HorseID = 1, FileName = "Old_Sorrel_2.jpg" },
                    new Image { ID = 3, HorseID = 2, FileName = "Poco_Lana.jpg" },
                    new Image { ID = 4, HorseID = 3, FileName = "Joker_B.jpg" },
                    new Image { ID = 5, HorseID = 3, FileName = "Joker_B_2.jpg" },
                    new Image { ID = 6, HorseID = 4, FileName = "magnum-psyche1.jpg" },
                    new Image { ID = 7, HorseID = 4, FileName = "MagnumPsyche2.jpg" },
                    new Image { ID = 8, HorseID = 5, FileName = "BaskeFire1.jpg" },
                    new Image { ID = 9, HorseID = 5, FileName = "BaskeFire2.jpg" },
                    new Image { ID = 10, HorseID = 6, FileName = "Marengo.JPG" },
                    new Image { ID = 11, HorseID = 6, FileName = "marengo1.jpg" },
                    new Image { ID = 12, HorseID = 6, FileName = "marengo2.jpg" }
                };

                Images.ForEach(i => context.Images.Add(i));
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                //TODO: Log this
            }


        }
    }
}