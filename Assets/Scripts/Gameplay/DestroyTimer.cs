using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTimer : MonoBehaviour
{
    [SerializeField] private float timerStart = 0.0f;
    private float timer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
	    timer = timerStart;
    }

    // Update is called once per frame
    void Update()
    {
	    timer -= Time.deltaTime;

	    if (timer <= 0)
	    {
		    Destroy(gameObject);
	    }
    }
}
