using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Treino.Repository.ViewModels
{
    public class CreateEmpregado
    {
        [Required]
        public int Matricula { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public char Sexo { get; set; }
    }
}
