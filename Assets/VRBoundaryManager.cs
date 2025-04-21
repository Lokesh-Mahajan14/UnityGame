using UnityEngine;
using Oculus.Platform;
using Oculus.Platform.Models;

public class VRBoundaryManager : MonoBehaviour
{
    public Transform environment; // Assign your VR environment (e.g., floor, walls)

    void Start()
    {
        if (OVRManager.boundary.GetConfigured())
        {
            Vector3 playAreaSize = OVRManager.boundary.GetDimensions(OVRBoundary.BoundaryType.PlayArea);
            ScaleEnvironment(playAreaSize);
        }
        else
        {
            Debug.Log("Oculus Guardian boundary not set up.");
        }
    }

    void ScaleEnvironment(Vector3 playAreaSize)
    {
        if (environment != null)
        {
            environment.localScale = new Vector3(playAreaSize.x, environment.localScale.y, playAreaSize.z);
            Debug.Log("Environment scaled to fit play area: " + playAreaSize);
        }
    }
}
