//using System.Collections;
//using UnityEngine;

//public class Arrow : MonoBehaviour
//{
//    public float speed = 10f;
//    public Transform tip;

//    private Rigidbody _rigidBody;
//    private bool _inAir = false;
//    private Vector3 _lastPosition = Vector3.zero;

//    private void Awake()
//    {
//        _rigidBody = GetComponent<Rigidbody>();
//        PullInteraction.PullActionReleased += Release;
//        Stop();
//    }

//    private void OnDestroy()
//    {
//        PullInteraction.PullActionReleased -= Release;
//    }

//    private void Release(float value)
//    {
//        gameObject.transform.parent = null;
//        _inAir = true;
//        SetPhysics(true);

//        Vector3 force = transform.forward * value * speed;
//        _rigidBody.AddForce(force, ForceMode.Impulse);

//        StartCoroutine(RotateWithVelocity());
//        _lastPosition = tip.position;
//    }

//    private IEnumerator RotateWithVelocity()
//    {
//        yield return new WaitForFixedUpdate();

//        while (_inAir)
//        {
//            Quaternion newRotation = Quaternion.LookRotation(_rigidBody.linearVelocity, transform.up);
//            transform.rotation = newRotation;

//            yield return null;
//        }
//    }

//    private void FixedUpdate()
//    {
//        if (_inAir)
//        {
//            CheckCollision();
//            _lastPosition = tip.position;
//        }
//    }

//    private void CheckCollision()
//    {
//        if (Physics.Linecast(_lastPosition, tip.position, out RaycastHit hitInfo))
//        {
//            if (hitInfo.transform.gameObject.layer != 8)
//            {
//                if (hitInfo.transform.TryGetComponent(out Rigidbody body))
//                {
//                    _rigidBody.interpolation = RigidbodyInterpolation.None;
//                    transform.parent = hitInfo.transform;
//                    body.AddForce(_rigidBody.linearVelocity, ForceMode.Impulse);
//                }

//                Stop();
//            }
//        }
//    }

//    private void Stop()
//    {
//        _inAir = false;
//        SetPhysics(false);
//    }

//    private void SetPhysics(bool usePhysics)
//    {
//        _rigidBody.useGravity = usePhysics;
//        _rigidBody.isKinematic = !usePhysics;
//    }
//}


using System.Collections;
using UnityEngine;
public class Arrow : MonoBehaviour
{
    public float speed = 10f;
    public Transform tip;
    private Rigidbody _rigidBody;
    private bool _inAir = false;
    private Vector3 _lastPosition = Vector3.zero;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
        PullInteraction.PullActionReleased += Release;
        Stop();
    }

    private void OnDestroy()
    {
        PullInteraction.PullActionReleased -= Release;
    }

    private void Release(float value)
    {
        gameObject.transform.parent = null;
        _inAir = true;
        SetPhysics(true);
        Vector3 force = transform.forward * value * speed;
        _rigidBody.AddForce(force, ForceMode.Impulse);
        StartCoroutine(RotateWithVelocity());
        _lastPosition = tip.position;
    }

    private IEnumerator RotateWithVelocity()
    {
        yield return new WaitForFixedUpdate();
        while (_inAir)
        {
            // Check if velocity is significant enough to rotate
            if (_rigidBody.linearVelocity.magnitude > 1.0f)
            {
                // Use velocity instead of linearVelocity
                Quaternion newRotation = Quaternion.LookRotation(_rigidBody.linearVelocity, transform.up);
                transform.rotation = newRotation;
            }
            yield return null;
        }
    }

    private void FixedUpdate()
    {
        if (_inAir)
        {
            CheckCollision();
            _lastPosition = tip.position;
        }
    }

    private void CheckCollision()
    {
        if (Physics.Linecast(_lastPosition, tip.position, out RaycastHit hitInfo))
        {
            // Layer 8 is likely the arrow or player layer - ignore collisions with it
            if (hitInfo.transform.gameObject.layer != 8)
            {
                // Check if it hit a target
                if (hitInfo.transform.CompareTag("Target"))
                {
                    // Trigger score increase
                    if (GameManager.instance != null)
                    {
                        GameManager.instance.AddScore(10);
                    }
                }

                // Handle embedding in objects with rigidbodies
                if (hitInfo.transform.TryGetComponent(out Rigidbody body))
                {
                    _rigidBody.interpolation = RigidbodyInterpolation.None;
                    transform.parent = hitInfo.transform;
                    body.AddForce(_rigidBody.linearVelocity, ForceMode.Impulse);
                }

                // Stop the arrow's flight
                Stop();
            }
        }
    }

    private void Stop()
    {
        _inAir = false;
        SetPhysics(false);
    }

    private void SetPhysics(bool usePhysics)
    {
        _rigidBody.useGravity = usePhysics;
        _rigidBody.isKinematic = !usePhysics;
    }
}