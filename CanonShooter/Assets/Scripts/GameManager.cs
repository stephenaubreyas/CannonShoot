using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    BackwardForward
    //ForwardRightSideRightClick,
    //ForwardLeftSideRightClick,
    //BehindRightSideRightClick,
    //BehindLeftSideRightClick
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

    public Text scoreText;

    public PlayerMovements movement;

    public ArrowKeys key1;

    public ArrowKeys key2;

    public int score;

	void Awake()
	{
		CreateSingleton();
    }

	void Start()
	{
		Camera.main.backgroundColor = new Color(1,1,1);
	}
	public static void Restart()
	{
		if (Projectile.isAlive == false)
			UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
	}

   

    public static void InvokeShootMethod()
    {
        shoot?.Invoke();
    }

    public static void InvokePlayerMethod(PlayerMovements movement)
    {
        playerMovement?.Invoke(movement);
    }
}