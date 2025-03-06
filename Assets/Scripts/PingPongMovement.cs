using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingPongMovement : MonoBehaviour
{

    public float speed = 2f;  
    public float distance = 3f;  // scale
    public bool moveHorizontally = false; // true: follow x axis; false; y axis

    private Vector2 startPosition;


    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (moveHorizontally)
        {
            float newX = startPosition.x + Mathf.PingPong(Time.time * speed, distance * 2) - distance;
            transform.position = new Vector2(newX, transform.position.y);
        }
        else
        {
            float newY = startPosition.y + Mathf.PingPong(Time.time * speed, distance * 2) - distance;
            transform.position = new Vector2(transform.position.x, newY);
        }
    }
}
