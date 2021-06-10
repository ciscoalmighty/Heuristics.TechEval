using Heuristics.TechEval.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace Heuristics.TechEval.Web.Models {

	/// <summary>
	/// DTO representing the creation of a new Member
	/// </summary>
	public class NewMember {

		[Required(ErrorMessage = "Name is required")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Field can't be empty")]
		[DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
		public string Email { get; set; }

		public Category catergoy { get; set;  }
	}
}