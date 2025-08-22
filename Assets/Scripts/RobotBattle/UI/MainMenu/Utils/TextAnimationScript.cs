using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TextAnimationScript : MonoBehaviour
{
    private Text numberText;
    private int currentValue;

    private int[] finalArray;
    private int[] currentArray;

    private string prefix;
    private int dotPosition;

    void Start()
    {
        numberText = GetComponent<Text>();
    }

    private void OnDisable()
    {
        numberText.text = prefix + (currentValue / Mathf.Pow(10, dotPosition)).ToString();
        StopAllCoroutines();
    }

    public void set(float newValueFloat, string newPrefix = "")
    {
        dotPosition = (newValueFloat - (int)newValueFloat).ToString().Length - 2;

        int newValue = (int)(newValueFloat * Mathf.Pow(10, dotPosition));

        prefix = newPrefix;

        if (newValue != currentValue)
        {
            int finalLength = newValue.ToString().Length;
            int currentLength = currentValue.ToString().Length;

            if (finalLength == currentLength)
            {
                finalArray = fragmentNumber(newValue, finalLength);
                currentArray = fragmentNumber(currentValue, finalLength);
            }
            else if (finalLength > currentLength)
            {
                finalArray = fragmentNumber(newValue, finalLength);
                currentArray = fragmentNumber(currentValue, finalLength);
            }
            else if (finalLength < currentLength)
            {
                finalArray = fragmentNumber(newValue, currentLength, -1);
                currentArray = fragmentNumber(currentValue, currentLength);
            }

            currentValue = newValue;

            StartCoroutine(showData());
        }
    }

    private IEnumerator showData()
    {
        while (currentValue == defragmentNumber(currentArray))
        {
            for (int i = 0; i < finalArray.Length; i++)
            {
                currentArray[i] = getCurrentNumber(currentArray[i], finalArray[i]);
            }
            numberText.text = prefix + (currentValue / Mathf.Pow(10, dotPosition)).ToString();

            yield return new WaitForEndOfFrame();
        }
    }

    private int getCurrentNumber(int from, int to)
    {
        if (from > to)
        {
            from--;
        }
        else if (from < to)
        {
            from++;
        }
        return from;
    }

    private int[] fragmentNumber(int number, int length, int defaultNumber = 0)
    {
        var array = new int[length];
        var numberLength = number.ToString().Length;

        for (int i = 0; i < length; i++)
        {
            if (i < numberLength)
            {
                array[i] = number % (int)Mathf.Pow(10, i + 1) / (int)Mathf.Pow(10, i);
                number -= array[i];
            }
            else
            {
                array[i] = defaultNumber;
            }
        }

        return array;
    }

    private int defragmentNumber(int[] array)
    {
        int number = 0;

        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] == -1) break;
            number += array[i] * (int)Mathf.Pow(10, i);
        }

        return number;
    }
}
