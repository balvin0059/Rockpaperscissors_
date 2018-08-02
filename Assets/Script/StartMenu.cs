using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour {
    public Image info;
    public bool OpenInfo = false;
    private void Awake()
    {

    }
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
