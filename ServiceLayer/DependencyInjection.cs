﻿using Microsoft.Extensions.DependencyInjection;
using ServiceLayer.Services.Interfaces;
using ServiceLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
	public static class DependecyInjection
	{
		public static IServiceCollection AddServiceLayer(this IServiceCollection services)
		{

			services.AddScoped<ISliderService, SliderService>();
			services.AddScoped<IHallService, HallService>();
			services.AddScoped<ICategoryService, CategoryService>();
			services.AddScoped<IEventService, EventService>();
			services.AddScoped<ISeansService, SeansService>();
			services.AddScoped<IAccountService, AccountService>();
			services.AddScoped<IEmailService, EmailService>();
			services.AddScoped<IOrderService, OrderService>();
			services.AddHttpContextAccessor();


			return services;
		}
	}
}
