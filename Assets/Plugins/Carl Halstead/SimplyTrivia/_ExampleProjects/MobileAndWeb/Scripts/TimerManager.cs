using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
	[SerializeField] private Slider timeSlider;
	[SerializeField] private bool isTimerStarted = false;
	[SerializeField] private float timeToCompleteQuestion;
	[SerializeField] private float timer;

	public event EventHandler OnTimerFinished;

	public float TimeRemaining
	{
		get
		{
			return timeToCompleteQuestion - timer;
		}
	}

	private void Awake()
	{
		timeSlider.minValue = 0f;
		timeSlider.maxValue = 1f;
	}

	private void Update()
	{
		if (isTimerStarted == false)
			return;

		timer += Time.deltaTime;
		timeSlider.value = 1f - (timer / timeToCompleteQuestion);

		if (timer > timeToCompleteQuestion)
		{
			OnTimerFinished(this, new EventArgs());
			isTimerStarted = false;
		}
	}

	public void StartTimer(float timeToComplete)
	{
		isTimerStarted = true;
		timer = 0f;
		timeToCompleteQuestion = timeToComplete;
	}

	public void SubtractTime()
	{
		timer += (timeToCompleteQuestion / 3f);
	}
}
