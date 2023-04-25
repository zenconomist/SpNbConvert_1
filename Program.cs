﻿using System.Text.RegularExpressions;

// testing the analyzer: I want to first only print out the blocks that are in the file:
// 1. I need to get the blocks from the file
// 2. I need to print out the blocks from the file
// using System.Text.RegularExpressions;

string inputFilePath = "TestSp2.sql";
// string outputFilePath = "TestSp2.ipynb";
// BlockModifier
BlockModifier blockToComment = new BlockModifier(BlockModifiers.Comment);
BlockModifier blockToUnComment = new BlockModifier(BlockModifiers.UnComment);
var removeDemoWhere = new BlockModifier(BlockModifiers.RemoveDemoWhere);

var blockTypes = new List<IBlockType>
{
    new MarkdownBlockType(@"^\s*--\s*SignedComment:"),
    new CodeBlockType(@"^\s*--\s*NewCellBegin_(\d+)") { ModifierFunctions = { blockToComment, blockToUnComment, removeDemoWhere } },
    // You can define more block types here
};



var analyzer = new Analyzer(blockTypes);

var blocks = analyzer.Analyze(inputFilePath);

foreach (var block in blocks)
{
    Console.WriteLine($"Block type: {block.Name}");
    Console.WriteLine($"Block lines: {block.Lines.Count}");
    Console.WriteLine($"Block opening line: {block.OpeningLine}");
    Console.WriteLine($"Block closing line: {block.ClosingLine}");
    
    // print out every line of the block
    foreach (var line in block.Lines)
    {
        Console.WriteLine(line);
    }

    Console.WriteLine();
}


// Instantiate and use the NotebookBuilder
var notebookBuilder = new NotebookBuilder();
notebookBuilder.BuildNotebook(inputFilePath, blocks);

