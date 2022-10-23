using BoardStats.Data.Services;
using BoardStats.Data.ViewModels;
using BoardStats.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Security.Claims;

namespace BoardStats.Controllers
{
    public class GameStatController : Controller
    {
        private readonly IGameStatService _service;

        public GameStatController(IGameStatService service)
        {
            _service = service;
        }

        public async Task<IActionResult> IndexAsync(string Id)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string id = Id.Split('&')[0];
            int game_id = Int32.Parse(id);
            var player_id = Id.Substring(Id.LastIndexOf('&') + 1);
            int pl_id = Int32.Parse(player_id);
            GameStatsVM model = new GameStatsVM();
        
            if (pl_id == -1)
            {
                model = _service.GetGameStats(game_id, -1, userId);
            }
            else
            {
                 model = _service.GetGameStats(game_id, pl_id, userId);
            }
             var pointsCategory = model.avgStats.Where(m=>m.SubCategory == "PunktacjaPart").Select(m => m.Stat);
            ViewBag.PointsCategories = pointsCategory;
            var pointsValues = model.avgStats.Where(m => m.SubCategory == "PunktacjaPart").Select(m => m.ValueI);
            ViewBag.PointsValues = pointsValues;

            var pointsWinnerCategory = model.avgWinStatsPoints.Where(m => m.SubCategory == "PunktacjaPart").Select(m => m.Stat);
            ViewBag.PointsWinnerCategories = pointsWinnerCategory;
            var pointsWinnerValues = model.avgWinStatsPoints.Where(m => m.SubCategory == "PunktacjaPart").Select(m => m.avgForWin);
            ViewBag.PointsWinnerValues = pointsWinnerValues;

            var winConditions = model.avgWinCon.Select(m => m.Stat);
            ViewBag.WinConditions = winConditions;
            var winConValues = model.avgWinCon.Select(m => m.avgForWin);
            ViewBag.WinValues = winConValues;

            var playersLister = model.playersCounter.Select(m => m.ValueS);
            ViewBag.playersLister = playersLister;
            var playersListerA = model.playersCounter.Select(m => m.ValueI);
            ViewBag.playersListerA = playersListerA;

            if (model.isOnStart4==true)
            {
                var onStart4 = model.avgStatsNoPoints.Where(m=>m.ValueS=="OnStart4").Select(m => m.Stat);
                ViewBag.onStart4 = onStart4;
                var onStart4_values = model.avgStatsNoPoints.Where(m => m.ValueS == "OnStart4").Select(m => m.avgForWin);
                ViewBag.onStart4_values = onStart4_values;

                var onStart4A = model.avgStatsNoPoints.Where(m => m.ValueS == "OnStart4").Select(m => m.Stat);
                ViewBag.onStart4A = onStart4A;
                var onStart4A_values = model.avgStatsNoPoints.Where(m => m.ValueS == "OnStart4").Select(m => m.ValueSAmount);
                ViewBag.onStart4A_values = onStart4A_values;

                var onStart3 = model.avgStatsNoPoints.Where(m => m.ValueS == "OnStart3").Select(m => m.Stat);
                ViewBag.onStart3 = onStart3;
                var onStart3_values = model.avgStatsNoPoints.Where(m => m.ValueS == "OnStart3").Select(m => m.avgForWin);
                ViewBag.onStart3_values = onStart3_values;

                var onStart3A = model.avgStatsNoPoints.Where(m => m.ValueS == "OnStart3").Select(m => m.Stat);
                ViewBag.onStart3A = onStart3A;
                var onStart3A_values = model.avgStatsNoPoints.Where(m => m.ValueS == "OnStart3").Select(m => m.ValueSAmount);
                ViewBag.onStart3A_values = onStart3A_values;

                var onStart2 = model.avgStatsNoPoints.Where(m => m.ValueS == "OnStart2").Select(m => m.Stat);
                ViewBag.onStart2 = onStart2;
                var onStart2_values = model.avgStatsNoPoints.Where(m => m.ValueS == "OnStart2").Select(m => m.avgForWin);
                ViewBag.onStart2_values = onStart2_values;


                var onStart2A = model.avgStatsNoPoints.Where(m => m.ValueS == "OnStart2").Select(m => m.Stat);
                ViewBag.onStart2A = onStart2A;
                var onStart2A_values = model.avgStatsNoPoints.Where(m => m.ValueS == "OnStart2").Select(m => m.ValueSAmount);
                ViewBag.onStart2A_values = onStart2A_values;

                var onStart = model.avgStatsNoPoints.Where(m => m.ValueS == "OnStart").Select(m => m.Stat);
                ViewBag.onStart = onStart;
                var onStart_values = model.avgStatsNoPoints.Where(m => m.ValueS == "OnStart").Select(m => m.avgForWin);
                ViewBag.onStart_values = onStart_values;

                var onStartA = model.avgStatsNoPoints.Where(m => m.ValueS == "OnStart").Select(m => m.Stat);
                ViewBag.onStartA = onStartA;
                var onStartA_values = model.avgStatsNoPoints.Where(m => m.ValueS == "OnStart").Select(m => m.ValueSAmount);
                ViewBag.onStartA_values = onStartA_values;
            }

