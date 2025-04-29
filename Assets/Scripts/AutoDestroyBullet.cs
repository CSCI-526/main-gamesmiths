using UnityEngine;

public class AutoDestroyUI : MonoBehaviour
{
    public float lifetime = 0.2f;
    void OnEnable()
    {
        Destroy(gameObject, lifetime);
    }
}