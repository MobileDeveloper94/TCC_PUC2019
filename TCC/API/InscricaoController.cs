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
    public class InscricaoController : ApiController
    {
        ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Inscricao/list/id_treinamento/id_usuario
        public List<Inscricao> Get(string method, int id, string id2)
        {
            List<Inscricao> retorno;

            if (method.CompareTo("list") == 0)
            {
                retorno = db.Inscricoes.Where(x => (x.Id_Treinamento == id && x.Id_Usuario.Equals(id2))).ToList();
            }
            
            else if (method.CompareTo("detail") == 0)
            {
                retorno = new List<Inscricao>() { db.Inscricoes.Find(id) };
            }
            else if (method.CompareTo("treinamento") == 0)
            {
                retorno = db.Inscricoes.Where(x => x.Id_Treinamento == id).ToList();
            }
            else
            {
                retorno = null;
            }

            return retorno;
        }

        // POST: api/Inscricao
        public void Post([FromBody][Bind(Include = "Id,Id_Usuario,Id_Treinamento,DataInscricao,NotaTreinamento,AvaliacaoTreinamento,Aprovado")] Inscricao inscricao)
        {
            if (ModelState.IsValid)
            {
                if (inscricao.Id == 0)
                {
                    db.Inscricoes.Add(inscricao);
                    db.SaveChanges();
                }
                else
                {
                    db.Entry(inscricao).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
        }

        
    }
}
