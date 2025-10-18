namespace Minsk.CodeAnalysis.Syntax;

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
    EqualsToken,

    // Expressions
    LiteralExpression,
    UnaryExpression,
    BinaryExpression,
    ParenthesizedExpression,
    NameExpression,
    AssignmentExpression,

    // Keywords
    TrueKeyword,
    FalseKeyword,
    
}