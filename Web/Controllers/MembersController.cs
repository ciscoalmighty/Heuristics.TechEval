﻿using System.Linq;
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

		public ActionResult List(string sortOrder) {
			ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name" : "";
			ViewBag.IdSortParm = sortOrder == "Id" ? "id_desc" : "Id";
			ViewBag.EmailSortParm = sortOrder == "Email" ? "id_desc" : "Id";
			var allMembers = from s in _context.Members
							 select s;
			switch (sortOrder)
			{
				case "Name":
					allMembers = allMembers.OrderByDescending(s => s.Name);
					break;
				case "Id":
					allMembers = allMembers.OrderBy(s => s.Id);
					break;
				case "date_desc":
					allMembers = allMembers.OrderBy(s => s.Email);
					break;
				default:
					allMembers = allMembers.OrderBy(s => s.Name);
					break;
			}
			return View(allMembers.ToList());

		}

		[HttpPost]
		public ActionResult New(NewMember data) {
			if (ModelState.IsValid)
			{
				//checks to see if Email is used
				if (CheckMemberEmail(data.Email) == false)
				{
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
			Member member = _context.Members.FirstOrDefault(x => x.Id == id);
			if (member != null)
			{
				return View(member);
			}
			return HttpNotFound();
		}

		[HttpPost]
		public ActionResult Edit(EditMember member)
        {
			if (ModelState.IsValid)
            {
				var editedMember = _context.Members.Find(member.Id);
				editedMember.Name = member.Name;
				editedMember.Email = member.Email;
				_context.SaveChanges();
				return Json(JsonConvert.SerializeObject(editedMember));
			}
			return View(member);
			
        }

		public Member GetMember (int Id)
        {
			Member member = _context.Members.FirstOrDefault(x => x.Id == Id);
			return member;
        }
	}
}