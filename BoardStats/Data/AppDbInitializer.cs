using BoardStats.Models;

namespace BoardStats.Data
{
    public class AppDbInitializer
    {

        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();


                context.Database.EnsureCreated();


                //Cinema
                if (!context.BoardGames.Any())
                {
                    context.BoardGames.AddRange(new List<Boardgames>()
                    {
                        new Boardgames()
                        {
                            Name = "Terraformacja Marsa",
                            BggId= "167791",
                            BggRate = "8.4",
                            Description = "Compete with rival CEOs to make Mars habitable and build your corporate empire.",
                            ImageUrl = "https://cf.geekdo-images.com/wg9oOLcsKvDesSUdZQ4rxw__itemrep/img/IwUOQfhP5c0KcRJBY4X_hi3LpsY=/fit-in/246x300/filters:strip_icc()/pic3536616.jpg",
                            MinPlayers = 1,
                            MaxPlayers = 5,
                            BestPlayers= 3,
                            PlayingTime= 120,
                            Expansion=false,
                            MainGame="Terraformacja Marsa",
                            Category=GamesCategory.Economic.ToString(),
                            OrderNumber=0,
                            InstructionUrl="https://www.wydawnictworebel.pl/repository/files/instrukcje/Terraformacja%20Marsa/TM_instrukcja%20www.pdf"

                        },

                           new Boardgames()
                        {
                            Name = "7 cudów świata - Pojedynek",
                            BggId= "173346",
                            BggRate = "8.1",
                            Description = "Science? Military? What will you draft to win this head-to-head version of 7 Wonders?",
                            ImageUrl = "https://cf.geekdo-images.com/WzNs1mA_o22ZWTR8fkLP2g__itemrep/img/sDjIG76VOwrlySbj_5rdnAaWO_0=/fit-in/246x300/filters:strip_icc()/pic3376065.jpg",
                            MinPlayers = 2,
                            MaxPlayers = 2,
                            BestPlayers= 2,
                            PlayingTime= 30,
                            Expansion=false,
                            MainGame="7 cudów świata",
                            Category=GamesCategory.Economic.ToString(),
                             OrderNumber=0,
                             InstructionUrl ="https://files.rebel.pl/files/instrukcje/Pojedynek_instrukcja_nowa.pdf"
                        },

                              new Boardgames()
                        {
                            Name = "7 cudów świata - Panteon",
                            BggId= "202976",
                            BggRate = "8.0",
                            Description = "Create a unique pantheon of gods to worship in this expansion for 7 Wonders Duel!",
                            ImageUrl = "https://cf.geekdo-images.com/iQRqtxgRuh-4J5vYLdhsMg__itemrep/img/k7m46Ji8BZVU0Imj_y-q0BpQD70=/fit-in/246x300/filters:strip_icc()/pic3143885.png",
                            MinPlayers = 2,
                            MaxPlayers = 2,
                            BestPlayers= 2,
                            PlayingTime= 30,
                            Expansion=true,
                            MainGame="7 cudów świata",
                            Category=GamesCategory.Economic.ToString(),
                             OrderNumber=0,
                             InstructionUrl="https://repository.rebel.pl/files/instrukcje/7%20cudow%20swiata%20pojedynek%20panteon%20PL.pdf"
                        },

                                 new Boardgames()
                        {
                            Name = "Bitwa 5 armii",
                            BggId= "135219",
                            BggRate = "7.9",
                            Description = "The Climactic Battle of the Hobbit, by the team that brought you War of the Ring!",
                            ImageUrl = "https://cf.geekdo-images.com/SQBx844N1Ay31pm368Ckkg__itemrep/img/cqSynbMthENsKb6eZdyJ8KQ6Z7s=/fit-in/246x300/filters:strip_icc()/pic1886964.jpg",
                            MinPlayers = 2,
                            MaxPlayers = 2,
                            BestPlayers= 3,
                            PlayingTime= 240,
                            Expansion=false,
                            MainGame="Bitwa 5 armii",
                            Category=GamesCategory.Wargame.ToString(),
                             OrderNumber=0,
                             InstructionUrl="http://galakta.pl/download/Bitwa_Pieciu_Armii_instrukcja.pdf"
                        },

                                    new Boardgames()
                        {
                            Name = "Battlestar Galactica",
                            BggId= "37111",
                            BggRate = "7.7",
                            Description = "How can the human race survive when you don't know who is actually human?",
                            ImageUrl = "https://cf.geekdo-images.com/5Q2w2rFJiFI_uV89KP6ECg__itemrep/img/2w_lWsJ_MHRfN7uJBMgcXm1aIbQ=/fit-in/246x300/filters:strip_icc()/pic354500.jpg",
                            MinPlayers = 3,
                            MaxPlayers = 6,
                            BestPlayers= 5,
                            PlayingTime= 180,
                            Expansion=false,
                            MainGame="Battlestar Galactica",
                            Category=GamesCategory.Cooperation.ToString(),
                             OrderNumber=0,
                             InstructionUrl ="http://galakta.pl/download/instrukcja_BSG.pdf"
                        },


                                       new Boardgames()
                        {
                            Name = "Battlestar Galactica - Świt",
                            BggId= "141648",
                            BggRate = "8.3",
                            Description = "Battlestar Galactica: Daybreak Expansion brings humanity's plight to its gripping climax.",
                            ImageUrl = "https://cf.geekdo-images.com/ntb5gU561f6cUJSzyARaKQ__itemrep/img/W_YtNToGWDuuOn6jsAqb6SqIn5E=/fit-in/246x300/filters:strip_icc()/pic1639528.jpg",
                            MinPlayers = 3,
                            MaxPlayers = 7,
                            BestPlayers= 5,
                            PlayingTime= 180,
                            Expansion=true,
                            MainGame="Battlestar Galactica",
                            Category=GamesCategory.Cooperation.ToString(),
                             OrderNumber=0,
                             InstructionUrl="http://aleplanszowki.pl/pliki/BSG%20Swit.pdf"
                        },

                    });
                    context.SaveChanges();
                }

