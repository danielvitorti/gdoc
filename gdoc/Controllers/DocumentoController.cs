using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using gdoc.Models;
using System.IO;
namespace gdoc.Controllers
{
    public class DocumentoController : Controller
    {
        private ModeloDados db = new ModeloDados();

        // GET: Documento
        public ActionResult index()
        {
            return View(db.Documento.ToList());
        }

        // GET: Documento/Details/5
        public ActionResult detalhe(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Documento documento = db.Documento.Find(id);
            if (documento == null)
            {
                return HttpNotFound();
            }
            return View(documento);
        }

        // GET: Documento/Create
        public ActionResult criar()
        {
            return View();
        }

        // POST: Documento/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult criar([Bind(Include = "idDocumento,nomeDocumento,tipoDocumento,observacaoDocumento,dataCadastro,nomeArquivo")] Documento documento)
        {
            // if (ModelState.IsValid)
            // {
            /* Tratamento de exceções para o upload do arquikvo*/

        
            try
                {
                
                    HttpPostedFileBase file = Request.Files[0];



                    string nomeArquivoSalvar = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "arquivos");

                    nomeArquivoSalvar = Path.Combine(nomeArquivoSalvar, Path.GetFileName(file.FileName));

                    file.SaveAs(nomeArquivoSalvar);

                    documento.nomeDocumento = Request.Form["nomeDocumento"];

                    documento.tipoDocumento = Request.Form["tipoDocumento"];
                    documento.observacaoDocumento = Request.Form["observacaoDocumento"];
                
                    documento.nomeArquivo = file.FileName;

                //return View(documento);

                    db.Documento.Add(documento);
                    db.SaveChanges();
                    return RedirectToAction("index");
                    //return View(documento);

            }
                catch(Exception e)
                {
                //ViewBag.Message = "Erro ao salvar arquivo";
                Response.Write("Erro: " + e.Message);
                Response.End();
                }
               
           // }

            return View(documento);
        }

        // GET: Documento/Edit/5
        public ActionResult editar(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Documento documento = db.Documento.Find(id);
            if (documento == null)
            {
                return HttpNotFound();
            }
            return View(documento);
        }

        // POST: Documento/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult editar([Bind(Include = "idDocumento,nomeDocumento,tipoDocumento,observacaoDocumento,dataCadastro")] Documento documento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(documento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("index");
            }
            return View(documento);
        }

        // GET: Documento/Delete/5
        public ActionResult excluir(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Documento documento = db.Documento.Find(id);
            if (documento == null)
            {
                return HttpNotFound();
            }
            return View(documento);
        }

        // POST: Documento/Delete/5
        [HttpPost, ActionName("excluir")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Documento documento = db.Documento.Find(id);
            db.Documento.Remove(documento);
            db.SaveChanges();
            return RedirectToAction("index");
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
