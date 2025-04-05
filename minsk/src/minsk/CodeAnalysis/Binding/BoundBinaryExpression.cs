namespace minsk.CodeAnalysis.Binding;

internal sealed class BoundBinaryExpression : BoundExpression
{
    public BoundBinaryExpression(BoundExpression left, BoundBinaryOperatorKind? operatorKind, BoundExpression right)
    {
        Left = left;
        OperatorKind = operatorKind;
        Right = right;
    }

    public override BoundNodeKind Kind => BoundNodeKind.UnaryExpression;
    public BoundExpression Left { get; }
    public BoundBinaryOperatorKind? OperatorKind { get; }
    public BoundExpression Right { get; }
    public override Type Type => Left.Type;
}
