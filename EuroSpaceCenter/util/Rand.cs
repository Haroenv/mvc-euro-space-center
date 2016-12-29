using System;
using System.Text;

namespace EuroSpaceCenter.util {
    public class Rand {
        private static Random random = new Random();
        const string chars = "0123456789ABCDEFGHJKLMNPQRSTUVWXYZ_abcdefghijkmnopqrstuvwxyz";

        public static string String(int length) {
            var result = new StringBuilder(length);
            for (int i = 0; i < length; ++i) {
                result.Append(chars[random.Next(chars.Length)]);
            }
            return result.ToString();
        }
    }
}