using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    public string bulletType;

    public float speed = 15f;
    public float lifeTime = 3f;
    public Vector2 direction = Vector2.right;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = direction.normalized * speed;
        Destroy(gameObject, lifeTime);  // destroy after some times
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(direction.normalized * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Breakable"))
        {
            other.GetComponent<DestroyOnHit>().OnHit(bulletType);  // cause effect on those blocks
            Destroy(gameObject); // destroy bullet
        }

        if (other.CompareTag("Unbreakable"))
        {
            Destroy(gameObject);  // destroy bullet
        }
    }


}