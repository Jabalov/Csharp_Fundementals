using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithmics
{
    static class GraphTraversalCalculator
    {
        public abstract class Expression
        {
            public abstract int Evaluate();
            public List<Expression> Children = new List<Expression>();
        }

        public class Plus : Expression
        {
            public override int Evaluate()
            {
                var sum = 0;
                foreach (var child in Children)
                    sum += child.Evaluate();
                return sum;
            }
        }

        public class Minus : Expression
        {
            public override int Evaluate()
            {
                var result = Children[0].Evaluate();
                for (int i = 1; i < Children.Count; i++)
                    result -= Children[i].Evaluate();
                return result;
            }
        }

        public class Negate : Expression
        {
            public override int Evaluate()
            {
                return -1 * Children[0].Evaluate();
            }
        }

        public class Value : Expression
        {
            int value;
            public Value(int value)
            {
                this.value = value;
            }

            public override int Evaluate()
            {
                return value;
            }
        }
    }
}
