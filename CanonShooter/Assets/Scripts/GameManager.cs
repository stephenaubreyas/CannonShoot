using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Movement
{
    None,
    Left,
    Right,
    Forward,
    ForwardRight,
    Backward,
    ForwardRightSideRightClick,
    ForwardLeftSideRightClick,
    BehindRightSideRightClick,
    BehindLeftSideRightClick
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

    public static Action<Movement> playerMovement;

    public Text scoreText;

    public Movement movement;

    public int score;

	void Awake()
	{
		CreateSingleton();

        playerMovement += Movements;
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

    void Movements(Movement movements)
    {
        if(movements == Movement.Left)
        {
            
        }
        else if (movements == Movement.Right)
        {

        }
        else if (movements == Movement.Forward)
        {

        }
        else if (movements == Movement.Backward)
        {

        }
        else if (movements == Movement.ForwardLeftSideRightClick)
        {

        }
        else if (movements == Movement.ForwardRight)
        {

        }
        else if (movements == Movement.ForwardRightSideRightClick)
        {

        }
        else if (movements == Movement.BehindLeftSideRightClick)
        {

        }
        else if (movements == Movement.BehindRightSideRightClick)
        {

        }
    }

    public static void InvokeShootMethod()
    {
        shoot?.Invoke();
    }

    public static void InvokePlayerMethod(Movement movement)
    {
        playerMovement?.Invoke(movement);
    }
}