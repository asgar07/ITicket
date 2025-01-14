﻿using ServiceLayer.DTOs.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.Interfaces
{
	public interface ICategoryService
	{
		Task CreateAsync(CategoryDto categoryDto);

		Task UpdateAsync(CategoryEditDto category);
		Task DeleteAsync(int id);
		Task<List<CategoryDto>> GetAllAsync();
		Task<CategoryDto> GetAsync(int id);
	}
}
