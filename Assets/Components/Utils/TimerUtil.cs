using UnityEngine;
using System.Collections;
using System;

public class TimerUtil
{

	private float timerStopTime = 0;

	/// <summary>
	/// Gets a value indicating whether this <see cref="TimerUtil"/> is paused.
	/// </summary>
	/// <value><c>true</c> if paused; otherwise, <c>false</c>.</value>
	public static bool paused {
		get {
			return Time.timeScale>0?false:true;
		}
	}

	/// <summary>
	/// Reset the timer
	/// </summary>
	public void reset ()
	{
		timerStopTime = 0;
	}

	/// <summary>
	/// Starts the timer if it is not in counting mode (already started)
	/// </summary>
	/// <returns><c>true</c>, if timer was started, <c>false</c> otherwise.</returns>
	/// <param name="timerTime">Timer time.</param>
	public bool startTimer (float timerTime)
	{
		//reinstall timer
		if (timerStopTime == 0) {
			timerStopTime = Time.time + timerTime;
			return true;
		}

		return false;
	}

	/// <summary>
	/// Starts the timer if it is not in counting mode (already started)
	/// </summary>
	/// <returns><c>true</c>, if timer was started, <c>false</c> otherwise.</returns>
	/// <param name="timerTimeMin">Timer time minimum.</param>
	/// <param name="timerTimeMax">Timer time max.</param>
	public bool startTimer (float timerTimeMin, float timerTimeMax)
	{
		return startTimer(UnityEngine.Random.Range(timerTimeMin, timerTimeMax));
	}

	/// <summary>
	/// Checks if the timer finished.
	/// </summary>
	/// <returns><c>true</c>, if timer finished, <c>false</c> otherwise.</returns>
	public bool isTimeFinished ()
	{
		if (Time.time > timerStopTime) {
				timerStopTime = 0;
				return true;
		}
		return false;
	}

    public static string timeToString(float time)
    {
        float minutes = Mathf.Floor(time / 60);
        if (minutes < 0)
            minutes = 0;
        float seconds = Mathf.Floor(time % 60);

        float hours;
        string hoursString = "";

        string minutesString;
        string secondsString;

        if (minutes > 59)
        {
            hours = Mathf.Floor(time / 3600);
            minutes = Mathf.Floor((time - hours * 3600) / 60);

            hoursString = hours + ":";
        }

        if (minutes < 10)
        {
            minutesString = "0" + minutes;
        }
        else
        {
            minutesString = "" + minutes;
        }

        if (seconds < 10)
        {
            secondsString = "0" + seconds;
        }
        else
        {
            secondsString = "" + seconds;
        }

        return hoursString + minutesString + ":" + secondsString;
    }

	public static float getDifference(DateTime from, DateTime to){
		TimeSpan difference = to.Subtract(from);
		return difference.Days*3600*24 + difference.Hours*3600+difference.Minutes*60+difference.Seconds;
	}

	public static float getDifferenceFromNow(DateTime date){
		TimeSpan difference = date.Subtract(DateTime.Now);
		return difference.Days*3600*24 + difference.Hours*3600+difference.Minutes*60+difference.Seconds;
	}

	public static void pause(){
		if (Time.timeScale != 0)
			Time.timeScale = 0;
	}

	public static void release(){
		if (Time.timeScale != 1)
			Time.timeScale = 1;
	}
}
