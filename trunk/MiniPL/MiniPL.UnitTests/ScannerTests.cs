﻿using System.Collections.Generic;
using MiniPL.FrontEnd;
using NUnit.Framework;

namespace MiniPL.UnitTests
{
    /// @author Jani Viherväs
    /// @version 27.2.2014
    ///
    /// <summary>
    /// Unit tests for the Scanner class
    /// </summary>
    [TestFixture]
    public class ScannerTests
    {
        private Scanner _scanner;

        [SetUp]
        protected void SetUp()
        {
            _scanner = new Scanner();
        }

        [Test]
        public void TestScannerCanTokenizeTypes()
        {
            var lines = new List<string>
                            {
                                "var x : int;",
                                "var y :  bool;",
                                "var z :   string;",
                            };
            
            var tokens = _scanner.Tokenize(lines);

            Assert.IsTrue(3 <= tokens.Count);
            
            Assert.IsTrue(tokens.Exists(x =>
                                  x.Lexeme == Type.Int &&
                                  x.Line == 1 &&
                                  x.StartColumn == 9
                              ));

            Assert.IsTrue(tokens.Exists(x =>
                                  x.Lexeme == Type.Bool &&
                                  x.Line == 2 &&
                                  x.StartColumn == 10
                              ));
            
            Assert.IsTrue(tokens.Exists(x =>
                                  x.Lexeme == Type.String &&
                                  x.Line == 3 &&
                                  x.StartColumn == 11
                              ));
        }

        [Test]
        public void TestScannerCanTokenizeOperators()
        {
            var lines = new List<string>
                            {
                                "var x : int := (4 + (6 * 2))/(2-0);",
                                "var y : bool := !true & true;",
                                "var z : bool := 3 < 2;",
                                "assert(x > 2);",
                                "assert(3 >= 2);",
                                "assert(x <= 12);",
                                "assert(x = 8);",
                            };

            var tokens = _scanner.Tokenize(lines);
            Assert.IsTrue(13 <= tokens.Count);

            #region Arithmetic
            Assert.IsTrue(tokens.Exists(x =>
                                  x.Lexeme == Operator.Plus &&
                                  x.Line == 1 &&
                                  x.StartColumn == 19
                              ));

            Assert.IsTrue(tokens.Exists(x =>
                                  x.Lexeme == Operator.Minus &&
                                  x.Line == 1 &&
                                  x.StartColumn == 32
                              ));

            Assert.IsTrue(tokens.Exists(x =>
                                  x.Lexeme == Operator.Multiply &&
                                  x.Line == 1 &&
                                  x.StartColumn == 24
                              ));

            Assert.IsTrue(tokens.Exists(x =>
                                  x.Lexeme == Operator.Divide &&
                                  x.Line == 1 &&
                                  x.StartColumn == 29
                              ));
            #endregion

            #region Parenthesis
            Assert.IsTrue(tokens.Exists(x =>
                                  x.Lexeme == Operator.ParenthesisLeft &&
                                  x.Line == 7 &&
                                  x.StartColumn == 7
                              ));

            Assert.IsTrue(tokens.Exists(x =>
                                  x.Lexeme == Operator.ParenthesisRight &&
                                  x.Line == 7 &&
                                  x.StartColumn == 13
                              ));

            #endregion

            #region Logical
            Assert.IsTrue(tokens.Exists(x =>
                                  x.Lexeme == Operator.And &&
                                  x.Line == 2 &&
                                  x.StartColumn == 23
                              ));

            Assert.IsTrue(tokens.Exists(x =>
                                  x.Lexeme == Operator.Not &&
                                  x.Line == 2 &&
                                  x.StartColumn == 17
                              ));

            #endregion

            #region Comparison

            Assert.IsTrue(tokens.Exists(x =>
                                  x.Lexeme == Operator.GreaterThan &&
                                  x.Line == 4 &&
                                  x.StartColumn == 10
                              ));

            Assert.IsTrue(tokens.Exists(x =>
                                  x.Lexeme == Operator.GreaterOrEqualThan &&
                                  x.Line == 5 &&
                                  x.StartColumn == 10
                              ));

            Assert.IsTrue(tokens.Exists(x =>
                                  x.Lexeme == Operator.LesserThan &&
                                  x.Line == 3 &&
                                  x.StartColumn == 19
                              ));

            Assert.IsTrue(tokens.Exists(x =>
                                  x.Lexeme == Operator.LesserOrEqualThan &&
                                  x.Line == 6 &&
                                  x.StartColumn == 10
                              ));

            Assert.IsTrue(tokens.Exists(x =>
                                  x.Lexeme == Operator.Equal &&
                                  x.Line == 7 &&
                                  x.StartColumn == 10
                              ));

            #endregion
        }

