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

    public List<string> ProcessBlock(string[] lines, int openingLine, int closingLine)
    {
        var blockLines = new List<string>();

        if (closingLine == -1)
        {
            blockLines.Add(ProcessLine(lines[openingLine]));
        }
        else
        {
            for (int i = openingLine; i <= closingLine; i++)
            {
                blockLines.Add(ProcessLine(lines[i]));
            }
        }

        return blockLines;
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