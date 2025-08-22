using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModelScript
{
    #region SINGLETON
    private PlayerModelScript()
    {

    }
    private static PlayerModelScript _instance;
    public static PlayerModelScript instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = new PlayerModelScript();
            }
            return _instance;
        }
    }
    #endregion

    #region CURRENCIES
    private int _diamonds = -1; 
    public int diamonds
    {
        get
        {
            if(_diamonds == -1)
            {
                _diamonds = PlayerPrefs.GetInt("player_diamonds", 1);
            }
            return _diamonds;
        }
        set
        {
            PlayerPrefs.SetInt("player_diamonds", value);
            _diamonds = value;
        }
    }

    public bool spendDiamonds(int count) {
        if(count <= diamonds)
        {
            diamonds -= count;
            return true;
        }
        return false;
    }

    private int _gold = -1;
    public int gold
    {
        get
        {
            if (_gold == -1)
            {
                _gold = PlayerPrefs.GetInt("player_gold", 10);
            }
            return _gold;
        }
        set
        {
            PlayerPrefs.SetInt("player_gold", value);
            _gold = value;
        }
    }

    public bool spendGold(int count)
    {
        if (count <= gold)
        {
            gold -= count;
            return true;
        }
        return false;
    }

    private int _silver = -1;
    public int silver
    {
        get
        {
            if (_silver == -1)
            {
                _silver = PlayerPrefs.GetInt("player_silver", 70000);
            }
            return _silver;
        }
        set
        {
            PlayerPrefs.SetInt("player_silver", value);
            _silver = value;
        }
    }

    public bool spendSilver(int count)
    {
        if (count <= silver)
        {
            silver -= count;
            return true;
        }
        return false;
    }

    public bool spendCurrency(int count, Currencies currency)
    {
        switch (currency)
        {
            case Currencies.Diamonds:
                return spendDiamonds(count);
                break;
            case Currencies.Silver:
                return spendSilver(count);
                break;
            case Currencies.Gold:
                return spendGold(count);
                break;
        }
        return false;
    }

    public void addCurrency(int count, Currencies currency)
    {
        switch (currency)
        {
            case Currencies.Diamonds:
                diamonds += count;
                break;
            case Currencies.Silver:
                silver += count;
                break;
            case Currencies.Gold:
                gold += count;
                break;
        }
    }
    #endregion

    private int _level = -1;
    public int level
    {
        get
        {
            if (_level == -1)
            {
                _level = PlayerPrefs.GetInt("player_level", 0);
            }
            return _level;
        }
        set
        {
            PlayerPrefs.SetInt("player_level", value);
            _level = value;
        }
    }
}
