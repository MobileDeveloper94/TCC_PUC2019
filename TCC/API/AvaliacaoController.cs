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
    public class AvaliacaoController : ApiController
    {
        ApplicationDbContext db = new ApplicationDbContext();

        List<Avaliacao> retorno;

        // GET: api/Avaliacao/5
        public List<Avaliacao> Get(string method, int id)
        {
            if (method.CompareTo("list") == 0)
            {
                retorno = db.Avaliacoes.Where(x => x.Id_Treinamento == id).ToList();
            }
            else if (method.CompareTo("delete") == 0)
            {

                Avaliacao avaliacao = db.Avaliacoes.Find(id);
                db.Avaliacoes.Remove(avaliacao);
                db.SaveChanges();
                retorno = null;
            }
            else if (method.CompareTo("detail") == 0)
            {
                retorno = new List<Avaliacao>() { db.Avaliacoes.Find(id) };
            }
            else
            {
                retorno = null;
            }

            return retorno;
        }

        // POST: api/Avaliacao
        public void Post([FromBody][Bind(Include = "Id,Id_Treinamento,Cod_Modulo,Descricao")] Avaliacao avaliacao)
        {
            if (ModelState.IsValid)
            {
                if (avaliacao.Id == 0)
                {
                    db.Avaliacoes.Add(avaliacao);
                    db.SaveChanges();
                }
                else
                {
                    db.Entry(avaliacao).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
        }

        
    }
}
