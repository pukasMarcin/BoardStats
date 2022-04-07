using AppointmentScheduling.Models.ViewModels;
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
                BggRate = data.BggRate,
                Category = data.Category,
                ImageUrl = data.ImageUrl,
                


            };

            var testt = new BoardViewModel()
            {
                statsVM = data.statsVM,
            };
            // await _db.BoardGames.Add(NewGame);

            //await _db.SaveChangesAsync();

            int test=99;

            foreach(var item in data.statsVM)
            {
                if (item.isChecked == true)
                {
                    var GameStat = new Game_Stat()
                    {
                        GameId = NewGame.IdGame,
                        StatId = item.StatsId


                    };
                    test = item.StatsId;
                }
                
              
            };
            // await _db.SaveChangesAsync();
            test = test;
            var result = await _db.BoardGames.OrderBy(m => m.Name).ToListAsync();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task <IEnumerable<Boardgames>> GetAll()
        {
            var result= await _db.BoardGames.OrderBy(m=>m.Name).ToListAsync(); 
            return result;
        }

        public Boardgames GetById(int id)
        {
            var result = _db.BoardGames.Include(wg => wg.Game_Win).ThenInclude(w => w.WinCon).FirstOrDefault(m => m.IdGame == id);

               
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


        public async Task<List<StatsVM>> GetNewGameDroopdownsValues2()
        {
             List<StatsVM>lala = await (from statsObj in _db.Stats
                                                         select new StatsVM()
                                                         {
                                                             StatsId = statsObj.IdStat,
                                                             Statistic = statsObj.Statistic,
                                                             isChecked=false
                                                             
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

            string descr = json.GetValue("description").ToString();

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
            return keeper;






        }

        public Boardgames Update(int id, Boardgames newGame)
        {
            throw new NotImplementedException();
        }

      
    }
}
