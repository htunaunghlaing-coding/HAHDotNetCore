using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HAHDotNetCore.MvcChartApp.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HAHDotNetCore.MvcChartApp.Controllers;

public class ApexChartController : Controller
{

    public IActionResult SimplePieChart()
    {
        PieChartModel pieChart = new PieChartModel();
        pieChart.Labels = new List<string>() { "Team A", "Team B", "Team C", "Team D", "Team E" };
        pieChart.Series = new List<int>() { 44, 55, 13, 43, 22 };
        return View(pieChart);
    }
}

