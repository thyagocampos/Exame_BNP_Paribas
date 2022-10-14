using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Exame.Models;
using Exame.ViewModels;

namespace Exame.Controllers
{
    public class MovimentoManualController : Controller
    {
        private readonly BNP_Paribas_DBContext _context;

        public MovimentoManualController(BNP_Paribas_DBContext context)
        {
            _context = context;
        }

        // GET: MovimentoManual/Index
        public IActionResult Index()
        {
            //Preparando a viewmodel
            MovimentoManualViewModel VM = new MovimentoManualViewModel()
            {
                ListaMovimentoManual = _context.MovimentosManuais.Include("Produto").ToList()
            };

            return View(VM);
        }

        public JsonResult GetProdutos()
        {
            var data = _context.Produtos.ToList();

            return Json(data);
        }

        public JsonResult GetProdutoCosif(string CodProduto)
        {
            var data = _context.ProdutoCosifs.Where(x => x.CodProduto == CodProduto).ToList();

            return Json(data);
        }

        //Adiciona um novo movimento manual        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdicionaMovimentoManual(MovimentoManualViewModel movimentoManualVM)
        {
            if (ModelState.IsValid)
            {
                movimentoManualVM.MovManual.DatMovimento = DateTime.Now;
                movimentoManualVM.MovManual.CodUsuario = "TESTE";

                var contadorMes = _context.MovimentosManuais.Where(
                    movimento =>
                    movimento.DatMes == movimentoManualVM.MovManual.DatMes &&
                    movimento.DatAno == movimentoManualVM.MovManual.DatAno
                ).Count();

                movimentoManualVM.MovManual.NumLancamento = contadorMes + 1;

                try
                {
                    _context.Add(movimentoManualVM.MovManual);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    return this.Ok(ex.Message);
                }
            }

            return this.Ok($"Movimento manual adicionado");
        }

        //Recarregar a lista de movimentos manuais para atualizar a partial view
        [HttpGet]
        public async Task<IActionResult> ReloadMovimentosManuais(string? AnoPesquisa, string? MesPesquisa)
        {
            //Preparando a viewmodel
            MovimentoManualViewModel VM = new MovimentoManualViewModel();

            int anoPesquisa= 0; 
            int mesPesquisa=0; 

            if (!string.IsNullOrEmpty(AnoPesquisa) && !string.IsNullOrEmpty(MesPesquisa))
            {
                anoPesquisa = int.Parse(AnoPesquisa);
                mesPesquisa = int.Parse(MesPesquisa);

                VM.ListaMovimentoManual =  await _context.MovimentosManuais
                    .Where(mov => mov.DatAno == anoPesquisa)
                    .Where(mov => mov.DatMes == mesPesquisa)
                    .ToListAsync();

                VM.AnoPesquisa = AnoPesquisa;
                VM.MesPesquisa = MesPesquisa;
                
            }
            else
            {
                VM.ListaMovimentoManual = await _context.MovimentosManuais.Include("Produto").ToListAsync();
            }                

            return PartialView("_ListaMovimentoManual", VM);
        }
    }
}