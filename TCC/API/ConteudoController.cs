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
    public class ConteudoController : ApiController
    {
        ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Conteudo/list/1
        public List<Conteudo> Get(string method, int id)
        {
            List<Conteudo> retorno;

            if (method.CompareTo("list") == 0)
            {
                retorno = db.Conteudos.Where(x => x.Id_Treinamento == id).ToList();
            }
            else if (method.CompareTo("delete") == 0)
            {

                Conteudo conteudo = db.Conteudos.Find(id);
                db.Conteudos.Remove(conteudo);
                db.SaveChanges();
                retorno = null;
            }
            else if (method.CompareTo("detail") == 0)
            {
                retorno = new List<Conteudo>() { db.Conteudos.Find(id) };
            }
            else
            {
                retorno = null;
            }
            
            return retorno;
        }

        // POST: api/Conteudo
        public void Post([FromBody][Bind(Include = "Id,Id_Treinamento,Descricao,Cod_Modulo,Link,ConteudoExterno,Observacao")] Conteudo conteudo)
        {
            if (ModelState.IsValid)
            {
                if (conteudo.Id == 0)
                {
                    db.Conteudos.Add(conteudo);
                    db.SaveChanges();
                }
                else
                {
                    db.Entry(conteudo).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
        }
    }
}
