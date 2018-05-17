using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DOMAIN;
using WEB.Models;
using System.Threading.Tasks;
using WEB.Servico;

namespace WEB.Controllers
{
    public class postagemsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        ImageService imageService = new ImageService();

        // GET: postagems
        public ActionResult Index()
        {
            return View(db.Postagem.ToList());
        }

        // GET: postagems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            postagem postagem = db.Postagem.Find(id);
            if (postagem == null)
            {
                return HttpNotFound();
            }
            return View(postagem);
        }

        // GET: postagems/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: postagems/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,idUsuario,emailUsuario,Conteudo,imagem")] postagem postagem)
        {
            postagem.emailUsuario = Session["EmailUsuario"].ToString();
            postagem.idUsuario = Session["idUsuario"].ToString();
            postagem.imagem = null;
           
                db.Postagem.Add(postagem);
                db.SaveChanges();
                return RedirectToAction("Index");
           


        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> PostarImagem(postagem model, HttpPostedFileBase photo)
        {


            model.idUsuario = Session["idUsuario"].ToString();
            model.emailUsuario = Session["EmailUsuario"].ToString();

            if (photo == null)
            {
                model.imagem = "Sem Imagem";

            }
            else
            {
                ImageService imageService = new ImageService();
                var uploadImagem = await imageService.UploadImageAsync(photo);

                model.imagem = uploadImagem.ToString();

            }

            db.Postagem.Add(model);
            db.SaveChanges();

            return RedirectToAction("Index");







        }

        // GET: postagems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            postagem postagem = db.Postagem.Find(id);
            if (postagem == null)
            {
                return HttpNotFound();
            }
            return View(postagem);
        }

        // POST: postagems/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,idUsuario,emailUsuario,Conteudo,imagem")] postagem postagem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(postagem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(postagem);
        }

        // GET: postagems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            postagem postagem = db.Postagem.Find(id);
            if (postagem == null)
            {
                return HttpNotFound();
            }
            return View(postagem);
        }

        // POST: postagems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            postagem postagem = db.Postagem.Find(id);
            db.Postagem.Remove(postagem);
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
