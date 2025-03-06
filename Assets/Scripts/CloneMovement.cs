using UnityEngine;

public class CloneMovement : MonoBehaviour
{
    private PlayerController player;

    public void Initialize(PlayerController playerController)
    {
        player = playerController;
    }

    void Update()
    {
        if (player != null)
        {
            transform.position = player.transform.position + new Vector3(0, 2f, 0);
        }
    }
}