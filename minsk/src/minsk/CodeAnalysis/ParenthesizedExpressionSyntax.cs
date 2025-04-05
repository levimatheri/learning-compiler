namespace minsk.CodeAnalysis;

public sealed class ParenthesizedExpressionSyntax : ExpressionSyntax
{
    public ParenthesizedExpressionSyntax(SyntaxToken openParenToken, ExpressionSyntax expression, SyntaxToken closeParen)
    {
        OpenParenToken = openParenToken;
        Expression = expression;
        CloseParen = closeParen;
    }

    public override SyntaxKind Kind => SyntaxKind.ParenthesizedExpression;
    public SyntaxToken OpenParenToken { get; }
    public ExpressionSyntax Expression { get; }
    public SyntaxToken CloseParen { get; }

    public override IEnumerable<SyntaxNode> GetChildren()
    {
        yield return OpenParenToken;
        yield return Expression;
        yield return CloseParen;
    }
}
