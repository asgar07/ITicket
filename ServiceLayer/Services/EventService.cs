﻿using AutoMapper;
using DomainLayer.Entities;
using RepositoryLayer.Repositories.Interfaces;
using ServiceLayer.DTOs.Event;
using ServiceLayer.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
	public class EventService : IEventService
	{
		private readonly IEventRepository _repository;
		private readonly IMapper _mapper;
		public EventService(IEventRepository repository, IMapper mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}
		public async Task CreateAsync(EventCreateDto eventCreateDto)
		{
			var model = _mapper.Map<Event>(eventCreateDto);
			await _repository.CreateAsync(model);
		}

		public async Task DeleteAsync(int id)
		{
			var levent = await _repository.GetAsync(id);
			await _repository.DeleteAsync(levent);
		}

		public async Task<List<EventDto>> GetAllAsync()
		{
			var model = await _repository.GetAllAsync();
			var res = _mapper.Map<List<EventDto>>(model);
			return res;
		}

		public async Task<EventDto> GetByIdAsync(int id)
		{
			var model = await _repository.GetEventAsync(id);
			var res = _mapper.Map<EventDto>(model);
			return res;
		}

		public async Task UpdateAsync(int id, EventEditDto levent)
		{
			var entity = await _repository.GetEventAsync(id);

			_mapper.Map(levent, entity);

			await _repository.UpdateAsync(entity);
		}

		public async Task<EventDto> GetAsync(int id)
		{
			var model = await _repository.GetAsync(id);
			var res = _mapper.Map<EventDto>(model);
			return res;
		}

		public async Task<List<EventDto>> GetByCateId(int id)
		{
			var model = await _repository.FindAllAsync(m => m.CategoryId == id);
			var res = _mapper.Map<List<EventDto>>(model);
			return res;
		}

		public async Task<IEnumerable<EventDto>> GetAllNameAsync(string search)
		{
			return _mapper.Map<IEnumerable<EventDto>>(await _repository.FindAllAsync(m => m.Name.Contains(search)));
		}

		public async Task<EventPaginateDto> Paginate(int num)
		{
			int skipdata = num * 10;
			var model = await _repository.GetAllAsync();
			IEnumerable<Event> enumerable = model as IEnumerable<Event>;
			var paginate = enumerable.Skip(skipdata).Take(10);
			var res = _mapper.Map<IEnumerable<EventDto>>(paginate);
			EventPaginateDto eventPaginateDto = new EventPaginateDto();
			eventPaginateDto.Length = model.Count();
			eventPaginateDto.EventDtos = res;
			return eventPaginateDto;
		}
	}
}
