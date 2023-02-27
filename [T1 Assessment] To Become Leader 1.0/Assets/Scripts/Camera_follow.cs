using UnityEngine;

public class Camera_follow : MonoBehaviour
{
    //keep camera at a given distance from the player
    private Vector3 offset;
    [SerializeField] private Transform target;
    [SerializeField] private float smoothTime;
    private Vector3 currentVelocity = Vector3.zero;

    private void Awake() {
        offset = transform.position - target.position;
    }

    //function SmoothDamp allows the camera to smoothly follows the player, like a spring-damping function
    private void LateUpdate() {
        Vector3 targetPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothTime);
    }
}
