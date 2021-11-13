using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LocomotionController : MonoBehaviour
{
    public XRController rightTeleportRay;
    public XRController leftTeleportRay;
    public InputHelpers.Button teleportActivationButton;
    public float activationThreshold = 0.1f;

    // Update is called once per frame
    void Update()
    {
        // Check for right teleport ray and enable it if button threshold is reached
	    if (rightTeleportRay)
	    {
		    rightTeleportRay.gameObject.SetActive(CheckIfActivated(rightTeleportRay));
	    }
	    // Check for left teleport ray and enable it if button threshold is reached
        if (leftTeleportRay) 
        {
		    leftTeleportRay.gameObject.SetActive(CheckIfActivated(leftTeleportRay));
	    }
    }

    // Checks to see if a trigger button threshold is reached
    public bool CheckIfActivated(XRController controller)
    {
	    InputHelpers.IsPressed(controller.inputDevice, teleportActivationButton, out bool isActivated, activationThreshold);

	    return isActivated;
    }
}
