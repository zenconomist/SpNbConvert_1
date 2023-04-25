using System.Text.RegularExpressions;
public class MarkdownBlockType : BlockType
{
    public override string Name => "Markdown";
    public override Regex Pattern { get; }

    public MarkdownBlockType(string pattern)
    {
        Pattern = new Regex(pattern, RegexOptions.Compiled);
    }

    public MarkdownBlockType(Regex pattern)
    {
        Pattern = pattern;
    }

    // constructor with Tags involved
    public MarkdownBlockType(Regex pattern, Dictionary<string, Tag> tags)
    {
        Pattern = pattern;
        Tags = tags;
    }

    /*    
    public override List<string> ProcessBlock(string[] lines, int openingLine, int closingLine, int blockNumber)
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
    */

    public override Regex GetClosingPattern(Match openingPatternMatch)
    {
        // Since MarkdownBlockType doesn't have a closing tag, return null
        return null;
    }

    public override string ProcessLine(string line)
    {
        // Remove the comment characters from the line
        return line.Replace("--", "").Trim();
    }
}