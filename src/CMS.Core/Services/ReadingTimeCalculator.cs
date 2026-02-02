using System.Text.RegularExpressions;

namespace CMS.Core.Services
{
    /// <summary>
    /// Helper service for calculating estimated reading time
    /// Based on average reading speed of 200 words per minute
    /// </summary>
    public static class ReadingTimeCalculator
    {
        private const int AverageWordsPerMinute = 200;

        /// <summary>
        /// Calculate estimated reading time in minutes from HTML content
        /// </summary>
        /// <param name="htmlContent">HTML content of the post</param>
        /// <returns>Estimated reading time in minutes (minimum 1 minute)</returns>
        public static int CalculateReadingTime(string? htmlContent)
        {
            if (string.IsNullOrWhiteSpace(htmlContent))
            {
                return 1;
            }

            // Remove HTML tags
            var plainText = Regex.Replace(htmlContent, "<[^>]*>", " ");
            
            // Remove extra whitespace
            plainText = Regex.Replace(plainText, @"\s+", " ").Trim();
            
            // Count words
            var wordCount = plainText.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Length;
            
            // Calculate reading time (minimum 1 minute)
            var readingTime = Math.Max(1, (int)Math.Ceiling((double)wordCount / AverageWordsPerMinute));
            
            return readingTime;
        }
    }
}
