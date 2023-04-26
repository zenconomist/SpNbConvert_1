using System.Text.RegularExpressions;
public abstract class BlockType : IBlockType
{
    public abstract string Name { get; }
    public abstract Regex Pattern { get; }
    public List<BlockModifierDelegate> ModifierFunctions { get; set; } = new List<BlockModifierDelegate>(); // Change to a list of BlockModifier
    public int? BlockNumber { get; set; } 
    public bool IsOpening => false;
    public bool IsClosing => false;
    public bool IsSimple => true;

    public Dictionary<string, Tag> Tags { get; set; } = new Dictionary<string, Tag>();

    public abstract string ProcessLine(string line);
    public List<string> ProcessBlock(string[] lines, int openingLine, int closingLine, int blockNumber)
    {
        var processedLines = new List<string>();

        if (closingLine == -1)
        {
            string line = lines[openingLine];
            line = ProcessLine(line);
            line = ApplyBlockModifiers(line, blockNumber);

            processedLines.Add(line);
        }
        else
        {
            for (int i = openingLine; i <= closingLine; i++)
            {
                string line = lines[i];
                line = ProcessLine(line);
                line = ApplyBlockModifiers(line, blockNumber);
                processedLines.Add(line);
            }
        }

        // use TagCleanUp to remove tags from the processed lines
        foreach (var tag in Tags.Values)
        {
            processedLines = processedLines.Select(line => tag.TagCleanUp(line)).ToList();
        }


        return processedLines;
    }

    private string ApplyBlockModifiers(string line, int blockNumber)
    {
        if (ModifierFunctions != null)
        {
            foreach (var modifier in ModifierFunctions)
            {
                line = modifier(line, blockNumber);
            }
        }

        return line;
    }
    public string AddHeaderComment(string line)
    {
        // Check if there is a Tag with the key "NewCellBegin"
        if (Tags.ContainsKey("NewCellBegin"))
        {
            string headerCommentPattern = Tags["NewCellBegin"].Pattern.ToString();
            Match match = Regex.Match(line, headerCommentPattern);

            if (match.Success)
            {
                string headerComment = match.Groups[2].Value;
                return $"#### {headerComment}";
            }
        }
        return null;
    }


    // logically important to only clean up after all block modifications were made
    public string UnTag(string line)
    {
        foreach (var tag in Tags.Values)
        {
            line = tag.TagCleanUp(line);
        }

        return line;
    }

    public string Replace(string line, string oldValue, string newValue)
    {
        return line.Replace(oldValue, newValue);
    }

    public string Comment(string line, string commentCharacters)
    {
        return commentCharacters + line;
    }

    public string Uncomment(string line, string commentCharacters)
    {
        return line.Trim().StartsWith(commentCharacters) ? line.Substring(commentCharacters.Length) : line;
    }

    public virtual Regex GetClosingPattern(Match openingPatternMatch)
    {
        // Default null implementation
        return null;
    }
}
