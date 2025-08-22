using UnityEngine;
using UnityEngine.UI;

  namespace RobotBattle.UI.MainMenu
  {
      public class ShowCurrencyScript : MonoBehaviour
      {
          public Currencies currencyType;
          [SerializeField] Text countText;

          private int currentCount = 0;
          public bool animate;

          private int[] finalArray;
          private int[] currentArray;


          private void Update()
          {
              getData();
              if (animate) showData();
          }

          private void showData()
          {
              if (currentCount == defragmentNumber(currentArray))
              {
                  animate = false;
              }
              else
              {
                  for (int i = 0; i < finalArray.Length; i++)
                  {
                      currentArray[i] = getCurrentNumber(currentArray[i], finalArray[i]);
                  }

                  countText.text = defragmentNumber(currentArray).ToString();
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

          private void getData()
          {
              int count = 0;
              if (currencyType == Currencies.Diamonds)
              {
                  count = PlayerModelScript.instance.diamonds;
              }
              else if (currencyType == Currencies.Gold)
              {
                  count = PlayerModelScript.instance.gold;
              }
              else if (currencyType == Currencies.Silver)
              {
                  count = PlayerModelScript.instance.silver;
              }

              if (count != currentCount)
              {
                  animate = true;

                  int finalLength = count.ToString().Length;
                  int currentLength = currentCount.ToString().Length;

                  if (finalLength == currentLength)
                  {
                      finalArray = fragmentNumber(count, finalLength);
                      currentArray = fragmentNumber(currentCount, finalLength);
                  }
                  else if (finalLength > currentLength)
                  {
                      finalArray = fragmentNumber(count, finalLength);
                      currentArray = fragmentNumber(currentCount, finalLength);
                  }
                  else if (finalLength < currentLength)
                  {
                      finalArray = fragmentNumber(count, currentLength, -1);
                      currentArray = fragmentNumber(currentCount, currentLength);
                  }

                  currentCount = count;
              }
          }

          public void onClick()
          {
              RobotSoundsScript.Instance.PlaySound(RobotSoundsScript.Instance.GetClickSound());
              MainMenuScript.Instance.OpenDonatePage(currencyType);
          }
      }
  }