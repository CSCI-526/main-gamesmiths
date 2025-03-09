using UnityEngine;

public class GhostBlock : MonoBehaviour
{
    public void DestroyBlock()
    {
        Debug.Log("Ghost Block Destroyed!");
        Destroy(gameObject);
    }
}