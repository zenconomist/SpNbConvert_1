public class BlockBuilder
{
    public List<Block> BuildBlocks(List<Block> blocks)
    {
        var builtBlocks = new List<Block>();

        foreach (var block in blocks)
        {
            var builtBlock = new Block
            {
                BlockType = block.BlockType,
                OpeningLine = block.OpeningLine,
                ClosingLine = block.ClosingLine,
                Lines = new List<string>()
            };

            if (block.ClosingLine == -1)
            {
                System.Console.WriteLine($"Block opening line: {block.OpeningLine}");
                builtBlock.Lines.Add(block.Lines[block.OpeningLine]);
                builtBlocks.Add(builtBlock);
                continue;
            }

            for (int i = block.OpeningLine; i <= block.ClosingLine; i++)
            {
                string line = block.Lines[i];

                // Process the line using the block type's ProcessLine method
                line = block.BlockType.ProcessLine(line);

                builtBlock.Lines.Add(line);
            }

            builtBlocks.Add(builtBlock);
        }

        return builtBlocks;
    }
}
