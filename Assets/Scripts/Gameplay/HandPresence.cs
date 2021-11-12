using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandPresence : MonoBehaviour
{
	[SerializeField] private GameObject normalFlat = null;
	[SerializeField] private GameObject xrayFlat = null;

	private InputDevice targetDevice;

	private bool xrayVision = false;

    // Start is called before the first frame update
    void Start()
    {
	    normalFlat.SetActive(true);
		xrayFlat.SetActive(false);

	    List<InputDevice> devices = new List<InputDevice>();
        InputDeviceCharacteristics rightControllerCharacteristics = InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(rightControllerCharacteristics, devices);

        foreach (var item in devices)
        {
	        Debug.Log(item.name + item.characteristics);
        }

        if (devices.Count > 0)
        { 
	        targetDevice = devices[0];
        }
       
    }

    // Update is called once per frame
    void Update()
    {
	    if (xrayVision == true)
	    {
		    normalFlat.SetActive(false);
		    xrayFlat.SetActive(true);
		}
	    else
	    {
		    normalFlat.SetActive(true);
		    xrayFlat.SetActive(false);
		}

		// Primary button
		if (targetDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonValue) && primaryButtonValue == true)
	    {

		    Debug.Log("Pressing primary button");
			xrayVision = true;
	    }
		else
		{
			xrayVision = false;
		}

		// Trigger
		if (targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue) && triggerValue > 0.1f) {
		    Debug.Log("Pressing trigger : " + triggerValue);
	    }

		// Joystick
		if (targetDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 primary2DAxisValue) && primary2DAxisValue !=  Vector2.zero) {
		    Debug.Log("Using joystick : " + primary2DAxisValue);
	    }
    }
}
