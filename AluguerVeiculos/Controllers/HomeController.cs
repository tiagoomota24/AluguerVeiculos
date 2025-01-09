using AluguerVeiculos.Data;
using Microsoft.AspNetCore.Mvc;

namespace AluguerVeiculos.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataContext _context;

        public HomeController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {

            int veiculosComErro = _context.Veiculos.Count(v => v.Quilometragem < 0);
            ViewBag.VeiculosComErro = veiculosComErro;

            ViewBag.VeiculosDisponiveis = _context.Veiculos.Count(v => v.Estado == "Disponível");
            ViewBag.ContratosAtivos = _context.Contrato.Count(c => c.Estado_Contrato == "Ativo");
            ViewBag.ClientesTotal = _context.Clientes.Count();

            return View();
        }
    }
}
