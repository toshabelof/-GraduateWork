using System;
using System.Collections.Generic;
using System.Text;

namespace HRSaveTime_Server
{
    class Inquiry
    {
        public String SendInq(string header, string body)
        {
            switch (header)
            {
                case "getprnr":
                    {
                        return getPrnr(body);
                    }
              
            }
            return "Команда не распознана";
        }

        public String getPrnr(string prnr)
        {
            return "Инфа о т.н." + prnr;
        }
    }
}
