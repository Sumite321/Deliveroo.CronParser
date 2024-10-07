# Cron Expression Parser

## Overview

This project is a **Cron Expression Parser** written in C#. It parses standard cron expressions with 5 fields (minute, hour, day of month, month, and day of week) plus a command, and expands each field to show the exact times it will run. The output is formatted in a human-readable table. Invalid cron expressions, such as those containing invalid characters or negative values, are properly handled and produce meaningful error messages.

The parser reads cron expressions from a text file, processes each line, and displays the parsed output or errors in the console.

### Example

Given the cron expression:

```*/15 0 1,15 * 1-5 /usr/bin/find```

The output will be:

```
Parsed cron expression: */15 0 1,15 * 1-5 /usr/bin/find
minute        0 15 30 45
hour          0
day of month  1 15
month         1 2 3 4 5 6 7 8 9 10 11 12
day of week   1 2 3 4 5
command       /usr/bin/find
```
## Features

- Supports all basic cron fields including ranges (`2-5`), lists (`1,2,3`), wildcards (`*`), and intervals (`*/10`).
- Handles edge cases such as:
  - Invalid characters in cron fields.
  - Negative values.
  - Out-of-range values (e.g., minute > 59).
  - Commands with spaces.
  - Proper error reporting for invalid cron expressions.
  
## File Structure

- CronParser.cs # Core logic for parsing cron expressions
- Program.cs # Main file to run the application
- cron_expressions.txt # Sample file containing cron expressions (one per line)
- README.md # This file

## Requirements

To run this project, you need to have **.NET 8.0** or later installed on your system.

### Install .NET SDK on OS X/Linux

1. **Install .NET SDK:**
   ```bash
   sudo apt-get update && sudo apt-get install -y dotnet-sdk-8.0

2. **Verify Instalation:**
   ```bash
   dotnet --version

## How To Run
1. ** Place Your Cron Expressions in a Text File**
   Ensure that your cron expressions are stored line-by-line in a file named cron_expressions.txt in the same directory as the program. Example:
      ```bash
    */15 0 1,15 * 1-5 /usr/bin/find
    0 12 10 6 3 /bin/backup

2. **Run The Program:**
   ```bash
   dotnet run
   
3. **Run The Tests:**
    ```bash
    dotnet test
   