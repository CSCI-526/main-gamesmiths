using UnityEngine;
using UnityEngine.U2D; // for SpriteShapeController

[RequireComponent(typeof(SpriteShapeController))]
public class CustomBoxColliders : MonoBehaviour
{
    public float wallThickness = 0.2f; // how tall or thick each box should be
    public int subdivisionsPerSegment = 5; // how many times to subdivide between spline points

    private SpriteShapeController shapeController;

    void Start()
    {
        shapeController = GetComponent<SpriteShapeController>();

        // Clear any old colliders
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        // Build box colliders along the shape
        GenerateBoxColliders();
    }

    void GenerateBoxColliders()
    {
        var spline = shapeController.spline;
        int pointCount = spline.GetPointCount();

        for (int i = 0; i < pointCount - 1; i++)
        {
            // For each pair of control points, we can subdivide
            Vector3 startPos = spline.GetPosition(i);
            Vector3 endPos   = spline.GetPosition(i + 1);

            // Convert local spline positions to world positions (if needed)
            startPos = transform.TransformPoint(startPos);
            endPos   = transform.TransformPoint(endPos);

            for (int s = 0; s < subdivisionsPerSegment; s++)
            {
                float t1 = s / (float)subdivisionsPerSegment;
                float t2 = (s + 1) / (float)subdivisionsPerSegment;

                // Interpolate along the segment
                Vector3 segStart = Vector3.Lerp(startPos, endPos, t1);
                Vector3 segEnd   = Vector3.Lerp(startPos, endPos, t2);

                // Create a child object to hold this box collider
                CreateBoxColliderSegment(segStart, segEnd, s + (i * subdivisionsPerSegment));
            }
        }
    }

    void CreateBoxColliderSegment(Vector3 start, Vector3 end, int index)
    {
        // Midpoint for the collider’s transform
        Vector3 midpoint = (start + end) * 0.5f;

        // Direction from start to end
        Vector3 direction = end - start;
        float length = direction.magnitude;

        // Create a child GameObject
        GameObject segmentObj = new GameObject("BoxColliderSegment_" + index);
        segmentObj.transform.SetParent(this.transform);
        segmentObj.transform.position = midpoint;

        // Rotate to match the segment direction
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        segmentObj.transform.rotation = Quaternion.Euler(0, 0, angle);

        // Add BoxCollider2D
        BoxCollider2D box = segmentObj.AddComponent<BoxCollider2D>();
        box.size = new Vector2(length, wallThickness);
    }
}
