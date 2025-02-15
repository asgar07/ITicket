﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Entities;

namespace DomainLayer.Configurations
{
	public class EventConfiguration : IEntityTypeConfiguration<Event>
	{
		public void Configure(EntityTypeBuilder<Event> builder)
		{
			builder.Property(m => m.Name).IsRequired();
			builder.Property(m => m.BackImage).IsRequired();
			builder.Property(m => m.Image).IsRequired();
			builder.Property(m => m.DetailImage).IsRequired();
			builder.Property(m => m.Date).IsRequired();
			builder.Property(m => m.Price).IsRequired();
			builder.Property(m => m.CategoryId).IsRequired();
			builder.Property(m => m.HallId).IsRequired();


		}
	}
}
