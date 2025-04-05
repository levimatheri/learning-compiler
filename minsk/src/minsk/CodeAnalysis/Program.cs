using minsk.CodeAnalysis;
using minsk.CodeAnalysis.Syntax;

var showTree = false;
while (true)
{
    Console.Write("> ");
    var line = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(line)) return;

    var parser = new Parser(line);
    var expression = parser.Parse();

    if (line == "#showTree")
    {
        showTree = !showTree;
        Console.WriteLine(showTree ? "Showing parse trees." : "Not showing parse trees.");
        continue;
    }
    else if (line == "#cls")
    {
        Console.Clear();
        continue;
    }

    var syntaxTree = SyntaxTree.Parse(line);

    if (showTree)
    {
        Console.ForegroundColor = ConsoleColor.DarkGray;
        PrettyPrint(syntaxTree.Root);
        Console.ResetColor();
    }

    if (!syntaxTree.Diagnostics.Any())
    {
        var evaluator = new Evaluator(syntaxTree.Root);
        var result = evaluator.Evaluate();
        Console.WriteLine(result);
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.DarkRed;
        foreach (var diagnostic in syntaxTree.Diagnostics)
            Console.WriteLine(diagnostic);
        Console.ResetColor();
    }
}

static void PrettyPrint(SyntaxNode node, string indent = "", bool isLast = false)
{
    var marker = isLast ? "└──" : "├──";

    Console.Write(indent);
    Console.Write(marker);
    Console.Write(node.Kind);

    if (node is SyntaxToken t && t.Value != null)
    {
        Console.Write(" ");
        Console.Write(t.Value);
    }

    Console.WriteLine();

    indent += isLast ? "    " : "│   ";

    var lastChild = node.GetChildren().LastOrDefault();

    foreach (var child in node.GetChildren())
        PrettyPrint(child, indent, child == lastChild);
}