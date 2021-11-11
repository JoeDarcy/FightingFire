using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{
	[SerializeField] private int sceneToLoad = 0;

	private void OnTriggerEnter(Collider other)
	{
		SceneManager.LoadScene(sceneToLoad);
	}

}
