using System;

namespace Treino.Domain.Models
{
    public class Treinamento
    {
        public int Codigo { get; set; }
        public string Descricao { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime Fim { get; set; }
        public int CargaHoraria { get; set; }
        public int Vagas { get; set; }

    }
}