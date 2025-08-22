using UnityEngine;
using System.Collections;

public class UtilFunctions {

	public static void setLayerRecursively(GameObject obj, int newLayer)
	{
		if (null == obj)
		{
			return;
		}
		
		obj.layer = newLayer;
		
		foreach (Transform child in obj.transform)
		{
			if (null == child)
			{
				continue;
			}
			setLayerRecursively(child.gameObject, newLayer);
		}
	}
}
