namespace Deliveroo.CronParser;

public static class CronParser
{
    public static string Parse(string cronExpression)
    {
        string[] parts = cronExpression.Split(' ');
        if (parts.Length < 6)
        {
            throw new ArgumentException("Cron expression must have 5 fields plus the command.");
        }

        string minute = ParsePart(parts[0], 0, 59);
        string hour = ParsePart(parts[1], 0, 23);
        string dayOfMonth = ParsePart(parts[2], 1, 31);
        string month = ParsePart(parts[3], 1, 12);
        string dayOfWeek = ParsePart(parts[4], 1, 7);
        string command = parts[5];

        return FormatOutput(minute, hour, dayOfMonth, month, dayOfWeek, command);
    }

    private static string ParsePart(string part, int min, int max)
    {
        // Validate input before parsing
        ValidatePart(part);
        
        return part switch
        {
            "*" => BuildRange(min, max), // handle wildcard
            _ when part.Contains(",") => string.Join(" ", part.Split(',').Select(p => p.Trim())), // handle specific values
            _ when part.Contains("-") => HandleRange(part, min, max), // handle range
            _ when part.Contains("/") => HandleInterval(part, min, max), // handle intervals
            _ => HandleSingleValue(part, min, max) // return specific value
        };
    }


    private static string HandleRange(string part, int min, int max)
    {
        var range = part.Split('-');
        int start = int.Parse(range[0]);
        int end = int.Parse(range[1]);

        if (start < 0 || end < 0)
        {
            throw new ArgumentOutOfRangeException("Range values must be within valid bounds and non-negative.");
        }

        return BuildRange(start, end);
    }

    private static string HandleInterval(string part, int min, int max)
    {
        var split = part.Split('/');
        if (split.Length != 2 || !int.TryParse(split[1], out int interval) || interval <= 0)
        {
            throw new ArgumentException("Invalid interval format.");
        }

        int start = split[0] == "*" ? min : int.Parse(split[0]);
        if (start < 0)
        {
            throw new ArgumentOutOfRangeException("Interval start must be within valid bounds and non-negative.");
        }

        return string.Join(" ", Enumerable.Range(start, (max - start + 1) / interval + 1)
            .Select(i => i * interval)
            .TakeWhile(i => i <= max));
    }

    private static string BuildRange(int start, int end)
    {
        return string.Join(" ", Enumerable.Range(start, end - start + 1));
    }

    private static string FormatOutput(string minute, string hour, string dayOfMonth, string month, string dayOfWeek, string command)
    {
        return $"minute        {minute}\n" +
               $"hour          {hour}\n" +
               $"day of month  {dayOfMonth}\n" +
               $"month         {month}\n" +
               $"day of week   {dayOfWeek}\n" +
               $"command       {command}";
    }
    
    private static string HandleSingleValue(string part, int min, int max)
    {
        if (!int.TryParse(part, out int value) || value < 0)
        {
            throw new ArgumentOutOfRangeException($"Value '{part}' is out of range or negative.");
        }
        return part;
    }

    private static void ValidatePart(string part)
    {
        // Validate that part contains only valid characters (digits, * , - , / , , )
        if (!System.Text.RegularExpressions.Regex.IsMatch(part, @"^[0-9\*\-,/]+$"))
        {
            throw new FormatException($"Invalid characters found in part '{part}'. Only digits, *, -, /, , are allowed.");
        }
    }
}
