using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TCC.Models;
using System.IO;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Web.Hosting;

namespace TCC.API
{
    public class CertificadoController : ApiController
    {
        ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Certificado/5
        public string Get(string method, int id, string id2)
        {
            Inscricao inscricao = db.Inscricoes.Where(x => (x.Id_Treinamento == id && x.Id_Usuario.Equals(id2))).Single();
            Treinamento treinamento = db.Treinamentos.Find(id);

            if (method.CompareTo("emitir") == 0)
            {
                Document doc = new Document(PageSize.A4.Rotate());
                doc.SetMargins(40, 40, 40, 40);
                
                doc.AddCreationDate();

                

                string nomeArquivo = id + "-" + id2 + ".pdf";
                var caminho = Path.Combine(HostingEnvironment.MapPath("~/Files/PDF"), nomeArquivo);

                PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(caminho, FileMode.Create));

                doc.Open();

                string dados = "";
                Font fonte = new Font(Font.NORMAL, 28);

                Paragraph paragrafo = new Paragraph(dados, fonte);

                paragrafo.Alignment = Element.ALIGN_CENTER;

                paragrafo.Add("CERTIFICADO DE CONCLUSÃO");

                doc.Add(paragrafo);

                doc.Add(new Chunk("\n"));
                doc.Add(new Chunk("\n"));
                doc.Add(new Chunk("\n"));


                paragrafo = new Paragraph(dados, new Font(Font.NORMAL, 18));

                paragrafo.Alignment = Element.ALIGN_JUSTIFIED;

                

                dados = "Certificamos que " + User.Identity.GetUserName() + " concluiu com êxito o treinamento " + treinamento.Titulo + ", ministrado por " + treinamento.Instrutor + ".";

                paragrafo.Add(dados);

                doc.Add(paragrafo);
                

                doc.Close();

                inscricao.Aprovado = true;
                db.Entry(inscricao).State = EntityState.Modified;
                db.SaveChanges();

                return nomeArquivo;

            }
            else
            {
                return "erro";
            }

            
        }

        // POST: api/Certificado
        public void Post([FromBody]string value)
        {
        }

        
    }
}
