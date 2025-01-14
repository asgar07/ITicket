﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.DTOs.Event;
using ServiceLayer.Services.Interfaces;
using System.Data;

namespace Api.Controllers
{

	public class EventController : BaseController
	{
		private readonly IEventService _service;
		public EventController(IEventService service)
		{
			_service = service;
		}

		[HttpPost]
		[Route("CreateEvent")]
		[Authorize(Roles = "SuperAdmin , Admin")]
		public async Task<IActionResult> Create([FromBody] EventCreateDto eventCreateDto)
		{
			await _service.CreateAsync(eventCreateDto);
			return Ok();
		}
		[HttpDelete]
		[Route("DeleteEvent/{id}")]
		[Authorize(Roles = "SuperAdmin , Admin")]
		public async Task<IActionResult> Delete([FromRoute] int id)
		{
			await _service.DeleteAsync(id);
			return Ok();
		}
		[HttpPut]
		[Route("UpdateEvent/{id}")]
		[Authorize(Roles = "SuperAdmin , Admin")]
		public async Task<IActionResult> Update([FromRoute] int id, [FromBody] EventEditDto levent)
		{


			await _service.UpdateAsync(id, levent);
			return Ok();
		}

		[HttpGet]
		[Route("GetAllEvents")]
		public async Task<IActionResult> GetAll()
		{
			var result = await _service.GetAllAsync();
			return Ok(result);
		}

		[HttpGet]
		[Route("GetById/{id}")]

		public async Task<IActionResult> GetById([FromRoute] int id)
		{
			var result = await _service.GetByIdAsync(id);
			return Ok(result);
		}

		[HttpGet]
		[Route("GetByCateId/{id}")]
		public async Task<IActionResult> GetByCateId([FromRoute] int id)
		{
			var result = await _service.GetByCateId(id);
			return Ok(result);
		}
		[HttpGet]
		[Route("GetAllByName/{txt}")]
		public async Task<IActionResult> GetAllByName([FromRoute] string txt)
		{
			return Ok(await _service.GetAllNameAsync(txt));
		}

		[HttpGet]
		[Route("Paginate/{num}")]
		public async Task<IActionResult> Paginate([FromRoute] int num)
		{
			return Ok(await _service.Paginate(num));
		}
	}
}
