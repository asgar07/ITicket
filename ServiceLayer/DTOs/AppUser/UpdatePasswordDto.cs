﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.DTOs.AppUser
{
	public class UpdatePasswordDto
	{


		public string CurrentPassword { get; set; }
		public string NewPassword { get; set; }
	}
}
