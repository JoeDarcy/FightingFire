using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTest : MonoBehaviour
{
	[SerializeField] private GameObject parent = null;
	[SerializeField] private GameObject smokePuff = null;
	private GameObject smokePuffInstance = null;
	[SerializeField] private GameObject chopSound = null;
	[SerializeField] private GameObject fireInstance = null;
	[SerializeField] private GameObject fireSFX_01 = null;
	[SerializeField] private Material burnt = null;
	[SerializeField] private int health = 0;
	
	public int hitCounter = 0;
	private int hitsTemp = 0;

	private List<ParticleSystem> abc = null;

    private void Start()
    {
		parent = gameObject.GetComponentInParent<InteractiveObject>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
	    // Instantiate smoke puff when fire goes out
		if ((hitCounter >= health) &&
			(parent != null) &&
			(smokePuff != null))
	    {
		    if (smokePuffInstance == null)
		    {
			    Vector3 smokeRotation = new Vector3(-90.0f, 0.0f, 0.0f);
			    smokePuffInstance = Instantiate(smokePuff, transform.position, Quaternion.Euler(smokeRotation));
			}
		    
			// Change material to burnt when fire goes out
			parent.GetComponent<Renderer>().material = burnt;
			parent = null;
	    }
    }

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Axe") || other.CompareTag("Rocket_Launcher") || other.CompareTag("Flame_Thrower") || other.CompareTag("Fire_Extinguisher"))
        {
			Debug.Log("Hit!");
			hitCounter += 1;
			Debug.Log("Box HP: " + hitCounter);
			Instantiate(chopSound, transform.position, Quaternion.identity);
        }
		
	}
}
