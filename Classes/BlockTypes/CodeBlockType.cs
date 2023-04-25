using System.Text.RegularExpressions;

public class CodeBlockType : IBlockType
{
    public string Name => "Code";
    public Regex Pattern { get; }
    public bool IsOpening => true;
    public bool IsClosing => false;
    public bool IsSimple => false;

    public CodeBlockType(string pattern)
    {
        Pattern = new Regex(pattern, RegexOptions.Compiled);
    }

    public List<string> ProcessBlock(string[] lines, int startLine, int endLine)
    {
        return lines.Skip(startLine).Take(endLine - startLine).ToList();
    }

    public Regex GetClosingPattern(Match openingPatternMatch)
    {
        string closingTagNumber = openingPatternMatch.Groups[1].Value;
        string closingPattern = $@"^\s*--\s*NewCellEnd_{closingTagNumber}";
        return new Regex(closingPattern);
    }

    public string ProcessLine(string line)
    {
        // Remove the NewCellBegin tag from the line
        return Pattern.Replace(line, "").Trim();
    }

}