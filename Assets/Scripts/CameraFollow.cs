using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Vector3 offset = new Vector3(0, 0, -10);
    [SerializeField] private float smoothSpeed = 0.125f;
    [SerializeField] private Transform player;

    [Header("Camera Bounds")]
    [SerializeField] private float minX = -Mathf.Infinity;
    [SerializeField] private float maxX = Mathf.Infinity;
    [SerializeField] private float minY = -Mathf.Infinity;
    [SerializeField] private float maxY = Mathf.Infinity;

    private void LateUpdate()
    {
        if (player == null) return;

        Vector3 targetPosition = player.position + offset;

        //clamp camera position to defined bounds
        float clampedX = Mathf.Clamp(targetPosition.x, minX, maxX);
        float clampedY = Mathf.Clamp(targetPosition.y, minY, maxY);

        Vector3 clampedPosition = new Vector3(clampedX, clampedY, targetPosition.z);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, clampedPosition, smoothSpeed);

        transform.position = smoothedPosition;
    }
}
