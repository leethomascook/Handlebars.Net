﻿using System;
using System.Linq.Expressions;

namespace Handlebars.Compiler
{
    internal class UnencodedStatementVisitor : HandlebarsExpressionVisitor
    {
        public static Expression Visit(Expression expr, CompilationContext context)
        {
            return new UnencodedStatementVisitor(context).Visit(expr);
        }

        private UnencodedStatementVisitor(CompilationContext context)
            : base(context)
        {
        }

        protected override Expression VisitStatementExpression(StatementExpression sex)
        {
            if (sex.IsEscaped == false)
            {
                return Expression.Block(
                    Expression.Assign(
                        Expression.Property(CompilationContext.BindingContext, "OutputMode"),
                        Expression.Constant(OutputMode.Unencoded)),
                    sex,
                    Expression.Assign(
                        Expression.Property(CompilationContext.BindingContext, "OutputMode"),
                        Expression.Constant(OutputMode.Encoded)));
            }
            else
            {
                return sex;
            }
        }
    }
}

