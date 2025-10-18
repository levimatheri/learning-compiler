using System.Collections;
using Minsk.CodeAnalysis.Syntax;

namespace Minsk.CodeAnalysis;

internal sealed class DiagnosticBag : IEnumerable<Diagnostic>
{
    private readonly List<Diagnostic> _diagnostics = [];
    public void AddRange(DiagnosticBag diagnostics)
    {
        _diagnostics.AddRange(diagnostics._diagnostics);
    }
    
    public void ReportBadCharacter(int position, char character)
    {
        var message = $"Bad character input: '{character}'.";
        Report(new TextSpan(position, 1), message);
    }
    
    public void ReportInvalidNumber(TextSpan span, string text, Type type)
    {
        var message = $"The number {text} isn't a valid {type}.";
        Report(span, message);
    }
    
    public void ReportUnexpectedToken(TextSpan currentSpan, SyntaxKind currentKind, SyntaxKind kind)
    {
        var message = $"Unexpected token <{currentKind}>, expected <{kind}>.";
        Report(currentSpan, message);
    }
    
    public IEnumerator<Diagnostic> GetEnumerator() => _diagnostics.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    
    private void Report(TextSpan span, string message)
    {
        var diagnostic = new Diagnostic(span, message);
        _diagnostics.Add(diagnostic);
    }

    public void ReportUndefinedUnaryOperator(TextSpan operatorTokenSpan, string operatorTokenText, SyntaxKind operatorTokenKind)
    {
        var message = $"Unary operator '{operatorTokenText}' is not defined for type {operatorTokenKind}.";
        Report(operatorTokenSpan, message);
    }

    public void ReportUndefinedBinaryOperator(TextSpan operatorTokenSpan, string operatorTokenText, Type boundLeftType, Type boundRightType)
    {
        var message = $"Binary operator '{operatorTokenText}' is not defined for types {boundLeftType} and {boundRightType}.";
        Report(operatorTokenSpan, message);
    }
}