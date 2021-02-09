using System;
using UnityEngine;

namespace SimplyTrivia.Data
{
	/// <summary>
	/// This class is used to easily pass parameters into the CreateQuiz method without
	/// too much room for error. It contains all of the API options available with handy
	/// drop downs in the inspector, making the values easy to change.
	/// </summary>
	[Serializable]
	public class QuizParams
	{
		/// <summary>
		/// Number of questions you would like
		/// </summary>
		[Range(1, 50), Tooltip("How many questions would you like?")]
		public int NumberOfQuestions = 5;

		/// <summary>
		/// The specific category you would like your questions based on.
		/// Alternatively, you can get questions from any category
		/// </summary>
		[Tooltip("Is there a specific category you would like your questions to be on?")]
		public TriviaCategory Category = TriviaCategory.ANY;

		/// <summary>
		/// Difficulty of the questions from the server, Any, Easy, Medium or Hard
		/// </summary>
		[Tooltip("How difficult would you like your questions?")]
		public TriviaDifficulty Difficulty = TriviaDifficulty.ANY;

		/// <summary>
		/// The type of questions you would like to retrieve, multiple choice,
		/// or true/false questions.
		/// </summary>
		[Tooltip("OpenTrivia supports both Multiple Choice and True/False questions, which would you like?")]
		public TriviaType QuestionType = TriviaType.ANY;

		/// <summary>
		/// The encoding used when fetching the data from the server
		/// </summary>
		[Tooltip("Which format would you like the results to come in?")]
		public TriviaEncoding Encoding = TriviaEncoding.DEFAULT;
	}
}
