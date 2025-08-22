using UnityEngine;
using RobotBattle.Weapon;

namespace RobotBattle.Robot
{
    public class RobotData
    {
        public string name;
        public RobotType robotType;
        public Currencies currencyType;
        public int price;
        public int health;
        public int shieldHealth;
        public int speed;
        public int weight;
        public bool jump;
        public bool fly;

        public WeaponParams[] weapons;

        public MaterialType materialType
        {
            get { return (MaterialType)PlayerPrefs.GetInt("player_material_" + robotType, 0); }
            set { PlayerPrefs.SetInt("player_material_" + robotType, (int)value); }
        }

        public bool isMaterialBought(MaterialType materialType)
        {
            return PlayerPrefs.GetInt("player_material_" + robotType + "_" + materialType, 0) == 1;
        }

        public bool isBought
        {
            get { return PlayerPrefs.GetInt("player_" + robotType + "_bought", 0) == 1; }
            set { PlayerPrefs.SetInt("player_" + robotType + "_bought", value ? 1 : 0); }
        }

        public void setMaterialBought(MaterialType materialType)
        {
            PlayerPrefs.SetInt("player_material_" + robotType + "_" + materialType, 1);
        }
    }
}