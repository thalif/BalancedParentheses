using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalancedParentheses
{
    class Program
    {
        static void Main(string[] args)
        {
            //string Equation = Console.ReadLine();
            string Equation = "(1+((2+3)*(4*5)))";
            string Answer = "";

            if (EquationValidation(Equation))
                Answer = Calculate(Equation);
            else
                Answer = "Unbalance Equation invalid";

            Console.WriteLine(Answer);
            Console.ReadKey();
        }
        public static bool EquationValidation(string Equation)
        {
            int openCount = 0, closeCount = 0;
            for (int i = 0; i < Equation.Length; i++)
                if (Equation[i] == '(')
                    openCount++;
                else if (Equation[i] == ')')
                    closeCount++;

            if (openCount == closeCount)
            {
                for (int i = 0; i < Equation.Length - 1; i++)
                {
                    if (Equation[i] == ')' && IsInteger(Equation[i + 1]))
                        return false;
                    else if (IsInteger(Equation[i]) && Equation[i + 1] == '(')
                        return false;
                    else if (Equation[i] == ')' && Equation[i + 1] == '(')
                        return false;
                }
            }
            else
            {
                return false;
            }
            return true;
        }

        public static string Calculate(string e)
        {
            
            int Close = int.MinValue;
            int Open = int.MinValue;
            for(int i = e.Length - 1; i >= 0; i--)
            {
                if (IsCloseBracket(e[i]))
                {
                    Close = i;
                }
                else if(IsOpenBracket(e[i]))
                {
                    Open = i;
                    break;
                }
            }

            string substring = e.Substring(Open, (Close + 1) - Open);
            string FinalValue = e.Replace(substring, Calc(substring));
            try
            {
                int value = int.Parse(FinalValue);
            }
            catch
            {
                FinalValue = Calculate(FinalValue);
            }
            return FinalValue;
        }

        public static string Calc(string part)
        {
            part = part.Replace("(", string.Empty);
            part = part.Replace(")", string.Empty);
            if (part.Contains('*'))
            {
                string[] items = part.Split('*');
                int product = int.Parse(items[0]) * int.Parse(items[1]);
                return product.ToString();
            }
            else if(part.Contains('+'))
            {
                string[] items = part.Split('+');
                int sum = int.Parse(items[0]) + int.Parse(items[1]);
                return sum.ToString();
            }
            else if(part.Contains('-'))
            {
                string[] items = part.Split('-');
                int subtract = int.Parse(items[0]) - int.Parse(items[1]);
                return subtract.ToString();
            }
            else
            {
                return "0";
            }
        }

        public static bool IsOpenBracket(char c)
        {
            return c == '(';
        }
        public static bool IsCloseBracket(char c)
        {
            return c == ')';
        }
        public static bool IsInteger(char c)
        {
            try
            {
                int check = int.Parse(c.ToString());
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
