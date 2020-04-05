using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
	GameObject enemy;
	GameObject player;
	public NavMeshAgent agent;
	public GameObject explosion;
	GameObject temp;
	private void Awake()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		enemy = this.gameObject;
		enemy.GetComponent<Renderer>().material.color = new Color(0, 1f, 1f);
	}

	void OnTriggerEnter(Collider hit)
	{
		if (hit.gameObject.tag == "Bullet")
		{
			temp = Instantiate(explosion, hit.gameObject.transform.position, Quaternion.identity) as GameObject;
			enemy.SetActive(false);
			enemy.transform.position = new Vector3(Random.Range(Random.Range(-45, 0), Random.Range(0, 45)), 1f, Random.Range(31, 50));
			//hit.gameObject.SetActive(false);
			Destroy(hit.gameObject);
			Destroy(temp, 1f);
		}
	}
	private void FixedUpdate()
	{
		if (Projectile.isAlive == false)
		{
			Stop();
		}
		else
		{
			GetPlayerSpawnPosition();
		}
	}
	public void GetPlayerSpawnPosition()
	{
		agent.SetDestination(player.transform.position);
	}
	public void Stop()
	{
		agent.isStopped = true;
	}
}
