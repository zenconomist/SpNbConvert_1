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
        Pattern = new Regex(pattern);
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
        // Process the line to remove the leading SignedComment tag
        return Regex.Replace(line, @"^\s*--\s*SignedComment:\s*", "");
    }
}