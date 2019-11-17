using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TCC.Models;

namespace TCC.API
{
    public class RelatorioController : ApiController
    {
        ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Relatorio/usuarios
        public List<ApplicationUser> Get(string method)
        {
            if (method.CompareTo("usuarios") == 0)
            {
                List<ApplicationUser> retorno = new List<ApplicationUser>();

                var users = db.Users.ToList();

                foreach (var item in users)
                {
                    List<Inscricao> inscricao = new List<Inscricao>();
                    inscricao = db.Inscricoes.Where(x => x.Id_Usuario.Equals(item.Id)).ToList();
                    if (inscricao.Count > 0)
                    {
                        retorno.Add(item);
                    }
                }

                return retorno;
            }
            else
            {
                return null;
            }

            
        }

        // GET: api/Relatorio/resultados/id_usuario
        public List<UsuarioResultado> Get(string method, string id)
        {
            if (method.CompareTo("resultados") == 0)
            {
                List<UsuarioResultado> retorno = new List<UsuarioResultado>();

                List<Inscricao> inscricoes = db.Inscricoes.Where(x => x.Id_Usuario.Equals(id)).ToList();


                foreach(var inscricao in inscricoes)
                {
                    UsuarioResultado resultado = new UsuarioResultado();

                    Treinamento treinamento = db.Treinamentos.Find(inscricao.Id_Treinamento);

                    resultado.Treinamento = treinamento.Titulo;

                    List<Nota> notas = db.Notas.Where(x => x.Id_Inscricao == inscricao.Id).ToList();

                    int numQuestoes = 0;
                    int numAcertos = 0;

                    foreach(var item in notas)
                    {
                        numQuestoes = numQuestoes + item.NumQuestoes;
                        numAcertos = numAcertos + item.NumAcertos;
                    }

                    resultado.Questoes =  numQuestoes;
                    resultado.Acertos = numAcertos;

                    List<Conteudo> conteudos = db.Conteudos.Where(x => x.Id_Treinamento == treinamento.Id).ToList();

                    List<Visualizacao> visualizacoes = db.Visualizacoes.Where(x => x.Id_Inscricao == inscricao.Id).ToList();

                    int numConteudos = conteudos.Count;
                    int numVisualizacoes = visualizacoes.Count;

                    if (numConteudos > 0)
                    {
                        resultado.ConteudoAssistido = (numVisualizacoes * 100) / numConteudos;
                    }
                    else
                    {
                        resultado.ConteudoAssistido = 0;

                    }


                    resultado.Aprovacao = inscricao.Aprovado;

                    retorno.Add(resultado);
                }

                return retorno;
            }
            else
            {
                return null;
            }


        }

        public class UsuarioResultado
        {
            public string Treinamento { get; set; }
            
            public int Questoes { get; set; }

            public int Acertos { get; set; }

            public double ConteudoAssistido { get; set; }

            public bool? Aprovacao { get; set; }

        }
    }
}