        [Test]
        public void TestCanTokenizeReservedKeyWords()
        {
            var lines = new List<string>
                            {
                                "var x : int := (4 + (6 * 2))/(2-0);",
                                "var y : bool := !true & true;",
                                "var z : bool := 3 < 2;",
                                "assert(x > 2);",
                                "assert(3 >= 2);",
                                "assert(x <= 12);",
                                "assert(x = 8);",
                                "for x in 0..nTimes-1 do", 
                                "print x;",
                                "end for;",
                                "read nTimes;"
                            };

            var tokens = _scanner.Tokenize(lines);
            Assert.IsTrue(12 <= tokens.Count);

            Assert.IsTrue(tokens.Exists(x =>
                                  x.Lexeme == ReservedKeyword.Assert &&
                                  x.Line == 4 &&
                                  x.StartColumn == 1
                              ));
            Assert.IsTrue(tokens.Exists(x =>
                                  x.Lexeme == ReservedKeyword.Do &&
                                  x.Line == 8 &&
                                  x.StartColumn == 22
                              ));
            Assert.IsTrue(tokens.Exists(x =>
                                  x.Lexeme == ReservedKeyword.End &&
                                  x.Line == 10 &&
                                  x.StartColumn == 1
                              ));
            Assert.IsTrue(tokens.Exists(x =>
                                  x.Lexeme == ReservedKeyword.For &&
                                  x.Line == 8 &&
                                  x.StartColumn == 1
                              ));
            Assert.IsTrue(tokens.Exists(x =>
                                  x.Lexeme == ReservedKeyword.In &&
                                  x.Line == 8 &&
                                  x.StartColumn == 7
                              ));
            Assert.IsTrue(tokens.Exists(x =>
                                  x.Lexeme == ReservedKeyword.Print &&
                                  x.Line == 9 &&
                                  x.StartColumn == 1
                              ));
            Assert.IsTrue(tokens.Exists(x =>
                                  x.Lexeme == ReservedKeyword.Read &&
                                  x.Line == 11 &&
                                  x.StartColumn == 1
                              ));
            Assert.IsTrue(tokens.Exists(x =>
                                  x.Lexeme == ReservedKeyword.Var &&
                                  x.Line == 3 &&
                                  x.StartColumn == 1
                              ));

            Assert.IsTrue(tokens.Exists(x =>
                                  x.Lexeme == ReservedKeyword.Assignment &&
                                  x.Line == 2 &&
                                  x.StartColumn == 14
                              ));
            Assert.IsTrue(tokens.Exists(x =>
                                  x.Lexeme == ReservedKeyword.Colon &&
                                  x.Line == 1 &&
                                  x.StartColumn == 7
                              ));
            Assert.IsTrue(tokens.Exists(x =>
                                  x.Lexeme == ReservedKeyword.Range &&
                                  x.Line == 8 &&
                                  x.StartColumn == 11
                              ));
            Assert.IsTrue(tokens.Exists(x =>
                                  x.Lexeme == ReservedKeyword.Semicolon &&
                                  x.Line == 6 &&
                                  x.StartColumn == 16
                              ));
        }


