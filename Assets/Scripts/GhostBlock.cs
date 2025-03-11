using UnityEngine;

public class GhostBlock : MonoBehaviour
{
    // This method is called when the wall collides with something.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the colliding object is the player.
        if (collision.gameObject.CompareTag("Player"))
        {
            // Check if the player has collected the ghost ability.
            if (PlayerController.ghostAbilityCollected)
            {
                Debug.Log("Pink wall (GhostBlock) broken by ghost ability!");
                DestroyBlock();
                
                // Optionally, consume the ghost ability so it can be used only once.
                // PlayerController.ghostAbilityCollected = false;
            }
            else
            {
                Debug.Log("Player does not have ghost ability yet, cannot break pink wall.");
            }
        }
    }

    public void DestroyBlock()
    {
        Debug.Log("Ghost Block Destroyed!");
        Destroy(gameObject);
    }
}
