using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TCC.Models
{
    public class Conteudo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Id_Treinamento { get; set; }

        [Required]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Required]
        [Display(Name = "Módulo")]
        public int Cod_Modulo { get; set; }

        [Required]
        public string Link { get; set; }

        [Required]
        public bool ConteudoExterno { get; set; }

        [Display(Name = "Observações")]
        public string Observacao { get; set; }
    }
}