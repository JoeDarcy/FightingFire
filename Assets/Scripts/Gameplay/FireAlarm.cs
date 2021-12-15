using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAlarm : MonoBehaviour
{
	[SerializeField] private float spinRate = 0.0f;
	[SerializeField] private GameObject light_1 = null;
	[SerializeField] private GameObject light_2 = null;
	[SerializeField] private GameObject fireAlarmSound = null;

	// Update is called once per frame
	void Update()
    {
	    if (InteractiveObject.totalFiresInScene <= 0 && InteractiveObject.fireCountStarted == true)
	    {
			// Turn off the fire alarm lights when the fires go out
			light_1.SetActive(false);
			light_2.SetActive(false);

			// Turn off the fire alarm sound when fires go out
			fireAlarmSound.SetActive(false);

		}

	    if (transform.eulerAngles.y < 360)
	    {
		    spinRate *= 1.0f;
	    }
	    else
	    {
			spinRate *= -1.0f;
		}


	    var transform1 = transform;
	    var eulerAngles = transform1.eulerAngles;
	    eulerAngles = new Vector3 (eulerAngles.x, eulerAngles.y + spinRate, eulerAngles.z );
	    transform1.eulerAngles = eulerAngles;
    }
}
