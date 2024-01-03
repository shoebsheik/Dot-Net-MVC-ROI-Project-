using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniqueTrade_App.CommonFunction
{
    public class SqlFIlter
    {
        public static string Filter(string filtername)
        {
            string FILLER;
            FILLER = filtername;

        if (string.IsNullOrEmpty(filtername)) {

                filtername = string.Empty;


            }
        else
            {
                FILLER = FILLER.Replace("UPDATE", "XXXXXXX");
                FILLER = FILLER.Replace("SELECT", "XXXXXXX");
                FILLER = FILLER.Replace("DELETE", "XXXXXXX");
                FILLER = FILLER.Replace("TRUNCATE", "XXXXX");
                FILLER = FILLER.Replace("UNION", "XXXXXXX");
                FILLER = FILLER.Replace("CASE", "XXXXXXX");
                FILLER = FILLER.Replace("WHEN", "XXXXXXX");
                FILLER = FILLER.Replace("THEN", "XXXXXXX");
                FILLER = FILLER.Replace("IF", "XXXXXXX");
                FILLER = FILLER.Replace("ELSE", "XXXXXXX");
                FILLER = FILLER.Replace("COUNT", "XXXXXXX");
                FILLER = FILLER.Replace("FROM", "XXXXXXX");
                FILLER = FILLER.Replace("END", "XXXXXXX");
                FILLER = FILLER.Replace("*", "XXXXXXX");
                FILLER = FILLER.Replace(":", "XXXXXXX");
                FILLER = FILLER.Replace(";", "XXXXXXX");
                FILLER = FILLER.Replace("(", "XXXXXXX");
                FILLER = FILLER.Replace(")", "XXXXXXX");
                FILLER = FILLER.Replace("/", "XXXXXXX");
                FILLER = FILLER.Replace("\\", "XXXXXXX");
                FILLER = FILLER.Replace("=", "XXXXXXX");
                FILLER = FILLER.Replace("+", "XXXXXXX");
                FILLER = FILLER.Replace("%", "XXXXXXX");
                FILLER = FILLER.Replace("&", "XXXXXXX");
                FILLER = FILLER.Replace("!", "XXXXXXX");
                FILLER = FILLER.Replace("'", "XXXXXXX");
                FILLER = FILLER.Replace("[", "XXXXXXX");
                FILLER = FILLER.Replace("]", "XXXXXXX");
                FILLER = FILLER.Replace(">", "XXXXXXX");
                FILLER = FILLER.Replace("<", "XXXXXXX");
                FILLER = FILLER.Replace(",", "XXXXXXX");
                FILLER = FILLER.Replace(".ini", "XXXXXXX");
            }


            return FILLER;


        }

    }
}
    