using UnityEngine;

public class AlwaysDisableKey : MonoBehaviour
{
    void OnEnable()
    {
        // Immediately disable this GameObject whenever it becomes enabled.
        gameObject.SetActive(false);
    }
}
