namespace minsk.CodeAnalysis;

public sealed class LiteralExpressionSyntax : ExpressionSyntax
{
    public SyntaxToken NumberToken { get; }

    public LiteralExpressionSyntax(SyntaxToken literalToken)
    {
        NumberToken = literalToken;
    }

    public override SyntaxKind Kind => SyntaxKind.LiteralExpression;
    public override IEnumerable<SyntaxNode> GetChildren()
    {
        yield return NumberToken;
    }
}