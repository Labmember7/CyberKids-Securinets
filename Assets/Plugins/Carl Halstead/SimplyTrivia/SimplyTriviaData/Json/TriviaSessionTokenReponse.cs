using System;

namespace SimplyTrivia.Data
{
	/// <summary>
	/// This is returned when requesting a new session token from the API
	/// </summary>
	[Serializable]
	public class TriviaSessionTokenReponse
	{
		/// <summary>
		/// Response code from the server
		/// </summary>
		public readonly int reponse_code;

		/// <summary>
		/// Message sent from the server
		/// e.g. "Token Generated Successfully!"
		/// </summary>
		public readonly string response_message;

		/// <summary>
		/// Your new session token
		/// </summary>
		public readonly string token;
	}
}