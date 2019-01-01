using mvcClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace mvcClient.Controllers
{
    public class CarrelliController : Controller
    {
        // GET: Carrelli
        public ActionResult Index()
        {
            IEnumerable<CarrelloModel> ListaCarrelli;
            HttpResponseMessage risposta = GlobalVariables.WebApiClient.GetAsync("Carrelli").Result;
            ListaCarrelli = risposta.Content.ReadAsAsync<IEnumerable<CarrelloModel>>().Result;
            return View(ListaCarrelli);
        }


        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new CarrelloModel());
            else
            {
                HttpResponseMessage risposta = GlobalVariables.WebApiClient.GetAsync("Carrelli/" + id.ToString()).Result;
                return View(risposta.Content.ReadAsAsync<CarrelloModel>().Result);
            }
        }
        [HttpPost]
        public ActionResult AddOrEdit(CarrelloModel carrello)
        {
            if (carrello.CarrelloID == 0)
            {
                HttpResponseMessage risposta = GlobalVariables.WebApiClient.PostAsJsonAsync("Carrelli", carrello).Result;
                //TempData["Risposta"] = risposta;
                TempData["SuccessMessage"] = "Salvato con Successo";
            }
            else
            {
                HttpResponseMessage risposta = GlobalVariables.WebApiClient.PutAsJsonAsync("Carrelli/" + carrello.CarrelloID, carrello).Result;
                TempData["SuccessMessage"] = "Modificato con Successo";
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            HttpResponseMessage risposta = GlobalVariables.WebApiClient.DeleteAsync("Carrelli/" + id.ToString()).Result;
            return RedirectToAction("Index");
        }

        public ActionResult Show(int id)
        {

            HttpResponseMessage risposta = GlobalVariables.WebApiClient.GetAsync("Carrelli/" + id.ToString()).Result;
            TempData["SuccessMessage"] = risposta;
            ViewBag.componentiCarrello = risposta.Content.ReadAsAsync<CarrelloModel>().Result.Componenti.ToList();


            return View(risposta.Content.ReadAsAsync<CarrelloModel>().Result);
            
        }
    }
}