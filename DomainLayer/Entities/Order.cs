using DomainLayer.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Entities
{
	public class Order : BaseEntity
	{
		public Event Event { get; set; }
		public int EventId { get; set; }
		public string SeatId { get; set; }
		public DateTime Date { get; set; }

	}
}
