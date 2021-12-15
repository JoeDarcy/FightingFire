using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrower : MonoBehaviour
{
	private void OnTriggerEnter(Collider other) 
	{
		if (other.CompareTag("Fire"))
		{
			other.GetComponent<CollisionTest>().hitCounter += 1;
		}
	}
}
