using UnityEngine;
using UnityEngine.UI;

public class BulletEdgeDot : MonoBehaviour
{
    [Header("UI")]
    public RectTransform dot;        // assign your BulletDot here
    public Canvas canvas;            // your UI Canvas

    [Header("Detection")]
    public Transform player;         // your player transform
    public string bulletTag = "EnemyBullet";
    public float showDistance = 10f;

    Camera cam;
    bool showing;

    void Awake()
    {
        cam = Camera.main;
        dot.gameObject.SetActive(false);
    }

    void Update()
    {
        // 1) Find closest bullet
        GameObject[] bullets = GameObject.FindGameObjectsWithTag(bulletTag);
        float minDist = float.MaxValue;
        Vector3 nearest = Vector3.zero;
        foreach (var b in bullets)
        {
            float d = Vector3.Distance(player.position, b.transform.position);
            if (d < minDist)
            {
                minDist = d;
                nearest = b.transform.position;
            }
        }

        // 2) Show/hide based on distance
        if (minDist <= showDistance)
        {
            // convert to viewport (0..1)
            Vector3 vp = cam.WorldToViewportPoint(nearest);
            // create direction from screen center
            Vector2 dir = new Vector2(vp.x - 0.5f, vp.y - 0.5f).normalized;
            // clamp so the dot stays just inside the edge
            Vector2 clamped = dir * 0.45f + Vector2.one * 0.5f;

            // map viewport to canvas local coords
            Vector2 size = canvas.GetComponent<RectTransform>().sizeDelta;
            Vector2 localPos = new Vector2(
                (clamped.x - 0.5f) * size.x,
                (clamped.y - 0.5f) * size.y
            );

            dot.anchoredPosition = localPos;

            if (!showing)
            {
                dot.gameObject.SetActive(true);
                showing = true;
            }
        }
        else if (showing)
        {
            dot.gameObject.SetActive(false);
            showing = false;
        }
    }
}
