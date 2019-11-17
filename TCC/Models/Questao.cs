using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TCC.Models
{
    public class Questao
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Id_Avaliacao { get; set; }

        [Required]
        public string Enunciado { get; set; }

        [Required]
        public string Alternativa1 { get; set; }

        [Required]
        public string Alternativa2 { get; set; }

        [Required]
        public string Alternativa3 { get; set; }

        [Required]
        public string Alternativa4 { get; set; }

        [Required]
        public string Alternativa5 { get; set; }

        [Required]
        [Range(1,5,ErrorMessage = "A alternativa correta deve ser entre 1 e 5")]
        public int Cod_AlternativaCorreta { get; set; }
    }
}