using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RobotOnScreen
{
    public bool isVisible;
    public bool isDestroyed;
    public bool isInited;
    public float distance;
    public Vector3 positionUI;
    public Transform positionReal;
    public Vector2 direction;
    public Team team;
    
    public RobotOnScreen()
    {
        //this.team = team;
        isDestroyed = false;
        isInited = false;
    }
    
    public void SetItemDirection(float nDirection)
    {
        if (nDirection < 1)
        {
            direction = Vector2.down;
        }
        else if (positionUI.x < 0)
        {
            direction = Vector2.left;
        }
        else if (positionUI.x > 1)
        {
            direction = Vector2.right;
        }
        else if (positionUI.y < 0)
        {
            direction = Vector2.down;
        }
        else if (positionUI.y > 1)
        {
            direction = Vector2.up;
        }
    }
}
