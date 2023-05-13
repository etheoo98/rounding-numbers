using System.Text.RegularExpressions;

namespace RoundingNumbers;

internal class RoundingNumbers
{
    private static void Main()
    {
        // Initialize variables
        bool isValidInput;
        double decimalValue = 0;
        var decimalLength = 0;
        var decimalSeparator = "";

        // Loop until valid input is entered
        do
        {
            Console.Write("Enter a number containing a decimal: ");
            var input = Console.ReadLine();

            // Validate input using regular expression
            isValidInput = ValidateInput(input, @"^[0-9]+([,.][0-9]{1,})$",
                "Error: The input must be a number containing at least one decimal. Please try again.");

            if (isValidInput)
            {
                // Determine decimal separator used and convert input to a double
                decimalSeparator = input!.Contains(",") ? "," : ".";
                decimalValue = double.Parse(input.Replace(",", "."));
            }
        } while (!isValidInput);

        // Loop until valid input is entered
        do
        {
            Console.Write("Enter the number of decimal places to round off to (1-15): ");
            var input = Console.ReadLine();

            // Validate input using regular expression
            isValidInput = ValidateInput(input, @"^\d+$",
                "Error: The input must be a whole number. Please try again.");

            if (isValidInput)
            {
                decimalLength = int.Parse(input!);

                // Ensure input is between 1 and 15
                if (decimalLength > 15)
                {
                    Console.WriteLine("Error: The input must be a whole number between 1 and 15. Please try again.");
                    isValidInput = false;
                }
            }
        } while (!isValidInput);

        // Round the decimal value and convert to a string with the desired number of decimal places
        var roundedString = Math.Round(decimalValue, decimalLength, MidpointRounding.AwayFromZero)
            .ToString($"F{decimalLength}");

        var output = roundedString;

        // Replace the first occurrence of the period character with the decimal separator
        output = output.Replace(".", decimalSeparator);

        // Print the rounded value
        Console.WriteLine($"Rounded value: {output}");
    }

    // Validate input using regular expression
    private static bool ValidateInput(string? input, string regex, string errorMessage)
    {
        if (!string.IsNullOrEmpty(input) && Regex.IsMatch(input, regex)) return true;

        Console.WriteLine(errorMessage);
        return false;
    }
}