﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.DTOs.Seans;
using ServiceLayer.Services.Interfaces;
using System.Data;

namespace Api.Controllers
{
	public class SeansController : BaseController
	{
		private readonly ISeansService _service;
		public SeansController(ISeansService service)
		{
			_service = service;
		}

		[HttpPost]
		[Route("CreateSeans")]
		[Authorize(Roles = "SuperAdmin , Admin")]
		public async Task<IActionResult> Create([FromBody] SeansDto seansDto)
		{
			await _service.CreateAsync(seansDto);
			return Ok();
		}
		[HttpDelete]
		[Route("DeleteSeans/{id}")]
		[Authorize(Roles = "SuperAdmin , Admin")]
		public async Task<IActionResult> Delete([FromRoute] int id)
		{
			await _service.DeleteAsync(id);
			return Ok();
		}
		[HttpPut]
		[Route("UpdateSeans/{id}")]
		[Authorize(Roles = "SuperAdmin , Admin")]
		public async Task<IActionResult> Update([FromRoute] int id, [FromBody] SeansEditDto seans)
		{


			await _service.UpdateAsync(id, seans);
			return Ok();
		}

		[HttpGet]
		[Route("GetById/{id}")]
		public async Task<IActionResult> GetById([FromRoute] int id)
		{
			var result = await _service.GetAsync(id);
			return Ok(result);
		}

		[HttpGet]
		[Route("GetAllSeans")]
		public async Task<IActionResult> GetAll()
		{
			var result = await _service.GetAllAsync();
			return Ok(result);
		}
	}
}
