using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace lex
{
    public enum TokenType
    {
        undefined,
        Empty,
        EndOfFile,

        Error,

        If,
        Else,
        For,
        Assignment,

        IORead,
        IOWrite,

        Identifier,
        Digit,

        EQ,
        LT,
        GT,

        ADD,
        SUB,
        MUL,
        DIV,

        LBrace,
        RBrace,
        LParan,
        RParan,

        Semicolons,//    分号

        Comment,
    }

    public struct TokenAttribute
    {
        public string stringVal;
        public int intVal;
        public bool isIntValUndefined;
    }

    public struct TokenRecord
    {
        public TokenType tokenType;
        public TokenAttribute tokenAttribute;
    }

    public enum State
    {
        undefined,
        START,
        ID,
        DIGIT,
        COMMENT,
        DONE,
    }

    public enum NodeType
    {
        undefined,
        Stmt,
        Expr,
    }

    public enum StmtType
    {
        undefined,
        If,
        For,
        Assignment,
        IORead,
        IOWrite,
    }
    public enum ExpType
    {
        undefined,
        Comparison_LT,
        Comparison_MT,
        Comparison_EQ,
        Operator_ADD,
        Operator_SUB,
        Operator_MUL,
        Operator_DIV,
        Const,
        Identifier,
    }

    public enum ValidMember
    {
        undefined,
        StmtType,
        ExprType,
        NodeType,
        TokenType,
        Type,
    }

    public enum ValidValue
    {
        undefined,
        intVal,
        strVal,
    }

    public class Node
    {
        public Node[] Childs { get; set; }

        public Node NextSubling { get; set; }

        public NodeType NodeType { get; set; }

        public string name { get; set; }


        public StmtType StmtType { get; set; }

        public ExpType ExprType { get; set; }

        public TokenType TokenType { get; set; }


        public int intVal { get; set; }

        public string strVal { get; set; }


        public Node(NodeType nt)
        {
            Childs = new Node[4];
            for (int i = 0; i != 4; ++i)
                Childs[i] = null;

            NextSubling = null;

            NodeType = nt;
        }

    }

    public class Tree
    {
        private Node _Head { get; set; }
        private int _Length { get; set; }

        public Tree(Node n)
        {
            _Head = n;
            _Length = 0;
        }
    }


}
