/*
 * Copyright (c) Meta Platforms, Inc. and affiliates.
 * All rights reserved.
 *
 * Licensed under the Oculus SDK License Agreement (the "License");
 * ...
 */

using UnityEngine;
using UnityEngine.EventSystems;

public class HandedInputSelector : MonoBehaviour
{
    private OVRCameraRig m_CameraRig;
    private OVRInputModule m_InputModule;

    void Start()
    {
        // Updated to use FindFirstObjectByType to avoid obsolete warnings
        m_CameraRig = FindFirstObjectByType<OVRCameraRig>();
        m_InputModule = FindFirstObjectByType<OVRInputModule>();
    }

    void Update()
    {
        if (OVRInput.GetActiveController() == OVRInput.Controller.LTouch)
        {
            SetActiveController(OVRInput.Controller.LTouch);
        }
        else
        {
            SetActiveController(OVRInput.Controller.RTouch);
        }
    }

    void SetActiveController(OVRInput.Controller c)
    {
        Transform t = (c == OVRInput.Controller.LTouch)
            ? m_CameraRig.leftHandAnchor
            : m_CameraRig.rightHandAnchor;

        m_InputModule.rayTransform = t;
    }
}
