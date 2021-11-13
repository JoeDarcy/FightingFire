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
	private GameObject spawnerController;
	private GameObject spawnHandModel;

	private bool deviceActive = false;

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
			spawnHandModel.SetActive(false);
			spawnerController.SetActive(true);
		}
		else
		{
			spawnHandModel.SetActive(true);
			spawnerController.SetActive(false);
		}

	    // Primary button Left Hand (X Button)
		if (targetDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonValue) && primaryButtonValue == true && gameObject.CompareTag("Left_Hand"))
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
			    spawnerController = Instantiate(prefab, transform);
				deviceActive = true;
		    } else {
			    Debug.LogError("Did not find corresponding controller model");
			    spawnerController = Instantiate(controllerPrefabs[0], transform);
		    }
	    }

	    spawnHandModel = Instantiate(handModelPrefab, transform);
    }
}
