using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelRestart : MonoBehaviour
{
	private int sceneToLoad = 0;

	private void Start()
	{
		sceneToLoad = SceneManager.GetActiveScene().buildIndex;
	}

	private void OnTriggerEnter(Collider other)
	{
		SceneManager.LoadScene(sceneToLoad);
	}
}
