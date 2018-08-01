﻿using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameCore : MonoBehaviour
{

    public enum Hand
    {
        None = 0,
        Rock,
        Paper,
        Scissors
    }
    public enum Result
    {
        None = 0,
        Draw,
        Win,
        Lose
    }
    [Serializable]
    public struct UIstuff
    {
        public Image timeLine;
        public Image Result;
        public Sprite ResultDraw;
        public Sprite ResultWin;
        public Sprite ResultLose;
        public Image gameOverPanel;
        public Sprite gameVictory;
        public Sprite gameDefeat;
        public Text winText;
        public Text loseText;
        public int win;
        public int lose;
    }
    [Serializable]
    public struct Enemystuff
    {
        public Image enemyHand;
        public Sprite enemyHandPaper;
        public Sprite enemyHandScissors;
        public Sprite enemyHandRock;
        public Image hpBar;
        public float hp;
    }
    [Serializable]
    public struct Playerstuff
    {
        public Image hpBar;
        public float hp;
        public Image damageBarRock;
        public Image damageBarPaper;
        public Image damageBarScissors;
    }
    [Serializable]
    public struct Effectstuff
    {
        public GameObject Eff_star;
        public GameObject Eff_lose;
        public GameObject Eff_draw;
    }
    private float turnTime = 3.0f;
    private bool turnStart = false;
    private float damage;

    public UIstuff myUI;
    public Enemystuff enemy;
    public Playerstuff player;
    public Effectstuff effect;
    private Hand hand;
    private Hand enemyHand;
    private Result result;
    void Start()
    {

    }
    void Update()
    {
        if (turnStart) { Timeline(); }
        enemy.hpBar.fillAmount = enemy.hp / 100;
        player.hpBar.fillAmount = player.hp / 100;
        if(enemy.hp <= 0.0f)
        {
            Gameover(0);
        }
        else if(player.hp <= 0.0f)
        {
            Gameover(1);
        }
    }
    #region 點擊按鈕
    public void OnClickRock()
    {
        if (!turnStart)
        {
            TurnBattle(Hand.Rock);
            hand = Hand.Rock;
            turnStart = true;
        }
    }
    public void OnClickPaper()
    {
        if (!turnStart)
        {
            TurnBattle(Hand.Paper);
            hand = Hand.Paper;
            turnStart = true;
        }
    }
    public void OnClickScissors()
    {
        if (!turnStart)
        {
            TurnBattle(Hand.Scissors);
            hand = Hand.Scissors;
            turnStart = true;
        }
    }
    public void OnRelese()
    {
        if (turnTime != 3)
        {
            damage = 3 - turnTime;
            turnTime = 0.0f;
        }
    }
    #endregion
    //計時器
    void Timeline()
    {
        turnTime -= Time.deltaTime;
        myUI.timeLine.fillAmount = turnTime / 3;
        switch (hand)
        {
            case Hand.Rock:
                player.damageBarRock.fillAmount = turnTime / 3;
                break;
            case Hand.Paper:
                player.damageBarPaper.fillAmount = turnTime / 3;
                break;
            case Hand.Scissors:
                player.damageBarScissors.fillAmount = turnTime / 3;
                break;
        }
        
        if (turnTime <= 0)
        {
            ResultWinOrLose();
        }
    }
    public void TurnBattle(Hand hand)
    {
        //關閉結果圖案
        myUI.Result.gameObject.SetActive(false);
        //關閉敵人出拳
        enemy.enemyHand.gameObject.SetActive(false);
        EnemyAiHand();
        if (hand == enemyHand)
        {
            result = Result.Draw;
        }
        else
        {
            if (hand == Hand.Paper)
            {
                result = (enemyHand == Hand.Rock) ? Result.Win : Result.Lose;
            }
            if (hand == Hand.Rock)
            {
                result = (enemyHand == Hand.Scissors) ? Result.Win : Result.Lose;
            }
            if (hand == Hand.Scissors)
            {
                result = (enemyHand == Hand.Paper) ? Result.Win : Result.Lose;
            }
        }
    }
    void EnemyAiHand()
    {
        enemyHand = (Hand)UnityEngine.Random.Range(1, 4);
        Debug.Log(enemyHand);
        switch (enemyHand)
        {
            case Hand.Paper:
                enemy.enemyHand.sprite = enemy.enemyHandPaper;
                break;
            case Hand.Rock:
                enemy.enemyHand.sprite = enemy.enemyHandRock;
                break;
            case Hand.Scissors:
                enemy.enemyHand.sprite = enemy.enemyHandScissors;
                break;
        }
    }
    void GetDamage(Result res)
    {
        if (res == Result.Win)
        {
            enemy.hp -= damage * 10;
            Debug.Log("對敵人造成了：" + damage + "傷害");
        }
        if (res == Result.Lose)
        {
            player.hp -= damage * 10;
            Debug.Log("敵人對你造成了：" + damage + "傷害");
        }
    }
    void ResultWinOrLose()
    {
        switch (result)
        {
            case Result.Draw:
                myUI.Result.sprite = myUI.ResultDraw;
                GameObject eff_d = Instantiate(effect.Eff_draw);
                eff_d.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
                break;
            case Result.Win:
                GetDamage(result);
                myUI.Result.sprite = myUI.ResultWin;
                myUI.win += 1;
                GameObject eff_w = Instantiate(effect.Eff_star);
                eff_w.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
                break;
            case Result.Lose:
                GetDamage(result);
                myUI.Result.sprite = myUI.ResultLose;
                myUI.lose += 1;
                GameObject eff_l = Instantiate(effect.Eff_lose);
                eff_l.transform.position = new Vector3(0.0f, 2.0f, 0.0f);
                break;
        }
        #region 重置狀態
        turnTime = 3.0f;
        myUI.timeLine.fillAmount = 1;
        player.damageBarRock.fillAmount = 1;
        player.damageBarPaper.fillAmount = 1;
        player.damageBarScissors.fillAmount = 1;
        turnStart = false;
        enemy.enemyHand.gameObject.SetActive(true);
        myUI.Result.gameObject.SetActive(true);
        #endregion
    }

    #region 遊戲結束選項
    void Gameover(int a)
    {
        myUI.gameOverPanel.gameObject.SetActive(true);
        #region 重置狀態
        myUI.Result.gameObject.SetActive(false);
        enemy.enemyHand.gameObject.SetActive(false);
        myUI.timeLine.fillAmount = 1;
        player.damageBarRock.fillAmount = 1;
        player.damageBarPaper.fillAmount = 1;
        player.damageBarScissors.fillAmount = 1;
        #endregion
        if(a == 0)
        {
            myUI.gameOverPanel.sprite = myUI.gameVictory;
        }else if(a == 1)
        {
            myUI.gameOverPanel.sprite = myUI.gameDefeat;
        }
        myUI.winText.text = myUI.win.ToString();
        myUI.loseText.text = myUI.lose.ToString();
    }
    public void OnContinue()
    {
        myUI.gameOverPanel.gameObject.SetActive(false);
        player.hp = 100;
        enemy.hp = 100;
    }
    public void OnQuit()
    {
        SceneManager.LoadScene(0);
    }
    #endregion
}

