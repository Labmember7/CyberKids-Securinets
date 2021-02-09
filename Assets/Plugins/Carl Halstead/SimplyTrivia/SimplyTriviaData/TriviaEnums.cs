namespace SimplyTrivia.Data
{
	/// <summary>
	/// The categories which are used by the server to return questions about specific topics
	/// </summary>
	public enum TriviaCategory
	{
		ANY = -1,
		GENERAL_KNOWLDGE = 9,
		ENTERTAINMENT_BOOKS = 10,
		ENTERTAINMENT_FILM = 11,
		ENTERTAINMENT_MUSIC = 12,
		ENTERTAINMENT_MUSCIALS_THEATRES = 13,
		ENTERTAINMENT_TELEVISION = 14,
		ENTERTAINMENT_VIDEO_GAMES = 15,
		ENTERTAINMENT_BOARD_GAMES = 16,
		SCIENCE_NATURE = 17,
		SCIENCE_COMPUTERS = 18,
		SCIENCE_MATHEMATICS = 19,
		MYTHOLOGY = 20,
		SPORTS = 21,
		GEOGRAPHY = 22,
		HISTORY = 23,
		POLITICS = 24,
		ART = 25,
		CELEBRITIES = 26,
		ANIMALS = 27,
		VEHICLES = 28,
		ENTERTAINMENT_COMICS = 29,
		SCIENCE_GADGETS = 30,
		ENTERTAINMENT_JAPANESE_ANIME_MANGA = 31,
		ENTERTAINMENT_CARTOON_ANIMATION = 32
	}

	/// <summary>
	/// The difficulty of questions ranging from easy to hard
	/// </summary>
	public enum TriviaDifficulty
	{
		ANY,
		EASY,
		MEDIUM,
		HARD
	}

	/// <summary>
	/// The type of questions you would like. Multiple choice or true/false
	/// </summary>
	public enum TriviaType
	{
		ANY,
		MULTIPLE,
		BOOLEAN
	}

	/// <summary>
	/// The encoding of the results from the server
	/// </summary>
	public enum TriviaEncoding
	{
		DEFAULT
	}

	/// <summary>
	/// The different response codes which could be returned by the server
	/// </summary>
	public enum TriviaResponseCodes
	{
		/// <summary>
		/// The query completed successfully
		/// </summary>
		SUCCESS = 0,

		/// <summary>
		/// There are not enough questions for the amount you requested
		/// </summary>
		NO_RESULTS = 1,

		/// <summary>
		/// An invalid parameter has been passed in. 0 questions for example when it should be between 1 and 50
		/// </summary>
		INVALID_PARAMETERS = 2,

		/// <summary>
		/// Session token could not be found
		/// </summary>
		TOKEN_NOT_FOUND = 3,

		/// <summary>
		/// You have used all of the questions available with this token. You can reset it but you would now recieve duplicate questions.
		/// </summary>
		TOKEN_EMPTY = 4
	}
}
