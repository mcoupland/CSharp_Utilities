using System;

namespace Utilities
{
    public class Numbers
    {   
        #region Numeric Methods
        public static ushort GetIntFloor(object decimal_value)
        {
            string object_type = decimal_value.GetType().Name;
            if (object_type == "float" || object_type == "double" || object_type == "decimal")
            {
                return Convert.ToUInt16(Math.Floor((decimal)decimal_value));
            }
            return Convert.ToUInt16(decimal_value);
        }
        public static ushort GetIntCeiling(object decimal_value)
        {
            string object_type = decimal_value.GetType().Name;
            if (object_type == "float" || object_type == "double" || object_type == "decimal")
            {
                return Convert.ToUInt16(Math.Ceiling((decimal)decimal_value));
            }
            return Convert.ToUInt16(decimal_value);
        }
        #endregion
    }
}
