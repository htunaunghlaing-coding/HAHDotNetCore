using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HAHDotNetCore.RestApiWithNLayer;

[Route("api/[controller]")]
public class LatHtaukBayDinController : Controller
{
    private async Task<LatHtaukBayDin> GetDataAsync()
    {
        string jsonString = await System.IO.File.ReadAllTextAsync("data.json");
        var model = JsonConvert.DeserializeObject<LatHtaukBayDin>(jsonString);
        return model;
    }

    [HttpGet("questions")]
    public async Task<IActionResult> Questions()
    {
        var model = await GetDataAsync();
        return Ok(model.questions);
    }

    [HttpGet("numberlist")]
    public async Task<IActionResult> NumberList()
    {
        var model = await GetDataAsync();
        return Ok(model.numberList);
    }

    [HttpGet("{questionNo}/{no}")]
    public async Task<IActionResult> QuestionAnswer(int questionNo, int no)
    {
        var model = await GetDataAsync();
        return Ok(model.answers.FirstOrDefault(x => x.questionNo == questionNo && x.answerNo == no));
    }

    public class LatHtaukBayDin
    {
        public List<Question> questions { get; set; }
        public List<Answer> answers { get; set; }
        public List<string> numberList { get; set; }
    }

    public class Question
    {
        public int questionNo { get; set; }
        public string questionName { get; set; }
    }

    public class Answer
    {
        public int questionNo { get; set; }
        public int answerNo { get; set; }
        public string answerResult { get; set; }
    }
}

