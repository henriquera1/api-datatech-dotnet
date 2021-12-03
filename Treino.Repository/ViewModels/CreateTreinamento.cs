using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Treino.Repository.ViewModels
{
    public class CreateTreinamento
    {
        public int Codigo { get; set; }
        public string Descricao { get; set; }
        public DateTime Inicio { get; set; } 
        public DateTime Fim { get; set; } 
        public int CargaHoraria { get; set; }
        public int Vagas { get; set; }
    }
}
