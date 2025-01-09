using AluguerVeiculos.Data;
using AluguerVeiculos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList.Extensions;

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
        public IActionResult Index(int? page)
        {

            int pageSize = 10;
            int pageNumber = page ?? 1;

            var veiculos = _context.Veiculos.OrderBy(v => v.Marca).ToPagedList(pageNumber, pageSize);
            return View(veiculos);
        }

        //GET: Formulário para criar veiculo
        public IActionResult Create()
        {
            return View();
        }

        //POST: Criar veiculo
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create (Veiculo veiculo)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Veiculos.Add(veiculo);
                    _context.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException ex)
                {
                    if (ex.InnerException != null && ex.InnerException.Message.Contains("IX_Veiculos_Matricula"))
                    {
                        ModelState.AddModelError("Matricula", "Já existe um veículo com esta matrícula.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Ocorreu um erro ao salvar o veículo. Tente novamente.");
                    }
                }            }
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

        //GET: Formulário para Editar Veiculo
        public IActionResult Edit(int id)
        {
            var veiculo = _context.Veiculos.Find(id);
            if (veiculo == null)
            {
                return NotFound();
            }
            return View(veiculo);
        }

        //POST: Editar Veiculo
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Veiculo veiculo)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Veiculos.Update(veiculo);

                    var contrato = _context.Contrato.Where(c => c.VeiculoId == veiculo.Id && c.Estado_Contrato == "Encerrado").OrderByDescending(c => c.DataFim).FirstOrDefault();
                    if (contrato != null)
                    {
                        contrato.QuilometragemFinal = veiculo.Quilometragem;
                        _context.Contrato.Update(contrato);
                    }
                    _context.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException ex)
                {
                    if (ex.InnerException != null && ex.InnerException.Message.Contains("IX_Veiculos_Matricula"))
                    {
                        ModelState.AddModelError("Matricula", "Já existe um veículo com esta matrícula.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Ocorreu um erro ao salvar o veículo. Tente novamente.");
                    }
                }
            }
            return View(veiculo);
        }

        //GET: Eliminar Veiculo
        public IActionResult Delete(int id)
        {
            var veiculo = _context.Veiculos.Find(id);
            if (veiculo == null)
            {
                return NotFound();
            }
            return View(veiculo);
        }

        //POST: Eliminar Veiculo
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Veiculo veiculo)
        {
            try
            {
                _context.Veiculos.Remove(veiculo);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao apagar o veículo: {ex.Message}");
                ModelState.AddModelError(string.Empty, "Erro ao apagar o veículo.");
            }
            return View(veiculo);
        }
    }
}