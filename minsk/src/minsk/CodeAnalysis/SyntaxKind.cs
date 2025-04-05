namespace minsk.CodeAnalysis;

public enum SyntaxKind
{
    // Tokens
    BadToken,
    EndOfFileToken,
    WhitespaceToken,
    NumberToken,
    PlusToken,
    MinusToken,
    StarToken,
    SlashToken,
    OpenParenToken,
    CloseParenToken,

    // Expressions
    LiteralExpression,
    BinaryExpression,
    ParenthesizedExpression
}