using System;

public class OpenTriviaSessionTokenUpdated : EventArgs
{
	public readonly string SessionToken;

	public OpenTriviaSessionTokenUpdated(string SessionToken)
	{
		this.SessionToken = SessionToken;
	}
}