            if (model.isOnStart3 == true && model.isOnStart4 == false)
            {
                var onStart3 = model.avgStatsNoPoints.Where(m => m.ValueS == "OnStart3").Select(m => m.Stat);
                ViewBag.onStart3 = onStart3;
                var onStart3_values = model.avgStatsNoPoints.Where(m => m.ValueS == "OnStart3").Select(m => m.avgForWin);
                ViewBag.onStart3_values = onStart3_values;

                var onStart3A = model.avgStatsNoPoints.Where(m => m.ValueS == "OnStart3").Select(m => m.Stat);
                ViewBag.onStart3A = onStart3A;
                var onStart3A_values = model.avgStatsNoPoints.Where(m => m.ValueS == "OnStart3").Select(m => m.ValueSAmount);
                ViewBag.onStart3A_values = onStart3A_values;

                var onStart2 = model.avgStatsNoPoints.Where(m => m.ValueS == "OnStart2").Select(m => m.Stat);
                ViewBag.onStart2 = onStart2;
                var onStart2_values = model.avgStatsNoPoints.Where(m => m.ValueS == "OnStart2").Select(m => m.avgForWin);
                ViewBag.onStart2_values = onStart2_values;

                var onStart2A = model.avgStatsNoPoints.Where(m => m.ValueS == "OnStart2").Select(m => m.Stat);
                ViewBag.onStart2A = onStart2A;
                var onStart2A_values = model.avgStatsNoPoints.Where(m => m.ValueS == "OnStart2").Select(m => m.ValueSAmount);
                ViewBag.onStart2A_values = onStart2A_values;

                var onStart = model.avgStatsNoPoints.Where(m => m.ValueS == "OnStart").Select(m => m.Stat);
                ViewBag.onStart = onStart;
                var onStart_values = model.avgStatsNoPoints.Where(m => m.ValueS == "OnStart").Select(m => m.avgForWin);
                ViewBag.onStart_values = onStart_values;

                var onStartA = model.avgStatsNoPoints.Where(m => m.ValueS == "OnStart").Select(m => m.Stat);
                ViewBag.onStartA = onStartA;
                var onStartA_values = model.avgStatsNoPoints.Where(m => m.ValueS == "OnStart").Select(m => m.ValueSAmount);
                ViewBag.onStartA_values = onStartA_values;
            }

            if (model.isOnStart2 == true && model.isOnStart3 == false)
            {
                var onStart2 = model.avgStatsNoPoints.Where(m => m.ValueS == "OnStart2").Select(m => m.Stat);
                ViewBag.onStart2 = onStart2;
                var onStart2_values = model.avgStatsNoPoints.Where(m => m.ValueS == "OnStart2").Select(m => m.avgForWin);
                ViewBag.onStart2_values = onStart2_values;

                var onStart2A = model.avgStatsNoPoints.Where(m => m.ValueS == "OnStart2").Select(m => m.Stat);
                ViewBag.onStart2A = onStart2A;
                var onStart2A_values = model.avgStatsNoPoints.Where(m => m.ValueS == "OnStart2").Select(m => m.ValueSAmount);
                ViewBag.onStart2A_values = onStart2A_values;

                var onStart = model.avgStatsNoPoints.Where(m => m.ValueS == "OnStart").Select(m => m.Stat);
                ViewBag.onStart = onStart;
                var onStart_values = model.avgStatsNoPoints.Where(m => m.ValueS == "OnStart").Select(m => m.avgForWin);
                ViewBag.onStart_values = onStart_values;

                var onStartA = model.avgStatsNoPoints.Where(m => m.ValueS == "OnStart").Select(m => m.Stat);
                ViewBag.onStartA = onStartA;
                var onStartA_values = model.avgStatsNoPoints.Where(m => m.ValueS == "OnStart").Select(m => m.ValueSAmount);
                ViewBag.onStartA_values = onStartA_values;
            }

