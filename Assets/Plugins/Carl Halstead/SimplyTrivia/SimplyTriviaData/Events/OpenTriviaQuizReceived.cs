using SimplyTrivia.Data;
using System;

public class OpenTriviaQuizReceived : EventArgs
{
	public readonly TriviaResponse Response;

	public OpenTriviaQuizReceived(TriviaResponse Response)
	{
		this.Response = Response;
	}
}