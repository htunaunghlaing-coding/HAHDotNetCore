using Newtonsoft.Json;

Console.WriteLine("Hello, World!");

string jsonString = await File.ReadAllTextAsync("data.json");
var model = JsonConvert.DeserializeObject<MainDto>(jsonString);

Console.WriteLine(jsonString);

Console.ReadLine();

public class MainDto
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






