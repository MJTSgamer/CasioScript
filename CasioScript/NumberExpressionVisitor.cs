using System;
using CasioScript.CasioScript;

namespace CasioScript
{
    public class NumberExpressionVisitor : CasioScriptBaseVisitor<object>
    {
        public override object VisitConstant(CasioScriptParser.ConstantContext context)
        {
            if (context.INTERGER() is { } i)
                return int.Parse(i.GetText());

            if (context.FLOAT() is { } f)
            {
                return Utilities.Utilities.TryParseFloat(f.GetText());
            }

            if (context.STRING() is { } s)
                return s.GetText()[1..^1];
            
            if (context.BOOL() is { } b)
                return bool.Parse(b.GetText());
            
            if (context.NULL() is { })
                return null;

            throw new Exception("Unknown constant type: " + context.GetText());
        }
        
        public override object VisitAddExpression(CasioScriptParser.AddExpressionContext context)
        {
            var left = Visit(context.expression(0));
            var right = Visit(context.expression(1));

            var op = context.addOp().GetText();

            return op switch
            {
                "+" => Add(left, right),
                "-" => Subtract(left, right),
                _ => throw new Exception("Unknown operator: " + op)
            };
        }

        private static object Add(object left, object right)
        {
            return left switch
            {
                int lInt when right is int rInt => lInt + rInt,
                float lFloat when right is float rFloat => lFloat + rFloat,
                int lInt2 when right is float rFloat2 => lInt2 + rFloat2,
                float lFloat2 when right is int rInt2 => lFloat2 + rInt2,
                _ => throw new Exception("Cannot add type values: " + left.GetType() + " and " + right.GetType())
            };
        }
        
        private static object Subtract(object left, object right)
        {
            return left switch
            {
                int lInt when right is int rInt => lInt - rInt,
                float lFloat when right is float rFloat => lFloat - rFloat,
                int lInt2 when right is float rFloat2 => lInt2 - rFloat2,
                float lFloat2 when right is int rInt2 => lFloat2 - rInt2,
                _ => throw new Exception("Cannot subtract type values: " + left.GetType() + " and " + right.GetType())
            };
        }
        
        public override object VisitMulExpression(CasioScriptParser.MulExpressionContext context)
        {
            var left = Visit(context.expression(0));
            var right = Visit(context.expression(1));

            var op = context.mulOp().GetText();

            return op switch
            {
                "*" => Multiply(left, right),
                "/" => Divide(left, right),
                _ => throw new Exception("Unknown operator: " + op)
            };
        }
        
        private static object Multiply(object left, object right)
        {
            return left switch
            {
                int lInt when right is int rInt => lInt * rInt,
                float lFloat when right is float rFloat => lFloat * rFloat,
                int lInt2 when right is float rFloat2 => lInt2 * rFloat2,
                float lFloat2 when right is int rInt2 => lFloat2 * rInt2,
                _ => throw new Exception("Cannot multiply type values: " + left.GetType() + " and " + right.GetType())
            };
        }
        
        private static object Divide(object left, object right)
        {
            return left switch
            {
                int lInt when right is int rInt => lInt / rInt,
                float lFloat when right is float rFloat => lFloat / rFloat,
                int lInt2 when right is float rFloat2 => lInt2 / rFloat2,
                float lFloat2 when right is int rInt2 => lFloat2 / rInt2,
                _ => throw new Exception("Cannot divide type values: " + left.GetType() + " and " + right.GetType())
            };
        }
    }
}