using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatsAndWavesMover : MonoBehaviour
{
    public float rotationSpeed = 30f; // Speed of rotation in degrees per second
    public float maxTiltAngle = 30f; // Maximum tilt angle in degrees

    void Update()
    {
        // Rotate the object clockwise around its y-axis
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);

        // Calculate the tilt angles based on sine function to create a smooth tilt effect
        float tiltAngleX = Mathf.Sin(Time.time) * maxTiltAngle;
        float tiltAngleZ = Mathf.Sin(Time.time * 0.5f) * maxTiltAngle;

        // Apply the tilt to the object
        Quaternion targetRotation = Quaternion.Euler(tiltAngleX, transform.rotation.eulerAngles.y, tiltAngleZ);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }
}

