using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	[SerializeField] GameObject attackerPrefab;
	GameObject[] Enemies;
	public int EnemyPoolSize = 10;
	public float spawnRate = 3f;
	int CurrentEnemy = 0;
	float randomX = 0.0f, randomY = 0.0f;
	void Awake()
	{
		Enemies = new GameObject[EnemyPoolSize];
	}
	void Start()
	{
		for (int i = 0; i < EnemyPoolSize; i++)
		{
			randomX = Random.Range(Random.Range(-45,0), Random.Range(0,45));
			randomY = Random.Range(31, 50);
			Enemies[i] = Instantiate(attackerPrefab, new Vector3(randomX, 1f, randomY), Quaternion.identity) as GameObject;
			Enemies[i].SetActive(false);
			Enemies[i].transform.SetParent(transform,true);
		}
		InvokeRepeating("Spawn", 0f, 2f);
	}
	void Spawn()
	{
		Enemies[CurrentEnemy].SetActive(true);

		CurrentEnemy++;

		if (CurrentEnemy >= EnemyPoolSize)
		{
			CurrentEnemy = 0;
		}
	}
}