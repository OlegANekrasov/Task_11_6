using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_11_6.Models;

namespace Task_11_6.Services
{
    public interface IStorage
    {
        /// <summary>
        /// Получение сессии пользователя по идентификатору
        /// </summary>
        Session GetSession(long chatId);
    }
}
