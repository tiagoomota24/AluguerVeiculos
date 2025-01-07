using AluguerVeiculos.Data;
using AluguerVeiculos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AluguerVeiculos.Controllers
{
    public class ContratoController : Controller
    {
        private readonly DataContext _context;

        public ContratoController( DataContext context)
        {
            _context = context;
        } 

        //Lista os contratos
        public IActionResult Index()
        {
            var contratos = _context.Contrato.Include(c => c.Veiculo).Include(c=> c.Cliente).ToList();
            return View(contratos);
        }

        //GET: Formulário para contrato
        public IActionResult Create()
        {
            ViewBag.Veiculos = _context.Veiculos.ToList();
            ViewBag.Clientes = _context.Clientes.ToList();
            return View();
        }

        //POST: Criação do contrato
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Contrato contrato)
        {
            if(ModelState.IsValid)
            {
                _context.Contrato.Add(contrato);

                //Atualiza o estado do veiculo
                var veiculo = _context.Veiculos.Find(contrato.VeiculoId);
                veiculo.Estado = "Alugado";

                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Veiculos = _context.Veiculos.ToList();
            ViewBag.Clientes = _context.Clientes.ToList();

            return View(contrato);
        }
    } 
}