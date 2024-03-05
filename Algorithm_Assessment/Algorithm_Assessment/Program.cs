// See https://aka.ms/new-console-template for more information

using Algorithm_Assessment;

var inputStrings = new List<string> { "fliower", "fliow", "flight", "flight" };

var longestCommonPrefix = Assessment.FindLongestCommonPrefix(inputStrings);


Console.WriteLine($"Longest Common Prefix: {longestCommonPrefix}");
