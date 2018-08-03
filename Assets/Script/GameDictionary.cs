using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameDictionary : MonoBehaviour {
    [Serializable]
    public struct Dictionarystuff
    {
        public Image[] pet;
        public Sprite[] pet_spr;
    }
    public Dictionarystuff dic;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void OnBack()
    {
        SceneManager.LoadScene(1);
    }
}
