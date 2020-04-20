using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public enum PlayerMovements
{
    None,
    Left,
    Right,
    Forward,
    Backward,
    ForwardRight,
    RightForward,
    ForwardLeft,
    LeftForward,
    BackwardRight,
    RightBackward,
    BackwardLeft,
    LeftBackward,
    LeftRight,
    RightLeft,
    ForwadBackward,
    BackwardForward,
    //ForwardRightSideRightClick,
    //ForwardLeftSideRightClick,
    //BehindRightSideRightClick,
    //BehindLeftSideRightClick
}

public enum MouseMovements
{
    None,
    RightClick,
    LeftClick,
    RightClickReleased
}

public enum ArrowKeys
{
    None,
    Up,
    Down,
    Left,
    Right
}

public class GameManager : MonoBehaviour
{
	public static GameManager Instance = null;

	#region Singleton
	private void CreateSingleton()
	{
		if (Instance == null)
			Instance = this;
		else if (Instance != this)
			Destroy(this.gameObject);
		DontDestroyOnLoad(this.gameObject);

		Time.timeScale = 1f;
	}
    #endregion

    public static Action shoot; 

    public static Action<PlayerMovements> playerMovement;

    public static Action<MouseMovements> mouseMovement;

    public Text scoreText;

    public PlayerMovements movement;

    public MouseMovements mouseMovements;

    public ArrowKeys key1;

    public ArrowKeys key2;

    public int score;

    public bool playerDead;

    public Text timerText;

    public float timerCount;

    public GameObject deathPanel;

    public Text deathSccoreText;

    public GameObject tower;

    public bool fenceDestroyed;

    public List<GameObject> fences = new List<GameObject>();

	void Awake()
	{
		CreateSingleton();

        Camera.main.transform.DOMoveZ(-140, 1.5f);
    }

    void Start()
	{
		Camera.main.DOColor(new Color(1,1,1),0.75f);

        timerCount = 300;
    }

    private void Update()
    {
        if (!playerDead)
        {
            timerCount -= Time.deltaTime;

            if ((int)(timerCount) < 0)
            {
                playerDead = true;
            }

            if ((int)timerCount % 60 <= 10)
            {
                timerText.text = "0" + ((int)(timerCount / 60)).ToString() + ":0" + ((int)(timerCount % 60)).ToString();
            }
            else
            {
                timerText.text = "0" + ((int)(timerCount / 60)).ToString() + ":" + ((int)(timerCount % 60)).ToString();
            }
        }
    }

    //public void Menu()
    //{

    //}

    public void Quit()
    {
        Application.Quit();
    }

    public void Restart()
	{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

    public static void InvokeShootMethod()
    {
        shoot?.Invoke();
    }

    public static void InvokePlayerMethod(PlayerMovements movement)
    {
        playerMovement?.Invoke(movement);
    }

    public static void InvokeMouseMethod(MouseMovements movement)
    {
        mouseMovement?.Invoke(movement);
    }
}