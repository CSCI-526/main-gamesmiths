using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum PowerUpType { SpeedBoost, DoubleJump }
    public PowerUpType powerUpType;
    public float duration = 5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement player = other.GetComponent<PlayerMovement>();
            if (player != null)
            {
                switch (powerUpType)
                {
                    case PowerUpType.SpeedBoost:
                        StartCoroutine(player.ActivateSpeedBoost(duration));
                        break;
                    case PowerUpType.DoubleJump:
                        StartCoroutine(player.EnableDoubleJump(duration));
                        break;
                }
                Destroy(gameObject);
            }
        }
    }
}
