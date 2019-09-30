using System;

namespace ProMassSpammer.Data.Entities
{
    public abstract class EntityBase
    {
        protected static T ConvertToEnum<T>(int enumIntegerValue) where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException("T must be an enumerated type");

            return (T) Enum.ToObject(typeof(T), enumIntegerValue);
        }
    }
}