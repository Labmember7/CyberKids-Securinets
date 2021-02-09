using SimplyTrivia.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace SimplyTrivia.Helpers
{
    public static class TriviaHelpers
    {
        #region Fields
        /// <summary>
        /// URL used to check the number of questions within a specific category
        /// </summary>
        private const string questionCountLookupUrl = "https://opentdb.com/api_count.php?category=";

        /// <summary>
        /// URL used to get a list of all categories available within the API
        /// </summary>
        private const string categoryLookupUrl = "https://opentdb.com/api_category.php";

        /// <summary>
        /// URL used to get a global count of questions and the amount for each difficulty
        /// </summary>
        private const string globalQuestionLookupUrl = "https://opentdb.com/api_count_global.php";
        #endregion

        #region Methods_Custom
        /// <summary>
        /// Get an array of categories available
        /// </summary>
        /// <returns></returns>
        public static TriviaCategory[] GetAvailableCategories()
        {
            return (TriviaCategory[])Enum.GetValues(typeof(TriviaCategory));
        }

        /// <summary>
        /// Format the category enum into a presentable string.
        /// This changes the first underscore into a hyphen, replace subsequent underscores with spaces
        /// and appropriately capitilises characters.
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public static string FormatCategoryName(TriviaCategory category)
        {
            string name = category.ToString().ToLower();

            if (name.Contains("_"))
            {
                int index = name.IndexOf('_');

                name = name.Substring(0, index) + " - " + name.Substring(index + 1);
                name = name.Replace('_', ' ');
            }

            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(name);
        }

        [Obsolete("Feature is currently not implemented due to code changes")]
        public static void GetCategoryQuestionsCount(TriviaCategory category, Action<CategoryQuestionsCount> OnCompleted)
        {
            throw new NotImplementedException("This feature is not currently implemented!");
        }

        [Obsolete("Feature is currently not implemented due to code changes")]
        public static void GetTriviaCategories(Action<TriviaCategories> OnCompleted)
        {
            throw new NotImplementedException("This feature is not currently implemented!");
        }

        [Obsolete("Feature is currently not implemented due to code changes")]
        private static void GetGlobalQuestionCount()
        {
            throw new NotImplementedException("This feature is not currently implemented!");
        }
        #endregion

        /// <summary>
        /// Class to hold information about the questions available for
        /// a specific category
        /// </summary>
        public class CategoryQuestionsCount
        {
            /// <summary>
            /// The ID of the category that was checked
            /// </summary>
            public int category_id;
            public QuestionCount category_question_count;

            /// <summary>
            /// Class to store the amount of questions
            /// </summary>
            public class QuestionCount
            {
                /// <summary>
                /// Total questions available for this category
                /// </summary>
                public int total_question_count;

                /// <summary>
                /// Total easy questions available
                /// </summary>
                public int total_easy_question_count;

                /// <summary>
                /// Total medium questions available
                /// </summary>
                public int total_medium_question_count;

                /// <summary>
                /// Total hard questions available
                /// </summary>
                public int total_hard_question_count;
            }
        }

        /// <summary>
        /// Class to hole information about the categories available for
        /// the API including their IDs and names.
        /// </summary>
        public class TriviaCategories
        {
            /// <summary>
            /// List of categories
            /// </summary>
            public List<Category> trivia_categories;

            public class Category
            {
                /// <summary>
                /// ID of the category
                /// </summary>
                public int id;

                /// <summary>
                /// Name of the category
                /// </summary>
                public string name;
            }
        }

    }
}
