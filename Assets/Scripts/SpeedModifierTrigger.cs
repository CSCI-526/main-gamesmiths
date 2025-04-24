using UnityEngine;

public class SpeedModifierTrigger : MonoBehaviour
{
    public enum ModifierType { SpeedBoost, Slowdown, Reverse }
    public ModifierType modifierType;

    public float duration = 1.5f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                switch (modifierType)
                {
                    case ModifierType.SpeedBoost:
                        player.ModifySpeedTemporarily(2f, duration);
                        break;
                    case ModifierType.Slowdown:
                        player.ModifySpeedTemporarily(0.5f, duration);
                        break;
                    case ModifierType.Reverse:
                        player.ModifySpeedTemporarily(-1f, duration);
                        break;
                }

                Destroy(gameObject); // Optional: remove the trigger after use
            }
        }
    }
}
