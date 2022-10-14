using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Exame.Models
{
    public partial class MovimentoManual
    {                
        public int NumLancamento { get; set; }
        
        [DisplayName("Mês")]
        [Required(ErrorMessage = "Preencha o mês")]
        [Range(1,12,ErrorMessage ="Preencha um mês válido")]
        public int DatMes { get; set; }
        
        [RegularExpression("[0-9]{4}",ErrorMessage = "Preencha o ano com 4 dígitos")]
        [Required(ErrorMessage ="Preencha o ano com 4 dígitos")]
        public int DatAno { get; set; }
               
        public string CodProduto { get; set; }
        
        public string CodCosif { get; set; }
        
        public string DesDescricao { get; set; }
        
        public DateTime DatMovimento { get; set; }
        
        public string CodUsuario { get; set; }
        
        public decimal ValValor { get; set; }

        public virtual ProdutoCosif Cod { get; set; }

        public virtual Produto Produto { get; set; }
    }
}
