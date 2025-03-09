using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateKey : MonoBehaviour
{
    public float rotationSpeed = 100f; // Adjust speed if needed

    void Update()
    {
        // Rotate continuously on the X-axis
        transform.Rotate(Vector3.right * rotationSpeed * Time.deltaTime);
    }
}
