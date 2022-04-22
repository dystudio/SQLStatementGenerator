using System;
using System.Collections.Generic;
using System.Text;

namespace SqlStatementGenerator
{
    public class Validation
    {
        /// <summary>
        /// Returns true if the given object is a valid number, or false if it's not
        /// </summary>
        /// <param name="sDateTime"></param>
        /// <returns></returns>
        public static bool IsNumeric(Object objValue)
        {
            bool _Valid = false;
            
            try
            {
                double y = Convert.ToDouble(objValue);
                _Valid = true;
                return _Valid;
            }
            catch
            {
                _Valid = false;
            }

            try
            {                
                int x = Convert.ToInt32(objValue);                
                _Valid = true;
                return _Valid;
            }
            catch
            {
                _Valid = false;
            }

            return _Valid;
        }

        /// <summary>
        /// Returns true if the given string is a valid date string, or false if it's not
        /// </summary>
        /// <param name="sDateTime"></param>
        /// <returns></returns>
        public static bool IsDateTime(string sDateTime)
        {
            bool bIsDateTime = false;

            try
            {
                System.DateTime.Parse(sDateTime);
                bIsDateTime = true;
            }
            catch
            {
                bIsDateTime = false;
            }

            return bIsDateTime;
        }
    }
}
