using System;
using UnityEngine;
using RobotBattle.Robot;
using RobotBattle.Weapon;

namespace Forge3D
{
    [AddComponentMenu("FORGE3D/Force Field/Force Field Mobile")]
    public class Forcefield_Mobile : MonoBehaviour
    {
        private Guid _ownerId;
        private Action<float> _onHit;
        private RobotAchievementScript _robotAchievementScript;
        

        // Force Field component cache variables
        private Material _mat;
        private MeshFilter _mesh;

        // Number of controllable interpolators (impact points)
        private int _interpolators = 6;

        // Unique shader propIDs (see http://docs.unity3d.com/ScriptReference/Shader.PropertyToID.html)
        // Used to modify shader interpolators by int id instead of string name
        private int[] _shaderPropsID;
        // Data containing xyz coordinate of impact and alpha in w stored in vector4 for each interpolator
        private Vector4[] _shaderProps;

        // Current active interpolator
        private int _curProp;
        // Timer used to advance trough interpolators
        private float _curTime;

        // Force Field game object
        // Should be assigned trough the inspector
        // 
        // * IMPORTANT NOTE *
        // Note that collision events are only sent if one of the colliders also has a non-kinematic rigidbody attached.
        public GameObject shield;

        // Collision events flags
        [Header("Collision events:")]
        public bool CollisionEnter;

        // Speed at which interpolators will fade
        [Header("Shield settings:")]
        public float DecaySpeed = 2.0f;

        // Force Field reaction speed
        public float ReactSpeed = 0.1f;

        // Non-uniform scale correction
        public bool FixNonUniformScale;

        private bool isFireDamage = false;
        private float fireDamage;

        public void SetupShield(Guid ownerId, Action<float> onHit)
        {
            gameObject.tag = "Shield";
            gameObject.SetActive(true);
            _ownerId = ownerId;
            _onHit = onHit;
            
            // Cache required components
            _mat = shield.GetComponent<Renderer>().material;
            _mesh = shield.GetComponent<MeshFilter>();

            // Generate unique IDs for optimised performance
            // since script has to access them each frame
            _shaderPropsID = new int[_interpolators];
            for (int i = 0; i < _interpolators; i++)
                _shaderPropsID[i] = Shader.PropertyToID($"_Pos_{i}");

            // Initialize interpolators array
            _shaderProps = new Vector4[_interpolators];
        }

        // COLLISIONS EVENTS
        private void OnCollisionEnter(Collision collisionInfo)
        {
            var bulletDamageScript = collisionInfo.gameObject.GetComponent<Bullet>();
            if (!bulletDamageScript) return;
            
            _onHit(bulletDamageScript.Damage);
            
            if (!CollisionEnter) return;
            
            foreach (ContactPoint contact in collisionInfo.contacts)
            {
                OnHit(contact.point);
            }
        }

        private void OnParticleCollision(GameObject other)
        {
            if (other.GetComponentInParent<PlayerController>().Id == _ownerId) return;
            isFireDamage = true;

            var fireGunControllerScript = other.GetComponentInParent<FireGunControllerScript>();
            fireDamage = fireGunControllerScript.GetDamagePerSecond();
        }

        // MASK MANAGEMENT
        // Use this method to pass new impact points from any other script
        public void OnHit(Vector3 hitPoint, float hitAlpha = 1.0f)
        {
            // Check reaction interval
            if (gameObject && _curTime >= ReactSpeed)
            {
                // Hit point coordinates are transforment into local space
                Vector4 newHitPoint = _mesh.transform.InverseTransformPoint(hitPoint);

                // Clamp alpha value
                newHitPoint.w = Mathf.Clamp(hitAlpha, 0.0f, 1.0f);

                // Store new hit point data using current counter
                _shaderProps[_curProp] = newHitPoint;

                // Fix non-uniform scale
                if (FixNonUniformScale)
                {
                    if (!Mathf.Approximately(transform.lossyScale.x, transform.lossyScale.y) || !Mathf.Approximately(transform.lossyScale.y, transform.lossyScale.z) || !Mathf.Approximately(transform.lossyScale.y, transform.lossyScale.z))
                    {
                        _shaderProps[_curProp].x *= transform.lossyScale.x;
                        _shaderProps[_curProp].y *= transform.lossyScale.y;
                        _shaderProps[_curProp].z *= transform.lossyScale.z;
                    }
                }

                // Reset timer and advance counter
                _curTime = 0.0f;
                _curProp++;
                if (_curProp == _interpolators) _curProp = 0;
            }
        }

        // Called each frame to pass values into a shader
        private void FadeMask()
        {
            for (int i = 0; i < _interpolators; i++)
            {
                if (_shaderProps[i].w > 0f)
                {
                    // Lerp alpha value for current interpolator
                    _shaderProps[i].w = Mathf.Lerp(_shaderProps[i].w, -0.0001f, Time.deltaTime * DecaySpeed);
                    _shaderProps[i].w = Mathf.Clamp(_shaderProps[i].w, 0f, 1f);
                    // Assign new value to a shader variable
                    _mat.SetVector(_shaderPropsID[i], _shaderProps[i]);
                }
            }
        }

        // UPDATE
        private void Update()
        {
            // Advance response timer
            _curTime += Time.deltaTime;

            if (isFireDamage)
            {
                _onHit(Time.deltaTime * fireDamage);
                isFireDamage = false;
            }

            // Update shader each frame
            FadeMask();
        }
    }
}