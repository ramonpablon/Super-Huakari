using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogoScript : MonoBehaviour 
{
	
	void Start () 
	{

	}
	
	void Update () 
	{
		if (Time.timeSinceLevelLoad > 5)
			SceneManager.LoadScene ("MainMenu");
	}

}
