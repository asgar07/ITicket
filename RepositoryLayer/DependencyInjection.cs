using Microsoft.Extensions.DependencyInjection;
using RepositoryLayer.Repositories.Interfaces;
using RepositoryLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddRepositoryLayer(this IServiceCollection services)
		{
			services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
			services.AddScoped<ISliderRepository, SliderRepository>();
			services.AddScoped<IHallRepository, HallRepository>();
			services.AddScoped<ICategoryRepository, CategoryRepository>();
			services.AddScoped<IEventRepository, EventRepository>();
			services.AddScoped<ISeansRepository, SeansRepository>();
			services.AddScoped<IOrderRepository, OrderRepository>();
			services.AddScoped<IUserRepository, UserRepository>();

			return services;
		}
	}
}
