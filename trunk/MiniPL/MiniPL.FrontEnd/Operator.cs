﻿using System.Collections.Generic;

namespace MiniPL.FrontEnd
{
    /// @author Jani Viherväs
    /// @version 28.2.2014
    /// 
    /// <summary>
    /// Operators
    /// </summary>
    public class Operator
    {
        #region Arithmetic operators

        /// <summary>
        /// Plus operator
        /// </summary>
        public const string Plus = "+";

        /// <summary>
        /// Minus operator
        /// </summary>
        public const string Minus = "-";

        /// <summary>
        /// Divide operator
        /// </summary>
        public const string Divide = "/";

        /// <summary>
        /// Multiply operator
        /// </summary>
        public const string Multiply = "*";

        #endregion

        #region Comparison

        /// <summary>
        /// Lesser than operator
        /// </summary>
        public const string LesserThan = "<";

        /// <summary>
        /// Lesser or equal than operator
        /// </summary>
        public const string LesserOrEqualThan = "<=";

        /// <summary>
        /// Greater than operator
        /// </summary>
        public const string GreaterThan = ">";

        /// <summary>
        /// Greater or equal than operator
        /// </summary>
        public const string GreaterOrEqualThan = ">=";

        /// <summary>
        /// Equal operator
        /// </summary>
        public const string Equal = "=";

        #endregion

        #region Logical

        /// <summary>
        /// Logical and operator
        /// </summary>
        public const string And = "&";

        /// <summary>
        /// Logical not operator
        /// </summary>
        public const string Not = "!";

        #endregion

        /// <summary>
        /// Left parenthesis
        /// </summary>
        public const string ParenthesisLeft = "(";

        /// <summary>
        /// Right parenthesis
        /// </summary>
        public const string ParenthesisRight = ")";


        /// <summary>
        /// Returns all the operators in an order that doesn't mess up the longest matching rule, i.e. ">=" is before ">".
        /// </summary>
        /// <returns>All the operators</returns>
        public static IEnumerable<string> Operators()
        {
            return new[]
                       {
                           Plus, Minus, Multiply, Divide,
                           ParenthesisLeft, ParenthesisRight,
                           And, Not,
                           GreaterOrEqualThan, LesserOrEqualThan,
                           GreaterThan, LesserThan, Equal
                       };
        }
    }
}
