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

    public abstract string ProcessLine(string line);

    public List<string> ProcessBlock(string[] lines, int openingLine, int closingLine, int blockNumber)
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

        // In both the MarkdownBlockType and CodeBlockType classes
        if (ModifierFunctions != null)
        {
            foreach (var modifier in ModifierFunctions)
            {
                for (int i = openingLine + 1; i <= closingLine; i++)
                {
                    blockLines[i] = modifier(blockLines[i], blockNumber);
                }
            }
        }


        return blockLines;
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
