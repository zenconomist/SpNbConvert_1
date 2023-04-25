using System.Text.RegularExpressions;
public interface IBlockType
{
    string Name { get; }
    Regex Pattern { get; }
    bool IsOpening { get; }
    bool IsClosing { get; }
    bool IsSimple { get; }

   List<string> ProcessBlock(string[] lines, int openingLine, int closingLine, int blockNumber);

    Regex GetClosingPattern(Match openingPatternMatch);
    string ProcessLine(string line);
    List<BlockModifier> ModifierFunctions { get; set; } // Change to a list of BlockModifier

}

public delegate string BlockModifier(string line, int blockNumber);
