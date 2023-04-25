using System.Text.RegularExpressions;
public abstract class BlockType : IBlockType
{
    public abstract string Name { get; }
    public abstract Regex Pattern { get; }
    public List<BlockModifier> ModifierFunctions { get; set; } = new List<BlockModifier>(); // Change to a list of BlockModifier
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
