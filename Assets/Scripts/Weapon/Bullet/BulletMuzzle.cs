namespace RobotBattle.Weapon
{
    public class BulletMuzzle : BasePoolObject
    {
        private float _explosionTimer = 1f; 
        
        private void OnEnable()
        {
            Invoke(nameof(Release), _explosionTimer);
        }
    }
}