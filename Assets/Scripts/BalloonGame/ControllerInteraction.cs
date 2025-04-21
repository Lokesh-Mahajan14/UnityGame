using UnityEngine;
using OVR;

public class ControllerInteraction : MonoBehaviour
{
    public OVRInput.Controller controller;
    public OculusTossableBalloon bigBalloon;

    void Update()
    {
        // Check for balloon toss action (release while holding)
        if (bigBalloon != null && bigBalloon.IsGrabbed &&
            OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger, controller))
        {
            bigBalloon.TossBalloon(controller); // Pass the controller
        }
    }
}