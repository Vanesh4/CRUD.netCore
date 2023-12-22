using Microsoft.AspNetCore.Mvc;
using WebApplication2.Repo;

namespace WebApplication2.Controllers
{
	public class CDistritos : Controller
	{
		RDistritos _repoDistritos = new RDistritos();
		public IActionResult GetDistritos()
		{
			return View(_repoDistritos.listarTodos());
		}
	}
}
