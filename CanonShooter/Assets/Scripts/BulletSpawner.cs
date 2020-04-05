using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
	[HideInInspector] public static bool playerAlive;
	public GameObject GrapeShot;
	GameObject[] Bullets;
	public GameObject Player;
	[SerializeField] Transform shoot;
	int bulletPool = 10;
	int bulletCount = 0;
	public float firePower;
	public float fireRate = 15f;
	float nextTimetoFire = 0f;
	public int maxAmmo = 10;
	int currentAmmo;
	public float reloadTime = 1f;
	bool isReloading = false;
	Vector3 pos;
	private void Awake()
	{
		playerAlive = true;
		bulletPool = maxAmmo;
		currentAmmo = maxAmmo;
	}
	private void Start()
	{
		Player = this.gameObject;
		Bullets = new GameObject[bulletPool];
		pos = transform.position;
		for (int i = 0; i < bulletPool; i++)
		{
			Bullets[i] = Instantiate(GrapeShot) as GameObject;
			Bullets[i].SetActive(false);
		}
	}
	private void OnEnable()
	{
		isReloading = false;
	}
	private void Update()
	{
		PlayerMovement();

		if (isReloading)
			return;

		if (currentAmmo <= 0 || Input.GetKeyDown(KeyCode.R))
		{
			StartCoroutine(Reload());
			return;
		}
		if (Input.GetMouseButtonDown(0) && Time.time >= nextTimetoFire)
		{
			nextTimetoFire = Time.timeScale + 1f / fireRate;
			Shoot();
		}
	}
	IEnumerator Reload()
	{
		isReloading = true;
		yield return new WaitForSeconds(reloadTime);
		currentAmmo = maxAmmo;
		isReloading = false;
	}
	void Shoot()
	{
		//if (Input.GetMouseButtonDown(0))
		currentAmmo--;
		Bullets[bulletCount].SetActive(true);
		Bullets[bulletCount].transform.position = shoot.transform.position;
		Bullets[bulletCount].transform.rotation = shoot.transform.rotation;
		Bullets[bulletCount].GetComponent<Rigidbody>().velocity = shoot.transform.up.normalized * firePower * Time.deltaTime;
		bulletCount++;

		if (bulletCount >= bulletPool)
		{
			bulletCount = 0;
		}
	}

	void PlayerMovement()
	{
		pos = transform.position;
		if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
		{
			pos.x -= 10 * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
		{
			pos.x += 10 * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
		{
			pos.z += 10 * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
		{
			pos.z -= 10 * Time.deltaTime;
		}
		transform.position = pos;
	}
	private void OnTriggerEnter(Collider hit)
	{
		if (hit.gameObject.tag == "Enemy")
		{
			Destroy(Player);
			playerAlive = false;
		}
	}
}