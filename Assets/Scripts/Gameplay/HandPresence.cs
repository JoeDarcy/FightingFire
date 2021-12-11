using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandPresence : MonoBehaviour
{
	public bool showController = false;
	public InputDeviceCharacteristics controllerCharacteristics;	
	public List<GameObject> controllerPrefabs;

	public GameObject handModelPrefab;

	private InputDevice targetDevice;
	private GameObject spawnedController = null;
	private GameObject spawnedHandModel = null;
	private Animator handAnimator;

	private bool deviceActive = false;

	private void Start()
	{
		CreateDeviceList();
	}

	// Update is called once per frame
	void Update()
    {
		// Create a list of connected devices
	    if (deviceActive == false)
	    {
			CreateDeviceList();
	    }

		// Show either the controller or the hand models
		if (showController == true)
		{
			// Show controller
			if (spawnedController && spawnedHandModel != null)
			{
				spawnedHandModel.SetActive(false);
				spawnedController.SetActive(true);
			}
		}
		else
		{
			// Show hands
			if (spawnedController && spawnedHandModel != null)
			{
				spawnedHandModel.SetActive(true);
				spawnedController.SetActive(false);
			}

			// Update hands animation
			UpdateHandAnimation();
		}

	    // Primary button Left Hand (X Button)
		if (targetDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonValue) && primaryButtonValue == true && gameObject.CompareTag("Left_Hand"))
	    {

		    Debug.Log("Pressing primary button X");
		    XrayVision.xrayVision = true;
			
	    }
		else if (primaryButtonValue == false && gameObject.CompareTag("Left_Hand"))
		{
			XrayVision.xrayVision = false;
		}

		// Secpondary Left Hand (Y button)
		if (targetDevice.TryGetFeatureValue(CommonUsages.secondaryButton, out bool secondaryButtonValue) && secondaryButtonValue == true && gameObject.CompareTag("Left_Hand")) {

			//Debug.Log("Pressing secondary button Y");
			showController = true;

		} else 
		{
			showController = false;
		}

		// Trigger
		if (targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue) && triggerValue > 0.1f) {
		    //Debug.Log("Pressing trigger : " + triggerValue);
	    }

		// Joystick
		if (targetDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 primary2DAxisValue) && primary2DAxisValue !=  Vector2.zero) {
		    //Debug.Log("Using joystick : " + primary2DAxisValue);
	    }
    }

	// Update hand animation based on buttons pressed
	void UpdateHandAnimation()
	{
		// If trigger is pressed update the trigger animatior float to set pinch animation
		if (targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
		{
			handAnimator.SetFloat("Trigger", triggerValue);
		}
		else
		{
			handAnimator.SetFloat("Trigger", 0);
		}

		// If grip is pressed update the grip animatior float to set fist animation
		if (targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue)) 
		{
			handAnimator.SetFloat("Grip", gripValue);
		} else 
		{
			handAnimator.SetFloat("Grip", 0);
		}
	}

    private void CreateDeviceList()
    {
	    List<InputDevice> devices = new List<InputDevice>();

	    InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, devices);

	    foreach (var item in devices) {
		    Debug.Log(item.name + item.characteristics);
	    }

	    if (devices.Count > 0) {
		    targetDevice = devices[0];
		    GameObject prefab = controllerPrefabs.Find(controller => controller.name == targetDevice.name);
		    if (prefab) {
			    spawnedController = Instantiate(prefab, transform);
				deviceActive = true;
		    } else {
			    Debug.LogError("Did not find corresponding controller model");
			    spawnedController = Instantiate(controllerPrefabs[0], transform);
		    }
	    }

	    spawnedHandModel = Instantiate(handModelPrefab, transform);
		handAnimator = spawnedHandModel.GetComponent<Animator>();
    }
}
