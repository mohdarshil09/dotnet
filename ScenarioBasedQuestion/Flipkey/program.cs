using System;

public class Program
{
    public string CleanseAndInvert(string input)
    {
        /// <summary>
        /// Validate the input and return the key if applicable
        ///</summary>
        if (string.IsNullOrEmpty(input) || input.Length < 6)
        {
            return string.Empty;
        }
        ///<summary>
        /// Validate the input and return the key if applicable
        /// </summary>
       
        foreach (char c in input)
        {
            if (!char.IsLetter(c))
            {
                return string.Empty;
            }
        }

        ///<summary>
        /// Convert the input to lowercase
        /// </summary>
        string lower = input.ToLower();

        ///<summary>
        /// Filter the input and return the key if applicable
        /// </summary>
        var filteredChars = new System.Collections.Generic.List<char>();
        foreach (char c in lower)
        {
            int ascii = (int)c;
            if (ascii % 2 != 0) // keep only odd ASCII
            {
                filteredChars.Add(c);
            }
        }

        // If everything was removed, the remaining string is empty but
        // the requirement does not say to treat this as invalid, so continue.

        // Reverse the remaining characters
        filteredChars.Reverse();

        // In the reversed string, convert even-indexed characters to uppercase
        for (int i = 0; i < filteredChars.Count; i++)
        {
            if (i % 2 == 0)
            {
                filteredChars[i] = char.ToUpper(filteredChars[i]);
            }
        }

        return new string(filteredChars.ToArray());
    }

    public static void Main(string[] args)
    {
        ///<summary>
        /// Main method to enter the word and generate the key
        /// </summary>
        Console.WriteLine("Enter the word");
        string input = Console.ReadLine();

        Program p = new Program();
        string result = p.CleanseAndInvert(input);

        if (string.IsNullOrEmpty(result))
        {
            Console.WriteLine("Invalid Input");
        }
        else
        {
            Console.WriteLine("The generated key is - " + result);
        }
    }
}
