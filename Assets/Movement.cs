using UnityEngine;

public class FlyCamera : MonoBehaviour
{
    public float speed = 10f;
    public float mouseSensitivity = 2f;

    float yaw = 0f;
    float pitch = 0f;

    void Update()
    {
        Vector3 dir = Vector3.zero;
        if (Input.GetKey(KeyCode.W)) dir += transform.forward;
        if (Input.GetKey(KeyCode.S)) dir -= transform.forward;
        if (Input.GetKey(KeyCode.D)) dir += transform.right;
        if (Input.GetKey(KeyCode.A)) dir -= transform.right;
        if (Input.GetKey(KeyCode.E)) dir += transform.up;
        if (Input.GetKey(KeyCode.Q)) dir -= transform.up;

        transform.position += dir.normalized * speed * Time.deltaTime;
    }
}