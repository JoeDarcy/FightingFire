using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTimer : MonoBehaviour
{
    [SerializeField] private GameObject normalWalls = null;
    [SerializeField] private GameObject xrayWalls = null;
    [SerializeField] private float timerStart = 0.0f;
    [SerializeField] private float timer = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
	    timer = timerStart;
	    xrayWalls.SetActive(false);
        normalWalls.SetActive(true);
    }

    // Update is called once per frame
   void Update()
    {
	    timer -= Time.deltaTime;

	    if (timer < timerStart / 2)
	    {
            normalWalls.SetActive(false);
            xrayWalls.SetActive(true);
	    }
	    else if (timer > timerStart / 2)
	    {
		    xrayWalls.SetActive(false);
		    normalWalls.SetActive(true);
        }

	    if (timer < 0)
	    {
		    timer = timerStart;
        }
    }
}
