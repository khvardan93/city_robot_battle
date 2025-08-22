using System;
using RobotBattle.Weapon;
using UnityEngine;

[CreateAssetMenu(fileName = "RobotSettings", menuName = "Settings/RobotSettings")]
public class RobotSettings : ScriptableObject
{
    [Serializable]
    public struct Camera
    {
        [SerializeField] private float _cameraHeight;
        [SerializeField] private float _cameraDistance;
        [SerializeField] private float _horizontalRotationSensitivity;
        
        public float CameraHeight => _cameraHeight;
        public float CameraDistance => _cameraDistance;
        public float HorizontalRotationSensitivity => _horizontalRotationSensitivity;
    }

    [SerializeField] private string _name;
    [SerializeField] private RobotType _robotType;
    [SerializeField] private Currencies _currencyType;
    [SerializeField] private int _price;
    [SerializeField] private int _health;
    [SerializeField] private int _shieldHealth;
    [SerializeField] private int _speed;
    [SerializeField] private int _weight;
    [SerializeField] private bool _jump;
    [SerializeField] private bool _fly;

    [SerializeField]  private WeaponParams[] _weapons;
    [SerializeField] private Camera _camera;

    public string Name => _name;
    public RobotType RobotType => _robotType;
    public Currencies CurrencyType => _currencyType;
    public int Price => _price;
    public int Health => _health;
    public int ShieldHealth => _shieldHealth;
    public int Speed => _speed;
    public int Weight => _weight;
    public bool Jump => _jump;
    public bool Fly => _fly;
    public WeaponParams[] Weapons => _weapons;
    public Camera CameraSettings => _camera;
}