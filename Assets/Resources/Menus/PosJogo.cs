using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PosJogo : MonoBehaviour
{
    public string nomeDaCena, menu;

	void Start ()
    {
		
	}

	void Update ()
    {
        if (Input.GetButtonDown("Voltar"))
        {
            SceneManager.LoadScene(nomeDaCena,LoadSceneMode.Single);
            Time.timeScale = 1;
        }
        else if (Input.GetButtonDown("Pause"))
        {
            SceneManager.LoadScene(menu);
        }
    }
}
