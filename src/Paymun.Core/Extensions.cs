using System;

namespace Paymun.Core.Extensions {

    public static class PaymunExtensions {

        public static bool IsNullOrEmpty(this string input)
            => string.IsNullOrWhiteSpace(input);

        public static bool IsNotNullOrEmpty(this string input)
            => !IsNullOrEmpty(input);

        /// <summary>
        /// returns true if the <paramref name="obj"/> is NOT null.
        /// </summary>
        /// <param name="obj">object to check</param>
        /// <returns>true if not null</returns>
        public static bool IsNotNull(this object obj) => obj != null;

        /// <summary>
        /// returns true if the <paramref name="obj"/> is null.
        /// </summary>
        /// <param name="obj">object to check</param>
        /// <returns>true if is null</returns>
        public static bool IsNull(this object obj) => obj == null;

        public static void CheckIsNullOrEmpty(this string str) {
            if (str == null || str.Length == 0)
                throw new ArgumentNullException(nameof(str));
        }

        /// <summary>
        /// Checks if the argument is null.
        /// </summary>
        public static void CheckArgumentIsNull(this object o, string name) {
            if (o == null)
                throw new ArgumentNullException(name);
        }

        /// <summary>
        /// Checks if a reference is null.
        /// </summary>
        public static void CheckReferenceIsNull(this object o, string name)
        {
            if (o == null)
                throw new NullReferenceException(name);
        }

    }
}
