using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_11_6.Services
{
    public class ProcessInputString : IProcess
    {
        public string Process(string inputString, int typeFunctions)
        {
            if(typeFunctions == 1)
            {
                return $"В вашем сообщении {inputString.Length} символов";
            }
            else
            {
                long sum = 0;
                string[] words = inputString.Split(' ');
                foreach (var word in words)
                {
                    if(long.TryParse(word, out long number))
                    {
                        sum += number;
                    }
                    else
                    {
                        return "Введено не корректное число.";
                    }
                }

                return $"сумма чисел: {sum}";
            }
        }
    }
}
