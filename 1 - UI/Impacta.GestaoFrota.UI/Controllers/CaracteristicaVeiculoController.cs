using Impacta.GestaoFrota.Application.Interfaces;
using Impacta.GestaoFrota.Application.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Impacta.GestaoFrota.UI.Controllers
{
    public class CaracteristicaVeiculoController : Controller
    {
        private readonly ICaracteristicaVeiculoAppService _caracteristicaVeiculoAppService;
        private readonly IVeiculoAppService _veiculoAppService;
        private readonly ICaracteristicaAppService _caracteristicaAppService;

        public CaracteristicaVeiculoController(
            ICaracteristicaVeiculoAppService caracteristicaVeiculoAppService,
            IVeiculoAppService veiculoAppService,
            ICaracteristicaAppService caracteristicaAppService)
        {
            _caracteristicaVeiculoAppService = caracteristicaVeiculoAppService;
            _veiculoAppService = veiculoAppService;
            _caracteristicaAppService = caracteristicaAppService;
        }

        public IActionResult Index(int page = 1, int pageSize = 10, string search = "")
        {
            page = Math.Max(page, 1);
            pageSize = pageSize is 5 or 10 or 25 or 50 ? pageSize : 10;

            var todos = _caracteristicaVeiculoAppService.ObterTodos().ToList();

            // Aplicar filtro de pesquisa
            if (!string.IsNullOrWhiteSpace(search))
            {
                todos = todos
                    .Where(cv => cv.VeiculoDescricao.Contains(search, StringComparison.CurrentCultureIgnoreCase) ||
                                cv.CaracteristicaDescricao.Contains(search, StringComparison.CurrentCultureIgnoreCase))
                    .ToList();
            }

            var totalPaginas = Math.Max((int)Math.Ceiling(todos.Count / (double)pageSize), 1);
            page = Math.Min(page, totalPaginas);

            ViewBag.Page = page;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalRegistros = todos.Count;
            ViewBag.TotalPaginas = totalPaginas;
            ViewBag.Search = search;

            return View(todos.Skip((page - 1) * pageSize).Take(pageSize));
        }

        [HttpGet]
        public IActionResult Create()
        {
            PopularDropdowns();

            if (IsAjaxRequest())
                return PartialView("_CreateModal", new CaracteristicaVeiculoViewModel());

            return View(new CaracteristicaVeiculoViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CaracteristicaVeiculoViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                if (IsAjaxRequest())
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors);
                    var errorMessage = string.Join(", ", errors.Select(e => e.ErrorMessage));
                    return Json(new { success = false, message = errorMessage });
                }

                PopularDropdowns();
                return IsAjaxRequest()
                    ? PartialView("_CreateModal", viewModel)
                    : View(viewModel);
            }

            if (_caracteristicaVeiculoAppService.Adicionar(viewModel))
            {
                if (IsAjaxRequest())
                    return Json(new { success = true, message = "Parametrização cadastrada com sucesso." });

                TempData["SuccessMessage"] = "Parametrização cadastrada com sucesso.";
                return RedirectToAction(nameof(Index));
            }

            var errorMsg = "Não foi possível cadastrar a parametrização.";
            if (IsAjaxRequest())
                return Json(new { success = false, message = errorMsg });

            PopularDropdowns();
            ModelState.AddModelError(string.Empty, errorMsg);
            return IsAjaxRequest()
                ? PartialView("_CreateModal", viewModel)
                : View(viewModel);
        }

        [HttpGet]
        public IActionResult Edit(int idVeiculo, int idCaracteristica)
        {
            var viewModel = _caracteristicaVeiculoAppService.ObterPorId(idVeiculo, idCaracteristica);
            if (viewModel is null)
                return NotFound();

            PopularDropdowns();

            if (IsAjaxRequest())
                return PartialView("_EditModal", viewModel);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int idVeiculo, int idCaracteristica, CaracteristicaVeiculoViewModel viewModel)
        {
            if (idVeiculo != viewModel.IdVeiculo || idCaracteristica != viewModel.IdCaracteristica)
                return BadRequest();

            if (!ModelState.IsValid)
            {
                if (IsAjaxRequest())
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors);
                    var errorMessage = string.Join(", ", errors.Select(e => e.ErrorMessage));
                    return Json(new { success = false, message = errorMessage });
                }

                PopularDropdowns();
                return IsAjaxRequest()
                    ? PartialView("_EditModal", viewModel)
                    : View(viewModel);
            }

            if (_caracteristicaVeiculoAppService.Atualizar(viewModel))
            {
                if (IsAjaxRequest())
                    return Json(new { success = true, message = "Parametrização atualizada com sucesso." });

                TempData["SuccessMessage"] = "Parametrização atualizada com sucesso.";
                return RedirectToAction(nameof(Index));
            }

            var errorMsg = "Não foi possível atualizar a parametrização.";
            if (IsAjaxRequest())
                return Json(new { success = false, message = errorMsg });

            PopularDropdowns();
            ModelState.AddModelError(string.Empty, errorMsg);
            return IsAjaxRequest()
                ? PartialView("_EditModal", viewModel)
                : View(viewModel);
        }

        [HttpGet]
        public IActionResult Delete(int idVeiculo, int idCaracteristica)
        {
            var viewModel = _caracteristicaVeiculoAppService.ObterPorId(idVeiculo, idCaracteristica);
            if (viewModel is null)
                return NotFound();

            if (IsAjaxRequest())
                return PartialView("_DeleteModal", viewModel);

            return View(viewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int idVeiculo, int idCaracteristica)
        {
            if (_caracteristicaVeiculoAppService.Remover(idVeiculo, idCaracteristica))
            {
                if (IsAjaxRequest())
                    return Json(new { success = true, message = "Parametrização excluída com sucesso." });

                TempData["SuccessMessage"] = "Parametrização removida com sucesso.";
            }
            else
            {
                if (IsAjaxRequest())
                    return Json(new { success = false, message = "Não foi possível excluir a parametrização." });

                TempData["ErrorMessage"] = "Não foi possível remover a parametrização.";
            }

            return RedirectToAction(nameof(Index));
        }

        private void PopularDropdowns()
        {
            var veiculos = _veiculoAppService.ObterTodos()
                .Select(v => new SelectListItem
                {
                    Value = v.IdVeiculo.ToString(),
                    Text = $"{v.Placa} - {v.Fabricante} ({v.AnoModelo})"
                })
                .ToList();

            var caracteristicas = _caracteristicaAppService.ObterTodos()
                .Select(c => new SelectListItem
                {
                    Value = c.IdCaracteristica.ToString(),
                    Text = c.Descricao
                })
                .ToList();

            ViewBag.Veiculos = veiculos;
            ViewBag.Caracteristicas = caracteristicas;
        }

        private bool IsAjaxRequest()
        {
            return string.Equals(
                Request.Headers["X-Requested-With"].ToString(),
                "XMLHttpRequest",
                StringComparison.OrdinalIgnoreCase);
        }
    }
}
