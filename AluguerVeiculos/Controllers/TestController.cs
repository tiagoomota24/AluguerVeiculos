using Microsoft.AspNetCore.Mvc;
using AluguerVeiculos.Data;
using AluguerVeiculos.Models;
using Microsoft.EntityFrameworkCore;

namespace AluguerVeiculos.Controllers
{
    public class TestController : Controller
    {
        private readonly DataContext _context;

        public TestController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> TestUniqueConstraints()
        {
            var veiculo1 = new Veiculo { Marca = "Marca1", Modelo = "Modelo1", Matricula = "ABC1234", Ano_de_fabrico = 2020, Tipo_de_Combustivel="Gasolina" };
            var veiculo2 = new Veiculo { Marca = "Marca2", Modelo = "Modelo2", Matricula = "ABC1235", Ano_de_fabrico = 2021, Tipo_de_Combustivel="Gasoleo" };

            var cliente1 = new Cliente { Nome_Completo = "Cliente1", Email = "cliente@example.com", Telefone = 123456789, Carta_de_Conducao = "123456" };
            var cliente2 = new Cliente { Nome_Completo = "Cliente2", Email = "client@example.com", Telefone = 987654321, Carta_de_Conducao = "654321" };

            _context.Veiculos.Add(veiculo1);
            _context.Veiculos.Add(veiculo2); 

            _context.Clientes.Add(cliente1);
            _context.Clientes.Add(cliente2); 

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                return Content($"Erro: {ex.Message} - Inner Exception: {ex.InnerException?.Message}");
            }

            return Content("Testes concluídos");
        }

        public async Task<IActionResult> ClearDatabase()
        {
            _context.Veiculos.RemoveRange(_context.Veiculos);
            _context.Clientes.RemoveRange(_context.Clientes);
            _context.Contrato.RemoveRange(_context.Contrato);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                return Content($"Erro ao limpar o banco de dados: {ex.Message} - Inner Exception: {ex.InnerException?.Message}");
            }

            return Content("Banco de dados limpo com sucesso");
        }
        public async Task<IActionResult> TestAlugarVeiculo()
        {
            // Criar um cliente
            var cliente = new Cliente
            {
                Nome_Completo = "Teste Cliente",
                Email = "teste@cliente.com",
                Telefone = 123456789,
                Carta_de_Conducao = "123456"
            };

            // Criar um veículo
            var veiculo = new Veiculo
            {
                Marca = "Teste Marca",
                Modelo = "Teste Modelo",
                Matricula = "TESTE123",
                Ano_de_fabrico = 2020,
                Tipo_de_Combustivel = "Gasolina",
                Estado = "Disponível"
            };

            // Adicionar cliente e veículo ao contexto
            _context.Clientes.Add(cliente);
            _context.Veiculos.Add(veiculo);
            await _context.SaveChangesAsync();

            // Criar um contrato
            var contrato = new Contrato
            {
                ClienteId = cliente.Id,
                VeiculoId = veiculo.Id,
                DataInicio = DateTime.Now,
                DataFim = DateTime.Now.AddDays(7),
                QuilometragemInicial = 1000
            };

            // Adicionar contrato ao contexto
            _context.Contrato.Add(contrato);
            await _context.SaveChangesAsync();

            // Atualizar o estado do veículo
            contrato.AtualizarEstadoVeiculo();
            _context.Veiculos.Update(veiculo);
            await _context.SaveChangesAsync();

            // Verificar o estado do veículo
            var veiculoAtualizado = await _context.Veiculos.FindAsync(veiculo.Id);
            if (veiculoAtualizado.Estado == "Alugado")
            {
                return Content("Teste bem-sucedido: O veículo está alugado.");
            }
            else
            {
                return Content("Teste falhou: O veículo não está alugado.");
            }
        }

        public async Task<IActionResult> TestAlugarVeiculoJaAlugado()
        {
            // Criar um cliente
            var cliente = new Cliente
            {
                Nome_Completo = "Teste Cliente 2",
                Email = "teste2@cliente.com",
                Telefone = 987654321,
                Carta_de_Conducao = "654321"
            };

            // Adicionar cliente ao contexto
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();

            // Tentar alugar o mesmo veículo que já está alugado
            var veiculo = await _context.Veiculos.FirstOrDefaultAsync(v => v.Matricula == "ABC1234");

            if (veiculo.Estado == "Alugado")
            {
                return Content("Erro: O veículo já está alugado.");
            }

            // Criar um contrato
            var contrato = new Contrato
            {
                ClienteId = cliente.Id,
                VeiculoId = veiculo.Id,
                DataInicio = DateTime.Now,
                DataFim = DateTime.Now.AddDays(7),
                QuilometragemInicial = 2000
            };

            // Adicionar contrato ao contexto
            _context.Contrato.Add(contrato);
            await _context.SaveChangesAsync();

            // Atualizar o estado do veículo
            contrato.AtualizarEstadoVeiculo();
            _context.Veiculos.Update(veiculo);
            await _context.SaveChangesAsync();

            return Content("Teste falhou: O veículo foi alugado novamente.");
        }
    }
}