using UnityEngine;
using OVR;

public class OculusGrabbableSmallBall : MonoBehaviour
{
    public BalloonGameManager gameManager;
    public GameObject rightBasket;

    private Rigidbody rb;
    private Vector3 initialPosition;
    private bool isTransferred = false;
    private bool isGrabbed = false;
    private Transform grabber;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        initialPosition = transform.position;
    }

    void Update()
    {
        if (isGrabbed && grabber != null)
        {
            // Move with hand
            transform.position = grabber.position;

            // Check for release button
            OVRInput.Controller controller = grabber.name.Contains("Left") ?
                OVRInput.Controller.LTouch : OVRInput.Controller.RTouch;

            if (!OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, controller))
            {
                ReleaseObject();
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("HandTrigger") && !isGrabbed && !isTransferred)
        {
            // Check if big ball is in the air to allow grabbing
            if (gameManager != null && !gameManager.IsBigBallInAir())
                return;

            OVRInput.Controller controller = other.name.Contains("Left") ?
                OVRInput.Controller.LTouch : OVRInput.Controller.RTouch;

            if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, controller))
            {
                GrabObject(other.transform);
            }
        }

        // Check if ball is in the right basket
        if (other.gameObject == rightBasket && isGrabbed == false && !isTransferred)
        {
            // Only count as transferred if the big ball is in the air
            if (gameManager != null && gameManager.IsBigBallInAir())
            {
                isTransferred = true;
                if (gameManager != null)
                    gameManager.BallTransferred();
            }
        }
    }

    void GrabObject(Transform hand)
    {
        isGrabbed = true;
        grabber = hand;
        rb.isKinematic = true;

        // Give haptic feedback
        OVRInput.Controller controller = hand.name.Contains("Left") ?
            OVRInput.Controller.LTouch : OVRInput.Controller.RTouch;
        OVRInput.SetControllerVibration(0.2f, 0.2f, controller);
    }

    void ReleaseObject()
    {
        isGrabbed = false;
        grabber = null;
        rb.isKinematic = false;

        // Check if near right basket
        if (rightBasket != null)
        {
            float distanceToRightBasket = Vector3.Distance(transform.position, rightBasket.transform.position);
            if (distanceToRightBasket < 0.2f)
            {
                // Successfully placed in right basket if big ball is in air
                if (gameManager != null && gameManager.IsBigBallInAir())
                {
                    isTransferred = true;
                    if (gameManager != null)
                        gameManager.BallTransferred();
                }
                else
                {
                    // Return to left basket since big ball isn't in air
                    ResetPosition();
                }
            }
        }
    }

    public void ResetPosition()
    {
        isGrabbed = false;
        isTransferred = false;
        grabber = null;
        rb.isKinematic = true;
        transform.position = initialPosition;
        rb.isKinematic = false;
    }
}