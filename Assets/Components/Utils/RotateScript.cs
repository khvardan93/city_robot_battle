using UnityEngine;
using System.Collections;

public class RotateScript : MonoBehaviour
{
    private Vector3 startRotation;
    private Transform thisTransform;

    public float rotationStep = 5f;
    public float rotationSpeed = 0.06f;
    public float rotateBackTime = 0.09f;

    private void Start()
    {
        this.startRotation = this.transform.localRotation.eulerAngles;
        this.thisTransform = this.transform;
    }

    private void Update()
    {
        //Quaternion newRotation = Quaternion.AngleAxis(transform.rotation.eulerAngles.y + 5, Vector3.up);
        Vector3 newRotationVector = transform.localRotation.eulerAngles;
        //Quaternion newRotation = Quaternion.Euler(new Vector3(newRotationVector.x, newRotationVector.y - InputManager.scrollVector.x + rotationStep, newRotationVector.z - InputManager.scrollVector.y / 2));
        Quaternion newRotation = Quaternion.Euler(new Vector3(newRotationVector.x, newRotationVector.y + rotationStep, newRotationVector.z));
        newRotation = Quaternion.Slerp(newRotation, Quaternion.Euler(new Vector3(startRotation.x, newRotationVector.y, startRotation.z)), rotateBackTime);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, newRotation, rotationSpeed);
    }
}