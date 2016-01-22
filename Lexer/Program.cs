using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace lex
{
    class Program
    {
        static void Main(string[] args)
        {
            Lexer l = new Lexer("source.mc");
            l.Scan();
            l.PrintTokenList();

            Parser p = new Parser(l._Tokens);
            p.Parse();
            p.PrintSyntaxTree();

            Console.ReadLine();
        }
    }
}
