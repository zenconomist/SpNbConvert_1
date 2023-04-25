using System.Text.RegularExpressions;
public class MarkdownBlockType : IBlockType
{
    public string Name => "Markdown";
    public Regex Pattern { get; }
    public bool IsOpening => false;
    public bool IsClosing => false;
    public bool IsSimple => true;

    public MarkdownBlockType(string pattern)
    {
        Pattern = new Regex(pattern, RegexOptions.Compiled);
    }

    public List<string> ProcessBlock(string[] lines, int startLine, int endLine)
    {
        return lines.Skip(startLine).Take(endLine - startLine).ToList();
    }


    public Regex GetClosingPattern(Match openingPatternMatch)
    {
        // Since MarkdownBlockType doesn't have a closing tag, return null
        return null;
    }

    public string ProcessLine(string line)
    {
        // Remove the comment characters from the line
        return line.Replace("--", "").Trim();
    }
}