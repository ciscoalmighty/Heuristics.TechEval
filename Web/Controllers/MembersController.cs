using System.Linq;
using System.Web.Mvc;
using Heuristics.TechEval.Core;
using Heuristics.TechEval.Web.Models;
using Heuristics.TechEval.Core.Models;
using Newtonsoft.Json;
using System.Data;
using System;
using System.Collections.Generic;
using System.Net;

namespace Heuristics.TechEval.Web.Controllers {

	public class MembersController : Controller {

		private readonly DataContext _context;

		public MembersController() {
			_context = new DataContext();
		}

		public ActionResult List() {
			var allMembers = _context.Members.ToList();

			return View(allMembers);
		}

		[HttpPost]
		public ActionResult New(NewMember data) {
			if (ModelState.IsValid)
			{
				//checks to see if Email is used
				if (CheckMemberEmail(data.Email) == false)
				{
					ModelState.AddModelError("ExisitingMemberError", "A member with that email already exists.");
					return View();
				};
				var newMember = new Member
				{
					Name = data.Name,
					Email = data.Email
				};
				
				_context.Members.Add(newMember);
				_context.SaveChanges();

				return Json(JsonConvert.SerializeObject(newMember));
			}

			return View(data);
		}

		private bool CheckMemberEmail(string email)
        {
			var members = _context.Members.ToList();
			foreach (Member member in members)
			{
				if(email == member.Email)
                {
					return false;
                }
            }
			return true;
        }

		[HttpGet]
		public ActionResult Edit(int id)
		{
			if (id == 0)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			var member = _context.Members.Find(id);
			if (member == null)
			{
				return HttpNotFound();
			}
			Response.RedirectToRoutePermanent("~/MembersController/List");
		}

		[HttpPost]
		public Member Edit(Member member)
        {
			var editedMember = _context.Members.Find(member);
			_context.Entry(editedMember).CurrentValues.SetValues(member);
			_context.SaveChanges();
			return editedMember;
			Response.Redirect("~/MembersController/List");


        }
	}
}