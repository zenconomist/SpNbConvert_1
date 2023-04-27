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
    List<BlockModifierDelegate> ModifierFunctions { get; set; } // Change to a list of BlockModifier

    public string AddHeaderComment(string line);
}


