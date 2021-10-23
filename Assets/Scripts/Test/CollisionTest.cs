using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTest : MonoBehaviour
{
	[SerializeField] private GameObject explosion = null;
	private int hitCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
	    if (hitCounter >= 5)
	    {
		    Destroy(gameObject);
	    }
    }

	private void OnTriggerEnter(Collider other)
	{
		Debug.Log("Hit!");
		hitCounter += 1;
		Instantiate(explosion, transform.position, Quaternion.identity);
	}
}
