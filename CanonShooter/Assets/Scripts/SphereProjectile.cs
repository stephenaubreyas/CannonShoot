using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SphereProjectile : MonoBehaviour
{
    public Transform healthBar;

    public float health;

    public float towerHealth;

    public bool tower;

    public bool player;

    private void Awake()
    {
        health = 1;
    }

    private void OnTriggerEnter(Collider hit)
    {
        if (!GameManager.Instance.playerDead)
        {
            if (hit.gameObject.CompareTag("EnemyBullet"))
            {
                hit.gameObject.SetActive(false);

                if (player)
                {
                    health -= 0.25f;

                    healthBar.DOScaleX(health, 0.5f);

                    Destroy(hit.gameObject, .5f);

                    if (health == 0)
                    {
                        healthBar.localScale = Vector3.zero;

                        Destroy(this.gameObject);

                        GameManager.Instance.deathPanel.SetActive(true);

                        GameManager.Instance.deathSccoreText.text = "Your Score : " + GameManager.Instance.score.ToString();

                        GameManager.Instance.playerDead = true;
                    }
                }
                else
                {
                    towerHealth -= 0.5f;

                    Destroy(hit.gameObject, .5f);

                    if(towerHealth <= 0)
                    {
                        Destroy(this.gameObject);

                        GameManager.Instance.fences.Remove(this.gameObject);

                        if(GameManager.Instance.fences.Count == 0)
                        {
                            GameManager.Instance.fenceDestroyed = true;
                        }
                    }
                }
            }
        }
    }
}