        [Test]
        public void TestCanTokenizeVariableIdentifiers()
        {
            var lines = new List<string>
                            {
                                "var x : int := 3;",
                                "var y2k : bool := true;",
                                "var CONSTANT_TEST : string := \"test\";",
                                "assert(x > 2);",
                                "assert(y2k != false);",
                                "assert(CONSTANT_TEST = \"test\");"
                            };

            var tokens = _scanner.Tokenize(lines);
            Assert.IsTrue(6 <= tokens.Count);

            Assert.IsTrue(tokens.Exists(x =>
                                        x.Lexeme == "x" &&
                                        x.Line == 1 &&
                                        x.StartColumn == 5
                              ));

            Assert.IsTrue(tokens.Exists(x =>
                                        x.Lexeme == "x" &&
                                        x.Line == 4 &&
                                        x.StartColumn == 8
                              ));

            Assert.IsTrue(tokens.Exists(x =>
                            x.Lexeme == "y2k" &&
                            x.Line == 2 &&
                            x.StartColumn == 5
                  ));

            Assert.IsTrue(tokens.Exists(x =>
                                        x.Lexeme == "y2k" &&
                                        x.Line == 5 &&
                                        x.StartColumn == 8
                              ));

            Assert.IsTrue(tokens.Exists(x =>
                            x.Lexeme == "CONSTANT_TEST" &&
                            x.Line == 3 &&
                            x.StartColumn == 5
                  ));

            Assert.IsTrue(tokens.Exists(x =>
                                        x.Lexeme == "CONSTANT_TEST" &&
                                        x.Line == 6 &&
                                        x.StartColumn == 8
                              ));
        }

        [Test]
        public void TestTokenTerminalBool()
        {
            var s = "var x : bool := true;";
            var tokens = _scanner.Tokenize(new List<string> { s });
            var token = (TokenTerminal<bool>)tokens.Find(x => x is TokenTerminal<bool>);

            Assert.IsTrue(token.Value);
            Assert.AreEqual("true", token.Lexeme);

            s = "var x : bool := false;";
            tokens = _scanner.Tokenize(new List<string> { s });
            token = (TokenTerminal<bool>)tokens.Find(x => x is TokenTerminal<bool>);

            Assert.IsFalse(token.Value);
            Assert.AreEqual("false", token.Lexeme);
        }

        [Test]
        public void TestTokenTerminalInt()
        {
            var s = "var x : int := 153;";
            var tokens = _scanner.Tokenize(new List<string> { s });
            var token = (TokenTerminal<int>)tokens.Find(x => x is TokenTerminal<int>);

            Assert.AreEqual(153, token.Value);
            Assert.AreEqual("153", token.Lexeme);

            s = "var x : int := -1133;";
            tokens = _scanner.Tokenize(new List<string> { s });
            token = null;
            Token previousToken = null;
            for (var i = 0; i < tokens.Count; i++)
            {
                if (tokens[i] is TokenTerminal<int>)
                {
                    token = (TokenTerminal<int>) tokens[i];
                    previousToken = tokens[i - 1];
                }
            }

            Assert.IsNotNull(token);
            Assert.IsNotNull(previousToken);

            Assert.AreEqual(1133, token.Value);
            Assert.AreEqual("1133", token.Lexeme);

            Assert.AreEqual(Operator.Minus, previousToken.Lexeme);
        }

        [Test]
        public void TestTokenTerminalString()
        {
            const string s = "var x : string := \"test\";";
            var tokens = _scanner.Tokenize(new List<string> { s });

            TokenTerminal<string> token = null;
            Token nextToken = null;

            for ( var i = 0; i < tokens.Count; i++ )
            {
                if ( tokens[i] is TokenTerminal<string> )
                {
                    token = (TokenTerminal<string>)tokens[i];
                    nextToken = tokens[i + 1];
                }
            }

            Assert.IsNotNull(token);
            Assert.IsNotNull(nextToken);

            Assert.AreEqual("test", token.Value);
            Assert.AreEqual("test", token.Lexeme);
            Assert.AreEqual(";", nextToken.Lexeme);
        }

