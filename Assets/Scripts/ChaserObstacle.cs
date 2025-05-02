using UnityEngine;

public class ChaserObstacle : MonoBehaviour
{
    public float chaseSpeed = 3f;
    private Transform target;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (target == null) return;

        Vector3 chaseDirection = (target.position - transform.position).normalized;
        transform.position += chaseDirection * chaseSpeed * Time.deltaTime;
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Player"))
            Debug.Log("Threat has caught the hero!");
    }
}
