using mvcClient.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace mvcClient.Controllers
{
    public class ComponentiController : Controller
    {
        // GET: Carrelli
        public ActionResult Index()
        {
           

            HttpResponseMessage risposta = GlobalVariables.WebApiClient.GetAsync("Componenti").Result;

            dynamic content = risposta.Content.ReadAsAsync<IEnumerable<ExpandoObject>>().Result;
            // ListaComponenti =  risposta.Content.ReadAsAsync<IEnumerable<ComponenteModel>>().Result ;

            ViewBag.ListaComponenti = content;
            return View(content);
        }


        public ActionResult AddOrEdit(int id = 0, int carrelloId = 0)
        {
            ComponenteModel cm = new ComponenteModel("", "", "", carrelloId);
            if (id == 0)

                return View(cm);
            else
            {
                HttpResponseMessage risposta = GlobalVariables.WebApiClient.GetAsync("Componenti/" + id.ToString()).Result;
                return View(risposta.Content.ReadAsAsync<ComponenteModel>().Result);
            }
        }
        [HttpPost]
        public ActionResult AddOrEdit(ComponenteModel componente)
        {
            if (componente.ComponenteID == 0)
            {
                HttpResponseMessage risposta = GlobalVariables.WebApiClient.PostAsJsonAsync("Componenti", componente).Result;
                //TempData["Risposta"] = risposta;
                TempData["SuccessMessage"] = "Salvato con Successo";
            }
            else
            {
                HttpResponseMessage risposta = GlobalVariables.WebApiClient.PutAsJsonAsync("Componenti/" + componente.ComponenteID, componente).Result;
                TempData["SuccessMessage"] = "Modificato con Successo";
            }



            return RedirectToAction("Show", "Carrelli", new { id = componente.CarrelloID });
        }

        public ActionResult Delete(int id)
        {

            HttpResponseMessage risposta = GlobalVariables.WebApiClient.DeleteAsync("Componenti/" + id.ToString()).Result;
            ComponenteModel componente = risposta.Content.ReadAsAsync<ComponenteModel>().Result;
            return RedirectToAction("Show", "Carrelli", new { id = componente.CarrelloID });
        }

        public ActionResult Show(int id)
        {

            HttpResponseMessage risposta = GlobalVariables.WebApiClient.GetAsync("Componenti/" + id.ToString()).Result;
            TempData["SuccessMessage"] = risposta;
            dynamic risultato = risposta.Content.ReadAsAsync<ExpandoObject>().Result;
            ViewBag.data = risultato;
            return View(risultato);

        }
    }


}