            if (model.isOnStart2 == false && model.isOnStart == true)
            {
                
                var onStart = model.avgStatsNoPoints.Where(m => m.ValueS == "OnStart").Select(m => m.Stat);
                ViewBag.onStart = onStart;
                var onStart_values = model.avgStatsNoPoints.Where(m => m.ValueS == "OnStart").Select(m => m.avgForWin);
                ViewBag.onStart_values = onStart_values;

                var onStartA = model.avgStatsNoPoints.Where(m => m.ValueS == "OnStart").Select(m => m.Stat);
                ViewBag.onStartA = onStartA;
                var onStartA_values = model.avgStatsNoPoints.Where(m => m.ValueS == "OnStart").Select(m => m.ValueSAmount);
                ViewBag.onStartA_values = onStartA_values;
            }


            if (model.isToDo4 == true)
            {
                var isToDo4 = model.avgStatsNoPoints.Where(m => m.ValueS == "ToDo4").Select(m => m.Stat);
                ViewBag.isToDo4 = isToDo4;
                var isToDo4_values = model.avgStatsNoPoints.Where(m => m.ValueS == "ToDo4").Select(m => m.avgForWin);
                ViewBag.isToDo4_values = isToDo4_values;

                var isToDo4A = model.avgStatsNoPoints.Where(m => m.ValueS == "ToDo4").Select(m => m.Stat);
                ViewBag.isToDo4A = isToDo4A;
                var isToDo4A_values = model.avgStatsNoPoints.Where(m => m.ValueS == "ToDo4").Select(m => m.ValueSAmount);
                ViewBag.isToDo4A_values = isToDo4A_values;

                var isToDo3 = model.avgStatsNoPoints.Where(m => m.ValueS == "ToDo3").Select(m => m.Stat);
                ViewBag.isToDo3 = isToDo3;
                var isToDo3_values = model.avgStatsNoPoints.Where(m => m.ValueS == "ToDo3").Select(m => m.avgForWin);
                ViewBag.isToDo3_values = isToDo3_values;

                var isToDo3A = model.avgStatsNoPoints.Where(m => m.ValueS == "ToDo3").Select(m => m.Stat);
                ViewBag.isToDo3A = isToDo3A;
                var isToDo3A_values = model.avgStatsNoPoints.Where(m => m.ValueS == "ToDo3").Select(m => m.ValueSAmount);
                ViewBag.isToDo3A_values = isToDo3A_values;

                var isToDo2 = model.avgStatsNoPoints.Where(m => m.ValueS == "ToDo2").Select(m => m.Stat);
                ViewBag.isToDo2 = isToDo2;
                var isToDo2_values = model.avgStatsNoPoints.Where(m => m.ValueS == "ToDo2").Select(m => m.avgForWin);
                ViewBag.isToDo2_values = isToDo2_values;

                var isToDo2A = model.avgStatsNoPoints.Where(m => m.ValueS == "ToDo2").Select(m => m.Stat);
                ViewBag.isToDo2A = isToDo2A;
                var isToDo2A_values = model.avgStatsNoPoints.Where(m => m.ValueS == "ToDo2").Select(m => m.ValueSAmount);
                ViewBag.isToDo2A_values = isToDo2A_values;

                var isToDo = model.avgStatsNoPoints.Where(m => m.ValueS == "ToDo").Select(m => m.Stat);
                ViewBag.isToDo = isToDo;
                var isToDo_values = model.avgStatsNoPoints.Where(m => m.ValueS == "ToDo").Select(m => m.avgForWin);
                ViewBag.isToDo_values = isToDo_values;

                var isToDoA = model.avgStatsNoPoints.Where(m => m.ValueS == "ToDo").Select(m => m.Stat);
                ViewBag.isToDoA = isToDoA;
                var isToDoA_values = model.avgStatsNoPoints.Where(m => m.ValueS == "ToDo").Select(m => m.ValueSAmount);
                ViewBag.isToDoA_values = isToDoA_values;
            }


