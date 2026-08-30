using System;
using TaskFlow.Domain;


var task = new TaskItem("Fix login bug", 1);

var user = new User("Ramees Pk", "newemail@example.com");

var project = new Project("New Project", 1);

task.AssignTo(5);
task.UpdateTitle("User Name Miss match");
task.SetDueDate(DateTime.UtcNow.AddDays(7));
task.ChangeStatus(TaskItemStatus.Done);
task.ReOpenTask();



Console.WriteLine($"Id: {task.AssigneeId}");
Console.WriteLine($"Task: {task.Title}");
Console.WriteLine($"Due Date: {task.DueDate}");
Console.WriteLine($"Status: {task.Status}");
Console.WriteLine($"New User: {user.Name} | {user.Email}");
Console.WriteLine($"New Project Name: {project.Name} |  {project.OwnerId}");







string Reverse(string input)
{
    string result = "";

    for (int i = input.Length - 1; i >= 0; i--)
    {

        result = result + input[i];

    }

    return result;
}

Console.WriteLine(Reverse("hello"));





List<int> GetEvenNumbers(List<int> numbers)
{
     List<int> newEvenArray = new List<int>();



    for (int i = 0; i < numbers.Count; i++)
    {
        if (numbers[i] % 2 == 0)
        {
            newEvenArray.Add(numbers[i]);

        }

    }

    return newEvenArray;

}


var evens = GetEvenNumbers(new List<int> { 1,  3, 4, 5, 6 });
Console.WriteLine(string.Join(", ", evens));



List<int> getOddNumbers(List<int> numbers)
{

    List<int> oddNumbers = new List<int>();


    foreach (int number in numbers)
    {
        if(number % 2 != 0)
        {
            oddNumbers.Add(number);
        }

    }

    return oddNumbers;

}

var odd = getOddNumbers(new List<int> { 1, 2, 3, 4, 5 });
Console.WriteLine(string.Join(",", odd));



int GetVowels(string input)
{
    string vowels = "aeiou";
    int count = 0;

    input = input.ToLower();

    foreach(char letter in input)
    {
        if( vowels.Contains(letter))
        {
            count = count + 1;
        }
    }

    return count;   
    
}

Console.WriteLine(GetVowels("RAmeedfwevsdiohuyudscs"));
Console.WriteLine(GetVowels("Ineed"));
Console.WriteLine("-----------------------");

Dictionary<string, int> CountWords(string sentence)
{
    Dictionary<string, int> counts = new Dictionary<string, int>();

        string[] words = sentence.Split(' ');


    foreach (string word in words)
    {
        if (counts.ContainsKey(word))
        {
            counts[word] = counts[word] + 1;  

        }else
        {
            counts[word] = 1;
        }



    }

    return counts;

}

var result = CountWords("the cat sat on the mat mat the end");

foreach(var word in result)
{
    Console.WriteLine($"{word.Value} - {word.Key}");

}






