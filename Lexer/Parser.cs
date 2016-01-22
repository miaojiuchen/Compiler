using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lex
{
    class Parser
    {
        private TokenRecord[] TokenList { get; set; }
        private int index { get; set; }
        public Node SyntaxTree { get; set; }

        public Parser(TokenRecord[] tlist)
        {
            TokenList = tlist;
            SyntaxTree = null;
        }

        public void Parse()
        {
            SyntaxTree = StmtSequence();
        }

        public void PrintSyntaxTree()
        {
            _PrintSyntaxTree(SyntaxTree, 0);
        }

        public void _PrintSyntaxTree(Node pTree, int prevLen)
        {
            if (pTree != null)
            {
                int len = 0;
                switch (pTree.NodeType)
                {
                    case NodeType.Stmt:
                        Console.Write("<{1},{0}>", pTree.StmtType, pTree.NodeType);
                        len = pTree.StmtType.ToString().Length;
                        break;
                    case NodeType.Expr:
                        string val,format;
                        format = "<{1},{0},{2}>";
                        switch (pTree.ExprType)
                        {
                            case ExpType.Identifier:
                                val = pTree.name;
                                Console.Write(format, pTree.ExprType, pTree.NodeType, val);
                                break;
                            case ExpType.Const:
                                val = pTree.intVal.ToString();
                                Console.Write(format, pTree.ExprType, pTree.NodeType, val);
                                break;
                            default:
                                Console.Write("<{1},{0}>", pTree.ExprType, pTree.NodeType);
                                break;

                        }
                        len = pTree.ExprType.ToString().Length;
                        break;
                }

                StringBuilder sb1 = new StringBuilder();
                StringBuilder sb2 = new StringBuilder();
                len = len + 1 + (prevLen != 0 ? prevLen + 3 : 0);
                for (int j = 0; j != len; ++j)
                    sb2.Append(' ');

                sb1.Append(sb2.ToString() + "|———");

                if (pTree.Childs[0] != null)
                {
                    Console.Write("\r\n" + sb1.ToString());
                    _PrintSyntaxTree(pTree.Childs[0], len);

                    if (pTree.Childs[1] != null)
                    {
                        Console.Write("\r\n\n" + sb1.ToString());
                        _PrintSyntaxTree(pTree.Childs[1], len);
                        if (pTree.Childs[2] != null)
                        {
                            Console.Write("\r\n\n" + sb1.ToString());
                            _PrintSyntaxTree(pTree.Childs[2], len);
                            if (pTree.Childs[3] != null)
                            {
                                Console.Write("\r\n\n" + sb1.ToString());
                                _PrintSyntaxTree(pTree.Childs[3], len);
                            }
                        }
                    }
                }
                pTree = pTree.NextSubling;
                if (pTree != null)
                {
                    Console.WriteLine();
                    _PrintSyntaxTree(pTree, prevLen);
                }
            }
        }

        private Node StmtSequence()
        {
            Node n = Stmt();
            Node pn = n;
            Match(TokenType.Semicolons, true);

            while (!(
                Match(TokenType.EndOfFile, false) ||
                Match(TokenType.RBrace, false)
            ))
            {
                pn.NextSubling = Stmt();
                Match(TokenType.Semicolons, true);
                pn = pn.NextSubling;
            }
            return n;
        }


        private Node Stmt()
        {
            if (Match(TokenType.If, true))
            {
                return If_Stmt();
            }
            else if (Match(TokenType.For, true))
            {
                return For_Stmt();
            }
            else if (Match(TokenType.IOWrite, true))
            {
                return IOWrite_Stmt();
            }
            else if (Match(TokenType.IORead, true))
            {
                return IORead_Stmt();
            }
            else if (Match(TokenType.Identifier, false))
            {
                return Assignment_Stmt();
            }
            else
            {
                Console.WriteLine("Error occurs in parsing process, " + TokenList[index].tokenType);
            }

            return null;
        }

        private Node Expr()
        {
            Node n = new Node(NodeType.Expr);
            n.Childs[0] = ArithmeticExpr();
            if (Match(TokenType.LT, false) || Match(TokenType.GT,false)|| Match(TokenType.EQ, false))
            {
                n.TokenType = TokenList[index].tokenType;
                switch (n.TokenType)
                {
                    case TokenType.LT:
                        n.ExprType = ExpType.Comparison_LT; break;
                    case TokenType.GT:
                        n.ExprType = ExpType.Comparison_MT; break;
                    case TokenType.EQ:
                        n.ExprType = ExpType.Comparison_EQ; break;
                    default:
                        n.ExprType = ExpType.undefined; break;
                }
                Match(n.TokenType, true);
                n.Childs[1] = ArithmeticExpr();
                return n;
            }
            return n.Childs[0];
        }

        private Node ArithmeticExpr()
        {
            Node n = new Node(NodeType.Expr);

            n.Childs[0] = Term();
            if (Match(TokenType.ADD, false) || Match(TokenType.SUB, false))
            {
                n.TokenType = TokenList[index].tokenType;
                switch (n.TokenType)
                {
                    case TokenType.ADD:
                        n.ExprType = ExpType.Operator_ADD; break;
                    case TokenType.SUB:
                        n.ExprType = ExpType.Operator_SUB; break;
                    default:
                        throw new Exception();
                }
                Match(n.TokenType, true);
                n.Childs[1] = ArithmeticExpr();
                return n;
            }
            return n.Childs[0];
        }

        private Node Term()
        {
            Node n = new Node(NodeType.Expr);

            n.Childs[0] = Factor();
            if (Match(TokenType.MUL, false) || Match(TokenType.DIV, false))
            {
                n.TokenType = TokenList[index].tokenType;
                switch (n.TokenType)
                {
                    case TokenType.MUL:
                        n.ExprType = ExpType.Operator_MUL; break;
                    case TokenType.DIV:
                        n.ExprType = ExpType.Operator_DIV; break;
                    default:
                        throw new Exception();
                }
                Match(n.TokenType, true);
                n.Childs[1] = Term();
                return n;
            }
            return n.Childs[0];
        }

        private Node Factor()
        {
            Node n = new Node(NodeType.Expr);

            if (Match(TokenType.LParan, true))
            {
                n = Expr();
                Match(TokenType.RParan, true);
            }
            else if (Match(TokenType.Identifier, false))
            {
                n.ExprType = ExpType.Identifier;
                n.name = TokenList[index].tokenAttribute.stringVal;
                Match(TokenType.Identifier, true);
            }
            else if (Match(TokenType.Digit, false))
            {
                n.ExprType = ExpType.Const;
                n.intVal = TokenList[index].tokenAttribute.intVal;
                Match(TokenType.Digit, true);
            }

            return n;
        }

        private bool Match(TokenType t, bool skip)
        {
            if (TokenList[index].tokenType == t)
            {
                if (skip)
                    index++;
                return true;
            }
            else
                return false;
        }

        private Node If_Stmt()
        {
            Node n = new Node(NodeType.Stmt);

            n.StmtType = StmtType.If;

            Match(TokenType.LParan, true);
            n.Childs[0] = Expr();                     //  (bool expression)
            Match(TokenType.RParan, true);

            if (Match(TokenType.LBrace, true))
            {
                n.Childs[1] = StmtSequence();                  //  {stmt queue}
                Match(TokenType.RBrace, true);

                if (Match(TokenType.Else, true))
                {
                    if (Match(TokenType.LBrace, true))
                    {
                        Match(TokenType.LBrace, true);
                        n.Childs[2] = StmtSequence();          //  {stmt queue}
                        Match(TokenType.RBrace, true);
                    }
                    else
                        n.Childs[2] = Stmt();               //  single stmt            
                }
            }
            else
            {
                n.Childs[1] = Stmt();                       //  single stmt
                Match(TokenType.Semicolons, true);
                if (Match(TokenType.Else, true))
                {
                    if (Match(TokenType.LBrace, true))
                    {
                        Match(TokenType.LBrace, true);
                        n.Childs[2] = StmtSequence();          //  {stmt queue}
                        Match(TokenType.RBrace, true);
                    }
                    else
                        n.Childs[2] = Stmt();               //  single stmt
                }
            }

            return n;
        }

        private Node For_Stmt()
        {
            Node n = new Node(NodeType.Stmt);
            n.StmtType = StmtType.For;

            Match(TokenType.LParan, true);

            //  do some type check for "for" stmt below
            n.Childs[0] = Stmt();
            Match(TokenType.Semicolons, true);
            n.Childs[1] = Expr();
            Match(TokenType.Semicolons, true);
            n.Childs[2] = Stmt();
            Match(TokenType.Semicolons, true);

            Match(TokenType.RParan, true);

            if (Match(TokenType.LBrace, true))
            {
                n.Childs[3] = StmtSequence();
                Match(TokenType.RBrace, true);
            }
            else
                n.Childs[3] = Stmt();

            return n;
        }

        private Node IOWrite_Stmt()
        {
            Node n = new Node(NodeType.Stmt);
            n.StmtType = StmtType.IOWrite;
            n.Childs[0] = Expr();
            return n;
        }

        private Node IORead_Stmt()
        {
            Node n = new Node(NodeType.Stmt);
            n.StmtType = StmtType.IORead;
            n.Childs[0] = Factor();
            return n;
        }

        private Node Assignment_Stmt()
        {
            Node n = new Node(NodeType.Stmt);
            n.StmtType = StmtType.Assignment;
            n.Childs[0] = Factor();
            Match(TokenType.Assignment, true);
            n.Childs[1] = Expr();
            return n;
        }
    }
}
