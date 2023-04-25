using System.Text.RegularExpressions;

// testing the analyzer: I want to first only print out the blocks that are in the file:
// 1. I need to get the blocks from the file
// 2. I need to print out the blocks from the file
// using System.Text.RegularExpressions;

string inputFilePath = "TestSp2.sql";
// string outputFilePath = "TestSp2.ipynb";

// Tags
var tagMap = new Dictionary<string, Tag>
{
    {
        "SignedComment",
        new Tag("-- SignedComment:", new Regex(@"\s*--\s*SignedComment:"))
    },
    {
        // SignedComment without the beginning --
        "SignedComment2",
        new Tag("SignedComment:", new Regex(@"\s*SignedComment:"))
    },
    {
        "NewCellBegin",
        new Tag("-- NewCellBegin_", new Regex(@"\s*--\s*NewCellBegin_\d+"))
    },
    {
        "NewCellEnd",
        new Tag("-- NewCellEnd_", new Regex(@"\s*--\s*NewCellEnd_\d+"))
    },
    {
        "DemoWhere",
        new Tag("-- DemoWhere:", new Regex(@"\s*--\s*DemoWhere:"))
    },
    {
        "NewBlockToComment",
        new Tag("-- NewBlockToComment_", new Regex(@"\s*--\s*NewBlockToComment_\d+"))
    },
    {
        "NewBlockToUnComment",
        new Tag("-- NewBlockToUnComment_", new Regex(@"\s*--\s*NewBlockToUnComment_\d+"))
    },
    {
        "RemoveDemoWhere",
        new Tag("-- RemoveDemoWhere:", new Regex(@"\s*--\s*RemoveDemoWhere:"))
    }

};



// BlockModifier
BlockModifier blockToComment = new BlockModifier(BlockModifiers.Comment);
BlockModifier blockToUnComment = new BlockModifier(BlockModifiers.UnComment);
var removeDemoWhere = new BlockModifier(BlockModifiers.RemoveDemoWhere);

var blockTypes = new List<IBlockType>
{
    new MarkdownBlockType(tagMap["SignedComment"].Pattern, tagMap),
    new CodeBlockType(tagMap["NewCellBegin"].Pattern, tagMap) 
        { ModifierFunctions = 
            { 
                blockToComment
                , blockToUnComment
                , removeDemoWhere 
            } 
    }
    // add Tags

    ,
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

