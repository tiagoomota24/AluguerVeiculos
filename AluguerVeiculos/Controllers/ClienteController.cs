using AluguerVeiculos.Data;
using AluguerVeiculos.Models;
using Microsoft.AspNetCore.Mvc;

namespace AluguerVeiculos.Controllers
{
    public class ClienteController : Controller
    {
        private readonly DataContext _context;

        public ClienteController( DataContext context)
        {
            _context = context;
        }

        //Lista os clientes
        public IActionResult Index()
        {
            var clientes = _context.Clientes.ToList();
            return View(clientes);
        }

        //GET: Formulário para criar cliente
        public IActionResult Create()
        {
            return View();
        }
        
        //POST: Criação do cliente
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                _context.Clientes.Add(cliente);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(cliente);
            
        }
    }
}