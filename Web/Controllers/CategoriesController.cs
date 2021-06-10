using System.Linq;
using System.Web.Mvc;
using Heuristics.TechEval.Core;
using Heuristics.TechEval.Web.Models;
using Heuristics.TechEval.Core.Models;
using Newtonsoft.Json;
using System.Data;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace Heuristics.TechEval.Web.Controllers {

	public class CategoriesController : Controller {

		private readonly DataContext _context;

		public CategoriesController() {
			_context = new DataContext();
		}

		public ActionResult List() {
			//var count = 0;
			var categories = _context.Categories.ToList();
			//var allMembers = _context.Members.ToList();
			//foreach (Category category in categories)
   //         {
			//	foreach (Member member in allMembers)
			//	{
			//		if (member.category == category)
			//		{
			//			category.count++;
			//		}
			//	}
			//}
			return View(categories);
		}

	}
}