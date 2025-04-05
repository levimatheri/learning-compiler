namespace minsk.CodeAnalysis.Syntax;

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
    BangToken,
    EqualsEqualsToken,
    BangEqualsToken,
    AmpersandAmpersandToken,
    PipePipeToken,
    OpenParenToken,
    CloseParenToken,
    IdentifierToken,

    // Expressions
    LiteralExpression,
    UnaryExpression,
    BinaryExpression,
    ParenthesizedExpression,

    // Keywords
    TrueKeyword,
    FalseKeyword,
}