using System;

namespace SimplyTrivia.Data
{
	/// <summary>
	/// This is returned when send a request to the API to reset your current session token
	/// </summary>
	[Serializable]
	public class TriviaSessionTokenResetResponse
	{
		/// <summary>
		/// Response code from the server
		/// </summary>
		public readonly int response_code;

		/// <summary>
		/// Token that has been reset
		/// </summary>
		public readonly string token;
	}
}
