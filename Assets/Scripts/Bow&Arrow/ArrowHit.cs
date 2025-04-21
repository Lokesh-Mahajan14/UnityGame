using UnityEngine;
public class ArrowHit : MonoBehaviour
{
    // Original collision method (keep for non-trigger colliders)
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Target"))
        {
            GameManager.instance.AddScore(10);
            Destroy(collision.gameObject); // Optional: destroy on hit
        }
    }

    // Add this method for trigger colliders
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Target"))
        {
            GameManager.instance.AddScore(10);
            Destroy(other.gameObject); // Optional: destroy on hit
        }
    }
}