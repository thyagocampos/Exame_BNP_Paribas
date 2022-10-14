using Exame.Models;
using System.Collections.Generic;

namespace Exame.ViewModels
{
    public class MovimentoManualViewModel
    {
        public List<MovimentoManual> ListaMovimentoManual { get; set; }
        
        public MovimentoManual MovManual { get; set; }
        
        public string MesPesquisa { get; set; }

        public string AnoPesquisa { get; set; }

        public MovimentoManualViewModel()
        {
            ListaMovimentoManual = new List<MovimentoManual>();
            MovManual = new MovimentoManual();         
        }

    }
}
