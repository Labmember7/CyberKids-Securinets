using System;

public class OpenTriviaAvailability : EventArgs
{
	public readonly bool IsAvailable;

	public OpenTriviaAvailability(bool IsAvailable)
	{
		this.IsAvailable = IsAvailable;
	}
}