                if (!context.WinCons.Any())
                {
                    context.WinCons.AddRange(new List<WinCon>()
                    {

                        new WinCon()
                        {
                            WinCondition="Punktacja"

                        },

                        new WinCon()
                        {
                            WinCondition="Dominacja militarna"

                        },
                         new WinCon()
                        {
                            WinCondition="Dominacja naukowa"

                        },

                          new WinCon()
                        {
                            WinCondition="Zwycięstwo Cylonów"

                        },

                             new WinCon()
                        {
                            WinCondition="Zwycięstwo ludzi"

                        },

                                  new WinCon()
                        {
                            WinCondition="Bolg wyeliminowany"

                        },
                                       new WinCon()
                        {
                            WinCondition="Beorn"

                        },

                                            new WinCon()
                        {
                            WinCondition="Przeznaczenie"

                        },

                    });
                    context.SaveChanges();

                }

                if (!context.Game_Wins.Any())
                {
                    context.Game_Wins.AddRange(new List<Game_Win>()
                    {

                        new Game_Win()
                        {
                            GameId=1,
                            WinConId=1

                        },

                         new Game_Win()
                        {
                            GameId=2,
                            WinConId=1

                        },

                            new Game_Win()
                        {
                            GameId=2,
                            WinConId=2

                        },

                               new Game_Win()
                        {
                            GameId=2,
                            WinConId=3

                        },

                                             new Game_Win()
                        {
                            GameId=3,
                            WinConId=1

                        },

                            new Game_Win()
                        {
                            GameId=3,
                            WinConId=2

                        },

                               new Game_Win()
                        {
                            GameId=3,
                            WinConId=3

                        },

                        new Game_Win()
                        {
                            GameId=4,
                            WinConId=1

                        },
                         new Game_Win()
                        {
                            GameId=4,
                            WinConId=6

                        },
                          new Game_Win()
                        {
                            GameId=4,
                            WinConId=7

                        },
                           new Game_Win()
                        {
                            GameId=4,
                            WinConId=8

                        },

                            new Game_Win()
                        {
                            GameId=5,
                            WinConId=4

                        },

                                 new Game_Win()
                        {
                            GameId=5,
                            WinConId=5

                        },

                                           new Game_Win()
                        {
                            GameId=6,
                            WinConId=4

                        },

                                 new Game_Win()
                        {
                            GameId=6,
                            WinConId=5

                        },


                    });
                    context.SaveChanges();

                }


                if (!context.Stats.Any())
                {
                    context.Stats.AddRange(new List<Stat>()
                    {

                        new Stat()
                        {
                            Statistic="Punktacja",
                            StatCategory="Punktacja"

                        },

                         new Stat()
                        {
                            Statistic="Żywność",
                            StatCategory="Zasoby"

                        },

                           new Stat()
                        {
                            Statistic="Ludzie",
                            StatCategory="Zasoby"

                        },
                          new Stat()
                        {
                            Statistic="Paliwo",
                            StatCategory="Zasoby"

                        },
                          new Stat()
                        {
                            Statistic="Morale",
                            StatCategory="Zasoby"

                        },
                              new Stat()
                        {
                            Statistic="Siła militarna",
                            StatCategory="Militaria"

                        },



                    });
                    context.SaveChanges();

                }

                if (!context.Game_Stats.Any())
                {
                    context.Game_Stats.AddRange(new List<Game_Stat>()
                    {

                        new Game_Stat()
                        {
                            GameId=1,
                            StatId=1

                        },

                         new Game_Stat()
                        {
                            GameId=2,
                            StatId=1

                        },
                          new Game_Stat()
                        {
                            GameId=2,
                            StatId=6

                        },

                                  new Game_Stat()
                        {
                            GameId=3,
                            StatId=1

                        },
                          new Game_Stat()
                        {
                            GameId=3,
                            StatId=6

                        },
                            new Game_Stat()
                        {
                            GameId=4,
                            StatId=1

                        },

                              new Game_Stat()
                        {
                            GameId=5,
                            StatId=2

                        },

                                      new Game_Stat()
                        {
                            GameId=5,
                            StatId=3

                        },
        new Game_Stat()
                        {
                            GameId=5,
                            StatId=4

                        },

                new Game_Stat()
                        {
                            GameId=5,
                            StatId=5

                        },
                               new Game_Stat()
                        {
                            GameId=6,
                            StatId=2

                        },

                                      new Game_Stat()
                        {
                            GameId=6,
                            StatId=3

                        },
        new Game_Stat()
                        {
                            GameId=6,
                            StatId=4

                        },

                new Game_Stat()
                        {
                            GameId=6,
                            StatId=5

                        },

                  });
                    context.SaveChanges();


                }
            }
        }
    }
}
