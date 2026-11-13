using Impacta.GestaoFrota.Application.ViewModel;
using Impacta.GestaoFrota.Data.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Impacta.GestaoFrota.UI.Controllers
{
    public class ConsultasController : Controller
    {
        private readonly FrotaContext _db;

        public ConsultasController(FrotaContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult VeiculosDisponiveis()
        {
            var veiculos = _db.Veiculos.AsNoTracking()
                .Where(item => item.IdStatusFrotaNavigation.NomeStatus == "DISPONIVEL")
                .OrderBy(item => item.Placa)
                .Select(item => new VeiculoDisponivelViewModel
                {
                    IdVeiculo = item.IdVeiculo,
                    Placa = item.Placa,
                    Fabricante = item.Fabricante,
                    AnoModelo = item.AnoModelo
                })
                .ToList();

            return View(veiculos);
        }

        public IActionResult VeiculosEmManutencao()
        {
            var veiculos = _db.Veiculos.AsNoTracking()
                .Where(item => item.IdStatusFrotaNavigation.NomeStatus == "MANUTENCAO")
                .OrderBy(item => item.Placa)
                .Select(item => new VeiculoEmManutencaoViewModel
                {
                    Placa = item.Placa,
                    Fabricante = item.Fabricante,
                    AnoModelo = item.AnoModelo,
                    NomeStatus = item.IdStatusFrotaNavigation.NomeStatus
                })
                .ToList();

            return View(veiculos);
        }

        public IActionResult ReservasAprovadas()
        {
            var reservas = (from reserva in _db.ReservaVeiculos.AsNoTracking()
                            join solicitante in _db.Pessoas.AsNoTracking()
                                on reserva.IdClienteSolicitante equals (int?)solicitante.IdPessoa
                            join aprovador in _db.Funcionarios.AsNoTracking()
                                on reserva.IdFuncionarioAprovador equals (int?)aprovador.IdFuncionario
                            where reserva.Aprovado == true
                            orderby reserva.Data descending, reserva.IdReserva
                            select new ReservaAprovadaConsultaViewModel
                            {
                                IdReserva = reserva.IdReserva,
                                Solicitante = solicitante.Nome,
                                Aprovador = aprovador.Nome,
                                Data = reserva.Data,
                                KmEstimada = reserva.KmEstimada
                            })
                .ToList();

            return View(reservas);
        }

        public IActionResult QuilometragemAtualFrota()
        {
            var quilometragem = _db.Veiculos.AsNoTracking()
                .GroupBy(_ => 1)
                .Select(veiculos => new QuilometragemAtualFrotaViewModel
                {
                    KmTotalFrota = veiculos.Sum(item => (long)item.KmAtual),
                    KmMediaFrota = veiculos.Average(item => (double)item.KmAtual)
                })
                .FirstOrDefault() ?? new QuilometragemAtualFrotaViewModel();

            return View(quilometragem);
        }
    }
}
