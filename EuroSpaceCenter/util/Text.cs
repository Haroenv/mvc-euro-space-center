namespace EuroSpaceCenter.util {

    /// <summary>
    /// Text methods
    /// </summary>
    public static class Text {

        /// <summary>
        /// Truncate a text 
        /// </summary>
        /// <param name="value">The text to truncate</param>
        /// <param name="maxChars">Amount of characters allowed</param>
        /// <returns>the truncated string</returns>
        public static string Truncate(this string value, int maxChars) {
            return value.Length <= maxChars ? value : value.Substring(0, maxChars) + "…";
        }
    }
}