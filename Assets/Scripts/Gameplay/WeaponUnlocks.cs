using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUnlocks : MonoBehaviour
{
    [SerializeField] private GameObject fireExtinguisher = null;
    [SerializeField] private GameObject flameThrower = null;
    [SerializeField] private GameObject rocketLauncher = null;
    [SerializeField] private GameObject explosion = null;

    // Update is called once per frame
	void Update()
    {
	    // Trigger range for lift TV
	    TV.fireExtinguisherUnlockRange = true;

		// Unlock the fire extinguisher
		if (Trampoline.points >= 500 && Trampoline.points < 1000)
	    {
			// Trigger smoke effect
		    if (fireExtinguisher.activeSelf == false)
		    {
				// Spawn smoke effect
			    NewUnlock(fireExtinguisher);
			    // Turn on fire extinguisher
				fireExtinguisher.SetActive(true);
				// Trigger range for lift TV
				TV.fireExtinguisherUnlockRange = false;
				TV.flameThrowerUnlockRange = true;
			}
	    }

		// Unlock the flame thrower
	    if (Trampoline.points >= 1000 && Trampoline.points < 2000)
	    {
		    // Trigger smoke effect
		    if (flameThrower.activeSelf == false) 
		    {
			    // Spawn smoke effect
				NewUnlock(flameThrower);
			    // Turn on flame thrower
			    flameThrower.SetActive(true);
				// Trigger range for lift TV
				TV.flameThrowerUnlockRange = false;
				TV.rocketLauncherUnlockRange = true;
			}
	    }

	    // Unlock the rocket launcher
	    if (Trampoline.points >= 2000 && Trampoline.points < 100000) 
	    {
		    // Trigger smoke effect
		    if (rocketLauncher.activeSelf == false)
		    {
			    // Spawn smoke effect
				NewUnlock(rocketLauncher);
			    // Turn on rocket launcher
			    rocketLauncher.SetActive(true);
		    }
	    }
	}

	// Instantiate a smoke puff where and when a weapon unlocks
	private void NewUnlock(GameObject weapon)
	{
		Instantiate(explosion, weapon.transform.position, Quaternion.identity);
	}
}
