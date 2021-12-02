using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAlarm : MonoBehaviour
{
	[SerializeField] private float spinRate = 0.0f;

	// Update is called once per frame
    void Update()
    {
	    
	    if (transform.eulerAngles.y < 360)
	    {
		    spinRate *= 1.0f;
	    }
	    else
	    {
			spinRate *= -1.0f;
		}
		

	    transform.eulerAngles = new Vector3 (transform.eulerAngles.x, transform.eulerAngles.y + spinRate, transform.eulerAngles.z );

    }
}
