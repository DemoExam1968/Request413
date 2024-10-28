using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ОООТехносервис.Classes
{
    public class Helper
    {
        //Объявили объект для связи с БД
        public static Model.DBRequests DB;
        //Объект-пользователь, вошедший в систему
        public static Model.User user;
    }
}
