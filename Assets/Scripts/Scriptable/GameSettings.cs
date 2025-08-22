using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "Settings/GameSettings")]
public class GameSettings : ScriptableObject
{
    [SerializeField] private RobotsSettings robots;
    
    public RobotsSettings Robots => robots;
}