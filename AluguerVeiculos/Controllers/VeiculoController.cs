using AluguerVeiculos.Data;
using AluguerVeiculos.Models;
using Microsoft.AspNetCore.Mvc;

namespace AluguerVeiculos.Controllers
{
    public class VeiculoController : Controller
    {
        private readonly DataContext _context;

        public VeiculoController(DataContext context)
        {
            _context = context;
        }

        //Listar os veiculos
        public IActionResult Index()
        {
            var veiculos = _context.Veiculos.ToList();
            return View(veiculos);
        }

        //GET: Formulário para criar novo veiculo
        public IActionResult Create()
        {
            return View();
        }

        //POST: Criação de um novo veiculo
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create (Veiculo veiculo)
        {
            if (ModelState.IsValid)
            {
                _context.Veiculos.Add(veiculo);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(veiculo);
        }
        
        public void AtualizarEstadoVeiculo(int veiculoId)
        {
            var veiculo = _context.Veiculos.Find(veiculoId);
            var contratoAtivo = _context.Contrato.Where(c=>c.VeiculoId == veiculoId && c.DataFim > DateTime.Now).FirstOrDefault();

            if (contratoAtivo != null)
            {
                veiculo.Estado = "Alugado";
            }
            else
            {
                veiculo.Estado = "Disponível";
            }
            _context.SaveChanges();
        }
    }
}