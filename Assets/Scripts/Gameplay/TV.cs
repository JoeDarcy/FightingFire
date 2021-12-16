using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TV : MonoBehaviour
{
    public static bool fireExtinguisherUnlockRange = false;
    public static bool flameThrowerUnlockRange = false;
    public static bool rocketLauncherUnlockRange = false;

    [SerializeField] private TextMeshPro pointsLeftToUnlock = null;

	[SerializeField] private GameObject fireExtinguisher = null;
	[SerializeField] private GameObject flameThrower = null;
	[SerializeField] private GameObject rocketLauncher = null;

	// Update is called once per frame
	void Update()
    {
	    if (fireExtinguisherUnlockRange == true)
	    {
			// Spawn fire extinguisher
			fireExtinguisher.SetActive(true);
		    pointsLeftToUnlock.text = (500 - Trampoline.points).ToString();
	    }

	    if (flameThrowerUnlockRange == true) 
	    {
			// Spawn flame thrower
			fireExtinguisher.SetActive(false);
			flameThrower.SetActive(true);
			pointsLeftToUnlock.text = (1000 - Trampoline.points).ToString();
	    }

	    if (rocketLauncherUnlockRange == true) 
	    {
			// Spawn rocket launcher
			flameThrower.SetActive(false);
			fireExtinguisher.SetActive(false);
			rocketLauncher.SetActive(true);
			pointsLeftToUnlock.text = (2000 - Trampoline.points).ToString();
	    }
	}
}
