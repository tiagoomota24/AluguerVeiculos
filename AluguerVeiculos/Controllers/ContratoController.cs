using AluguerVeiculos.Data;
using AluguerVeiculos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList.Extensions;

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
        public IActionResult Index(int? page)
        {
            int pageSize = 10; 
            int pageNumber = page ?? 1; 

            var contratos = _context.Contrato.Include(c => c.Veiculo).Include(c => c.Cliente).OrderByDescending(c => c.DataInicio).ToList();
            foreach (var contrato in contratos)
            {
                contrato.AtualizarEstadoVeiculo(); 
                _context.Update(contrato.Veiculo);
                _context.Update(contrato);
            }
            _context.SaveChanges();

            var contratosPaginados = contratos.ToPagedList(pageNumber, pageSize);

            return View(contratosPaginados);
        }

        //GET: Formulário para criar contrato
        public IActionResult Create()
        {
            var clientes = _context.Clientes.Where(cliente => !_context.Contrato.Any(contrato => contrato.ClienteId == cliente.Id && contrato.Estado_Contrato == "Ativo")).ToList();
            var veiculos = _context.Veiculos.Where(Veiculo => Veiculo.Estado == "Disponível").ToList();

            Console.WriteLine($"Clientes: {clientes.Count}, Veículos: {veiculos.Count}");

            ViewBag.Clientes = clientes;
            ViewBag.Veiculos = veiculos;

            return View();
        }

        //POST: Criar contrato
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Contrato contrato)
        {
            ModelState.Remove("Cliente");
            ModelState.Remove("Veiculo");

            if (!ModelState.IsValid)
            {
                foreach (var key in ModelState.Keys)
                {
                    var errors = ModelState[key].Errors;
                    foreach (var error in errors)
                    {
                        Console.WriteLine($"Erro no campo {key}: {error.ErrorMessage}");
                    }
                }

                ViewBag.Clientes = _context.Clientes.ToList();
                ViewBag.Veiculos = _context.Veiculos.Where(v => v.Estado == "Disponível").ToList();
                return View(contrato);
            }

            try
            {
                var veiculo = _context.Veiculos.Find(contrato.VeiculoId);
                if (veiculo != null)
                {
                    contrato.QuilometragemFinal = veiculo.Quilometragem;
                    veiculo.Estado = "Alugado";
                }
                contrato.Estado_Contrato = "Ativo";
                _context.Contrato.Add(contrato);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao salvar o contrato: {ex.Message}");
                ModelState.AddModelError(string.Empty, "Erro ao salvar o contrato.");
            }

            ViewBag.Clientes = _context.Clientes.ToList();
            ViewBag.Veiculos = _context.Veiculos.Where(v => v.Estado == "Disponível").ToList();
            return View(contrato);
        }

        //GET: Eliminar Contrato
        public IActionResult Delete(int id)
        {
            var contrato = _context.Contrato
                .Include(c => c.Veiculo)
                .Include(c => c.Cliente)
                .FirstOrDefault(c => c.Id == id);

            if (contrato == null)
            {
                return NotFound();
            }

            return View(contrato);
        }

        //POST: Eliminar Contrato
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Contrato contrato)
        {
            try
            {
                var veiculo = contrato.Veiculo;
                if (veiculo != null)
                {
                    veiculo.Estado = "Disponível";
                    _context.Veiculos.Update(veiculo);
                }
                _context.Contrato.Remove(contrato);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao apagar o contrato: {ex.Message}");
                ModelState.AddModelError(string.Empty, "Erro ao apagar o contrato.");
            }

            return View(contrato);
        }
    } 
}