namespace Models
{
    public static class EntityBaseValidator
    {
        public const int Invalid_Id = -1;

        public static int ToLong(this int value)
        {
            if (value < 0)
            {
                return Invalid_Id;
            }
            return value;
        }

        public static bool IsInvalidId(this int value) 
            => value< 0;

        public static bool IsValidId(this int value)
            => value > 0;

        public static bool IsNotNull(this object value)
            => value != null;

        public static bool IsNull(this object value)
            => value == null;
    }
}
