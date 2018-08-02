using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class EggSystem : MonoBehaviour {
    public static EggSystem _instance;//單例模式
    public static EggSystem instance
    {
        get
        {
            return _instance;
        }
    }
    [Serializable]
    public struct Eggstuff
    {
        public Image eggimage;
        public Image dinosaurimage;
        public Image eggValue;
        public int eggSpawnValue;
        public int eggSpawnMaxValue;
        public Sprite[] dinosaur;
        public Text dinosaurText;
    }

    public Eggstuff egg;
    private void Awake()
    {
        egg.eggSpawnValue = 0;
        egg.eggSpawnMaxValue = 5;
        _instance = this;
    }
    // Use this for initialization
    void Start () {
        egg.eggSpawnValue = PlayerPrefs.GetInt("Eggvalue", EggSystem.instance.egg.eggSpawnValue);
	}
	
	// Update is called once per frame
	void Update () {
        egg.eggValue.fillAmount = (float)egg.eggSpawnValue / egg.eggSpawnMaxValue;
    }
    public void OnContinue()
    {
        PlayerPrefs.SetInt("Eggvalue", EggSystem.instance.egg.eggSpawnValue);
        SceneManager.LoadScene(2);
    }
    public void OnBack()
    {
        PlayerPrefs.SetInt("Eggvalue", EggSystem.instance.egg.eggSpawnValue);
        SceneManager.LoadScene(0);
    }
    public void OnIncubation()
    {
        if (egg.eggSpawnValue == egg.eggSpawnMaxValue)
        {
            egg.eggimage.gameObject.SetActive(false);
            int n = UnityEngine.Random.Range(0, 4);
            egg.dinosaurimage.sprite = egg.dinosaur[n];
            egg.dinosaurimage.gameObject.SetActive(true);
            egg.eggSpawnValue = 0;
            egg.dinosaurText.text = GetDinosaurName(n);
        }
    }
    string GetDinosaurName(int a)
    {
        string str = "";
        switch (a)
        {
            case 0:
                str = "長 頸 龍";
                break;
            case 1:
                str = "暴 龍";
                break;
            case 2:
                str = "劍 龍";
                break;
            case 3:
                str = "翼 龍";
                break;
        }
        return str;
    }
}
