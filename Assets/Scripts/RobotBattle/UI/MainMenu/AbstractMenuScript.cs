using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractMenuScript : MonoBehaviour
{
    public virtual void open()
    {
        gameObject.SetActive(true);
    }

    public virtual void close()
    {
        gameObject.SetActive(false);
    }
}