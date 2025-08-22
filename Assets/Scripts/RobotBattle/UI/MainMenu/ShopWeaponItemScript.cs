using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopWeaponItemScript : MonoBehaviour
{
    [SerializeField] Text titleText;
    [SerializeField] Image weaponImage;

    public void show(RobotType robotType, WeaponType weaponType)
    {
        gameObject.SetActive(true);
        titleText.text = weaponType.ToString();
        weaponImage.sprite = Resources.Load<Sprite>("Weapons/" + robotType + "/" + weaponType);

        if (weaponImage.sprite == null) Debug.Log(robotType + "  " + weaponType);
    }

    public void hide()
    {
        gameObject.SetActive(false);
    }
}
