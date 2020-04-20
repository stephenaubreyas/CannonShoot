using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float chaseSpeed=5f;
    public float FallSpeed=2f;
    GameObject enemy;
	GameObject player;
	public NavMeshAgent agent;
	public GameObject explosion;
    public GameObject explosion1;

    GameObject temp;
    GameObject temp1;

    private Animator animator;
    public bool fall=false;

	private void Awake()
	{
        animator = GetComponent<Animator>();

        player = GameObject.FindGameObjectWithTag("Player");
        
        agent.speed = chaseSpeed;
		enemy = this.gameObject;
		enemy.GetComponent<Renderer>().material.color = new Color(0, 1f, 1f);

        Debug.Log("Player : " + player);

        animator.SetBool("idle", true);
	}

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("hey kedar?");

            Destroy(player);
        }
       
    }


    void OnTriggerEnter(Collider hit)
	{
		if (hit.gameObject.tag == "Bullet")
		{
            StartCoroutine((EnemyDead(hit)));
        }
       
	}

    IEnumerator EnemyDead(Collider hit)
    {
        temp = Instantiate(explosion, hit.gameObject.transform.position, Quaternion.identity) as GameObject;
        temp1 = Instantiate(explosion1, this.gameObject.transform.position, Quaternion.identity) as GameObject;

        Destroy(hit.gameObject);
        Destroy(temp, 1f);
        animator.SetBool("Fall", true);

        animator.SetBool("Walk", false);

        Stop();

        Destroy(temp1, 1.16f);


        yield return new WaitForSeconds(1.167f);

        enemy.SetActive(false);

        agent.isStopped = false;

        enemy.transform.position = new Vector3(Random.Range(Random.Range(-45, 0), Random.Range(0, 45)), 1f, Random.Range(31, 50));

        GameManager.Instance.score++;

        GameManager.Instance.scoreText.text = "Score : " + GameManager.Instance.score.ToString();
      
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
        animator.SetBool("idle", false);
        animator.SetBool("Walk", true);
        agent.speed = chaseSpeed;
        Debug.Log("Walking"); 
	}
	public void Stop()
	{
		agent.isStopped = true;
	}
}
