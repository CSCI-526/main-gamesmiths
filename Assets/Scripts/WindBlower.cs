using UnityEngine;

public class WindBlower : MonoBehaviour
{
    [Header("Wind Settings")]
    public float blowForce = 10f;
    public float blowDuration = 0.5f;
    public Vector2 blowDirection = Vector2.right;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                StartCoroutine(BlowPlayer(rb));
            }
        }
    }

    System.Collections.IEnumerator BlowPlayer(Rigidbody2D rb)
    {
        float timer = 0f;
        while (timer < blowDuration)
        {
            rb.AddForce(blowDirection.normalized * blowForce);
            timer += Time.deltaTime;
            yield return null;
        }
    }
}
