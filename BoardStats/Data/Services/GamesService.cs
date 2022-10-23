
using BoardStats.Data.ViewModels;
using BoardStats.Models;
using BoardStats.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Xml;

namespace BoardStats.Data.Services
{
    public class GamesService : IGamesService
    {

        private readonly ApplicationDbContext _db;

        public GamesService(ApplicationDbContext db)
        {
            _db = db;
        }
        public void Add(Boardgames game)
        {
            throw new NotImplementedException();
        }

        public async Task AddNewGameAsync(BoardViewModel data)
        {




            var NewGame = new Boardgames()
            {

                Name = data.Name,
                Description = data.Description,
                BestPlayers = data.BestPlayers,

                MaxPlayers = data.MaxPlayers,
                MinPlayers = data.MinPlayers,
                BggRate = data.BggRate.Substring(0, 4),
                Category = data.Category.ToString(),
                ImageUrl = data.ImageUrl,
                BggId = data.BggId,
                PlayingTime = data.PlayingTime,
                OrderNumber = 0,
                IsInCollection = false,
      
                



            };
            if (data.MainGame == "Jeśli gra jest dodatkiem, wybierz domyślną gre")
            {
                NewGame.Expansion = false;
                NewGame.MainGame = NewGame.Name;
            }

            else
            {
                NewGame.MainGame = data.MainGame;
                NewGame.Expansion = true;
            }

           


            if (data.InstructionUrl == null) NewGame.InstructionUrl = "brak";
            else NewGame.InstructionUrl = data.InstructionUrl;

            if (NewGame.Category == "1") NewGame.Category = "Economic";
            else if (NewGame.Category == "2") NewGame.Category = "Wargame";
            else if (NewGame.Category == "3") NewGame.Category = "Cooperation";
            else if (NewGame.Category == "4") NewGame.Category = "Strategy";
            else if (NewGame.Category == "5") NewGame.Category = "WorkerPlacement";
            else if (NewGame.Category == "6") NewGame.Category = "AreaControl";
            else if (NewGame.Category == "7") NewGame.Category = "DeckBuilder";
            else if (NewGame.Category == "8") NewGame.Category = "Party";

            await _db.BoardGames.AddAsync(NewGame);

            await _db.SaveChangesAsync();

            var testt = new BoardViewModel()
            {
                statsVM = data.statsVM,
            };




            foreach (var item in data.statsVM)
            {
                if (item.isChecked == true)
                {
                    var GameStat = new Game_Stat()
                    {
                        GameId = NewGame.IdGame,
                        StatId = item.StatsId


                    };
                    await _db.Game_Stats.AddAsync(GameStat);
                }


            };
            await _db.SaveChangesAsync();

            foreach (var item in data.winVM)
            {
                if (item.isChecked == true)
                {
                    var GameWin = new Game_Win()
                    {
                        GameId = NewGame.IdGame,
                        WinConId = item.WinId


                    };
                    await _db.Game_Wins.AddAsync(GameWin);

                }


            };
            await _db.SaveChangesAsync();

            if(NewGame.Expansion==true)
            {
                var newExpansion = new Expansion()
                {
                    IdGame = NewGame.IdGame,
                };
                await _db.Expansions.AddAsync(newExpansion);
                await _db.SaveChangesAsync();

            }




        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Boardgames>> GetAll()
        {
            
            
            var result = await _db.BoardGames.OrderBy(m => m.Name).ToListAsync();
          
            return result;
        }

        public async Task<IEnumerable<Boardgames>> GetAllToSelector()
        {
            List<Boardgames> result = new List<Boardgames>();
            Boardgames hold = new Boardgames();
            hold.Name = "Jeśli gra jest dodatkiem, wybierz domyślną gre";
            hold.Expansion = false;
            hold.IdGame = -1;
            result.Add(hold);

            var result2 = await _db.BoardGames.OrderBy(m => m.Name).ToListAsync();
            foreach (var item in result2)
            {
                result.Add(item);
            }
            return result;
        }


        public async Task<IEnumerable<Boardgames>> GetAllNoAdds()
        {
            var result = await _db.BoardGames.Where(m => m.Expansion==false).OrderBy(m => m.Name).ToListAsync();
            
            return result;
        }

        public Boardgames GetById(int id)
        {
            var result = _db.BoardGames
                .Include(wg => wg.Game_Win).ThenInclude(w => w.WinCon)
                .Include(wg => wg.Game_Stat).ThenInclude(w => w.Stat)
                .FirstOrDefault(m => m.IdGame == id);

           


            return result;
        }

        public async Task<NewGameVM> GetNewGameDroopdownsValues()
        {
            var response = new NewGameVM()
            {


                WinCons = await _db.WinCons.OrderBy(n => n.WinCondition).ToListAsync(),
                Stats = await _db.Stats.OrderBy(n => n.Statistic).ToListAsync()
            };





            return response;
        }


        public async Task<List<StatsVM>> GetNewGameDroopdownStats()
        {
            List<StatsVM> lala = await (from statsObj in _db.Stats
                                        select new StatsVM()
                                        {
                                            StatsId = statsObj.IdStat,
                                            Statistic = statsObj.Statistic,
                                            isChecked = false

                                        }).ToListAsync();





            return lala;
        }




        public BoardViewModel Seeding(string id)
        {

            string filename = "https://boardgamegeek.com/xmlapi2/thing?id=";
            string filename2 = "?stats=1";


            XmlDocument doccc = new XmlDocument();
            doccc.Load(filename + id + filename2);
            XmlNodeList holder;
            holder = doccc.GetElementsByTagName("maxplayers");
            var maxplayers = holder[0].Attributes.GetNamedItem("value").InnerText;

            holder = doccc.GetElementsByTagName("minplayers");
            var minplayers = holder[0].Attributes.GetNamedItem("value").InnerText;

            holder = doccc.GetElementsByTagName("playingtime");
            var time = holder[0].Attributes.GetNamedItem("value").InnerText;
            int k = Int32.Parse(maxplayers);

            XmlNodeList elemList = doccc.GetElementsByTagName("result");
            List<HoldBestPlayer> lista = new List<HoldBestPlayer>();
            int j = 1;
            for (int i = 0; i < k * 3; i++)
            {

                if (elemList[i].Attributes.GetNamedItem("value").InnerText == "Best")
                {
                    lista.Add(new HoldBestPlayer() { Category = elemList[i].Attributes.GetNamedItem("value").InnerText, HowMany = Int32.Parse(elemList[i].Attributes.GetNamedItem("numvotes").InnerText), IdH = j });
                    j++;
                }
            }

            var eh = lista.Max(p => p.HowMany);
            var best = lista.Where(p => p.HowMany == eh).ToList();
            string bestPlayers = best[0].IdH.ToString();



            string filename3 = "https://boardgamegeek.com/boardgame/";
            HtmlAgilityPack.HtmlWeb website = new HtmlAgilityPack.HtmlWeb();

            HtmlAgilityPack.HtmlDocument document = website.Load(filename3 + id + "/");

            var jayson = document.DocumentNode.SelectSingleNode("//script[contains(text(), 'aggregateRating')]").InnerHtml;

            JObject json = JObject.Parse(jayson);

            string imgUrl = json.GetValue("image").ToString();

            string name = json.GetValue("name").ToString();
            string descr="brak";
            try
            {
                 descr = json.GetValue("description").ToString();
            }
            catch
            {
                descr = "Brak";
            }
            

            string rate = (string)json["aggregateRating"]["ratingValue"];


            BoardViewModel keeper = new BoardViewModel();
            keeper.Name = name;
            keeper.BggRate = rate;
            keeper.MinPlayers = Int32.Parse(minplayers);
            keeper.MaxPlayers = Int32.Parse(maxplayers);
            keeper.BestPlayers = Int32.Parse(bestPlayers);
            keeper.Description = descr;
            keeper.PlayingTime = Int32.Parse(time);
            keeper.ImageUrl = imgUrl;
            keeper.BggId = id;
            return keeper;






        }

        public Boardgames Update(int id, Boardgames newGame)
        {
            throw new NotImplementedException();
        }

        public async Task<List<WinVM>> GetNewGameDroopdownWins()
        {
            List<WinVM> lala = await (from statsObj in _db.WinCons
                                      select new WinVM()
                                      {
                                          WinId = statsObj.IdWinCon,
                                          Win = statsObj.WinCondition,
                                          isChecked = false

                                      }).ToListAsync();





            return lala;
        }




        public async Task UpdateGameAsync(BoardViewModel data)
        {

            var game = await _db.BoardGames.FirstOrDefaultAsync(n => n.IdGame == data.holdId);


            var isExpansion = game.Expansion;

            game.Name = data.Name;
            game.Description = data.Description;
            game.BestPlayers = data.BestPlayers;

            game.MaxPlayers = data.MaxPlayers;
            game.MinPlayers = data.MinPlayers;
            game.BggRate = data.BggRate.Substring(0, 4);
            game.Category = data.Category.ToString();
            game.ImageUrl = data.ImageUrl;
            game.BggId = data.BggId;
            game.PlayingTime = data.PlayingTime;
            game.OrderNumber = 0;
            game.IsInCollection = false;

            if (data.MainGame == "Jeśli gra jest dodatkiem, wybierz domyślną gre")
            {
                game.Expansion = false;
                game.MainGame = game.Name;
            }

            else
            {
                game.MainGame = data.MainGame;
                game.Expansion = true;
            }




            if (data.InstructionUrl == null) game.InstructionUrl = "brak";
            else game.InstructionUrl = data.InstructionUrl;

            if (data.Category == "1") game.Category = "Economic";
            else if (data.Category == "2") game.Category = "Wargame";
            else if (data.Category == "3") game.Category = "Cooperation";
            else if (data.Category == "4") game.Category = "Strategy";
            else if (data.Category == "5") game.Category = "WorkerPlacement";
            else if (data.Category == "6") game.Category = "AreaControl";
            else if (data.Category == "7") game.Category = "DeckBuilder";
            else if (data.Category == "8") game.Category = "Party";


            await _db.SaveChangesAsync();

            var exStats = _db.Game_Stats.Where(n => n.GameId == game.IdGame).ToList();
            var exWins = _db.Game_Wins.Where(n => n.GameId == game.IdGame).ToList();

            _db.Game_Stats.RemoveRange(exStats);
            await _db.SaveChangesAsync();
            _db.Game_Wins.RemoveRange(exWins);
            await _db.SaveChangesAsync();



            var testt = new BoardViewModel()
            {
                statsVM = data.statsVM,
            };




            foreach (var item in data.statsVM)
            {
                if (item.isChecked == true)
                {
                    var GameStat = new Game_Stat()
                    {
                        GameId = data.holdId,
                        StatId = item.StatsId


                    };
                    await _db.Game_Stats.AddAsync(GameStat);
                }


            };
            await _db.SaveChangesAsync();

            foreach (var item in data.winVM)
            {
                if (item.isChecked == true)
                {
                    var GameWin = new Game_Win()
                    {
                        GameId = data.holdId,
                        WinConId = item.WinId


                    };
                    await _db.Game_Wins.AddAsync(GameWin);

                }


            };
            await _db.SaveChangesAsync();

            if (game.Expansion == true && isExpansion==false)
            {
                var newExpansion = new Expansion()
                {
                    IdGame = game.IdGame,
                };
                await _db.Expansions.AddAsync(newExpansion);
                await _db.SaveChangesAsync();

            }
            else if(game.Expansion == false && isExpansion == true)
            {
                var expansion = await _db.Expansions.Where(x => x.IdGame == game.IdGame).ToListAsync();
                foreach(var item in expansion)
                {
                     _db.Expansions.Remove(item);
                    await _db.SaveChangesAsync();
                }
            }




        }

        public async Task DeleteGameAsync(int Id)
        {

            var result = await _db.BoardGames.FirstOrDefaultAsync(n => n.IdGame == Id);
            _db.BoardGames.Remove(result);
            await _db.SaveChangesAsync();

            if(result.Expansion == true)
            {
                var result2 = await _db.Expansions.FirstOrDefaultAsync(n => n.IdGame == Id);
                _db.Expansions.Remove(result2);
            }
          
            await _db.SaveChangesAsync();

        }
    }
}
