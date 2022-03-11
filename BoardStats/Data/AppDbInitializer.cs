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
                            Category=GamesCategory.Economic.ToString()
                        },

                           new Boardgames()
                        {
                            Name = "7 cudów świata",
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
                            Category=GamesCategory.Economic.ToString()
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
                            Category=GamesCategory.Economic.ToString()
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
                            Category=GamesCategory.Wargame.ToString()
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
                            Category=GamesCategory.Cooperation.ToString()
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
                            Category=GamesCategory.Cooperation.ToString()
                        },

                    });
                    context.SaveChanges();
                }




            }
        }
    }
}
