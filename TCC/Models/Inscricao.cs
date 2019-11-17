using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TCC.Models
{
    public class Inscricao
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Id_Usuario { get; set; }

        [Required]
        public int Id_Treinamento { get; set; }

        [Required]
        [Display(Name = "Data de Inscrição")]
        public DateTime DataInscricao { get; set; }

        [Display(Name = "Nota do Treinamento")]
        [Range(0,5,ErrorMessage = "A nota do treinamento deve ser de 1 a 5")]
        public int? NotaTreinamento { get; set; }

        [Display(Name = "Comentário sobre o treinamento")]
        public string AvaliacaoTreinamento { get; set; }

        public bool? Aprovado { get; set; }


    }
}