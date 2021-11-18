using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RDLCReportingV2019.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RDLCReportingV2019.Data
{
	public class ApplicationDbContext : IdentityDbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}
		public DbSet<Employee> employees { get; set; }
	}
}
