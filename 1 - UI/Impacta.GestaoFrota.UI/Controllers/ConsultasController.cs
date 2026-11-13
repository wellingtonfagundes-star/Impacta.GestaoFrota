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
    }
}
