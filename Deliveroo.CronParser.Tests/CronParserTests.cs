namespace Deliveroo.CronParser.Tests;

public class CronParserTests
{
    [Fact]
    public void Parse_ShouldThrowException_WhenCronExpressionIsEmpty()
    {
        string cronExpression = "";
        
        Assert.Throws<ArgumentException>(() => CronParser.Parse(cronExpression));
    }

    [Fact]
    public void Parse_ShouldThrowException_WhenCronExpressionHasInvalidNumberOfFields()
    {
        string cronExpression = "*/15 0 1,15 *"; // Only 4 fields instead of 6

        Assert.Throws<ArgumentException>(() => CronParser.Parse(cronExpression));
    }
    
    [Fact]
    public void Parse_ShouldThrowException_WhenCronExpressionHasInvalidCharacters()
    {
        string cronExpression = "*/a 0 1,15 * 1-5 /usr/bin/find"; // Invalid character 'a'

        Assert.Throws<FormatException>(() => CronParser.Parse(cronExpression));
    }

    [Fact]
    public void Parse_ShouldThrowException_WhenCronExpressionContainsNegativeValues()
    {
        string cronExpression = "-1 12 10 6 3 /bin/backup"; // Negative minute value

        Assert.Throws<FormatException>(() => CronParser.Parse(cronExpression));
    }
    
    [Fact]
    public void Parse_ShouldCorrectlyHandleWildcardAndListValues()
    {
        string cronExpression = "*/15 0 1,15 * 1-5 /usr/bin/find";
        string expectedOutput = 
            "minute        0 15 30 45\n" +
            "hour          0\n" +
            "day of month  1 15\n" +
            "month         1 2 3 4 5 6 7 8 9 10 11 12\n" +
            "day of week   1 2 3 4 5\n" +
            "command       /usr/bin/find";

        string output = CronParser.Parse(cronExpression);
        
        Assert.Equal(expectedOutput, output);
    }

    [Fact]
    public void Parse_ShouldCorrectlyHandleSingleSpecificValues()
    {
        string cronExpression = "0 12 10 6 3 /bin/backup";
        string expectedOutput = 
            "minute        0\n" +
            "hour          12\n" +
            "day of month  10\n" +
            "month         6\n" +
            "day of week   3\n" +
            "command       /bin/backup";

        string output = CronParser.Parse(cronExpression);
        Assert.Equal(expectedOutput, output);
    }

    [Fact]
    public void Parse_ShouldCorrectlyHandleRangeValues()
    {
        string cronExpression = "5-10 1-3 1 * * /bin/task";
        string expectedOutput = 
            "minute        5 6 7 8 9 10\n" +
            "hour          1 2 3\n" +
            "day of month  1\n" +
            "month         1 2 3 4 5 6 7 8 9 10 11 12\n" +
            "day of week   1 2 3 4 5 6 7\n" +
            "command       /bin/task";

        string output = CronParser.Parse(cronExpression);
        Assert.Equal(expectedOutput, output);
    }

    [Fact]
    public void Parse_ShouldCorrectlyHandleIncrementValues()
    {
        string cronExpression = "*/10 0 1 * * /bin/run";
        string expectedOutput = 
            "minute        0 10 20 30 40 50\n" +
            "hour          0\n" +
            "day of month  1\n" +
            "month         1 2 3 4 5 6 7 8 9 10 11 12\n" +
            "day of week   1 2 3 4 5 6 7\n" +
            "command       /bin/run";

        string output = CronParser.Parse(cronExpression);
        Assert.Equal(expectedOutput, output);
    }
}
