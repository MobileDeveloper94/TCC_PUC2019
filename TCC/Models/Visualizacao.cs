using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TCC.Models
{
    public class Visualizacao
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Id_Inscricao { get; set; }

        [Required]
        public int Id_Conteudo { get; set; }

        public bool Visualizado { get; set; }
    }
}