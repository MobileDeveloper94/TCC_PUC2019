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
    public class VisualizacaoController : ApiController
    {
        ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Visualizacao/list/1/1
        public List<Visualizacao> Get(string method, int id, int id2)
        {
            List<Visualizacao> retorno;

            if (method.CompareTo("list") == 0)
            {
                retorno = db.Visualizacoes.Where(x => x.Id_Inscricao == id).ToList();
            }

            else if (method.CompareTo("detail") == 0)
            {
                retorno = db.Visualizacoes.Where(x => (x.Id_Inscricao == id && x.Id_Conteudo == id2)).ToList();
            }
            else
            {
                retorno = null;
            }

            return retorno;
        }

        // POST: api/Visualizacao
        public void Post([FromBody][Bind(Include = "Id,Id_Inscricao,Id_Conteudo,Visualizado")] Visualizacao visualizacao)
        {
            if (ModelState.IsValid)
            {
                Visualizacao existe;

                try
                {
                  existe = db.Visualizacoes.Where(x => (x.Id_Conteudo == visualizacao.Id_Conteudo && x.Id_Inscricao == visualizacao.Id_Inscricao)).Single();

                }
                catch (Exception e)
                {
                    existe = null;
                }



                if (existe == null)
                {
                    db.Visualizacoes.Add(visualizacao);
                    db.SaveChanges();
                }
                else
                {
                    existe.Visualizado = visualizacao.Visualizado;

                    db.Entry(existe).State = EntityState.Modified;

                    db.SaveChanges();
                }
            }
        }

        
    }
}
