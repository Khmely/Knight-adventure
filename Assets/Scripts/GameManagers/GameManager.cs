﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public Text scoreHolder;
	public Text HealthCount;
    public Text monolog;
	public Image HealthUI;

    PlayerMovement pm;

    private int MAX_HEALTHCOUNT = 3;
	private int livesRemain;
    private int enemies;

    public static int score;

    public static bool GameIsPaused = false;

    public GameObject GameOverUI;

    private void Start()
    {
        //SetZero();
        PlayerPrefs.SetString("SceneName", SceneManager.GetActiveScene().name);
        if (SceneManager.GetActiveScene().name == "Level1") {
            ShowMonolog();
            Invoke("DisableText", 2f);
        }
    }

    private void Awake()
    {
        pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        livesRemain = MAX_HEALTHCOUNT;
        SetHealthUI();
    }

    public void ShowMonolog() 
    {
        monolog.text = "I need to find Ms Anna!";
    }

    public void Update()
    {
        scoreHolder.text = "Karma: " + score.ToString();
    }
    public void SetZero(){
        score = 0;
    }

    public void SetScore (int value) {
		score = score + value;
	}

	public void SetPlayerHealth (float healthRatio) {
		HealthUI.rectTransform.localScale = new Vector3 (healthRatio, 1, 0);
		if (healthRatio <= 0) {
			PlayerDied();
			HealthUI.rectTransform.localScale = new Vector3 (1, 1, 0);
		}
	}

    void DisableText()
    {
        monolog.enabled = false;
    }
    public void PlayerDied () {
		livesRemain -= 1;
        if (livesRemain <= 0)
        {
            pm.PlayerDead();
            Invoke("GameOverMenu", 0.8f);
        }
        SetHealthUI();
	}

	void SetHealthUI () {
		HealthCount.text = "X" + livesRemain.ToString ();
	}

    public void LoadMainMenu ()
    {
        SetTimeScale();
        SceneManager.LoadScene("MainMenu");
    }

    void SetTimeScale ()
    {
        Time.timeScale = 1;
    }

    public void GameOverMenu()
    {
        Time.timeScale = 0f;
        SetScore(-200);
        GameOverUI.SetActive(true);
        GameIsPaused = true;
    }

}
