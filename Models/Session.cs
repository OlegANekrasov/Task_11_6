using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_11_6.Models
{
    public class Session
    {
        /// <summary>
        /// Тип функции
        /// 1 - подсчёт количества символов в тексте 
        /// 2 - вычисление суммы чисел
        /// </summary>
        public string TypeFunctions { get; set; }

        /// <summary>
        /// Режим обработки текста ввода
        /// </summary>
        public bool ProcessingMode { get; set; } = false;
    }
}
