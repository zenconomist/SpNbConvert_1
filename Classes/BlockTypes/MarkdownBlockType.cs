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


/*
//     I'd like to refactor this method:     public override Regex GetClosingPattern(Match openingPatternMatch)
//     {
//         // Since MarkdownBlockType doesn't have a closing tag, return null
//         return null;
//     }
// that if an openingPatternMatch begins with /*, then find the closing pattern of */ //, and if the opening pattern starts with --, then the closing line is the last line which does start with --. When I mean start, I mean that it can only have whitespaces or tabs before the --.
//*/

    public override Regex GetClosingPattern(Match openingPatternMatch)
    {
        // Check if the opening pattern starts with "/*"
        if (openingPatternMatch.Value.TrimStart().StartsWith("/*"))
        {
            // If it starts with "/*", the closing pattern is "*/"
            return new Regex(@"\s*\*/");
        }
        else if (openingPatternMatch.Value.TrimStart().StartsWith("--"))
        {
            // If it starts with "--", the closing pattern is a line that doesn't start with "--"
            return new Regex(@"^(?!\s*--).*$", RegexOptions.Multiline);
        }

        // If it doesn't match either case, return null
        return null;
    }


    public override string ProcessLine(string line)
    {
        // Remove the comment characters from the line
        return line.Replace("--", "").Trim();
    }
}