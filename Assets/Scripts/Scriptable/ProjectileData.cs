using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileData", menuName = "ScriptableObjects/ProjectileData")]
public class ProjectileData : ScriptableObject
{
    public string projectleName;
    public Rigidbody bombPrefab;
    public GameObject muzzleflare;
    public float min, max;
    public bool rapidFire;
    public float rapidFireCooldown;

    public bool shotgunBehavior;
    public int shotgunPellets;
    public GameObject shellPrefab;
    public bool hasShells;
}
