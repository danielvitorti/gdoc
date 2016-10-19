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
        [Authorize]
        public ActionResult index()
        {
            return View(db.Documento.ToList());
        }

        // GET: Documento/Details/5
        [Authorize]
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
        [Authorize]
        public ActionResult criar()
        {
            return View();
        }

        // POST: Documento/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult criar([Bind(Include = "idDocumento,nomeDocumento,tipoDocumento,observacaoDocumento,dataCadastro,nomeArquivo")] Documento documento)
        {
 

        
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

                

                    db.Documento.Add(documento);
                    db.SaveChanges();
                    return RedirectToAction("index");
                
            }
                catch(Exception e)
                {
                
                Response.Write("Erro: " + e.Message);
                Response.End();
                }
               
           

            return View(documento);
        }

        // GET: Documento/Edit/5
        [Authorize]
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
        [Authorize]
        public ActionResult editar([Bind(Include = "idDocumento,nomeDocumento,tipoDocumento,observacaoDocumento,dataCadastro,nomeArquivo")] Documento documento)
        {


            try
            {

                HttpPostedFileBase file = Request.Files[0];


                /* Excluo o arquivo pra depois subir outro */
                if (file.ContentLength > 0)
                {

                    System.IO.File.Delete(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "arquivos\\" + documento.nomeArquivo));

                    string nomeArquivoSalvar = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "arquivos");

                    nomeArquivoSalvar = Path.Combine(nomeArquivoSalvar, Path.GetFileName(file.FileName));

                    file.SaveAs(nomeArquivoSalvar);

                }

                documento.nomeDocumento = Request.Form["nomeDocumento"];

                documento.tipoDocumento = Request.Form["tipoDocumento"];
                documento.observacaoDocumento = Request.Form["observacaoDocumento"];

                documento.nomeArquivo = file.FileName;


                db.Entry(documento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("index");


            }
            catch(Exception e)
            {

                Response.Write("Erro: " + e.Message);
                Response.End();

            }
         
            return View(documento);
        }

        // GET: Documento/Delete/5
        [Authorize]
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
        [Authorize]
        public ActionResult DeleteConfirmed(long id)
        {
            Documento documento = db.Documento.Find(id);
                try
                {
                    System.IO.File.Delete(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "arquivos\\" + documento.nomeArquivo));
                    db.Documento.Remove(documento);
                    db.SaveChanges();
                    return RedirectToAction("index");

                }
                catch(Exception e)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError); 

                }
     
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
