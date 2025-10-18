using Minsk.CodeAnalysis;
using Minsk.CodeAnalysis.Binding;
using Minsk.CodeAnalysis.Syntax;

var showTree = false;
while (true)
{
    Console.Write("> ");
    var line = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(line)) return;

    switch (line)
    {
        case "#showTree":
            showTree = !showTree;
            Console.WriteLine(showTree ? "Showing parse trees." : "Not showing parse trees.");
            continue;
        case "#cls":
            Console.Clear();
            continue;
    }

    var syntaxTree = SyntaxTree.Parse(line);
    var compilation = new Compilation(syntaxTree);
    var result = compilation.Evaluate();
    var diagnostics = result.Diagnostics;

    if (showTree)
    {
        Console.ForegroundColor = ConsoleColor.DarkGray;
        PrettyPrint(syntaxTree.Root);
        Console.ResetColor();
    }

    if (diagnostics.Count == 0)
    {
        Console.WriteLine(result.Value);
    }
    else
    {
        
        foreach (var diagnostic in diagnostics)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(diagnostic);
            Console.ResetColor();
            
            var prefix = line[..diagnostic.Span.Start];
            var error = line[diagnostic.Span.Start..(diagnostic.Span.Start + diagnostic.Span.Length)];
            var suffix = line[diagnostic.Span.End..];

            Console.Write("    ");
            Console.Write(prefix);
            
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write(error);
            Console.ResetColor();
            
            Console.Write(suffix);

            Console.WriteLine();
        }
        
        Console.WriteLine();
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

    indent += isLast ? "    " : "│  ";

    var lastChild = node.GetChildren().LastOrDefault();

    foreach (var child in node.GetChildren())
        PrettyPrint(child, indent, child == lastChild);
}