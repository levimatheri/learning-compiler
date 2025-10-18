using Minsk.CodeAnalysis.Binding;
using Minsk.CodeAnalysis.Syntax;

namespace Minsk.CodeAnalysis;

public class Compilation(SyntaxTree syntax)
{
    private SyntaxTree Syntax { get; } = syntax;

    public EvaluationResult Evaluate()
    {
        var binder = new Binder();
        var boundExpression = binder.BindExpression(Syntax.Root);

        List<Diagnostic> diagnostics = [..Syntax.Diagnostics.Concat(binder.Diagnostics)];
        if (diagnostics.Count != 0)
        {
            return new EvaluationResult(diagnostics, null);
        }
        
        var evaluator = new Evaluator(boundExpression);
        var value = evaluator.Evaluate();
        return new EvaluationResult([], value);
    }
}