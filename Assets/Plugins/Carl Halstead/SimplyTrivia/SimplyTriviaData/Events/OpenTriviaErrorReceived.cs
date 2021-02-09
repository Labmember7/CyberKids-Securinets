using SimplyTrivia.Data;
using System;

public class OpenTriviaErrorReceived : EventArgs
{
	public readonly TriviaResponse Response;

	public OpenTriviaErrorReceived(TriviaResponse Response)
	{
		this.Response = Response;
	}
}