﻿using Cron.Parser.Tokens;
using Cron.Parser.Visitors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cron.Parser.Enums;
using Cron.Parser.Extensions;

namespace Cron.Parser.Syntax
{
    public class RangeNode : SyntaxOperatorNode
    {
        private MinRangeValueNode left;
        private MaxRangeValueNode right;

        public RangeNode(MinRangeValueNode left, MaxRangeValueNode right)
        {
            this.left = left;
            this.right = right;
        }

        public RangeNode(SyntaxOperatorNode left, SyntaxOperatorNode right)
        {
            this.left = new MinRangeValueNode(left);
            this.right = new MaxRangeValueNode(right);
        }

        public override void Accept(INodeVisitor visitor)
        {
            visitor.Visit(this);
            left.Accept(visitor);
            right.Accept(visitor);
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
    }
}