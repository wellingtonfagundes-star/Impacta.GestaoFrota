using Impacta.GestaoFrota.Application.Interfaces;
using Impacta.GestaoFrota.Application.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Impacta.GestaoFrota.UI.Controllers
{
    public class VeiculoController : Controller
    {
        private readonly IVeiculoAppService _veiculoAppService;
        private readonly IStatusFrotaAppService _statusFrotaService;
        public VeiculoController(IVeiculoAppService veiculoAppService,IStatusFrotaAppService statusFrotaService)
        {
            _veiculoAppService = veiculoAppService;
            _statusFrotaService = statusFrotaService;
        }

        List<StatusFrotaViewModel> ListaStatusFrota = new List<StatusFrotaViewModel>();
        public IActionResult Index(int page = 1, int pageSize = 10, string search = "")
        {
            page = Math.Max(page, 1);
            pageSize = pageSize is 5 or 10 or 25 or 50 ? pageSize : 10;

            var todos = _veiculoAppService.ObterTodos().ToList();
            ListaStatusFrota = _statusFrotaService.ObterTodos().ToList();

            // Aplicar filtro de pesquisa
            if (!string.IsNullOrWhiteSpace(search))
            {
                todos = todos
                    .Where(v => v.Placa.Contains(search, StringComparison.CurrentCultureIgnoreCase) ||
                                v.Fabricante.Contains(search, StringComparison.CurrentCultureIgnoreCase) ||
                                v.NrIdentificacao.Contains(search, StringComparison.CurrentCultureIgnoreCase))
                    .ToList();
            }

            var totalPaginas = Math.Max((int)Math.Ceiling(todos.Count / (double)pageSize), 1);
            page = Math.Min(page, totalPaginas);

            ViewBag.Page = page;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalRegistros = todos.Count;
            ViewBag.TotalPaginas = totalPaginas;
            ViewBag.Search = search;
            //ViewBag.StatusFrota = statusFrota.ToList();

            return View(todos.Skip((page - 1) * pageSize).Take(pageSize));

        }

        [HttpGet]
        public IActionResult Create()
        {
            PopularDropdowns();

            if (IsAjaxRequest())
                return PartialView("_CreateModal", new VeiculoViewModel());

            return View(new VeiculoViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(VeiculoViewModel viewModel)
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

            if (_veiculoAppService.Adicionar(viewModel))
            {
                if (IsAjaxRequest())
                    return Json(new { success = true, message = "Veículo cadastrado com sucesso." });

                TempData["SuccessMessage"] = "Veículo cadastrado com sucesso.";
                return RedirectToAction(nameof(Index));
            }

            var errorMsg = "Não foi possível cadastrar o veículo.";
            if (IsAjaxRequest())
                return Json(new { success = false, message = errorMsg });

            PopularDropdowns();
            ModelState.AddModelError(string.Empty, errorMsg);
            return IsAjaxRequest()
                ? PartialView("_CreateModal", viewModel)
                : View(viewModel);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var viewModel = _veiculoAppService.ObterPorId(id);
            if (viewModel is null)
                return NotFound();

            PopularDropdowns();

            if (IsAjaxRequest())
                return PartialView("_EditModal", viewModel);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, VeiculoViewModel viewModel)
        {
            if (id != viewModel.IdVeiculo)
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

            if (_veiculoAppService.Atualizar(viewModel))
            {
                if (IsAjaxRequest())
                    return Json(new { success = true, message = "Veículo atualizado com sucesso." });

                TempData["SuccessMessage"] = "Veículo atualizado com sucesso.";
                return RedirectToAction(nameof(Index));
            }

            var errorMsg = "Não foi possível atualizar o veículo.";
            if (IsAjaxRequest())
                return Json(new { success = false, message = errorMsg });

            PopularDropdowns();
            ModelState.AddModelError(string.Empty, errorMsg);
            return IsAjaxRequest()
                ? PartialView("_EditModal", viewModel)
                : View(viewModel);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var viewModel = _veiculoAppService.ObterPorId(id);
            if (viewModel is null)
                return NotFound();

            if (IsAjaxRequest())
                return PartialView("_DeleteModal", viewModel);

            return View(viewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (_veiculoAppService.Remover(id))
            {
                if (IsAjaxRequest())
                    return Json(new { success = true, message = "Veículo excluído com sucesso." });

                TempData["SuccessMessage"] = "Veículo removido com sucesso.";
            }
            else
            {
                if (IsAjaxRequest())
                    return Json(new { success = false, message = "Não foi possível excluir o veículo." });

                TempData["ErrorMessage"] = "Não foi possível remover o veículo.";
            }

            return RedirectToAction(nameof(Index));
        }

        private bool IsAjaxRequest()
        {
            return string.Equals(
                Request.Headers["X-Requested-With"].ToString(),
                "XMLHttpRequest",
                StringComparison.OrdinalIgnoreCase);
        }

        private void PopularDropdowns()
        {
            var statusFrota = _statusFrotaService.ObterTodos().ToList();
            ViewBag.StatusFrota = statusFrota;
        }
    }
}
