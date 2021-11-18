using AspNetCore.Reporting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RDLCReportingV2019.Data;
using RDLCReportingV2019.Data.Entity;
using RDLCReportingV2019.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace RDLCReportingV2019.Controllers
{
	public class HomeController : Controller
	{
		private readonly IWebHostEnvironment webHostEnvironment;
		private readonly ApplicationDbContext _context;

		public HomeController(IWebHostEnvironment webHostEnvironment, ApplicationDbContext context)
		{
			this.webHostEnvironment = webHostEnvironment;
			_context = context;
		}

		public async Task<IActionResult> Index()
		{
			//for (int i = 1; i <= 100; i++)
			//{
			//	var data = new Employee
			//	{
			//		name = "Mr. X" + i,
			//		email = "mr.x" + i + "@gmail.com",
			//		phone = "01711242" + (101 + i).ToString()
			//	};
			//	_context.employees.Add(data);
			//	await _context.SaveChangesAsync();
			//}
			var employees = await _context.employees.ToListAsync();

			return View(employees);
		}

		public async Task<IActionResult> ExportToPdf()
		{
			var employees = await _context.employees.ToListAsync();
			string mimeType = "";
			int extension = 1;
			var path = $"{this.webHostEnvironment.WebRootPath}\\Report\\EmployeeReport.rdlc";
			Dictionary<string, string> parameters = new Dictionary<string, string>();
			//parameters.Add("prm", "RDLC Report");
			LocalReport localReport = new LocalReport(path);
			localReport.AddDataSource("DataSet1", employees);

			var result = localReport.Execute(RenderType.Pdf, extension, parameters, mimeType);

			return File(result.MainStream, "application/pdf");
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
