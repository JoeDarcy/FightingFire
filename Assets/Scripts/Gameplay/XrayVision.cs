using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XrayVision : MonoBehaviour
{
	[SerializeField] private GameObject normalFlat = null;
	[SerializeField] private GameObject xrayFlat = null;

	public static bool xrayVision = false;

    // Start is called before the first frame update
    void Start()
    {
	    normalFlat.SetActive(true);
	    xrayFlat.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
	    Debug.Log("XrayVision vision: " + xrayVision);

		if (xrayVision == true) {
			normalFlat.SetActive(false);
			xrayFlat.SetActive(true);
		} else {
			normalFlat.SetActive(true);
			xrayFlat.SetActive(false);
		}
	}
}
