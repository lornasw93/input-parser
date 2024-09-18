using System.Text.RegularExpressions;

Console.WriteLine("### Warehouse Counter ###");
Console.WriteLine("What is your stock?");

var stock = Console.ReadLine();

if (string.IsNullOrWhiteSpace(stock))
{
    Console.WriteLine("Please enter a valid stock");
    return;
}

var result = "";
var letterCounts = new Dictionary<char, int>();

var isOlderVersionMatch = Regex.IsMatch(stock, @"^[a-zA-Z ]+$");
var isNewerVersionMatch = Regex.IsMatch(stock, @"^#p2#(?: \d+[a-zA-Z])+$");

if (!isOlderVersionMatch && !isNewerVersionMatch)
{
    Console.WriteLine("Invalid stock format");
    return;
}

if (isOlderVersionMatch)
{
    var letters = stock.Replace(" ", "").Where(c => char.IsLetter(c));

    foreach (var c in letters)
    {
        letterCounts[c] = letterCounts.TryGetValue(c, out int value) ? ++value : 1;
    }
}
else if (isNewerVersionMatch)
{
    var parts = stock.Split(' ').Skip(1);

    foreach (var part in parts)
    {
        var letter = part.Last();
        var count = int.Parse(part[..^1]);

        if (letterCounts.ContainsKey(letter))
        {
            letterCounts[letter] += count;
        }
        else
        {
            letterCounts[letter] = count;
        }
    }
}

result = string.Join(" ", letterCounts.OrderBy(kvp => kvp.Key).Select(kvp => $"{kvp.Value}{kvp.Key}"));

Console.WriteLine($"Output: {result}");
Console.ReadLine();