using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour {
    public Image info;
    public bool OpenInfo = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void OnStart()
    {        
        SceneManager.LoadScene(1);
    }
    public void OnQuit()
    {
        PlayerPrefs.SetInt("Eggvalue", EggSystem.instance.egg.eggSpawnValue);
        Application.Quit();
    }
    public void OnInfo()
    {
        if (OpenInfo)
        {
            info.gameObject.SetActive(false);
            OpenInfo = false;
        }
        else if (!OpenInfo) {
            info.gameObject.SetActive(true);
            OpenInfo = true;
        }  
    }
}
