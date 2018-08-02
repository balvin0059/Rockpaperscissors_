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
        public string[] dinosaur_name;
        public Text dinosaurText;
        public GameObject bgShine;
        public GameObject barholder;
        public Image again;
    }
    [Serializable]
    public struct Effectstuff
    {
        public GameObject eff_broke;
    }
    float rotateBg = 0.0f;
    bool eggIncubation = false;
    public Eggstuff egg;
    public Effectstuff eff;
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
    void Update() {
        //背景旋轉
        if (eggIncubation == true)
        {
            egg.bgShine.gameObject.SetActive(true);
            rotateBg += Time.deltaTime * 15;
            egg.bgShine.transform.localRotation = Quaternion.Euler(0, 0, rotateBg);
        }
        if (egg.eggSpawnValue == egg.eggSpawnMaxValue)
        {
            eggIncubation = true;
            Incubation();
        }
        egg.eggValue.fillAmount = (float)egg.eggSpawnValue / egg.eggSpawnMaxValue;
    }
    #region 按鈕部分
    public void OnContinue()
    {
        eggIncubation = false;
        egg.barholder.SetActive(true);
        egg.again.gameObject.SetActive(false);
        PlayerPrefs.SetInt("Eggvalue", EggSystem.instance.egg.eggSpawnValue);
        SceneManager.LoadScene(2);
    }
    public void OnBack()
    {
        eggIncubation = false;
        egg.barholder.SetActive(true);
        egg.again.gameObject.SetActive(false);
        PlayerPrefs.SetInt("Eggvalue", EggSystem.instance.egg.eggSpawnValue);
        SceneManager.LoadScene(0);
    }
    #endregion

    //孵化
    public void Incubation()
    {
        Firework();
        if (egg.eggSpawnValue == egg.eggSpawnMaxValue)
        {
            egg.barholder.SetActive(false);
            egg.again.gameObject.SetActive(true);
            egg.eggimage.gameObject.SetActive(false);
            int n = UnityEngine.Random.Range(0, 15);
            egg.dinosaurimage.sprite = egg.dinosaur[n];
            egg.dinosaurimage.gameObject.SetActive(true);
            egg.eggSpawnValue = 0;
            egg.dinosaurText.text = GetDinosaurName(n);
        }
    }
    //放煙火
    public void Firework()
    {
        Instantiate(eff.eff_broke);
    }
    //名字
    string GetDinosaurName(int a)
    {
        return "恭 喜 孵 出 了"+ egg.dinosaur_name[a];
    }
}