        [Test]
        public void CanTokenizeCorrectSourceCode()
        {
            var lines = new List<string>
                            {
                                "var nTimes : int := 0;",
                                "var s : string := \"How many times?\";",
                                "print s;", 
                                "read nTimes;", 
                                "var x : int;",
                                "for x in 0..nTimes-1 do ",
                                "    print x;",
                                "    print \" : Hello, World!\\n\";",
                                "end for;",
                                "var b : bool := x = nTimes;",
                                "assert (b);"
                            };

            var tokens = _scanner.Tokenize(lines);
            Assert.IsTrue(57 == tokens.Count);
            var i = 0;

            var token = tokens[i++];
            Assert.AreEqual(ReservedKeyword.Var, token.Lexeme); Assert.AreEqual(1, token.Line); Assert.AreEqual(1, token.StartColumn); 

            token = tokens[i++];
            Assert.AreEqual("nTimes", token.Lexeme);             
            Assert.IsTrue(token is TokenIdentifier);
            Assert.AreEqual("nTimes", ((TokenIdentifier)token).Identifier);

            token = tokens[i++];
            Assert.AreEqual(ReservedKeyword.Colon, token.Lexeme);              
            token = tokens[i++];
            Assert.AreEqual(Type.Int, token.Lexeme);
            
            token = tokens[i++];
            Assert.AreEqual(ReservedKeyword.Assignment, token.Lexeme);  
            
            token = tokens[i++];
            Assert.AreEqual("0", token.Lexeme);  
            Assert.IsTrue(token is TokenTerminal<int>);
            Assert.AreEqual(0, ((TokenTerminal<int>)token).Value);

            token = tokens[i++];
            Assert.AreEqual(ReservedKeyword.Semicolon, token.Lexeme);  
            
            token = tokens[i++];
            Assert.AreEqual(ReservedKeyword.Var, token.Lexeme);  
            
            token = tokens[i++];
            Assert.AreEqual("s", token.Lexeme);  
            Assert.IsTrue(token is TokenIdentifier);
            Assert.AreEqual("s", ((TokenIdentifier)token).Identifier);

            token = tokens[i++];
            Assert.AreEqual(ReservedKeyword.Colon, token.Lexeme);  
            
            token = tokens[i++];
            Assert.AreEqual(Type.String, token.Lexeme);  
            
            token = tokens[i++];
            Assert.AreEqual(ReservedKeyword.Assignment, token.Lexeme);  
            
            token = tokens[i++];
            Assert.AreEqual("How many times?", token.Lexeme);  
            
            token = tokens[i++];
            Assert.AreEqual(ReservedKeyword.Semicolon, token.Lexeme);  
            
            token = tokens[i++];
            Assert.AreEqual(ReservedKeyword.Print, token.Lexeme);  
            
            token = tokens[i++];
            Assert.AreEqual("s", token.Lexeme);  
            Assert.IsTrue(token is TokenIdentifier);
            Assert.AreEqual("s", ((TokenIdentifier)token).Identifier);

            token = tokens[i++];
            Assert.AreEqual(ReservedKeyword.Semicolon, token.Lexeme);  
            
            token = tokens[i++];
            Assert.AreEqual(ReservedKeyword.Read, token.Lexeme);  
            
            token = tokens[i++]; 
            Assert.AreEqual("nTimes", token.Lexeme);  
            Assert.IsTrue(token is TokenIdentifier);
            Assert.AreEqual("nTimes", ((TokenIdentifier)token).Identifier);

            token = tokens[i++];
            Assert.AreEqual(ReservedKeyword.Semicolon, token.Lexeme);  
            
            token = tokens[i++];
            Assert.AreEqual(ReservedKeyword.Var, token.Lexeme);  
            
            token = tokens[i++];
            Assert.AreEqual("x", token.Lexeme);  
            Assert.IsTrue(token is TokenIdentifier);
            Assert.AreEqual("x", ((TokenIdentifier)token).Identifier);

            token = tokens[i++];
            Assert.AreEqual(ReservedKeyword.Colon, token.Lexeme);  
            
            token = tokens[i++];
            Assert.AreEqual(Type.Int, token.Lexeme);  
            
            token = tokens[i++];
            Assert.AreEqual(ReservedKeyword.Semicolon, token.Lexeme);  
            
            token = tokens[i++];
            Assert.AreEqual(ReservedKeyword.For, token.Lexeme);  
            
            token = tokens[i++];
            Assert.AreEqual("x", token.Lexeme);  
            Assert.IsTrue(token is TokenIdentifier);
            Assert.AreEqual("x", ((TokenIdentifier)token).Identifier);

            token = tokens[i++];
            Assert.AreEqual(ReservedKeyword.In, token.Lexeme);  
            
            token = tokens[i++];
            Assert.AreEqual("0", token.Lexeme);  
            Assert.IsTrue(token is TokenTerminal<int>);
            Assert.AreEqual(0, ((TokenTerminal<int>)token).Value);

            token = tokens[i++];
            Assert.AreEqual(ReservedKeyword.Range, token.Lexeme);  
            
            token = tokens[i++];
            Assert.AreEqual("nTimes", token.Lexeme);  
            Assert.IsTrue(token is TokenIdentifier);
            Assert.AreEqual("nTimes", ((TokenIdentifier)token).Identifier);

            token = tokens[i++];
            Assert.AreEqual(Operator.Minus, token.Lexeme);  
            
            token = tokens[i++];
            Assert.AreEqual("1", token.Lexeme);  
            Assert.IsTrue(token is TokenTerminal<int>);
            Assert.AreEqual(1, ((TokenTerminal<int>)token).Value);

            token = tokens[i++];
            Assert.AreEqual(ReservedKeyword.Do, token.Lexeme);  
            
            token = tokens[i++];
            Assert.AreEqual(ReservedKeyword.Print, token.Lexeme);  
            
            token = tokens[i++];
            Assert.AreEqual("x", token.Lexeme);  
            Assert.IsTrue(token is TokenIdentifier);
            Assert.AreEqual("x", ((TokenIdentifier)token).Identifier);

            token = tokens[i++];
            Assert.AreEqual(ReservedKeyword.Semicolon, token.Lexeme);  
            
            token = tokens[i++];
            Assert.AreEqual(ReservedKeyword.Print, token.Lexeme);  
            
            token = tokens[i++];
            Assert.AreEqual(" : Hello, World!\\n", token.Lexeme);  
            Assert.IsTrue(token is TokenTerminal<string>);
            Assert.AreEqual(" : Hello, World!\\n", ((TokenTerminal<string>)token).Value);

            token = tokens[i++];
            Assert.AreEqual(ReservedKeyword.Semicolon, token.Lexeme);  
            
            token = tokens[i++];
            Assert.AreEqual(ReservedKeyword.End, token.Lexeme);  
            
            token = tokens[i++];
            Assert.AreEqual(ReservedKeyword.For, token.Lexeme);  
            
            token = tokens[i++];
            Assert.AreEqual(ReservedKeyword.Semicolon, token.Lexeme);  
            
            token = tokens[i++];
            Assert.AreEqual(ReservedKeyword.Var, token.Lexeme);  
            
            token = tokens[i++];
            Assert.AreEqual("b", token.Lexeme);  
            Assert.IsTrue(token is TokenIdentifier);
            Assert.AreEqual("b", ((TokenIdentifier)token).Identifier);

            token = tokens[i++];
            Assert.AreEqual(ReservedKeyword.Colon, token.Lexeme);  
            
            token = tokens[i++];
            Assert.AreEqual(Type.Bool, token.Lexeme);  
            
            token = tokens[i++];
            Assert.AreEqual(ReservedKeyword.Assignment, token.Lexeme);  
            
            token = tokens[i++];
            Assert.AreEqual("x", token.Lexeme);  
            Assert.IsTrue(token is TokenIdentifier);
            Assert.AreEqual("x", ((TokenIdentifier)token).Identifier);

            token = tokens[i++];
            Assert.AreEqual(Operator.Equal, token.Lexeme);  
            
            token = tokens[i++];
            Assert.AreEqual("nTimes", token.Lexeme);  
            Assert.IsTrue(token is TokenIdentifier);
            Assert.AreEqual("nTimes", ((TokenIdentifier)token).Identifier);

            token = tokens[i++];
            Assert.AreEqual(ReservedKeyword.Semicolon, token.Lexeme);  
            
            token = tokens[i++];
            Assert.AreEqual(ReservedKeyword.Assert, token.Lexeme);  
            
            token = tokens[i++];
            Assert.AreEqual(Operator.ParenthesisLeft, token.Lexeme);  
            
            token = tokens[i++];
            Assert.AreEqual("b", token.Lexeme);  
            Assert.IsTrue(token is TokenIdentifier);
            Assert.AreEqual("b", ((TokenIdentifier)token).Identifier);

            token = tokens[i++];
            Assert.AreEqual(Operator.ParenthesisRight, token.Lexeme);  
            
            token = tokens[i++];
            Assert.AreEqual(ReservedKeyword.Semicolon, token.Lexeme);  

            Assert.AreEqual(tokens.Count, i);
        }

