using UnityEngine;

public class HandTrigger : MonoBehaviour
{
    void Start()
    {
        // Tag this object as HandTrigger for easy detection
        gameObject.tag = "HandTrigger";
    }
}