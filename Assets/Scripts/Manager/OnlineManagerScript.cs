using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlineManagerScript : MonoBehaviour
{
    public static OnlineManagerScript instance
    {
        get;
        internal set;
    }

    private void Awake()
    {
        instance = this;
    }
}
