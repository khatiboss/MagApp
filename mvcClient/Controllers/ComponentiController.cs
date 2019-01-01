using mvcClient.Models;
using System;
using System.Collections.Generic;
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
            IEnumerable<ComponenteModel> ListaComponenti;
            HttpResponseMessage risposta = GlobalVariables.WebApiClient.GetAsync("Componenti").Result;
            ListaComponenti = risposta.Content.ReadAsAsync<IEnumerable<ComponenteModel>>().Result;
            return View(ListaComponenti);
        }


        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new ComponenteModel());
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
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            HttpResponseMessage risposta = GlobalVariables.WebApiClient.DeleteAsync("Componenti/" + id.ToString()).Result;
            return RedirectToAction("Index");
        }

        public ActionResult Show(int id)
        {

            HttpResponseMessage risposta = GlobalVariables.WebApiClient.GetAsync("Componenti/" + id.ToString()).Result;
            TempData["SuccessMessage"] = risposta;
           

            return View(risposta.Content.ReadAsAsync<ComponenteModel>().Result);
            
        }
    }
}