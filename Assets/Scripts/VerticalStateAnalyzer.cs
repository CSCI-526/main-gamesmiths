using UnityEngine;

/// <summary>
/// Analyzes the vertical motion state of a floating object in a 2D environment.
/// Ideal for characters that never touch the ground (e.g., flying, hovering).
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class VerticalStateAnalyzer : MonoBehaviour
{
    private Rigidbody2D rb;

    public enum VerticalState
    {
        Ascending,
        Falling,
        Hovering
    }

    public VerticalState CurrentState { get; private set; }

    [SerializeField] private float hoverThreshold = 0.05f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        AnalyzeVerticalMotion();
    }

    /// <summary>
    /// Determines whether the player is ascending, falling, or hovering.
    /// </summary>
    private void AnalyzeVerticalMotion()
    {
        float yVel = rb.velocity.y;

        if (yVel > hoverThreshold)
        {
            CurrentState = VerticalState.Ascending;
        }
        else if (yVel < -hoverThreshold)
        {
            CurrentState = VerticalState.Falling;
        }
        else
        {
            CurrentState = VerticalState.Hovering;
        }

        // Optional: Uncomment for debugging
        // Debug.Log($"Vertical State: {CurrentState}");
    }
}
