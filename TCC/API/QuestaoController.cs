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
    public class QuestaoController : ApiController
    {
        ApplicationDbContext db = new ApplicationDbContext();

        

        // GET: api/Questao/detail/5
        public List<Questao> Get(string method, int id)
        {
            List<Questao> retorno;

            if (method.CompareTo("list") == 0)
            {
                retorno = db.Questoes.Where(x => x.Id_Avaliacao == id).ToList();
            }
            else if (method.CompareTo("delete") == 0)
            {
                Questao questao = db.Questoes.Find(id);
                db.Questoes.Remove(questao);
                db.SaveChanges();
                retorno = null;
            }
            else if (method.CompareTo("detail") == 0)
            {
                retorno = new List<Questao>() { db.Questoes.Find(id) };
            }
            else
            {
                retorno = null;
            }

            return retorno;
        }

        // POST: api/Questao
        public void Post([FromBody][Bind(Include = "Id,Id_Avaliacao,Enunciado,Alternativa1,Alternativa2,Alternativa3,Alternativa4,Alternativa5,Cod_AlternativaCorreta")] Questao questao)
        {
            if (ModelState.IsValid)
            {
                if (questao.Id == 0)
                {
                    db.Questoes.Add(questao);
                    db.SaveChanges();
                }
                else
                {
                    db.Entry(questao).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
        }
    }
        
    
}
