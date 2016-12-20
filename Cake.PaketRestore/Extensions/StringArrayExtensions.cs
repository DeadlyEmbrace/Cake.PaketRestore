using System.Collections.Generic;

namespace Cake.PaketRestore.Extensions
{
    /// <summary>
    /// Extension methods for string arrays
    /// </summary>
    public static class StringArrayExtensions
    {
        #region Public Methods

        /// <summary>
        /// Convert a collection of string values to a collection of objects
        /// </summary>
        /// <param name="stringArray"></param>
        /// <returns></returns>
        public static object[] ConvertToObjectArray(this IReadOnlyList<string> stringArray)
        {
            var objectArray = new object[stringArray.Count];
            for (var r = 0; r < stringArray.Count; r++)
            {
                objectArray[r] = stringArray[r];
            }
            return objectArray;
        }

        #endregion
    }
}