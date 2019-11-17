using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TCC.Models;

namespace TCC.Controllers
{
    [Authorize]
    public class TreinamentosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Treinamentos
        public ActionResult Index()
        {
            return View(db.Treinamentos.ToList());
        }


        // GET: Treinamentos/Details/5
        [Authorize(Roles = "Usuário")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Treinamento treinamento = db.Treinamentos.Find(id);
            if (treinamento == null)
            {
                return HttpNotFound();
            }
            return View(treinamento);
        }


        // GET: Treinamentos/Create
        [Authorize(Roles = "Administrador")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Treinamentos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Titulo,Categoria,DataInicio,DataFim,Indeterminado,Instrutor,Modulos,PalavrasChave")] Treinamento treinamento)
        {
            if (ModelState.IsValid)
            {
                db.Treinamentos.Add(treinamento);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(treinamento);
        }

        // GET: Treinamentos/Edit/5
        [Authorize(Roles = "Administrador")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Treinamento treinamento = db.Treinamentos.Find(id);
            if (treinamento == null)
            {
                return HttpNotFound();
            }
            return View(treinamento);
        }

        // POST: Treinamentos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Titulo,Categoria,DataInicio,Indeterminado,Instrutor,Modulos,PalavrasChave")] Treinamento treinamento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(treinamento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(treinamento);
        }

        // GET: Treinamentos/Delete/5
        [Authorize(Roles = "Administrador")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Treinamento treinamento = db.Treinamentos.Find(id);
            if (treinamento == null)
            {
                return HttpNotFound();
            }
            return View(treinamento);
        }

        // POST: Treinamentos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Treinamento treinamento = db.Treinamentos.Find(id);
            db.Treinamentos.Remove(treinamento);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Treinamentos/_EditQuestoes/5
        public PartialViewResult _EditQuestoes(int? id)
        {
            
            ViewBag.Id_Avaliacao = id;
            return PartialView();
        }

        // GET: Treinamentos/Inscricao/5
        [Authorize(Roles = "Usuário")]
        public ActionResult Inscricao(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Treinamento treinamento = db.Treinamentos.Find(id);
            if (treinamento == null)
            {
                return HttpNotFound();
            }
            return View(treinamento);
        }

        // GET: Treinamentos/Conteudo/5
        [Authorize(Roles = "Usuário")]
        public ActionResult Conteudo(int? id, string cod)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (cod != null && (cod.Equals("cf807912f783ce97bbf78a52c74b2529")))
            {
                Conteudo conteudo = db.Conteudos.Find(id);
                return View(conteudo);
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }
            
        }

        // GET: Treinamentos/Avaliacao/5
        [Authorize(Roles = "Usuário")]
        public ActionResult Avaliacao(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Avaliacao avaliacao = db.Avaliacoes.Find(id);
            if (avaliacao == null)
            {
                return HttpNotFound();
            }
            return View(avaliacao);
        }

        [Authorize(Roles = "Administrador")]
        public ActionResult Relatorio()
        {
            
            return View(db.Users.ToList());
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase Arquivo_Conteudo, int Id_Treinamento)
        {
            try
            {
                string nomeArquivo = "";
                string arquivoEnviados = "";
                
                    if (Arquivo_Conteudo.ContentLength > 0)
                    {
                        nomeArquivo = Path.GetFileName(Arquivo_Conteudo.FileName);
                        var caminho = Path.Combine(Server.MapPath("~/Files/Conteudo"), nomeArquivo);
                        Arquivo_Conteudo.SaveAs(caminho);
                    }

                    arquivoEnviados = arquivoEnviados + " , " + nomeArquivo;
                
                ViewBag.Mensagem = "Arquivo enviados com sucesso: " + arquivoEnviados;
            }
            catch (Exception ex)
            {
                ViewBag.Mensagem = "Erro : " + ex.Message;
            }

            return RedirectToAction("Edit", "Treinamentos", new { id = Id_Treinamento });

        }

        [HttpPost]
        public ActionResult Inscricao(int Id_Treinamento, string Id_Usuario)
        {
            Inscricao inscricao = new Inscricao();

            inscricao.Id_Treinamento = Id_Treinamento;
            inscricao.Id_Usuario = Id_Usuario;
            inscricao.DataInscricao = DateTime.Now;
            inscricao.NotaTreinamento = 0;
            inscricao.AvaliacaoTreinamento = "";
            inscricao.Aprovado = false;

            db.Inscricoes.Add(inscricao);
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
