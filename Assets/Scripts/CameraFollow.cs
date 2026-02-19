using Unity.VisualScripting;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    [SerializeField]
    private Vector3 offset;

    private Vector3 velocity;

    private Vector3 nextPosition;

    [SerializeField]
    private float smoothTime = 0.25f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void LateUpdate()
    {
        float direction = Mathf.Sign(target.localScale.x);
        
        nextPosition = target.position + new Vector3(
            offset.x * direction,
            offset.y,
            transform.position.z
        );
        transform.position = Vector3.SmoothDamp(
            transform.position,
            nextPosition,
            ref velocity,
            smoothTime
        );
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
