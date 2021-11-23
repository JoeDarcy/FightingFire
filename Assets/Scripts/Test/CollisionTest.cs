using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTest : MonoBehaviour
{
	[SerializeField] private GameObject parent = null;
	[SerializeField] private GameObject smokePuff = null;
	[SerializeField] private GameObject fireInstance = null;
	[SerializeField] private GameObject fireSFX_01 = null;
	[SerializeField] private Material burnt = null;
	[SerializeField] private int health = 0;
	
	private int hitCounter = 0;
	private int hitsTemp = 0;

	private List<ParticleSystem> abc = null;

    private void Start()
    {
		parent = gameObject.GetComponentInParent<InteractiveObject>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
	    if ((hitCounter >= health) &&
			(parent != null) &&
			(smokePuff != null))
	    {
			Vector3 smokeRotaion = new Vector3(-90.0f, 0.0f, 0.0f);
			GameObject obj = Instantiate(smokePuff, transform.position, Quaternion.Euler(smokeRotaion));
			//Destroy(parent); //change to instansiate puff of smoke, dont destroy
			//call function in interactiveObject to make it not on fire
			//parent.GetComponent<InteractiveObject>().isBurning = false;
			//parent.GetComponent<InteractiveObject>().burntOut = false;

			parent.GetComponent<Renderer>().material = burnt;
			parent = null;
	    }

		if (hitCounter <= health && (parent != null) && hitCounter != hitsTemp)
        {
			hitsTemp = hitCounter;

			fireInstance = gameObject.transform.parent.gameObject.GetComponentInChildren<InteractiveObject>().gameObject;

			ParticleSystem[] ps = fireInstance.GetComponentsInChildren<ParticleSystem>();

			for (int i = 0; i < ps.Length; ++i)
            {
				ps[i].gameObject.transform.localScale = ps[i].gameObject.transform.localScale * 0.8f;
			}

			fireInstance = null;
			ps = null;
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Axe"))
        {
			Debug.Log("Hit!");
			hitCounter += 1;
			Debug.Log("Box HP: " + hitCounter);
		}
		
	}
}
