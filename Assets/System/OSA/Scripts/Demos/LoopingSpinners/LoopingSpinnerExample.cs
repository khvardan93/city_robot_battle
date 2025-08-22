using System;
using UnityEngine;
using UnityEngine.UI;
using Com.TheFallenGames.OSA.Core;
using Com.TheFallenGames.OSA.CustomParams;

/// <summary>
/// Very basic example with a spinner that loops its items, similarly to a time picker in an alarm app.
/// Minimal implementation of the adapter that initializes and updates the viewsholders. The size of each item is fixed in this case and it's the same as the prefab's
/// </summary>
public class LoopingSpinnerExample : OSA<MyParams, MyItemViewsHolder>
{
    // Keeping the approx. minimum required items to prevent gaps in the viewport (otherwise we'd have to duplicate items or simply stop looping)
    public const int MIN_ITEMS = 4;

    [Header("------------")]
    public Action<int> onChangeAction;
    private int lastIndex = -1;

    #region OSA implementation
    /// <inheritdoc/>
    protected override void Update()
    {
        base.Update();

        if (_VisibleItemsCount == 0)
            return;

        int middleVHIndex = _VisibleItemsCount / 2;
        var middleVH = _VisibleItems[middleVHIndex];

        if (lastIndex != middleVH.ItemIndex)
        {
            lastIndex = middleVH.ItemIndex;
            onChangeAction?.Invoke(lastIndex);
            if (_Params.currentSelectedIndicatorText) _Params.currentSelectedIndicatorText.text = GameData.currentCase[_Params.GetItemValueAtIndex(middleVH.ItemIndex)].currency.ToString();
        }
    }

    /// <inheritdoc/>
    protected override MyItemViewsHolder CreateViewsHolder(int itemIndex)
    {
        var instance = new MyItemViewsHolder();
        instance.Init(_Params.itemPrefab, itemIndex);

        return instance;
    }

    /// <inheritdoc/>
    protected override void UpdateViewsHolder(MyItemViewsHolder newOrRecycled)
    {
        newOrRecycled.titleText.transform.parent.GetComponent<AbstractSlotItemScript>().init(_Params.GetItemValueAtIndex(newOrRecycled.ItemIndex));
    }
    #endregion

    public void ChangeItemsCountWithChecks(int newCount)
    {
        if (newCount < MIN_ITEMS)
            newCount = MIN_ITEMS;

        ResetItems(newCount);
    }

    public int getLastIndex()
    {
        return lastIndex;
    }
}



[Serializable] // serializable, so it can be shown in inspector
public class MyParams : BaseParamsWithPrefab
{
    public int startItemNumber = 0;
    public int increment = 1;
    public Color selectedColor, nonSelectedColor;
    public Text currentSelectedIndicatorText;

    /// <summary>The value of each item is calculated dynamically using its <paramref name="index"/>, <see cref="startItemNumber"/> and the <see cref="increment"/></summary>
    /// <returns>The item's value (the displayed number)</returns>
    public int GetItemValueAtIndex(int index) { return startItemNumber + increment * index; }
}


public class MyItemViewsHolder : BaseItemViewsHolder
{
    public Image background;
    public Text titleText;

    /// <inheritdoc/>
    public override void CollectViews()
    {
        base.CollectViews();

        background = root.GetComponent<Image>();
        titleText = root.GetComponentInChildren<Text>();
    }
}