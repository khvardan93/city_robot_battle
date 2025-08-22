using  RobotBattle.Robot;

[System.Serializable]
public class RobotAchievementScript
{
    public RobotAchievementScript(RobotType robotType, AbstractBehaviour abstractBehaviour)
    {
        this.robotType = robotType;
        this.Behaviour = abstractBehaviour;
    }

    public bool isPlayer()
    {
        return Behaviour is PlayerBehaviour;
    }

    public RobotType robotType;
    public AbstractBehaviour Behaviour;
    public int killedCount;
    public float damage;
    public bool isDead;
}
