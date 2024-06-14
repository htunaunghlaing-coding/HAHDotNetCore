using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HAHDotNetCore.RestApi.Controllers;

[Route("api/[controller]")]
public class BlogController : Controller
{
    [HttpGet]
    public IActionResult Read()
    {
        return Ok("Read");
    }

    [HttpPost]
    public IActionResult Create()
    {
        return Ok("Create");
    }

    [HttpPut]
    public IActionResult Update()
    {
        return Ok("Update");
    }

    [HttpPatch]
    public IActionResult Patch()
    {
        return Ok("Patch");
    }

    [HttpDelete]
    public IActionResult Delete()
    {
        return Ok("Delete");
    }
}

