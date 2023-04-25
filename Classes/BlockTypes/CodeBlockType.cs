using System.Text.RegularExpressions;

public class CodeBlockType : BlockType
{
    public override string Name => "Code";
    public override Regex Pattern { get; }


    public CodeBlockType(string pattern)
    {
        Pattern = new Regex(pattern, RegexOptions.Compiled);
    }

    public CodeBlockType(Regex pattern)
    {
        Pattern = pattern;
    }

    /*
    public override List<string> ProcessBlock(string[] lines, int startLine, int endLine)
    {
        return lines.Skip(startLine).Take(endLine - startLine).ToList();
    }
    */

    public override Regex GetClosingPattern(Match openingPatternMatch)
    {
        string closingTagNumber = openingPatternMatch.Groups[1].Value;
        string closingPattern = $@"^\s*--\s*NewCellEnd_{closingTagNumber}";
        return new Regex(closingPattern);
    }

    public override string ProcessLine(string line)
    {
        // Remove the NewCellBegin tag from the line
        return line;
    }

}