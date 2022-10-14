using System;
using System.Collections.Generic;

#nullable disable

namespace Exame.Models
{
    public partial class ProdutoCosif
    {
        public ProdutoCosif()
        {
            MovimentosManuais = new HashSet<MovimentoManual>();
        }

        public string CodProduto { get; set; }
        public string CodCosif { get; set; }
        public string CodClassificacao { get; set; }
        public string StaStatus { get; set; }

        public virtual Produto CodProdutoNavigation { get; set; }
        public virtual ICollection<MovimentoManual> MovimentosManuais { get; set; }
    }
}
