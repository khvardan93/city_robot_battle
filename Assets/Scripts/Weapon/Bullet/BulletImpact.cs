namespace RobotBattle.Weapon
{
    public class BulletImpact : BasePoolObject
    {
        private float _explosionTimer = 1f; 
        
        private void OnEnable()
        {
            Invoke(nameof(Release), _explosionTimer);
        }
    }
}
