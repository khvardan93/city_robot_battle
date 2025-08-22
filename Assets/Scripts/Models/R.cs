using System;
using UnityEngine;

public static class R
{
    public static GameSettings GameSettings { get; private set; }
    
    static R()
    {
        
        GameSettings = Resources.Load<GameSettings>("GameSettings");
    }
}
