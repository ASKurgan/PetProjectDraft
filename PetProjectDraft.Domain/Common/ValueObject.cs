using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProjectDraft.Domain.Common
{
    public abstract class ValueObject
    {
        protected abstract IEnumerable<object> GetEqualityComponents();

        public override bool Equals(object? obj)
        {
            if (obj == null)
                return false;

            if (GetType() != obj.GetType())
                return false;

            //if (obj is not ValueObject valueObjectInstance)
            //{
            //    return false;
            //}

            var valueObject = (ValueObject)obj;

            return GetEqualityComponents().SequenceEqual(valueObject.GetEqualityComponents()); // возвращает коллекцию 
                                                                                               // object-ов SequenceEqual() -
        }                                                                                  // - метод сравнивает множества 
                                                                                           // сравнивает одну коллекцию с другой
                                                                                           // коллекцией
        public override int GetHashCode() =>
            GetEqualityComponents().Aggregate(
                default(int),
                (hashcode, value) =>
                    HashCode.Combine(hashcode, value.GetHashCode()));
        // Aggregate() вернёт уникальный хэш-код комбинации всех элементов
        // например, скомбинирует хэшкоды свойств Street, Builder, City. 
        // в итоге получим уникальный хэш - код, скомбинированный из всех свойств
           // (hashcode, value) => HashCode.Combine(hashcode, value.GetHashCode())) означает, что hashcode - хэш-код
           // прошлого значения, а value - текущее значение, соответственно value.GetHashCode() - его хэш-код. В Combine()
           // происходит их комбинация.
           // default(int) - начальное значение коллекции, от какого элемента далее функция будет комбинировать хэш - коды
           // в самой первой комбинации в hashcode будет 0, далее - хэш-код первого value и тд

        // переопределяем оператор сравнения
        public static bool operator ==(ValueObject a, ValueObject b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(ValueObject a, ValueObject b)
        {
            return !(a == b);
        }
    }
}
