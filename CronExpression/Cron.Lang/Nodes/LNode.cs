﻿using Cron.Parser.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cron.Parser.Visitors;
using Cron.Parser.Extensions;
using Cron.Parser.Enums;

namespace Cron.Parser.Nodes
{
    public class LNode : NumberNode
    {
        public LNode(Token token)
            : base(token)
        { }

        public override void Accept(INodeVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override IList<int> Evaluate(Segment segment)
        {
            return ListExtension.Empty();
        }

        public override string ToString()
        {
            return "L";
        }
    }
}
