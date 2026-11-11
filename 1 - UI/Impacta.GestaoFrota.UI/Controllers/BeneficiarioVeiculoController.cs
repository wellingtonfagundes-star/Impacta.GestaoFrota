using Impacta.GestaoFrota.Application.Interfaces;
using Impacta.GestaoFrota.Application.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Impacta.GestaoFrota.UI.Controllers;

public class BeneficiarioVeiculoController : Controller
{
    private readonly IVeiculoBeneficioAppService _appService;

    public BeneficiarioVeiculoController(IVeiculoBeneficioAppService appService)
    {
        _appService = appService;
    }

    public IActionResult Index(int page = 1, int pageSize = 10, string search = "")
    {
        page = Math.Max(page, 1);
        pageSize = pageSize is 5 or 10 or 25 or 50 ? pageSize : 10;

        var beneficios = _appService.ObterTodos().ToList();
        if (!string.IsNullOrWhiteSpace(search))
        {
            beneficios = beneficios.Where(item =>
                    item.VeiculoDescricao.Contains(search, StringComparison.CurrentCultureIgnoreCase)
                    || item.BeneficiadoNome.Contains(search, StringComparison.CurrentCultureIgnoreCase)
                    || item.MotoristaNome.Contains(search, StringComparison.CurrentCultureIgnoreCase))
                .ToList();
        }

        var totalPaginas = Math.Max((int)Math.Ceiling(beneficios.Count / (double)pageSize), 1);
        page = Math.Min(page, totalPaginas);
        ViewBag.Page = page;
        ViewBag.PageSize = pageSize;
        ViewBag.TotalRegistros = beneficios.Count;
        ViewBag.TotalPaginas = totalPaginas;
        ViewBag.Search = search;

        return View(beneficios.Skip((page - 1) * pageSize).Take(pageSize));
    }

    [HttpGet]
    public IActionResult Create()
    {
        var viewModel = new VeiculoBeneficioViewModel();
        PopularListas(viewModel);
        return IsAjaxRequest() ? PartialView("_CreateModal", viewModel) : View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(VeiculoBeneficioViewModel viewModel)
    {
        ValidarSelecoes(viewModel);
        if (!ModelState.IsValid)
            return RetornarFormulario(viewModel, "_CreateModal", "Create");

        if (_appService.Adicionar(viewModel))
        {
            if (IsAjaxRequest())
                return Json(new { success = true, message = "Benefício de veículo cadastrado com sucesso." });

            TempData["SuccessMessage"] = "Benefício de veículo cadastrado com sucesso.";
            return RedirectToAction(nameof(Index));
        }

        ModelState.AddModelError(string.Empty, "Não foi possível cadastrar o benefício. Verifique as opções selecionadas.");
        return RetornarFormulario(viewModel, "_CreateModal", "Create");
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var viewModel = _appService.ObterPorId(id);
        if (viewModel is null)
            return NotFound();

        PopularListas(viewModel);
        return IsAjaxRequest() ? PartialView("_EditModal", viewModel) : View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, VeiculoBeneficioViewModel viewModel)
    {
        if (id != viewModel.IdBeneficio)
            return BadRequest();

        ValidarSelecoes(viewModel);
        if (!ModelState.IsValid)
            return RetornarFormulario(viewModel, "_EditModal", "Edit");

        if (_appService.Atualizar(viewModel))
        {
            if (IsAjaxRequest())
                return Json(new { success = true, message = "Benefício de veículo atualizado com sucesso." });

            TempData["SuccessMessage"] = "Benefício de veículo atualizado com sucesso.";
            return RedirectToAction(nameof(Index));
        }

        ModelState.AddModelError(string.Empty, "Não foi possível atualizar o benefício. Verifique as opções selecionadas.");
        return RetornarFormulario(viewModel, "_EditModal", "Edit");
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        var viewModel = _appService.ObterPorId(id);
        if (viewModel is null)
            return NotFound();

        return IsAjaxRequest() ? PartialView("_DeleteModal", viewModel) : View(viewModel);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        if (_appService.Remover(id))
        {
            if (IsAjaxRequest())
                return Json(new { success = true, message = "Benefício de veículo excluído com sucesso." });

            TempData["SuccessMessage"] = "Benefício de veículo excluído com sucesso.";
        }
        else
        {
            if (IsAjaxRequest())
                return Json(new { success = false, message = "Não foi possível excluir o benefício de veículo." });

            TempData["ErrorMessage"] = "Não foi possível excluir o benefício de veículo.";
        }

        return RedirectToAction(nameof(Index));
    }

    private IActionResult RetornarFormulario(VeiculoBeneficioViewModel viewModel, string partialName, string viewName)
    {
        if (IsAjaxRequest())
        {
            var mensagem = string.Join(" ", ModelState.Values
                .SelectMany(value => value.Errors)
                .Select(error => error.ErrorMessage));
            return Json(new { success = false, message = mensagem });
        }

        PopularListas(viewModel);
        return View(viewName, viewModel);
    }

    private void PopularListas(VeiculoBeneficioViewModel viewModel)
    {
        var pessoas = _appService.ObterPessoas().ToList();
        ViewBag.Pessoas = pessoas;
        ViewBag.Veiculos = _appService.ObterVeiculos()
            .Select(veiculo => new SelectListItem
            {
                Value = veiculo.IdVeiculo.ToString(),
                Text = veiculo.Descricao
            })
            .ToList();

        if (string.IsNullOrWhiteSpace(viewModel.BeneficiadoSelecao))
            viewModel.BeneficiadoSelecao = pessoas.SingleOrDefault(pessoa => pessoa.IdPessoa == viewModel.IdBeneficiado)?.Rotulo ?? string.Empty;
        if (string.IsNullOrWhiteSpace(viewModel.MotoristaSelecao))
            viewModel.MotoristaSelecao = pessoas.SingleOrDefault(pessoa => pessoa.IdPessoa == viewModel.IdMotorista)?.Rotulo ?? string.Empty;
    }

    private void ValidarSelecoes(VeiculoBeneficioViewModel viewModel)
    {
        var pessoas = _appService.ObterPessoas().ToDictionary(pessoa => pessoa.IdPessoa);
        if (!pessoas.TryGetValue(viewModel.IdBeneficiado, out var beneficiado)
            || !string.Equals(beneficiado.Rotulo, viewModel.BeneficiadoSelecao, StringComparison.Ordinal))
        {
            ModelState.AddModelError(nameof(viewModel.IdBeneficiado), "Selecione um beneficiado na lista.");
        }

        if (!pessoas.TryGetValue(viewModel.IdMotorista, out var motorista)
            || !string.Equals(motorista.Rotulo, viewModel.MotoristaSelecao, StringComparison.Ordinal))
        {
            ModelState.AddModelError(nameof(viewModel.IdMotorista), "Selecione um motorista na lista.");
        }
    }

    private bool IsAjaxRequest()
    {
        return string.Equals(Request.Headers["X-Requested-With"].ToString(), "XMLHttpRequest", StringComparison.OrdinalIgnoreCase);
    }
}
