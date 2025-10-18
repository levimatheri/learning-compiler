using Minsk.CodeAnalysis.Binding;
using Minsk.CodeAnalysis.Syntax;

namespace Minsk.CodeAnalysis;

public class Compilation(SyntaxTree syntax)
{
    public EvaluationResult Evaluate(Dictionary<VariableSymbol, object> variables)
    {
        var binder = new Binder(variables);
        var boundExpression = binder.BindExpression(syntax.Root);

        List<Diagnostic> diagnostics = [..syntax.Diagnostics.Concat(binder.Diagnostics)];
        if (diagnostics.Count != 0)
        {
            return new EvaluationResult(diagnostics, null);
        }
        
        var evaluator = new Evaluator(boundExpression, variables);
        var value = evaluator.Evaluate();
        return new EvaluationResult([], value);
    }
}