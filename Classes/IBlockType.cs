using System.Text.RegularExpressions;
public interface IBlockType
{
    string Name { get; }
    Regex Pattern { get; }
    bool IsOpening { get; }
    bool IsClosing { get; }
    bool IsSimple { get; }

    List<string> ProcessBlock(string[] lines, int startLine, int endLine);

    Regex GetClosingPattern(Match openingPatternMatch);
    string ProcessLine(string line);
    BlockModifier ModifierFunction { get; set; }

}

public delegate string BlockModifier(string line, int blockNumber);
