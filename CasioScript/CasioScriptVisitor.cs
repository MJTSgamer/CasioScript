using System;
using System.Collections.Generic;
using CasioScript.CasioScript;

namespace CasioScript
{
    public class CasioScriptVisitor : CasioScriptBaseVisitor<object>
    {
        Dictionary<string, object> variables { get; } = new Dictionary<string, object>();

        public override object VisitAssignment(CasioScriptParser.AssignmentContext context)
        {
            var varName = context.IDENTIFIER().GetText();
            var value = Visit(context.expression());
            
            variables[varName] = value;
            
            Console.WriteLine($"Variable {varName} set to {value}");
            
            return null;
        }

        #region Variable Declaration
        public override object VisitIdentifierExpression(CasioScriptParser.IdentifierExpressionContext context)
        {
            var varName = context.IDENTIFIER().GetText();
            
            if (!variables.ContainsKey(varName))
                throw new Exception($"Variable {varName} is not defined");
            
            return variables[varName];
        }

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
        #endregion

        public override object VisitComment(CasioScriptParser.CommentContext context)
        {
            var comment = context.COMMENT().GetText();
            Console.WriteLine(comment);
            return null;
        }
        
        public override object VisitWhileBlock(CasioScriptParser.WhileBlockContext context)
        {
            Func<object, bool> condition = context.WHILE().GetText() switch
            {
                "while" => IsTrue,
                "until" => IsFalse,
                _ => throw new Exception("Unknown while condition: " + context.WHILE().GetText())
            };

            if (condition(Visit(context.expression())))
            {
                do
                {
                    Visit(context.block());
                } while (condition(Visit(context.expression())));
            }
            else
            {
                Visit(context.elseIfBlock());
            }

            return null;
        }

        public bool IsTrue(object value)
        {
            if (value is bool b)
                return b;
            
            throw new Exception("Cannot convert to bool: " + value);
        }
        
        public bool IsFalse(object value) => !IsTrue(value);

        public override object VisitCompExpression(CasioScriptParser.CompExpressionContext context)
        {
            var left = Visit(context.expression(0));
            var right = Visit(context.expression(1));

            var op = context.compOp().GetText();

            return op switch
            {
                "==" => Equal(left, right),
                "!=" => NotEqual(left, right),
                ">" => GreaterThan(left, right),
                "<" => LessThan(left, right),
                ">=" => GreaterThanOrEqual(left, right),
                "<=" => LessThanOrEqual(left, right),
                _ => throw new Exception("Unknown operator: " + op)
            };
        }

        private object LessThanOrEqual(object left, object right) => ((bool)Equal(left, right) || (bool)LessThan(left, right));
        private object GreaterThanOrEqual(object left, object right) => ((bool) Equal(left, right) || (bool) GreaterThan(left, right));

        private object LessThan(object left, object right) => GreaterThan(right, left);
        private object GreaterThan(object left, object right)
        {
            return left switch
            {
                int lInt when right is int rInt => lInt > rInt,
                float lFloat when right is float rFloat => lFloat > rFloat,
                int lInt2 when right is float rFloat2 => lInt2 > rFloat2,
                float lFloat2 when right is int rInt2 => lFloat2 > rInt2,
                _ => throw new Exception("Cannot compare type values: " + left.GetType() + " and " + right.GetType())
            };
        }

        private object NotEqual(object left, object right) => !(bool)Equal(left, right);
        private object Equal(object left, object right)
        {
            return left switch
            {
                int lInt when right is int rInt => lInt == rInt,
                float lFloat when right is float rFloat => Math.Abs(lFloat - rFloat) < 0.0000001f,
                int lInt2 when right is float rFloat2 => Math.Abs(lInt2 - rFloat2) < 0.0000001f,
                float lFloat2 when right is int rInt2 => Math.Abs(lFloat2 - rInt2) < 0.0000001f,
                string lString when right is string rString => lString == rString,
                bool lBool when right is bool rBool => lBool == rBool,
                null when right is null => true,
                _ => false
            };
        }
        
    }
}