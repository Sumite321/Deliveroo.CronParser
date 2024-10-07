// See https://aka.ms/new-console-template for more information

using Deliveroo.CronParser;

// Path to the text file containing cron expressions
string filePath = "./cron_expressions.txt";

// Ensure the file exists before proceeding
if (!File.Exists(filePath))
{
    Console.WriteLine("File not found: " + filePath);
    return;
}

// Read all lines from the file
string[] cronExpressions = File.ReadAllLines(filePath);

foreach (var cronExpression in cronExpressions)
{
    try
    {
        // Parse and display the output for each cron expression
        string result = CronParser.Parse(cronExpression);
        Console.WriteLine($"Parsed cron expression: {cronExpression}");
        Console.WriteLine(result);
        Console.WriteLine(new string('-', 50)); // Just a separator
    }
    catch (Exception ex)
    {
        // Handle any exceptions thrown by the parser
        Console.WriteLine($"Error parsing cron expression: {cronExpression}");
        Console.WriteLine($"Error: {ex.Message}");
        Console.WriteLine(new string('-', 50));
    }
}
