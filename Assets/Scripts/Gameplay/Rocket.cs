using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
	private void OnTriggerEnter(Collider other) 
	{
		Destroy(this);
	}
}
