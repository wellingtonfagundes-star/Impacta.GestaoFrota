using Impacta.GestaoFrota.Application.Interfaces;
using Impacta.GestaoFrota.UI.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Impacta.GestaoFrota.UI.Controllers;

[Authorize(Policy = PolicyNames.AdminOnly)]
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
