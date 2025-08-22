using UnityEngine;

/* THIS CODE IS JUST FOR PREVIEW AND TESTING */
// Feel free to use any code and picking on it, I cannot guaratnee it will fit into your project
public class ExplodingProjectile : MonoBehaviour
{
    public GameObject impactPrefab;
    public GameObject explosionPrefab;
    public float thrust;

    public Rigidbody thisRigidbody;

    public GameObject particleKillGroup;
    private Collider thisCollider;

    public bool LookRotation = true;
    public bool Missile = false;
    public Transform missileTarget;
    public float projectileSpeed;
    public float projectileSpeedMultiplier;

    public bool ignorePrevRotation = false;

    public bool explodeOnTimer = false;
    public float explosionTimer;
    float timer;

    private void Start()
    {
        Destroy(gameObject,5);
        thisRigidbody = GetComponent<Rigidbody>();
        if (Missile)
        {
            missileTarget = GameObject.FindWithTag("Target").transform;
        }
        thisCollider = GetComponent<Collider>();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= explosionTimer && explodeOnTimer)
        {
            Explode();
        }
    }

    void FixedUpdate()
    {
        if (Missile)
        {
            projectileSpeed += projectileSpeed * projectileSpeedMultiplier;
            //   transform.position = Vector3.MoveTowards(transform.position, missileTarget.transform.position, 0);

            transform.LookAt(missileTarget);

            thisRigidbody.AddForce(transform.forward * projectileSpeed);
        }

        if (LookRotation && timer >= 0.05f)
        {
            transform.rotation = Quaternion.LookRotation(thisRigidbody.linearVelocity);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "FX")
        {
            ContactPoint contact = collision.contacts[0];
            Quaternion rot = Quaternion.FromToRotation(Vector3.forward, contact.normal);
            if (ignorePrevRotation)
            {
                rot = Quaternion.Euler(0, 0, 0);
            }

            var pos = contact.point;
            Instantiate(impactPrefab, pos, rot);
            if (!explodeOnTimer && Missile == false)
            {
                Destroy(gameObject);
            }
            else if (Missile)
            {
                thisCollider.enabled = false;
                particleKillGroup.SetActive(false);
                thisRigidbody.linearVelocity = Vector3.zero;

                Destroy(gameObject, 5);
            }
        }
    }

    private void Explode()
    {
        Instantiate(explosionPrefab, gameObject.transform.position, Quaternion.Euler(0, 0, 0));
        Destroy(gameObject);
    }
}