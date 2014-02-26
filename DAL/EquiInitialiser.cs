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
//                    new Horse { ID = 1, BreedID = 1,  Name = "Old Sorrel", FathersName = "Hickory Bill", MothersName = "Dr. Rose TB Mare", 
//                        BirthDate = DateTime.Parse("2005-09-01"), KVH = 170, Price = 60000, Sex = Models.Sex.Stallion,
//                        Description=@"Old Sorrel was foaled in 1915 and was sold that same year to the King Ranch of Texas. 
//                                    He proved himself worth breeding through ranch work on the ranch, before being used 
//                                    as the foundation of the King Ranch Quarter Horse linebreeding program" },
                
//                    new Horse { ID = 2, BreedID = 2, Name="Poco Lena", FathersName = "Poco Bueno", MothersName = "Sheilwin", 
//                        BirthDate = DateTime.Parse("2001-09-01"), KVH = 165, Price = 50000, Sex = Models.Sex.Mare,
//                        Description=@"Poco Lena earned her AQHA Championship, a Performance Register of Merit, a Superior Cutting Horse award and a 
//                                    Superior Halter Horse award. She was also the AQHA High Point Cutting Horse in 1959, 1960, and 1961." },

//                    new Horse { ID = 3, BreedID = 2, Name = "JOKER B", FathersName = "Red Dog", MothersName = "Blue Vitrol", 
//                        BirthDate = DateTime.Parse("2000-09-01"), KVH = 165, Price = 50000, Sex = Models.Sex.Mare,
//                        Description=@"Joker B was the unexpected result of 52 years of planned breeding 
//                                    by Jack and Dan Casement at their ranch in northern Colorado." }

                    new Horse { ID = 1, BreedID = 16, Name = "Andaluská černá klisna CHOVNÁ", 
                        BirthDate = DateTime.Parse("2004-09-01"), KVH = 160, Price = 6500, Sex = Models.Sex.Mare,
                        Description=@"Nabízíme vzácnou španělskou klisnu černé barvy pro chov nebo pro lehčí sporty nebo jako rodinného hobby koně s přátelskou, naprosto klidnou až flegmatickou povahou. Zdravotní stav v pořádku, pravidelné očkování, odčervení, strouhání kopyt. Osedlaná, ježděná spíše méně, nyní po hříběti. Dovoz ze španělské Andalusie 2008. Jako jedna z mála registrovaná v plemenné knize P.R.E. s licencí ke španělskému chovu ! " },

                    new Horse { ID = 2, BreedID = 11, Name = "BLACK MARBLE", 
                        BirthDate = DateTime.Parse("2004-09-01"), KVH = 160, Price = 20000, Sex = Models.Sex.Mare,
                        Description=@"9 ročná kobylka tmavá hnedka s velkým potenciálom do vysokého športu, vyniká velkou silou odrazu, na parkure dobre jazditelná, dobrá povaha v boxe pri čistení na výchádzke. Vhodný pre ambiciozného jazdca i začiatočníka. Možná dohoda." },

                    new Horse { ID = 3, BreedID = 2, Name = "QH Plemenný hřebec", 
                        BirthDate = DateTime.Parse("2008-10-01"), KVH = 160, Price = 6666, Sex = Models.Sex.Stallion,
                        Description=@"Prodám mladého 6 ti letého plemenného QH hřebce s kvalitním původem (bez A1/1), Otec: Fives Rooster. Matka: AP Vanilla Stars. Hřebec je vyššího vzrůstu (KVH cca 160 cm), ušlechtilé, mohutné, atletické a souměrné stavby těla – krásná hlava, pevné nohy s kvalitními kopyty. Má atraktivní, na slunci doslova zlatou barvu -buckskin . Má skvělou povaha, snadno a rychle se učí, příjemně jezditelný přiměřeného temperamentu. Připouštěcí licenci má na klisny Quarter horse a Paint horse. V ČR je uchovněný. Na své potomky předává barvu. Vyšší cena - rychlé jednání - sleva." },

                    new Horse { ID = 4, BreedID = 2, Name = "ICE COLD ORIMA", FathersName = "Jac Orima", MothersName = "Shining Spark",
                        BirthDate = DateTime.Parse("2011-01-20"), KVH = 160, Price = 5780, Sex = Models.Sex.Gelding,
                        Description=@"Vo výuke, prítulná, pohodlná s pekným pohybom. Viac informácií: http://www.icranch.hu/pages-english/cody.htm " },

                    new Horse { ID = 5, BreedID = 12, Name = "kobyla Quarter horse",
                        BirthDate = DateTime.Parse("2010-07-19"), KVH = 157, Price = 3900, Sex = Models.Sex.Gelding,
                        Description=@"redam 4 rocnu 3/2010/ cistokrvnu kobylu plemena Quarter horse .Kobyla ma pedegree ,americke papiere,je prijazdena .Zdravotny stav perfektny,cipovana,odcervovana a ockovana pravidelne.Vhodna aj pre zacinajucich jazdcov,kedze ma pokojnu povahu.Mame ju od malicka a je zvyknuta na ludi a aj v stade koni.Absolvovala vycvik "horsemanship" v kruhovke a tiez vychadzky.Ak hladate pohodoveho konika bez zlozvykov ona je ten spravny kon.Volajte len vazni zaujemci." }

                };

                Horses.ForEach(h => context.Horses.Add(h));
                context.SaveChanges();

                var Images = new List<Image>
                {
                    //new Image { ID = 1, HorseID = 1, FileName = "Old_Sorrel_1.jpg" },
                    //new Image { ID = 2, HorseID = 1, FileName = "Old_Sorrel_2.jpg" },
                    //new Image { ID = 3, HorseID = 2, FileName = "Poco_Lana.jpg" },
                    //new Image { ID = 4, HorseID = 3, FileName = "Joker_B.jpg" }
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