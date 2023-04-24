using System.Text.RegularExpressions;
class Analyzer
{
    private List<IBlockType> _blockTypes;

    public Analyzer(List<IBlockType> blockTypes)
    {
        _blockTypes = blockTypes;
    }

    public List<Block> Analyze(string inputFilePath)
    {
        var blocks = new List<Block>();
        var lines = File.ReadAllLines(inputFilePath);

        for (int i = 0; i < lines.Length; i++)
        {
            string line = lines[i];

            foreach (var blockType in _blockTypes)
            {
                Match match = blockType.Pattern.Match(line);

                if (match.Success)
                {
                    int closingLine = -1;

                    if (blockType.IsOpening)
                    {
                        closingLine = FindClosingLine(lines, blockType, match, i);
                    }

                    var blockLines = blockType.ProcessBlock(lines, i, closingLine);
                    var block = new Block
                    {
                        Name = blockType.Name,
                        Lines = blockLines,
                        OpeningLine = i,
                        ClosingLine = closingLine,
                    };

                    blocks.Add(block);
                }
            }
        }

        return blocks;
    }
    private int FindClosingLine(string[] lines, IBlockType blockType, Match openingPatternMatch, int openingLine)
    {
        Regex closingPattern = blockType.GetClosingPattern(openingPatternMatch);

        for (int i = openingLine + 1; i < lines.Length; i++)
        {
            if (closingPattern.IsMatch(lines[i]))
            {
                return i;
            }
        }

        return -1;
    }

}
