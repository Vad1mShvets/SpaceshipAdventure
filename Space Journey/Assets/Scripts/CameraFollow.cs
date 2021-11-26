using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    Transform target; //camera will follow this object
    Transform camTransform; //camera transform
    Vector3 offset; //offset between camera and target
    float smoothTime = 0.3f; //change this value to get desired smoothness
    private Vector3 velocity = Vector3.zero;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        camTransform = FindObjectOfType<Camera>().transform;
        offset = camTransform.position - target.position;
    }

    private void LateUpdate()
    {
        // update position
        Vector3 targetPosition = target.position + offset;
        camTransform.position = Vector3.SmoothDamp(this.transform.position, targetPosition, ref velocity, smoothTime);
    }
}
