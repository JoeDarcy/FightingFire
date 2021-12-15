using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
	[SerializeField] private GameObject explosion = null;
	[SerializeField] private GameObject smoke = null;

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Fire") || other.CompareTag("Wall"))
		{
			// Instantiate explosion and smoke
			Instantiate(explosion, transform.position, Quaternion.identity);
			Instantiate(smoke, transform.position, Quaternion.identity);
			// Destroy self on collision
			Destroy(gameObject);
		}
	}
}
