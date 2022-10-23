﻿using BoardStats.Models;
using BoardStats.Utility;
using Microsoft.AspNetCore.Identity;

namespace BoardStats.Data
{
    public class AppDbInitializer
    {


        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {

                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(RoleHelper.Admin))
                    await roleManager.CreateAsync(new IdentityRole(RoleHelper.Admin));
                if (!await roleManager.RoleExistsAsync(RoleHelper.User))
                    await roleManager.CreateAsync(new IdentityRole(RoleHelper.User));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                string adminUserEmail = "marcin.pukas_admin@gmail.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new ApplicationUser()
                    {

                        UserName = "Admin_AF",
                        Email = adminUserEmail,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAdminUser, "!123Qwe");
                    await userManager.AddToRoleAsync(newAdminUser, RoleHelper.Admin);
                }


                string appUserEmail = "marcin.pukas2@gmail.com";

                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new ApplicationUser()
                    {

                        UserName = "AvadaFeedavra",
                        Email = appUserEmail,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAppUser, "!123Qwe");
                    await userManager.AddToRoleAsync(newAppUser, RoleHelper.User);
                }
            }
        }

        public static void SeedPlayer(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();


                context.Database.EnsureCreated();
                var user = context.Users.FirstOrDefault(n => n.UserName == "AvadaFeedavra");

                //Cinema
                if (!context.Players.Any())
                {
                    context.Players.AddRange(new List<Player>()
                    {
                        new Player()
                        {

                            PlayerName="Game",

                            PlayerTag ="Game_Players",

                            IsActive = true,

                            UserId=user.Id,

                            UserName =user.UserName,
                        },
                        new Player()
                        {

                            PlayerName="Players",

                            PlayerTag ="Game_Players",

                            IsActive = true,

                            UserId=user.Id,

                            UserName =user.UserName,
                        },

                        new Player()
                        {

                            PlayerName="AvadaFeedavra",

                            PlayerTag ="Rookie",

                            IsActive = true,

                            UserId=user.Id,

                            UserName =user.UserName,
                        },

                          new Player()
                        {

                            PlayerName="Testowy",

                            PlayerTag ="Rookie",

                            IsActive = true,

                            UserId=user.Id,

                            UserName =user.UserName,
                        },
                           new Player()
                        {

                            PlayerName="Testowy2",

                            PlayerTag ="Rookie",

                            IsActive = true,

                            UserId=user.Id,

                            UserName =user.UserName,
                        },
                            new Player()
                        {

                            PlayerName="Testowy3",

                            PlayerTag ="Rookie",

                            IsActive = true,

                            UserId=user.Id,

                            UserName =user.UserName,
                        },
                             new Player()
                        {

                            PlayerName="Testowy4",

                            PlayerTag ="Rookie",

                            IsActive = true,

                            UserId=user.Id,

                            UserName =user.UserName,
                        },
                              new Player()
                        {

                            PlayerName="Testowy5",

                            PlayerTag ="Rookie",

                            IsActive = true,

                            UserId=user.Id,

                            UserName =user.UserName,
                        },
                               new Player()
                        {

                            PlayerName="Testowy6",

                            PlayerTag ="Rookie",

                            IsActive = true,

                            UserId=user.Id,

                            UserName =user.UserName,
                        }


                    });
                    context.SaveChanges();
                }

            }

        }
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();


                context.Database.EnsureCreated();
                var user = context.Users.FirstOrDefault(n => n.UserName == "AvadaFeedavra");


                if(!context.Challanges.Any())
                {
                    context.Challanges.AddRange(new List<Challange>()
                    {
                        new Challange()
                        {
                            ChallangeName="Testowy",
                             UserId=user.Id,

                            UserName =user.UserName,
                        }
                    });
                    context.SaveChanges();
                }
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
                            MainGame="7 cudów świata - Pojedynek",
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
                            BestPlayers= 2,
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
                            Statistic="PunktacjaAll",
                            StatCategory="PunktacjaSum",
                            Category ="Player"

                        },

                         new Stat()
                        {
                            Statistic="Żywność",
                            StatCategory="PunktacjaPart",
                            Category ="Game"

                        },

                           new Stat()
                        {
                            Statistic="Ludzie",
                            StatCategory="PunktacjaPart",
                            Category ="Game"

                        },
                          new Stat()
                        {
                            Statistic="Paliwo",
                            StatCategory="PunktacjaPart",
                            Category ="Game"

                        },
                          new Stat()
                        {
                            Statistic="Morale",
                            StatCategory="PunktacjaPart",
                            Category ="Game"

                        },
                              new Stat()
                        {
                            Statistic="Punkty przewagi militarnej",
                            StatCategory="PunktacjaPart",
                            Category ="Player"

                        },
                                new Stat()
                        {
                            Statistic="Niebieskie budowle",
                            StatCategory="PunktacjaPart",
                            Category ="Player"

                        },
                                            new Stat()
                        {
                            Statistic="Żółte budowle",
                            StatCategory="PunktacjaPart",
                            Category ="Player"

                        },

                           new Stat()
                        {
                            Statistic="Fioletowe budowle",
                            StatCategory="PunktacjaPart",
                            Category ="Player"

                        },
                            new Stat()
                        {
                            Statistic="Punkty Cudów",
                            StatCategory="PunktacjaPart",
                            Category ="Player"

                        },

                                  new Stat()
                        {
                            Statistic="Punkty Żetonów Postępu",
                            StatCategory="PunktacjaPart",
                            Category ="Player"

                        },

                          new Stat()
                        {
                            Statistic="Punkty Skarbca",
                            StatCategory="PunktacjaPart",
                            Category ="Player"

                        },

                           new Stat()
                        {
                            Statistic="Cud",
                            StatCategory="ToDo",
                            Category ="Player"

                        },
                                  new Stat()
                        {
                            Statistic="Cud",
                            StatCategory="ToDo",
                            Category ="Player"

                        },
                                  new Stat()
                        {
                            Statistic="Cud",
                            StatCategory="ToDo",
                            Category ="Player"

                        },
                                            new Stat()
                        {
                            Statistic="Cud",
                            StatCategory="ToDo",
                            Category ="Player"

                        },
                                                      


                    });
                    context.SaveChanges();

                }

                if (!context.Matches.Any())
                {
                    context.Matches.AddRange(new List<Match>()
                    {

                        new Match()
                        {
                           IdGame=2,
                           GameName="7 cudów świata - Pojedynek",
                           StartDate=DateTime.Now,
                           Duration=27,
                           UserName="AvadaFeedavra",
                           IdWinCon=2,
                           WhoWIn="AvadaFeedavra",
                           IdWinner = 3,
                           UserId = user.Id,
                           ChallangeId=1,
                           isPlayed=true,

                        },
                          new Match()
                        {
                           IdGame=2,
                           GameName="7 cudów świata - Pojedynek",
                           StartDate=DateTime.Now,
                           Duration=34,
                           UserName="AvadaFeedavra",
                           IdWinCon=1,
                           WhoWIn="Testowy3",
                           IdWinner = 6,
                           UserId = user.Id,
                           ChallangeId=1,
                           isPlayed=true,

                        },
                            new Match()
                        {
                           IdGame=2,
                           GameName="7 cudów świata - Pojedynek",
                           StartDate=DateTime.Now,
                           Duration=31,
                           UserName="AvadaFeedavra",
                           IdWinCon=2,
                           WhoWIn="AvadaFeedavra",
                           IdWinner = 3,
                           UserId = user.Id,
                           ChallangeId=1,
                           isPlayed=true,

                        },
                              new Match()
                        {
                           IdGame=2,
                           GameName="7 cudów świata - Pojedynek",
                           StartDate=DateTime.Now,
                           Duration=24,
                           UserName="AvadaFeedavra",
                           IdWinCon=1,
                           WhoWIn="Testowy4",
                           IdWinner = 7,
                           UserId = user.Id,
                           ChallangeId=1,
                           isPlayed=true,

                        },
                                new Match()
                        {
                           IdGame=2,
                           GameName="7 cudów świata - Pojedynek",
                           StartDate=DateTime.Now,
                           Duration=29,
                           UserName="AvadaFeedavra",
                           IdWinCon=1,
                           WhoWIn="AvadaFeedavra",
                           IdWinner = 3,
                           UserId = user.Id,
                           ChallangeId=1,
                           isPlayed=true,

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
                            GameId=2,
                            StatId=7

                        },

                                    new Game_Stat()
                        {
                            GameId=2,
                            StatId=8

                        },

                                 new Game_Stat()
                        {
                            GameId=2,
                            StatId=9

                        },

                                 new Game_Stat()
                        {
                            GameId=2,
                            StatId=10

                        },

                             new Game_Stat()
                        {
                            GameId=2,
                            StatId=11

                        },

                             new Game_Stat()
                        {
                            GameId=2,
                            StatId=12

                        },

                             new Game_Stat()
                        {
                            GameId=2,
                            StatId=13

                        },

                             new Game_Stat()
                        {
                            GameId=2,
                            StatId=14

                        },

                             new Game_Stat()
                        {
                            GameId=2,
                            StatId=15

                        },

                             new Game_Stat()
                        {
                            GameId=2,
                            StatId=16

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

                if (!context.Match_Stats.Any())
                {
                    context.Match_Stats.AddRange(new List<Match_Stat>()
                    {

                        new Match_Stat()
                        {
                           MatchId=1,
                           PlayerId=3,
                           IdStat=6,
                           Value="10"

                        },
                          new Match_Stat()
                        {
                           MatchId=1,
                           PlayerId=3,
                           IdStat=1,
                           Value="20"

                        },
                              new Match_Stat()
                        {
                           MatchId=1,
                           PlayerId=3,
                           IdStat=7,
                           Value="3"

                        },
                            new Match_Stat()
                        {
                           MatchId=1,
                           PlayerId=3,
                           IdStat=8,
                           Value="1"

                        },
                           new Match_Stat()
                        {
                           MatchId=1,
                           PlayerId=3,
                           IdStat=9,
                           Value="3"

                        },
                           new Match_Stat()
                        {
                           MatchId=1,
                           PlayerId=3,
                           IdStat=10,
                           Value="3"

                        },
                            new Match_Stat()
                        {
                           MatchId=1,
                           PlayerId=3,
                           IdStat=11,
                           Value="0"

                        },
                          new Match_Stat()
                        {
                           MatchId=1,
                           PlayerId=3,
                           IdStat=12,
                           Value="0"

                        },
                           new Match_Stat()
                        {
                           MatchId=1,
                           PlayerId=3,
                           IdStat=13,
                           Value="Circus Maximus"

                        },
                        new Match_Stat()
                        {
                           MatchId=1,
                           PlayerId=3,
                           IdStat=14,
                           Value="N/D"

                        },
                          new Match_Stat()
                        {
                           MatchId=1,
                           PlayerId=3,
                           IdStat=15,
                           Value="N/D"

                        },
                            new Match_Stat()
                        {
                           MatchId=1,
                           PlayerId=3,
                           IdStat=16,
                           Value="N/D"

                        },



                                 new Match_Stat()
                        {
                           MatchId=1,
                           PlayerId=4,
                           IdStat=6,
                           Value="2"

                        },
                          new Match_Stat()
                        {
                           MatchId=1,
                           PlayerId=4,
                           IdStat=1,
                           Value="33"

                        },

                        new Match_Stat()
                        {
                           MatchId=1,
                           PlayerId=4,
                           IdStat=7,
                           Value="20"

                        },
                                  new Match_Stat()
                        {
                           MatchId=1,
                           PlayerId=4,
                           IdStat=8,
                           Value="3"

                        },
                                      new Match_Stat()
                        {
                           MatchId=1,
                           PlayerId=4,
                           IdStat=9,
                           Value="2"

                        },
                                          new Match_Stat()
                        {
                           MatchId=1,
                           PlayerId=4,
                           IdStat=10,
                           Value="6"

                        },
                                              new Match_Stat()
                        {
                           MatchId=1,
                           PlayerId=4,
                           IdStat=11,
                           Value="0"

                        },
                                                  new Match_Stat()
                        {
                           MatchId=1,
                           PlayerId=4,
                           IdStat=12,
                           Value="0"

                        },
                                                      new Match_Stat()
                        {
                           MatchId=1,
                           PlayerId=4,
                           IdStat=13,
                           Value="Via Appia"

                        },
                                                          new Match_Stat()
                        {
                           MatchId=1,
                           PlayerId=4,
                           IdStat=14,
                           Value="Wiszące ogrody Semiramidy"

                        },
                                                              new Match_Stat()
                        {
                           MatchId=1,
                           PlayerId=4,
                           IdStat=15,
                           Value="Świątynia Artemidy w Efezie"

                        },
                                                                  new Match_Stat()
                        {
                           MatchId=1,
                           PlayerId=4,
                           IdStat=16,
                           Value="N/D"

                        },






                     new Match_Stat()
                        {
                           MatchId=2,
                           PlayerId=3,
                           IdStat=6,
                           Value="5"

                        },
                          new Match_Stat()
                        {
                           MatchId=2,
                           PlayerId=3,
                           IdStat=1,
                           Value="17"

                        },
                              new Match_Stat()
                        {
                           MatchId=2,
                           PlayerId=3,
                           IdStat=7,
                           Value="0"

                        },
                            new Match_Stat()
                        {
                           MatchId=2,
                           PlayerId=3,
                           IdStat=8,
                           Value="0"

                        },
                           new Match_Stat()
                        {
                           MatchId=2,
                           PlayerId=3,
                           IdStat=9,
                           Value="5"

                        },
                           new Match_Stat()
                        {
                           MatchId=2,
                           PlayerId=3,
                           IdStat=10,
                           Value="0"

                        },
                            new Match_Stat()
                        {
                           MatchId=2,
                           PlayerId=3,
                           IdStat=11,
                           Value="0"

                        },
                          new Match_Stat()
                        {
                           MatchId=2,
                           PlayerId=3,
                           IdStat=12,
                           Value="1"

                        },
                           new Match_Stat()
                        {
                           MatchId=2,
                           PlayerId=3,
                           IdStat=13,
                           Value="Circus Maximus"

                        },
                        new Match_Stat()
                        {
                           MatchId=2,
                           PlayerId=3,
                           IdStat=14,
                           Value="Kolos Rodyjski"

                        },
                          new Match_Stat()
                        {
                           MatchId=2,
                           PlayerId=3,
                           IdStat=15,
                           Value="N/D"

                        },
                            new Match_Stat()
                        {
                           MatchId=2,
                           PlayerId=3,
                           IdStat=16,
                           Value="N/D"

                        },



                                 new Match_Stat()
                        {
                           MatchId=2,
                           PlayerId=6,
                           IdStat=6,
                           Value="0"

                        },
                          new Match_Stat()
                        {
                           MatchId=2,
                           PlayerId=6,
                           IdStat=1,
                           Value="47"

                        },

                        new Match_Stat()
                        {
                           MatchId=2,
                           PlayerId=6,
                           IdStat=7,
                           Value="18"

                        },
                                  new Match_Stat()
                        {
                           MatchId=2,
                           PlayerId=6,
                           IdStat=8,
                           Value="5"

                        },
                                      new Match_Stat()
                        {
                           MatchId=2,
                           PlayerId=6,
                           IdStat=9,
                           Value="4"

                        },
                                          new Match_Stat()
                        {
                           MatchId=2,
                           PlayerId=6,
                           IdStat=10,
                           Value="15"

                        },
                                              new Match_Stat()
                        {
                           MatchId=2,
                           PlayerId=6,
                           IdStat=11,
                           Value="3"

                        },
                                                  new Match_Stat()
                        {
                           MatchId=2,
                           PlayerId=6,
                           IdStat=12,
                           Value="2"

                        },
                                                      new Match_Stat()
                        {
                           MatchId=2,
                           PlayerId=6,
                           IdStat=13,
                           Value="Piramida Cheopsa"

                        },
                                                          new Match_Stat()
                        {
                           MatchId=2,
                           PlayerId=6,
                           IdStat=14,
                           Value="Wiszące ogrody Semiramidy"

                        },
                                                              new Match_Stat()
                        {
                           MatchId=2,
                           PlayerId=6,
                           IdStat=15,
                           Value="Via Appia"

                        },
                                                                  new Match_Stat()
                        {
                           MatchId=2,
                           PlayerId=6,
                           IdStat=16,
                           Value="N/D"

                        },









                        new Match_Stat()
                        {
                           MatchId=3,
                           PlayerId=3,
                           IdStat=6,
                           Value="10"

                        },
                          new Match_Stat()
                        {
                           MatchId=3,
                           PlayerId=3,
                           IdStat=1,
                           Value="20"

                        },
                              new Match_Stat()
                        {
                           MatchId=3,
                           PlayerId=3,
                           IdStat=7,
                           Value="2"

                        },
                            new Match_Stat()
                        {
                           MatchId=3,
                           PlayerId=3,
                           IdStat=8,
                           Value="0"

                        },
                           new Match_Stat()
                        {
                           MatchId=3,
                           PlayerId=3,
                           IdStat=9,
                           Value="0"

                        },
                           new Match_Stat()
                        {
                           MatchId=3,
                           PlayerId=3,
                           IdStat=10,
                           Value="6"

                        },
                            new Match_Stat()
                        {
                           MatchId=3,
                           PlayerId=3,
                           IdStat=11,
                           Value="0"

                        },
                          new Match_Stat()
                        {
                           MatchId=3,
                           PlayerId=3,
                           IdStat=12,
                           Value="2"

                        },
                           new Match_Stat()
                        {
                           MatchId=3,
                           PlayerId=3,
                           IdStat=13,
                           Value="Posąg Zeusa w Olimpii"

                        },
                        new Match_Stat()
                        {
                           MatchId=3,
                           PlayerId=3,
                           IdStat=14,
                           Value="Kolos Rodyjski"

                        },
                          new Match_Stat()
                        {
                           MatchId=3,
                           PlayerId=3,
                           IdStat=15,
                           Value="N/D"

                        },
                            new Match_Stat()
                        {
                           MatchId=3,
                           PlayerId=3,
                           IdStat=16,
                           Value="N/D"

                        },



                                 new Match_Stat()
                        {
                           MatchId=3,
                           PlayerId=6,
                           IdStat=6,
                           Value="0"

                        },
                          new Match_Stat()
                        {
                           MatchId=3,
                           PlayerId=6,
                           IdStat=1,
                           Value="30"

                        },

                        new Match_Stat()
                        {
                           MatchId=3,
                           PlayerId=6,
                           IdStat=7,
                           Value="12"

                        },
                                  new Match_Stat()
                        {
                           MatchId=3,
                           PlayerId=6,
                           IdStat=8,
                           Value="3"

                        },
                                      new Match_Stat()
                        {
                           MatchId=3,
                           PlayerId=6,
                           IdStat=9,
                           Value="5"

                        },
                                          new Match_Stat()
                        {
                           MatchId=3,
                           PlayerId=6,
                           IdStat=10,
                           Value="3"

                        },
                                              new Match_Stat()
                        {
                           MatchId=3,
                           PlayerId=6,
                           IdStat=11,
                           Value="7"

                        },
                                                  new Match_Stat()
                        {
                           MatchId=3,
                           PlayerId=6,
                           IdStat=12,
                           Value="0"

                        },
                                                      new Match_Stat()
                        {
                           MatchId=3,
                           PlayerId=6,
                           IdStat=13,
                           Value="Via Appia"

                        },
                                                          new Match_Stat()
                        {
                           MatchId=3,
                           PlayerId=6,
                           IdStat=14,
                           Value="Świątynia Artemidy w Efezie"

                        },
                                                              new Match_Stat()
                        {
                           MatchId=3,
                           PlayerId=6,
                           IdStat=15,
                           Value="N/D"

                        },
                                                                  new Match_Stat()
                        {
                           MatchId=3,
                           PlayerId=6,
                           IdStat=16,
                           Value="N/D"

                        },











                        new Match_Stat()
                        {
                           MatchId=4,
                           PlayerId=3,
                           IdStat=6,
                           Value="5"

                        },
                          new Match_Stat()
                        {
                           MatchId=4,
                           PlayerId=3,
                           IdStat=1,
                           Value="12"

                        },
                              new Match_Stat()
                        {
                           MatchId=4,
                           PlayerId=3,
                           IdStat=7,
                           Value="0"

                        },
                            new Match_Stat()
                        {
                           MatchId=4,
                           PlayerId=3,
                           IdStat=8,
                           Value="0"

                        },
                           new Match_Stat()
                        {
                           MatchId=4,
                           PlayerId=3,
                           IdStat=9,
                           Value="0"

                        },
                           new Match_Stat()
                        {
                           MatchId=4,
                           PlayerId=3,
                           IdStat=10,
                           Value="4"

                        },
                            new Match_Stat()
                        {
                           MatchId=4,
                           PlayerId=3,
                           IdStat=11,
                           Value="0"

                        },
                          new Match_Stat()
                        {
                           MatchId=4,
                           PlayerId=3,
                           IdStat=12,
                           Value="3"

                        },
                           new Match_Stat()
                        {
                           MatchId=4,
                           PlayerId=3,
                           IdStat=13,
                           Value="Biblioteka Aleksandryjska"

                        },
                        new Match_Stat()
                        {
                           MatchId=4,
                           PlayerId=3,
                           IdStat=14,
                           Value="N/D"

                        },
                          new Match_Stat()
                        {
                           MatchId=4,
                           PlayerId=3,
                           IdStat=15,
                           Value="N/D"

                        },
                            new Match_Stat()
                        {
                           MatchId=4,
                           PlayerId=3,
                           IdStat=16,
                           Value="N/D"

                        },



                                 new Match_Stat()
                        {
                           MatchId=4,
                           PlayerId=7,
                           IdStat=6,
                           Value="0"

                        },
                          new Match_Stat()
                        {
                           MatchId=4,
                           PlayerId=7,
                           IdStat=1,
                           Value="50"

                        },

                        new Match_Stat()
                        {
                           MatchId=4,
                           PlayerId=7,
                           IdStat=7,
                           Value="23"

                        },
                                  new Match_Stat()
                        {
                           MatchId=4,
                           PlayerId=7,
                           IdStat=8,
                           Value="2"

                        },
                                      new Match_Stat()
                        {
                           MatchId=4,
                           PlayerId=7,
                           IdStat=9,
                           Value="9"

                        },
                                          new Match_Stat()
                        {
                           MatchId=4,
                           PlayerId=7,
                           IdStat=10,
                           Value="9"

                        },
                                              new Match_Stat()
                        {
                           MatchId=4,
                           PlayerId=7,
                           IdStat=11,
                           Value="7"

                        },
                                                  new Match_Stat()
                        {
                           MatchId=4,
                           PlayerId=7,
                           IdStat=12,
                           Value="0"

                        },
                                                      new Match_Stat()
                        {
                           MatchId=4,
                           PlayerId=7,
                           IdStat=13,
                           Value="Piramida Cheopsa"

                        },
                                                          new Match_Stat()
                        {
                           MatchId=4,
                           PlayerId=7,
                           IdStat=14,
                           Value="N/D"

                        },
                                                              new Match_Stat()
                        {
                           MatchId=4,
                           PlayerId=7,
                           IdStat=15,
                           Value="N/D"

                        },
                                                                  new Match_Stat()
                        {
                           MatchId=4,
                           PlayerId=7,
                           IdStat=16,
                           Value="N/D"

                        },













                     new Match_Stat()
                        {
                           MatchId=5,
                           PlayerId=3,
                           IdStat=6,
                           Value="2"

                        },
                          new Match_Stat()
                        {
                           MatchId=5,
                           PlayerId=3,
                           IdStat=1,
                           Value="44"

                        },
                              new Match_Stat()
                        {
                           MatchId=5,
                           PlayerId=3,
                           IdStat=7,
                           Value="15"

                        },
                            new Match_Stat()
                        {
                           MatchId=5,
                           PlayerId=3,
                           IdStat=8,
                           Value="0"

                        },
                           new Match_Stat()
                        {
                           MatchId=5,
                           PlayerId=3,
                           IdStat=9,
                           Value="9"

                        },
                           new Match_Stat()
                        {
                           MatchId=5,
                           PlayerId=3,
                           IdStat=10,
                           Value="7"

                        },
                            new Match_Stat()
                        {
                           MatchId=5,
                           PlayerId=3,
                           IdStat=11,
                           Value="11"

                        },
                          new Match_Stat()
                        {
                           MatchId=5,
                           PlayerId=3,
                           IdStat=12,
                           Value="1"

                        },
                           new Match_Stat()
                        {
                           MatchId=5,
                           PlayerId=3,
                           IdStat=13,
                           Value="Mauzoleum w Halikarnasie"

                        },
                        new Match_Stat()
                        {
                           MatchId=5,
                           PlayerId=3,
                           IdStat=14,
                           Value="Pireus"

                        },
                          new Match_Stat()
                        {
                           MatchId=5,
                           PlayerId=3,
                           IdStat=15,
                           Value="Posąg Zeusa w Olimpii"

                        },
                            new Match_Stat()
                        {
                           MatchId=5,
                           PlayerId=3,
                           IdStat=16,
                           Value="N/D"

                        },



                                 new Match_Stat()
                        {
                           MatchId=5,
                           PlayerId=9,
                           IdStat=6,
                           Value="0"

                        },
                          new Match_Stat()
                        {
                           MatchId=5,
                           PlayerId=9,
                           IdStat=1,
                           Value="12"

                        },

                        new Match_Stat()
                        {
                           MatchId=5,
                           PlayerId=9,
                           IdStat=7,
                           Value="10"

                        },
                                  new Match_Stat()
                        {
                           MatchId=5,
                           PlayerId=9,
                           IdStat=8,
                           Value="0"

                        },
                                      new Match_Stat()
                        {
                           MatchId=5,
                           PlayerId=9,
                           IdStat=9,
                           Value="0"

                        },
                                          new Match_Stat()
                        {
                           MatchId=5,
                           PlayerId=9,
                           IdStat=10,
                           Value="0"

                        },
                                              new Match_Stat()
                        {
                           MatchId=5,
                           PlayerId=9,
                           IdStat=11,
                           Value="0"

                        },
                                                  new Match_Stat()
                        {
                           MatchId=5,
                           PlayerId=9,
                           IdStat=12,
                           Value="1"

                        },
                                                      new Match_Stat()
                        {
                           MatchId=5,
                           PlayerId=9,
                           IdStat=13,
                           Value="Świątynia Artemidy w Efezie"

                        },
                                                          new Match_Stat()
                        {
                           MatchId=5,
                           PlayerId=9,
                           IdStat=14,
                           Value="N/D"

                        },
                                                              new Match_Stat()
                        {
                           MatchId=5,
                           PlayerId=9,
                           IdStat=15,
                           Value="N/D"

                        },
                                                                  new Match_Stat()
                        {
                           MatchId=5,
                           PlayerId=9,
                           IdStat=16,
                           Value="N/D"

                        },




                  });
                    context.SaveChanges();


                }

                if(!context.Match_Players.Any())
                {
                    context.Match_Players.AddRange(new List<Match_Player>()
                    {

                        new Match_Player()
                        {
                           MatchId=1,
                           PlayerId=3,
                           

                        },

                         new Match_Player()
                        {
                           MatchId=1,
                           PlayerId=4,


                        },

                         new Match_Player()
                        {
                           MatchId=2,
                           PlayerId=3,


                        },
                          new Match_Player()
                        {
                           MatchId=2,
                           PlayerId=6,


                        },
                           new Match_Player()
                        {
                           MatchId=3,
                           PlayerId=3,


                        },
                            new Match_Player()
                        {
                           MatchId=3,
                           PlayerId=6,


                        },
                             new Match_Player()
                        {
                           MatchId=4,
                           PlayerId=3,


                        },
                              new Match_Player()
                        {
                           MatchId=4,
                           PlayerId=7,


                        },
                               new Match_Player()
                        {
                           MatchId=5,
                           PlayerId=3,


                        },
                                new Match_Player()
                        {
                           MatchId=5,
                           PlayerId=9,


                        },


                     });

                    context.SaveChanges();
                }

                if (!context.Expansions.Any())
                {
                    context.Expansions.AddRange(new List<Expansion>()
                    {

                        new Expansion()
                        {
                            IdGame=3,
                           

                        },

                       new Expansion()
                        {
                            IdGame=6,
                           

                        },

                          


                    });
                    context.SaveChanges();

                }
            }
        }

       


      
    }
}
