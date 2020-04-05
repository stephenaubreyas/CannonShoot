using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}