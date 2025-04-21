using UnityEngine;

public class BalloonEffect : MonoBehaviour
{
    public float floatStrength = 5f; // Adjust in Inspector
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false; // Optional: Disable default gravity
    }

    void FixedUpdate()
    {
        rb.AddForce(Vector3.up * floatStrength, ForceMode.Force);
    }
}