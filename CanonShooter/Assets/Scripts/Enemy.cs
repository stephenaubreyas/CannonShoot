using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float distance, radius;

    public float count;

    public Vector3 center;

    public Rigidbody prefab;


    public float chaseSpeed = 5f;
    public float FallSpeed = 2f;
    GameObject enemy;
    GameObject player;
    public NavMeshAgent agent;
    public GameObject explosion;
    public GameObject explosion1;

    GameObject temp;
    GameObject temp1;

    private Animator animator;
    public bool fall = false;

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
        if (collision.gameObject.tag == "Player")
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

        GetPlayerSpawnPosition();
    }

    public void GetPlayerSpawnPosition()
    {
        if (!GameManager.Instance.playerDead)
        {
            //Debug.Log( Time.deltaTime);

            agent.SetDestination(player.transform.position);

            animator.SetBool("idle", false);

            animator.SetBool("Walk", true);

            agent.speed = chaseSpeed;

            distance = Vector3.Distance(transform.position, center);

            if (distance <= radius)
            {
                Stop();

                count += Time.deltaTime;

                if (count <= 5 && (int)count > 0)
                {
                    Rigidbody obj = Instantiate(prefab, new Vector3(transform.position.x, 0.5f, transform.position.z), Quaternion.identity);

                    obj.transform.SetParent(this.transform);

                    if (GameManager.Instance.fenceDestroyed)
                    {
                        obj.velocity = new Vector3(player.transform.position.x - transform.position.x, -0.5f, player.transform.position.z - transform.position.z);
                    }
                    else
                    {
                        foreach (var i in GameManager.Instance.fences)
                        {
                            obj.velocity = new Vector3(i.transform.position.x - transform.position.x, -0.5f, i.transform.position.z - transform.position.z);
                        }
                    }

                    count--;
                }
            }
        }
        else
        {
            Destroy(enemy);
        }
    }

    public void Stop()
    {
        animator.SetBool("idle", true);

        animator.SetBool("Walk", false);

        agent.isStopped = true;
    }
}
