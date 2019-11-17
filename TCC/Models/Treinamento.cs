using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TCC.Models
{
    public class Treinamento
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name ="Título")]
        public string Titulo { get; set; }

        [Required]
        public string Categoria { get; set; }

        [Required]
        [Display(Name = "Data de início")]
        public DateTime DataInicio { get; set; }
        
        [Display(Name = "Data de término")]
        public DateTime? DataFim { get; set; }

        [Required]
        [Display(Name = "Término indeterminado")]
        public bool Indeterminado { get; set; }

        [Required]
        [Display(Name = "Nome do instrutor")]
        public string Instrutor { get; set; }

        [Required]
        [Display(Name = "Número de módulos")]
        public int Modulos { get; set; }

        [Display(Name = "Palavras-chave")]
        public string PalavrasChave { get; set; }
    }
}