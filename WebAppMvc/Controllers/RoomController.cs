using log4net;
using ClientPatientManagement.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppMvc.Data;

namespace WebAppMvc.Controllers
{
    public class RoomController : Controller
    {
        private static log4net.ILog Log { get; set; }

        ILog log = log4net.LogManager.GetLogger(typeof(RoomController));

        // GET: Room/**************************/
        public ActionResult Index() //mostrar la lista en el index, en la pantalla principal
        {
            var rooms = new RepositoryRoom().List();
            return View(rooms);
        }

        [HttpGet]
        public ActionResult Create() //la vista del formulario para crear el objeto
        {
            
            return View();
        }
        [HttpPost]
        public ActionResult Create(Room model) //accion para insertar el objeto creado en el repositorio
        {
            new RepositoryRoom().Insert(model);
            log.Debug("Datos insertados de Salas: " +"código de sala: " +  model.Id + "Localización: "+ model.Location + "Nombre de sala: " +model.Name);
           
            return RedirectToAction("Index");
        }



        [HttpGet]
        public ActionResult Edit(int id) //trae la vista del formulario para editar
        {
            var room = new RepositoryRoom().GetById(id);
            return View(room);
        }
        [HttpPost]
        public ActionResult Edit(Room model) //actuALIZA en el repositorio
        {
           new RepositoryRoom().Update(model);
            return RedirectToAction("Index");
        }

        /****delete ***/
        [HttpGet]
        public ActionResult Delete(int id) //trae la vista del formulario para editar
        {
           var room = new RepositoryRoom().GetById(id);
            return View(room);
        }

        [HttpPost]
        public ActionResult Delete2(int id) //elimina
        {
            new RepositoryRoom().Delete(id);
            return RedirectToAction("Index");
        }

        /*************/
    }
}