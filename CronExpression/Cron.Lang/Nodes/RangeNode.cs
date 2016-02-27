﻿using Cron.Parser.Tokens;
using Cron.Parser.Visitors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cron.Parser.Enums;
using Cron.Parser.Extensions;

namespace Cron.Parser.Nodes
{
    public class RangeNode : SyntaxOperatorNode
    {
        private SyntaxOperatorNode left;
        private SyntaxOperatorNode right;

        public RangeNode(SyntaxOperatorNode left, SyntaxOperatorNode right)
        {
            this.left = left;
            this.right = right;
        }

        public override void Accept(INodeVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override IList<int> Evaluate(Segment segment)
        {
            return ListExtension.Empty();
        }

        public SyntaxOperatorNode Left
        {
            get
            {
                return left;
            }
        }

        public SyntaxOperatorNode Right
        {
            get
            {
                return right;
            }
        }

        public override SyntaxNode[] Items
        {
            get
            {
                return new SyntaxNode[] {
                    left,
                    right
                };
            }
        }

        public override Token Token
        {
            get
            {
                return new RangeToken();
            }
        }

        public override string ToString()
        {
            return Left.ToString() + Token.Value + Right.ToString();
        }
    }
}
