using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // 玩家对象
    public float smoothSpeed = 5f; // 摄像机跟随的平滑速度
    public Vector3 offset = new Vector3(0, 0, 0); // 偏移量，Z轴为 -10 保持摄像机深度

    void LateUpdate()
    {
        if (player != null)
        {
            // 目标位置 = 玩家位置 + 偏移量
            Vector3 targetPosition = player.position + offset;

            // 平滑移动摄像机
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
        }
    }
}
