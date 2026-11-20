using Impacta.GestaoFrota.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Impacta.GestaoFrota.UI.Controllers;

public class DashboardController : Controller
{
    private readonly IConsultaAppService _consultaAppService;

    public DashboardController(IConsultaAppService consultaAppService)
    {
        _consultaAppService = consultaAppService;
    }

    public IActionResult ConsultaConsolidadaIndicadores()
    {
        return View(_consultaAppService.ObterIndicadoresDashboardFrota());
    }
}
