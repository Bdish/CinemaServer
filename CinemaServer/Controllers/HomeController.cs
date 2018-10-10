
using CinemaDomain.EFRepository.Interfaces;
using CinemaDomain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Hubs;
using Web.Models;

namespace CinemaServer.Controllers
{
    
    public class HomeController : Controller
    {
        private IGenericRepository<Seance> _seanceRepo;
        private IHubContext<Notify> _hubContext;

        public HomeController(IHubContext<Notify> hubContext, IGenericRepository<Seance> seanceRepo)
        {
            _hubContext = hubContext;
            _seanceRepo = seanceRepo;
        }

        public IActionResult Index()
        {
            
                return View();
            
        }
        
        /// <summary>
        /// Метод добавляет новый киносеанс в БД и отовещает всех загруженные клиенты
        /// </summary>
        /// <param name="seanse">Новый киносеанс для добавление в БД</param>
        /// <returns></returns>
        public async Task<string> AddSeanseAsync(string seanse)
        {
            try
            {
               // SeanceView newSeanceView = JsonConvert.DeserializeObject<SeanceView>(seanse);//не работает
                //парсим вручную
                SeanceView newSeanceView = JSonStringToSeance(seanse);
                //валидация нового киносеанс 
                TryValidateModel(newSeanceView);

                if (ModelState.IsValid)
                {
                    //Приведение модели к типу в БД
                    Seance newSeance = new Seance() {Name= newSeanceView.Name, Start=(DateTime)newSeanceView.Start };
                    try
                    {
                        _seanceRepo.Create(newSeance);
                    }
                    catch (Exception ex)
                    {
                        return "Ошибка выполнения операции создания нового элемента в базе данных.";
                    }

                    //Оповещаем всех
                    await NotifyAllClientsAsync(new List<Seance>() { newSeance }, new List<Seance>() );

                    return "";//все хорошо
                }


                return string.Join(";<br>", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));//все плохо, сбор ошибок
            }
            catch(Exception ex)
            {
                return "Проверте, все ли поля заполнены." ;
            }
        }

        /// <summary>
        /// Удаляем из списка киносеанс
        /// </summary>
        /// <param name="id">Идентификатор киносеанса</param>
        /// <returns></returns>
        public async Task<string> DelSeanseAsync(string id)
        {
            try
            {
                string result = "";

                string str = id;
                Seance delSeance;
                try
                {
                     delSeance = _seanceRepo.FindById(int.Parse(id));
                }
                catch (Exception ex)
                {
                    return "Ошибка выполнения операции поиска в  базе данных.";
                }
            

            if (delSeance == null)
                {
                    result = "error";
                }
                else
                {
                    try
                    {
                        _seanceRepo.Remove(delSeance);// стоит ли при ошибке удалять
                    }
                    catch (Exception ex)
                    {
                        return "Ошибка выполнения операции удаления из базы данных.";
                    }

                    //оповещаем всех web клиентов 
                    bool resultNotify;
                    resultNotify=await NotifyAllClientsAsync(new List<Seance>(), new List<Seance>() { delSeance });

                    if (!resultNotify)//оповещение не прошло
                    {
                        result="Error Notify Clients" ;
                    }

                   }
                return result;
            }
            catch (Exception ex)
            {
                return "Ошибка удаления: "+ ex.Message;
            }
        }


        /// <summary>
        /// Получаем весь список киносеансов
        /// </summary>
        /// <returns></returns>
        public string GetAllSeanses()
        {
            try
            {

                return JsonSerializeObjectWithSettings(_seanceRepo.Get());
            }
            catch (Exception ex)
            {
                return "Ошибка получения всех элементов из базы данных.";
            }
        }

        /// <summary>
        /// Парсим Json строку в Seance.
        /// </summary>
        /// <param name="strSeance">Входная json строка.</param>
        /// <returns>Seance не валедированный и может быть пустым</returns>
        [NonAction]
        public SeanceView JSonStringToSeance(string strSeance)
        {
            try
            {
                SeanceView seance = new SeanceView();

                strSeance = strSeance.Replace("{\"", "").Replace("\"}", "");
                string[] filter = { "\",\"", "\":\"" };

                string[] words = strSeance.Split(filter, StringSplitOptions.None);

                //распихиваем весь класс в словарь
                Dictionary<string, string> list = new Dictionary<string, string>();

                for (int i = 0; i < words.Length / 2; i++)
                {

                    list.Add(words[i * 2], words[i * 2 + 1]);
                }

                //инициализируем киносеанс из словаря

                seance.Name = list["Name"];
                seance.Start = DateTime.ParseExact(list["Start"].Replace(" ", ""), "yyyy-MM-ddHH:mm", System.Globalization.CultureInfo.InvariantCulture);

                return seance;
            }
            catch (Exception)
            {
                return null;
            }
        }


        /// <summary>
        /// Оповещение всех веб клиентов о добавлении и удалении киносеансов
        /// </summary>
        /// <param name="addSeances">Список для добавления киносеансов</param>
        /// <param name="delSeances">Список для удаления киносеансов</param>
        /// <returns>true - удачно оповестили, false не удачно. </returns>
        [NonAction]
        public async Task<bool> NotifyAllClientsAsync(List<Seance> addSeances, List<Seance> delSeances)
        {
            try
            {
                //разсылаем только единственную строку
                await _hubContext.Clients.All.SendAsync("Notify", JsonSerializeObjectWithSettings(new { AddSeances = addSeances, DelSeances = delSeances }));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Json Сериализация c настройками.
        /// </summary>
        /// <param name="value">Входные данные.</param>
        /// <returns>Входные данные в Json формате.</returns>
        [NonAction]
        public string JsonSerializeObjectWithSettings(Object value)
        {
            return JsonConvert.SerializeObject(value, new IsoDateTimeConverter() { DateTimeFormat = "yyyy-MM-dd HH:mm" });
        }
    }
}
