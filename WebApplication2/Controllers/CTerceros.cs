using Microsoft.AspNetCore.Mvc;
using WebApplication2.Repo;

namespace WebApplication2.Controllers
{
	public class CTerceros : Controller
	{
		RTerceros _repoTerceros = new RTerceros();
		public IActionResult GetTerceros()
		{
			var ListaPastores = _repoTerceros.listar20();
			return View(ListaPastores);
		}
		public IActionResult GetTercerosTodos()
		{
			return View("GetTerceros", _repoTerceros.listarTodos());
		}


		//[HttpGet]
		//[HttpPost]
		//public IActionResult FiltroPorCodTer(string codTer)
		//{
		//	return View(_repoTerceros.FiltroId(codTer));
		//}
	}
}
