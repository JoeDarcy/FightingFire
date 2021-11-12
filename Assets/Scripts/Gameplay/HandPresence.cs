using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandPresence : MonoBehaviour
{
	
	public List<GameObject> controllerPrefabs;
	private InputDevice targetDevice;
	private GameObject spawnerController;

	

    // Start is called before the first frame update
    void Start()
    {
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
			GameObject prefab = controllerPrefabs.Find(controller => controller.name == targetDevice.name);
			if (prefab)
			{
				spawnerController = Instantiate(prefab, transform);
			}
			else
			{
				Debug.LogError("Did not find corresponding controller model");
				spawnerController = Instantiate(controllerPrefabs[0], transform);
			}
        }
       
    }

    // Update is called once per frame
    void Update()
    {
	    // Primary button
		if (targetDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonValue) && primaryButtonValue == true)
	    {

		    Debug.Log("Pressing primary button");
			XrayVision.xrayVision = true;
	    }
		else
		{
			XrayVision.xrayVision = false;
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
