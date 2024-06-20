using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HAHDotNetCore.RestApiWithNLayer;

[Route("api/[controller]")]
public class MyanmarProverbsController : Controller
{
    private async Task<Tbl_MMproverbs> GetDataFromApi()
    {
        //HttpClient client = new HttpClient();
        //var response = await client.GetAsync("https://raw.githubusercontent.com/sannlynnhtun-coding/Myanmar-Proverbs/main/MyanmarProverbs.json");
        //if (response.IsSuccessStatusCode)
        //{
        //    string jsonStr = await response.Content.ReadAsStringAsync();
        //    var model = JsonConvert.DeserializeObject<Tbl_MMproverbs>(jsonStr);
        //    return model;
        //}
        //return null;

        var jsonStr = await System.IO.File.ReadAllTextAsync("MyanmarProverbs.json");
        var model = JsonConvert.DeserializeObject<Tbl_MMproverbs>(jsonStr);
        return model;
    }

    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        var model = await GetDataFromApi();
        if (model == null || model.Tbl_MMProverbsTitle == null)
        {
            return NotFound("No proverbs found.");
        }
        return Ok(model.Tbl_MMProverbsTitle);
    }

    [HttpGet("{titleName}")]
    public async Task<IActionResult> GetAsync(string titleName)
    {
        var model = await GetDataFromApi();
        var item = model.Tbl_MMProverbsTitle.FirstOrDefault(x => x.TitleName == titleName);
        if (item == null)
        {
            return NotFound();
        }

        var titleId = item.TitleId;
        var lst = model.Tbl_MMProverbs.Where(x => x.TitleId == titleId);
        return Ok(lst);
    }

    [HttpGet("{titleId}/{proverbsId}")]
    public async Task<IActionResult> GetAsync(int titleId, int proverbsId)
    {
        var model = await GetDataFromApi();
        var item = model.Tbl_MMProverbs.FirstOrDefault(x => x.TitleId == titleId && x.ProverbId == proverbsId);
        return Ok(item);
    }
}

public class Tbl_MMproverbs
{
    public TblMMProverbsTitle[] Tbl_MMProverbsTitle { get; set; }
    public TblMMProverbDetails[] Tbl_MMProverbs { get; set; }
}

public class TblMMProverbDetails
{
    public int TitleId { get; set; }
    public int ProverbId { get; set; }
    public string ProverbName { get; set; }
    public string ProverbDesp { get; set; }
}

public class TblMMProverbsTitle
{
    public int TitleId { get; set; }
    public string TitleName { get; set; }
}
