using System;
using System.Collections.Generic;
using System.Linq;

namespace SimplyTrivia.Data
{
	/// <summary>
	/// The response from the API containing the questions that you requested.
	/// As well as a few methods to help manage the answers to said questions.
	/// </summary>
	[Serializable]
	public class TriviaResponse
	{
		/// <summary>
		/// Response code from the server
		/// </summary>
		public int response_code;

		/// <summary>
		/// Response code as an enum which easily describes what the error code means
		/// </summary>
		public TriviaResponseCodes response_code_enum
        {
            get
            {
                return (TriviaResponseCodes)response_code;
            }
        }

		/// <summary>
		/// List of questions retrieved from the server
		/// </summary>
		public List<TriviaQuestion> results = new List<TriviaQuestion>();

		[Serializable]
		public class TriviaQuestion
		{
			/// <summary>
			/// Category this question belongs to
			/// </summary>
			public string category;

			/// <summary>
			/// Type of question, Multiple choice/True or false
			/// </summary>
			public string type;

			/// <summary>
			/// Difficulty of this question - Easy/Medium/Hard
			/// </summary>
			public string difficulty;

			/// <summary>
			/// This specific question
			/// </summary>
			public string question;

			/// <summary>
			/// Correct answer for the question
			/// </summary>
			public string correct_answer;

			/// <summary>
			/// Incorrect answers to go along with this question
			/// </summary>
			public List<string> incorrect_answers = new List<string>();

			/// <summary>
			/// List of all the questions combined
			/// </summary>
			public List<string> allAnswers
            {
                get
                {
                    return new List<string>(incorrect_answers) { correct_answer };
                }
            }

			/// <summary>
			/// List of all the questions combined and then shuffled by a random number
			/// </summary>
			public List<string> allAnswersShuffled
            {
                get
                {
                    return allAnswers.OrderBy(i => UnityEngine.Random.Range(0, 100)).ToList();
                }
            }

			/// <summary>
			/// Check whether an answer passed in is the correct answer for this question.
			/// Also allows you to compare the answers case insensitively.
			/// </summary>
			/// <param name="answer">Answer to check</param>
			/// <param name="ignoreCase">Ignore the casing of the answers</param>
			/// <returns>True if the answer is correct</returns>
			public bool IsCorrectAnswer(string answer, bool ignoreCase)
            {
                return string.Compare(correct_answer, answer, ignoreCase) == 0;
            }
		}
	}
}
