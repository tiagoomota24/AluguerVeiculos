using AluguerVeiculos.Data;
using AluguerVeiculos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList.Extensions;

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
        public IActionResult Index(int? page)
        {
            int pageSize = 10;
            int pageNumber = page ?? 1;

            var clientes = _context.Clientes.OrderBy(clientes => clientes.Nome_Completo).ToPagedList(pageNumber, pageSize);
            return View(clientes);
        }

        //GET: Formulário para criar cliente
        public IActionResult Create()
        {
            return View();
        }
        
        //POST: Criar Cliente
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Clientes.Add(cliente);
                    _context.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException ex)
                {
                    if (ex.InnerException != null && ex.InnerException.Message.Contains("IX_Clientes_Email"))
                    {
                        ModelState.AddModelError("Email", "Já existe um cliente com este email.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Ocorreu um erro ao salvar o cliente. Tente novamente.");
                    }
                }
            }
            return View(cliente);
        }

        //GET: Formulário para Editar Cliente
        public IActionResult Edit(int id)
        {
            var cliente = _context.Clientes.Find(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        //POST: Editar Cliente
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Clientes.Update(cliente);
                    _context.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException ex)
                {
                    if (ex.InnerException != null && ex.InnerException.Message.Contains("IX_Clientes_Email"))
                    {
                        ModelState.AddModelError("Email", "Já existe um cliente com este email.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Ocorreu um erro ao salvar o cliente. Tente novamente.");
                    }
                }
            }
            return View(cliente);
        }

        //GET: Eliminar Cliente
        public IActionResult Delete(int id)
        {
            var cliente = _context.Clientes.Find(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        //POST: Eliminar Cliente
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Cliente cliente)
        {
            try
            {
                _context.Clientes.Remove(cliente);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {
                ModelState.AddModelError(string.Empty, "Ocorreu um erro ao apagar o cliente. Tente novamente.");
            }
            return View(cliente);
        }
    }
}