using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lex
{
    public class Lexer
    {
        public TokenRecord[] _Tokens { get; set; }
        private string _SourceFileName { get; set; }
        private int _TokenCount { get; set; }
        private int _MaxTokenValueLength { get; set; }

        public Lexer(string fileName, int maxTokenNum = 1000)
        {
            _Tokens = new TokenRecord[maxTokenNum];
            _SourceFileName = fileName;
        }

        public void Scan()
        {
            State state = State.START;
            TokenType type = TokenType.Identifier;
            int index = 0;
            string strVal = string.Empty;

            byte[] data = new byte[1];
            char c;

            using (FileStream fs = File.Open(_SourceFileName, FileMode.Open))
            {
                while (type != TokenType.EndOfFile)
                {
                    while (state != State.DONE)
                    {
                        if (fs.Read(data, 0, 1) == 0)
                        {
                            type = TokenType.EndOfFile;
                            strVal += "EOF";
                            break;
                        }

                        c = (char)data[0];
                        switch (state)
                        {
                            case State.START:
                                {
                                    if (char.IsLetter(c))
                                    {
                                        state = State.ID;
                                        strVal += c;
                                    }
                                    else if (char.IsDigit(c))
                                    {
                                        state = State.DIGIT;
                                        strVal += c;
                                    }
                                    else if (c == '=')
                                    {
                                        fs.Read(data, 0, 1);
                                        char c_next = (char)data[0];
                                        if (c_next == '=')
                                        {
                                            type = TokenType.EQ;
                                            state = State.DONE;
                                        }
                                        else
                                        {
                                            type = TokenType.Assignment;
                                            state = State.DONE;
                                            fs.Seek(-1, SeekOrigin.Current);
                                        }

                                        strVal += c;
                                    }
                                    else if (c == '/')
                                    {
                                        //  向后读一位以此判断是否处于 comment state
                                        fs.Read(data, 0, 1);
                                        char c_next = (char)data[0];
                                        if (c_next == '/')
                                        {
                                            //type = TokenType.Comment;
                                            //state = State.COMMENT;
                                        }
                                        else
                                        {
                                            type = TokenType.DIV;
                                            strVal += c;
                                            state = State.DONE;

                                            fs.Seek(-1, SeekOrigin.Current);
                                        }
                                    }
                                    else if (c == ' ' || c == '\n' || c == '\r' || c == '\t')
                                    {
                                        //  do nothing
                                    }
                                    else
                                    {
                                        switch (c)
                                        {
                                            case '+':
                                                type = TokenType.ADD; break;
                                            case '-':
                                                type = TokenType.SUB; break;
                                            case '*':
                                                type = TokenType.MUL; break;
                                            case '{':
                                                type = TokenType.LBrace; break;
                                            case '}':
                                                type = TokenType.RBrace; break;
                                            case '(':
                                                type = TokenType.LParan; break;
                                            case ')':
                                                type = TokenType.RParan; break;
                                            case '<':
                                                type = TokenType.LT; break;
                                            case '>':
                                                type = TokenType.GT; break;
                                            case ';':
                                                type = TokenType.Semicolons; break;
                                        }
                                        strVal += c;
                                        state = State.DONE;
                                    }
                                    break;
                                }
                            case State.COMMENT://   此时两个'/'都已被读入
                                {
                                    if (c == '\n')
                                    {
                                        //strVal = "......";
                                        //state = State.DONE;
                                    }
                            
                                    break;
                                }
                            case State.ID:
                                {
                                    if (char.IsLetter(c) || char.IsDigit(c))
                                        strVal += c;
                                    else
                                    {
                                        if (strVal == "if")
                                            type = TokenType.If;
                                        else if (strVal == "else")
                                            type = TokenType.Else;
                                        else if (strVal == "read")
                                            type = TokenType.IORead;
                                        else if (strVal == "write")
                                            type = TokenType.IOWrite;
                                        else if (strVal == "for")
                                            type = TokenType.For;
                                        else
                                            type = TokenType.Identifier;

                                        state = State.DONE;
                                        fs.Seek(-1, SeekOrigin.Current);
                                    }

                                    break;
                                }
                            case State.DIGIT:
                                {
                                    if (!char.IsDigit(c))
                                    {
                                        state = State.DONE;
                                        type = TokenType.Digit;
                                        fs.Seek(-1, SeekOrigin.Current);
                                    }
                                    else
                                        strVal += c;

                                    break;
                                }
                        }
                    }
                    state = State.START;
                    _Tokens[index].tokenType = type;
                    if (type == TokenType.Digit)
                    {
                        _Tokens[index].tokenAttribute.intVal = int.Parse(strVal);
                        _Tokens[index].tokenAttribute.isIntValUndefined = true;
                    }
                    else
                    {
                        _Tokens[index].tokenAttribute.stringVal = strVal;
                        if (strVal.Length > _MaxTokenValueLength)
                            _MaxTokenValueLength = strVal.Length;
                    }

                    strVal = string.Empty;
                    index++;
                }
            }
            _TokenCount = index;
        }

        public void PrintTokenList()
        {
            //  为了对齐输出
            _MaxTokenValueLength += 2;
            if (_MaxTokenValueLength < 11)
                _MaxTokenValueLength = 11;

            string format = "{3:D4}  < {0,-10} , intVal= {1,-13} , strVal= {2,-" + _MaxTokenValueLength + "} >";
            for (int i = 0; i != _TokenCount; ++i)
            {
                Console.WriteLine(format,
                    _Tokens[i].tokenType.ToString(),
                    !_Tokens[i].tokenAttribute.isIntValUndefined ? "<undefined>" : _Tokens[i].tokenAttribute.intVal.ToString(),
                    _Tokens[i].tokenAttribute.stringVal == null ? "<undefined>" : _Tokens[i].tokenAttribute.stringVal == string.Empty || _Tokens[i].tokenAttribute.stringVal == " " ? "<empty>" : "\"" + _Tokens[i].tokenAttribute.stringVal + "\"",
                    i
                    );
            }
        }
    }
}
