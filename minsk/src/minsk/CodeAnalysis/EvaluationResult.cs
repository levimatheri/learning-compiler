namespace Minsk.CodeAnalysis;

public sealed class EvaluationResult
{
    public object? Value { get; }
    public IReadOnlyList<Diagnostic> Diagnostics { get; }

    public EvaluationResult(IEnumerable<Diagnostic> diagnostics, object? value)
    {
        Value = value;
        Diagnostics = [..diagnostics];
    }
}