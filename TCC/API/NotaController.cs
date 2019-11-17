using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using TCC.Models;

namespace TCC.API
{
    public class NotaController : ApiController
    {

        ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Nota/list/5
        public List<Nota> Get(string method, int id, int id2)
        {
            List<Nota> retorno;

            if (method.CompareTo("list") == 0)
            {
                retorno = db.Notas.Where(x => x.Id_Inscricao == id).ToList();
            }
            else if (method.CompareTo("delete") == 0)
            {
                Nota nota = db.Notas.Find(id);
                db.Notas.Remove(nota);
                db.SaveChanges();
                retorno = null;
            }
            else if (method.CompareTo("detail") == 0)
            {
                retorno = db.Notas.Where(x => (x.Id_Inscricao == id && x.Id_Avaliacao == id2)).ToList();
            }
            else
            {
                retorno = null;
            }

            return retorno;
        }

        // POST: api/Nota
        public void Post([FromBody][Bind(Include = "Id,Id_Avaliacao,Id_Inscricao,NumQuestoes,NumAcertos")]Nota nota)
        {
            if (ModelState.IsValid)
            {
                Nota existe;

                try
                {
                    existe = db.Notas.Where(x => (x.Id_Avaliacao == nota.Id_Avaliacao && x.Id_Inscricao == nota.Id_Inscricao)).Single();

                }
                catch (Exception e)
                {
                    existe = null;
                }



                if (existe == null)
                {
                    db.Notas.Add(nota);
                    db.SaveChanges();
                }
                else
                {
                    existe.NumQuestoes = nota.NumQuestoes;
                    existe.NumAcertos = nota.NumAcertos;

                    db.Entry(existe).State = EntityState.Modified;

                    db.SaveChanges();
                }
            }
        }

        
    }
}
