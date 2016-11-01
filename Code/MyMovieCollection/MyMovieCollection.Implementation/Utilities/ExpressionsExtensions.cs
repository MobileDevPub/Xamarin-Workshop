using System;
using System.Linq.Expressions;

namespace MyMovieCollection.Implementation.Utilities
{
    public static class ExpressionsExtensions
    {
        /// <summary>
        /// Returns the expression member name
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">expression</exception>
        /// <exception cref="System.ArgumentException">Invalid expression:  + expression.Body.NodeType;expression</exception>
        public static string AsString(Expression<Func<object>> expression)
        {
            if (null == expression) throw new ArgumentNullException("expression");

            var member = expression.Body as MemberExpression;
            if (member == null)
            {
                var ubody = expression.Body as UnaryExpression;
                member = ubody.Operand as MemberExpression;
            }

            if (null == member) throw new ArgumentException("Invalid expression: " + expression.Body.NodeType, "expression");
            return member.Member.Name;
        }
    }
}