        [Test]
        public void CanSkipLineComments()
        {
            var lines = new List<string>
                            {
                                "var i : int; // := 0;",
                                "//var s : string := \"How many times?\";",
                                "var b : bool := true;", 
                            };

            var tokens = _scanner.Tokenize(lines);
            Assert.AreEqual(12, tokens.Count);
            var i = 0;

            var token = tokens[i++];
            Assert.AreEqual(ReservedKeyword.Var, token.Lexeme);
            Assert.AreEqual(1, token.Line);

            token = tokens[i++];
            Assert.AreEqual("i", token.Lexeme);
            Assert.AreEqual("i", ((TokenIdentifier)token).Identifier);
            Assert.AreEqual(1, token.Line);

            token = tokens[i++];
            Assert.AreEqual(ReservedKeyword.Colon, token.Lexeme);
            Assert.AreEqual(1, token.Line);

            token = tokens[i++];
            Assert.AreEqual(Type.Int, token.Lexeme);
            Assert.AreEqual(1, token.Line);

            token = tokens[i++];
            Assert.AreEqual(ReservedKeyword.Semicolon, token.Lexeme);
            Assert.AreEqual(1, token.Line);

            token = tokens[i++];
            Assert.AreEqual(ReservedKeyword.Var, token.Lexeme);
            Assert.AreEqual(3, token.Line);

            token = tokens[i++];
            Assert.AreEqual("b", token.Lexeme);
            Assert.AreEqual("b", ((TokenIdentifier)token).Identifier);
            Assert.AreEqual(3, token.Line);

            token = tokens[i++];
            Assert.AreEqual(ReservedKeyword.Colon, token.Lexeme);
            Assert.AreEqual(3, token.Line);

            token = tokens[i++];
            Assert.AreEqual(Type.Bool, token.Lexeme);
            Assert.AreEqual(3, token.Line);

            token = tokens[i++];
            Assert.AreEqual(ReservedKeyword.Assignment, token.Lexeme);
            Assert.AreEqual(3, token.Line);

            token = tokens[i++];
            Assert.AreEqual("true", token.Lexeme);
            Assert.IsTrue(((TokenTerminal<bool>)token).Value);
            Assert.AreEqual(3, token.Line);

            token = tokens[i++];
            Assert.AreEqual(ReservedKeyword.Semicolon, token.Lexeme);
            Assert.AreEqual(3, token.Line);

        }
    }
}
