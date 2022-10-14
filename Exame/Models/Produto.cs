using System;
using System.Collections.Generic;

#nullable disable

namespace Exame.Models
{
    public partial class Produto
    {
        public Produto()
        {
            ProdutoCosifs = new HashSet<ProdutoCosif>();
        }

        public string CodProduto { get; set; }
        public string DesProduto { get; set; }
        public string StaStatus { get; set; }

        public virtual ICollection<ProdutoCosif> ProdutoCosifs { get; set; }
    }
}