            if (model.isToDo4 == false && model.isToDo3 == true)
            {
               

                var isToDo3 = model.avgStatsNoPoints.Where(m => m.ValueS == "ToDo3").Select(m => m.Stat);
                ViewBag.isToDo3 = isToDo3;
                var isToDo3_values = model.avgStatsNoPoints.Where(m => m.ValueS == "ToDo3").Select(m => m.avgForWin);
                ViewBag.isToDo3_values = isToDo3_values;

                var isToDo3A = model.avgStatsNoPoints.Where(m => m.ValueS == "ToDo3").Select(m => m.Stat);
                ViewBag.isToDo3A = isToDo3A;
                var isToDo3A_values = model.avgStatsNoPoints.Where(m => m.ValueS == "ToDo3").Select(m => m.ValueSAmount);
                ViewBag.isToDo3A_values = isToDo3A_values;

                var isToDo2 = model.avgStatsNoPoints.Where(m => m.ValueS == "ToDo2").Select(m => m.Stat);
                ViewBag.isToDo2 = isToDo2;
                var isToDo2_values = model.avgStatsNoPoints.Where(m => m.ValueS == "ToDo2").Select(m => m.avgForWin);
                ViewBag.isToDo2_values = isToDo2_values;

                var isToDo2A = model.avgStatsNoPoints.Where(m => m.ValueS == "ToDo2").Select(m => m.Stat);
                ViewBag.isToDo2A = isToDo2A;
                var isToDo2A_values = model.avgStatsNoPoints.Where(m => m.ValueS == "ToDo2").Select(m => m.ValueSAmount);
                ViewBag.isToDo2A_values = isToDo2A_values;

                var isToDo = model.avgStatsNoPoints.Where(m => m.ValueS == "ToDo").Select(m => m.Stat);
                ViewBag.isToDo = isToDo;
                var isToDo_values = model.avgStatsNoPoints.Where(m => m.ValueS == "ToDo").Select(m => m.avgForWin);
                ViewBag.isToDo_values = isToDo_values;

                var isToDoA = model.avgStatsNoPoints.Where(m => m.ValueS == "ToDo").Select(m => m.Stat);
                ViewBag.isToDoA = isToDoA;
                var isToDoA_values = model.avgStatsNoPoints.Where(m => m.ValueS == "ToDo").Select(m => m.ValueSAmount);
                ViewBag.isToDoA_values = isToDoA_values;
            }

            if (model.isToDo3 == false && model.isToDo2 == true)
            {


                var isToDo2 = model.avgStatsNoPoints.Where(m => m.ValueS == "ToDo2").Select(m => m.Stat);
                ViewBag.isToDo2 = isToDo2;
                var isToDo2_values = model.avgStatsNoPoints.Where(m => m.ValueS == "ToDo2").Select(m => m.avgForWin);
                ViewBag.isToDo2_values = isToDo2_values;

                var isToDo2A = model.avgStatsNoPoints.Where(m => m.ValueS == "ToDo2").Select(m => m.Stat);
                ViewBag.isToDo2A = isToDo2A;
                var isToDo2A_values = model.avgStatsNoPoints.Where(m => m.ValueS == "ToDo2").Select(m => m.ValueSAmount);
                ViewBag.isToDo2A_values = isToDo2A_values;

                var isToDo = model.avgStatsNoPoints.Where(m => m.ValueS == "ToDo").Select(m => m.Stat);
                ViewBag.isToDo = isToDo;
                var isToDo_values = model.avgStatsNoPoints.Where(m => m.ValueS == "ToDo").Select(m => m.avgForWin);
                ViewBag.isToDo_values = isToDo_values;

                var isToDoA = model.avgStatsNoPoints.Where(m => m.ValueS == "ToDo").Select(m => m.Stat);
                ViewBag.isToDoA = isToDoA;
                var isToDoA_values = model.avgStatsNoPoints.Where(m => m.ValueS == "ToDo").Select(m => m.ValueSAmount);
                ViewBag.isToDoA_values = isToDoA_values;
            }

            if (model.isToDo2 == false && model.isToDo == true)
            {


                var isToDo = model.avgStatsNoPoints.Where(m => m.ValueS == "ToDo").Select(m => m.Stat);
                ViewBag.isToDo = isToDo;
                var isToDo_values = model.avgStatsNoPoints.Where(m => m.ValueS == "ToDo").Select(m => m.avgForWin);
                ViewBag.isToDo_values = isToDo_values;

                var isToDoA = model.avgStatsNoPoints.Where(m => m.ValueS == "ToDo").Select(m => m.Stat);
                ViewBag.isToDoA = isToDoA;
                var isToDoA_values = model.avgStatsNoPoints.Where(m => m.ValueS == "ToDo").Select(m => m.ValueSAmount);
                ViewBag.isToDoA_values = isToDoA_values;
            }

            var playersWinN = model.top5Winners.Select(m => m.Stat);
            ViewBag.playersWinName = playersWinN;
            var playersWinA = model.top5Winners.Select(m => m.ValueI);
            ViewBag.playersWinAmount = playersWinA;

            var expansionsN = model.playedExpansions.Select(m => m.Stat);
            ViewBag.expansionsN = expansionsN;
            var expansionsNValue = model.playedExpansions.Select(m => m.ValueI);
            ViewBag.expansionsNValue = expansionsNValue;

         
            var dropdown_player = await _service.GetPlayersDroopDown(userId);
            List<PlayerVM> drop_player = new List<PlayerVM>();
            PlayerVM description_player = new PlayerVM();
            description_player.PlayerName = "Filtruj po graczu";
            description_player.HoldId = -1;
            drop_player.Add(description_player);
            foreach (var item in dropdown_player)
            {
                drop_player.Add(item);
            }
            ViewBag.Players = new SelectList(drop_player, "HoldId", "PlayerName");
            return View(model);
           
        }
    }
}
