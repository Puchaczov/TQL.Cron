﻿using Cron.Parser.Tokens;

namespace Cron.Parser.Nodes
{
    public abstract class BinaryExpressionNode : SyntaxNode
    {
        private readonly Token token;

        protected BinaryExpressionNode(Token token)
        {
            this.token = token;
        }

        public override TextSpan FullSpan
        {
            get
            {
                var start = Left.Token.Span.Start;
                var stop = Right.Token.Span.End;
                return new TextSpan(start, stop - start);
            }
        }

        public override bool IsLeaf => true;
        public abstract SyntaxNode Left { get; }
        public abstract SyntaxNode Right { get; }

        public override Token Token => token;
    }
}
