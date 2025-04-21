using UnityEngine;
using OVR;

public class OculusTossableBalloon : MonoBehaviour
{
    [Header("Tuning Settings")]
    public float floatForce = 8f;           // Upward force when tossed
    public float floatDrag = 1.2f;          // Air resistance when floating
    public float gravityScale = 0.3f;       // Custom gravity multiplier
    public float resetHeight = 0.5f;        // Height at which balloon resets
    public float maxHeight = 5f;            // Maximum height to limit floating

    public BalloonGameManager gameManager;

    [Header("References")]
    private Rigidbody rb;
    private Vector3 initialPosition;
    private Transform grabber;

    [Header("State")]
    private bool isInAir = false;
    private bool isGrabbed = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        initialPosition = transform.position;
        ConfigureRigidbody();
    }

    void ConfigureRigidbody()
    {
        rb.useGravity = false;
        rb.linearDamping = 0f;
        rb.angularDamping = 0.5f;
        rb.mass = 0.7f;
    }

    void Update()
    {
        HandleGrabbing();
        HandleRelease();
        ApplyFloatingPhysics();
        CheckResetCondition();
    }

    void HandleGrabbing()
    {
        if (isGrabbed && grabber != null)
        {
            transform.position = grabber.position;
        }
    }

    void HandleRelease()
    {
        if (isGrabbed && grabber != null)
        {
            var controller = grabber.name.Contains("Left") ?
                OVRInput.Controller.LTouch : OVRInput.Controller.RTouch;

            if (OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger, controller))
            {
                TossBalloon(controller);
            }
        }
    }

    void ApplyFloatingPhysics()
    {
        if (isInAir)
        {
            rb.AddForce(Physics.gravity * gravityScale, ForceMode.Acceleration);

            if (transform.position.y > maxHeight)
            {
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
            }
        }
    }

    void CheckResetCondition()
    {
        if (isInAir && transform.position.y < resetHeight)
        {
            ResetBalloon();
            if (gameManager != null)
                gameManager.BalloonDropped();
        }
    }

    //void OnTriggerStay(Collider other)
    //{
    //    if (other.CompareTag("HandTrigger") && !isGrabbed && !isInAir)
    //    {
    //        var controller = other.name.Contains("Left") ?
    //            OVRInput.Controller.LTouch : OVRInput.Controller.RTouch;

    //        if (OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, controller))
    //        {
    //            GrabBalloon(other.transform);
    //        }
    //    }
    //}

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("HandTrigger") && !isGrabbed) // Removed !isInAir condition
        {
            var controller = other.name.Contains("Left") ?
                OVRInput.Controller.LTouch : OVRInput.Controller.RTouch;

            if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, controller))
            {
                GrabBalloon(other.transform);
                isInAir = false; // Reset isInAir when grabbed
            }
        }
    }

    void GrabBalloon(Transform hand)
    {
        isGrabbed = true;
        grabber = hand;
        rb.isKinematic = true;
        rb.linearVelocity = Vector3.zero;

        var controller = hand.name.Contains("Left") ?
            OVRInput.Controller.LTouch : OVRInput.Controller.RTouch;
        OVRInput.SetControllerVibration(0.3f, 0.3f, controller);
    }

    public void TossBalloon(OVRInput.Controller controller)
    {
        if (isGrabbed)
        {
            isGrabbed = false;
            isInAir = true;
            rb.isKinematic = false;
            rb.linearDamping = floatDrag;

            // Use controller velocity + upward boost
            Vector3 controllerVelocity = OVRInput.GetLocalControllerVelocity(controller);
            rb.linearVelocity = controllerVelocity + Vector3.up * floatForce;

            grabber = null;

            if (gameManager != null)
                gameManager.BalloonTossed();
        }
    }

    public void ResetBalloon()
    {
        isInAir = false;
        isGrabbed = false;
        rb.isKinematic = true;
        rb.linearDamping = 0f;
        rb.linearVelocity = Vector3.zero;
        transform.position = initialPosition;
    }

    public bool IsInAir() => isInAir;
    public bool IsGrabbed => isGrabbed;
}