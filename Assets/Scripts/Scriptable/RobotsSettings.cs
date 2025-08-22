using UnityEngine;

[CreateAssetMenu(fileName = "RobotsSettings", menuName = "Settings/RobotsSettings")]
public class RobotsSettings : ScriptableObject
{
    [SerializeField] private RobotSettings[] robots;
    
    public RobotSettings[] Robots => robots;
}