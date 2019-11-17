using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TCC.Models
{
    public class Nota
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Id_Avaliacao { get; set; }

        [Required]
        public int Id_Inscricao { get; set; }

        [Required]
        public int NumQuestoes { get; set; }

        [Required]
        public int NumAcertos { get; set; }
    }
}