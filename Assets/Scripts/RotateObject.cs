using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{

    public float rotationSpeed = 100f;
    public bool rotateClockwise = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float direction = rotateClockwise ? -1f : 1f;

        transform.Rotate(0, 0, direction * rotationSpeed * Time.deltaTime);
    }
}
