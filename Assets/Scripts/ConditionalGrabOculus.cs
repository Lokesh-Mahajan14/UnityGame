using UnityEngine;

public class ConditionalGrabOculus : OVRGrabbable
{
    public BalloonTracker balloonTracker;

    // This replaces the non-existent CanGrab method
    protected override void Start()
    {
        base.Start();
        // Make sure grab points are properly initialized
        if (m_grabPoints == null || m_grabPoints.Length == 0)
        {
            Collider collider = GetComponent<Collider>();
            if (collider != null)
            {
                m_grabPoints = new Collider[] { collider };
            }
        }
    }

    // Primary grab validation
    public override void GrabBegin(OVRGrabber hand, Collider grabPoint)
    {
        if (CanGrab(hand))
        {
            base.GrabBegin(hand, grabPoint);
        }
    }

    // Our custom grab condition check
    private bool CanGrab(OVRGrabber hand)
    {
        return balloonTracker != null &&
               balloonTracker.IsBalloonInAir &&
               !isGrabbed &&
               (hand == null || hand.grabbedObject == null);
    }
}