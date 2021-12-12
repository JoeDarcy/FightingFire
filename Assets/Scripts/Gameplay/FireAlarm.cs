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


	    var transform1 = transform;
	    var eulerAngles = transform1.eulerAngles;
	    eulerAngles = new Vector3 (eulerAngles.x, eulerAngles.y + spinRate, eulerAngles.z );
	    transform1.eulerAngles = eulerAngles;
    }
}
