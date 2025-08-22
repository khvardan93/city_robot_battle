using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotItemScript : AbstractSlotItemScript
{
    [SerializeField] Image[] images;
    [SerializeField] Sprite[] sprites;
    [Space]
    [SerializeField] Text countText;
    public int index;

    public override void init(int index)
    {
        this.index = index;

        var current = GameData.currentCase[index];

        if (current.count > 0)
        {
            countText.text = current.count.ToString();
            foreach (var image in images)
            {
                image.sprite = sprites[(int)current.currency];
            }
        }
        else
        {

        }
    }
}
