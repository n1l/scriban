// Copyright (c) Alexandre Mutel. All rights reserved.
// Licensed under the BSD-Clause 2 license. 
// See license.txt file in the project root for full license information.

using System.Collections.Generic;
using System.IO;

namespace Scriban.Syntax
{
    [ScriptSyntax("expression statement", "<expression>")]
    public partial class ScriptExpressionStatement : ScriptStatement
    {
        public ScriptExpression Expression { get; set; }

        public override object Evaluate(TemplateContext context)
        {
            var result = context.Evaluate(Expression);
            // This code is necessary for wrap to work
            var codeDelegate = result as ScriptNode;
            if (codeDelegate != null)
            {
                return context.Evaluate(codeDelegate);
            }
            return result;
        }

        public override void Write(TemplateRewriterContext context)
        {
            context.Write(Expression);
            context.ExpectEos();
        }

        public override string ToString()
        {
            return Expression?.ToString();
        }

        public override void Accept(ScriptVisitor visitor) => visitor.Visit(this);

        public override TResult Accept<TResult>(ScriptVisitor<TResult> visitor) => visitor.Visit(this);

        protected override IEnumerable<ScriptNode> GetChildren()
        {
            yield return Expression;
        }
    }
}