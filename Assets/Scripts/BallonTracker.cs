using UnityEngine;

public class BalloonTracker : MonoBehaviour
{
    public bool IsBalloonInAir = false;
    private Rigidbody rb;

    void Start() => rb = GetComponent<Rigidbody>();

    void Update()
    {
        IsBalloonInAir = (transform.position.y > 1.0f);
    }
}