using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebAppMvc.Data;
using WebAppMvc.Models;

namespace WebAppMvc.Controllers
{
    public class TurnoesController : Controller
    {
        private VetDbContext db = new VetDbContext();

        // GET: Turnoes
        public ActionResult Index()
        {
            var turnos = db.Turnos.Include(t => t.Doctor).Include(t => t.Paciente).Include(t => t.Sala);
            return View(turnos.ToList());
        }

        // GET: Turnoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Turno turno = db.Turnos.Find(id);
            if (turno == null)
            {
                return HttpNotFound();
            }
            return View(turno);
        }

        // GET: Turnoes/Create
        public ActionResult Create()
        {
            ViewBag.IdDoctor = new SelectList(db.Doctors, "Id", "Name");
            ViewBag.IdPaciente = new SelectList(db.Patients, "Id", "Name");
            ViewBag.IdSala = new SelectList(db.Rooms, "Id", "Name");
            return View();
        }

        // POST: Turnoes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TipoEspecialidad,Estado,Fecha,IdPaciente,IdSala,IdDoctor,Hora")] Turno turno)
        {

            //buscar en la base de ese paciente, cliente si tiene un turno activo, si lo tiene no podrá elegir otro
            //hasta cancelar dicho turno
            var turnoYaExistente = from t in db.Turnos
                                   where t.IdPaciente == turno.IdPaciente &&
                                   t.TipoEspecialidad == turno.TipoEspecialidad &&
                                   t.Estado == SharedKernel.Estado.Activo
                                   select t;
            if (turnoYaExistente.Any())
            {
                ViewBag.Mensaje = "El paciente ya tiene un turno pendiente para esa especialidad, para seleccionar otro debe cancelar dicho turno";

                ViewBag.IdDoctor = new SelectList(db.Doctors, "Id", "Name", turno.IdDoctor);
                ViewBag.IdPaciente = new SelectList(db.Patients, "Id", "Name", turno.IdPaciente);
                ViewBag.IdSala = new SelectList(db.Rooms, "Id", "Name", turno.IdSala);
                return View(turno);
            }

            //busco del turno seleccionado en la base si coinciden todas las opciones
            var turnoBuscado = from t in db.Turnos
                               where t.IdPaciente == turno.IdPaciente &&
                               t.IdSala == turno.IdSala &&
                               t.IdDoctor == turno.IdDoctor &&
                               t.TipoEspecialidad == turno.TipoEspecialidad && t.Estado == turno.Estado &&
                               t.Fecha == t.Fecha && t.Hora == turno.Hora
                               select t;

            if (turnoBuscado.Any())
            {
                ViewBag.Mensaje = "El turno ya fue seleccionado, seleccione otras opciones";

                ViewBag.IdDoctor = new SelectList(db.Doctors, "Id", "Name", turno.IdDoctor);
                ViewBag.IdPaciente = new SelectList(db.Patients, "Id", "Name", turno.IdPaciente);
                ViewBag.IdSala = new SelectList(db.Rooms, "Id", "Name", turno.IdSala);
                return View(turno);
            }

       
            if (ModelState.IsValid)
                {
                    db.Turnos.Add(turno);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.IdDoctor = new SelectList(db.Doctors, "Id", "Name", turno.IdDoctor);
                ViewBag.IdPaciente = new SelectList(db.Patients, "Id", "Name", turno.IdPaciente);
                ViewBag.IdSala = new SelectList(db.Rooms, "Id", "Name", turno.IdSala);
                return View(turno);
            

            
        }

        // GET: Turnoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Turno turno = db.Turnos.Find(id);
            if (turno == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdDoctor = new SelectList(db.Doctors, "Id", "Name", turno.IdDoctor);
            ViewBag.IdPaciente = new SelectList(db.Patients, "Id", "Name", turno.IdPaciente);
            ViewBag.IdSala = new SelectList(db.Rooms, "Id", "Name", turno.IdSala);
            return View(turno);
        }

        // POST: Turnoes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TipoEspecialidad,Estado,Fecha,IdPaciente,IdSala,IdDoctor,Hora")] Turno turno)
        {
            if (ModelState.IsValid)
            {
                db.Entry(turno).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdDoctor = new SelectList(db.Doctors, "Id", "Name", turno.IdDoctor);
            ViewBag.IdPaciente = new SelectList(db.Patients, "Id", "Name", turno.IdPaciente);
            ViewBag.IdSala = new SelectList(db.Rooms, "Id", "Name", turno.IdSala);
            return View(turno);
        }

        // GET: Turnoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Turno turno = db.Turnos.Find(id); //traigo el turno
            turno.Doctor = db.Doctors.Find(turno.IdDoctor); //traigo el doctor
            turno.Sala = db.Rooms.Find(turno.IdSala);
            turno.Paciente = db.Patients.Find(turno.IdPaciente);
             
            if (turno == null)
            {
                return HttpNotFound();
            }
            return View(turno);
        }

        // POST: Turnoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Turno turno = db.Turnos.Find(id);
            db.Turnos.Remove(turno);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
