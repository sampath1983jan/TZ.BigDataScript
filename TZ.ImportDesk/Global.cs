namespace TZ.ImportDesk
{
    public static class Global
    {
        public static CompExtention.AttributeType GetAttributeType(int tzFieldID)
        {
            if (tzFieldID == 0)
            {
                return CompExtention.AttributeType._date;

            }
            else if (tzFieldID == 1)
            {
                return CompExtention.AttributeType._datetime;
            }
            else if (tzFieldID == 22)
            {
                return CompExtention.AttributeType._componentlookup;
            }
            else if (tzFieldID == 13)
            {
                return CompExtention.AttributeType._decimal;
            }
            else if (tzFieldID == 12)
            {
                return CompExtention.AttributeType._decimal;
            }
            else if (tzFieldID == 20)
            {
                return CompExtention.AttributeType._file;
            }
            else if (tzFieldID == 19)
            {
                return CompExtention.AttributeType._string;
            }
            else if (tzFieldID == 6)
            {
                return CompExtention.AttributeType._longstring;
            }
            else if (tzFieldID == 14)
            {
                return CompExtention.AttributeType._multilookup;
            }
            else if (tzFieldID == 1)
            {
                return CompExtention.AttributeType._number;
            }
            else if (tzFieldID == 18)
            {
                return CompExtention.AttributeType._picture;
            }
            else if (tzFieldID == 8)
            {
                return CompExtention.AttributeType._bit;
            }
            else if (tzFieldID == 3)
            {
                return CompExtention.AttributeType._lookup;
            }
            else if (tzFieldID == 2)
            {
                return CompExtention.AttributeType._string;
            }
            else if (tzFieldID == 21)
            {
                return CompExtention.AttributeType._datetime;
            }
            else
            {
                return CompExtention.AttributeType._string;
            }

        }
        public static CompExtention.ComponentType GetType(int type)
        {
            if (type == 1)
            {
                return CompExtention.ComponentType.core;
            }
            else if (type == 2)
            {
                return CompExtention.ComponentType.link;
            }
            else if (type == 3)
            {
                return CompExtention.ComponentType.attribute;
            }
            else if (type == 4)
            {
                return CompExtention.ComponentType.core;
            }
            else
            {
                return CompExtention.ComponentType.core;
            }
        }
    }
}
