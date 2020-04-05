using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Level : MonoBehaviour
{
	public NavMeshSurface surface;

	public float width, height, DGroundPos;

	public GameObject wall, player, Ground, Restriction;

	[HideInInspector] public static Vector3 playerSpawnPosition;

	public static bool playerSpawned = false;

	GameObject Wall;
	void Start()
	{
		UnityEngine.SceneManagement.Scene scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
		if (scene.name == "Main")
		{
			GenerateLevel();
			surface.BuildNavMesh();
		}
		else if (scene.name == "3D_CannonShooter")
			Restriction = null;
	}
	// Create a grid based level
	void GenerateLevel()
	{
		// Loop over the grid
		for (int x = 0; x <= width; x += 2)
		{
			for (int y = 0; y <= height; y += 2)
			{
				// Should we spawn a wall?
				if (Random.value > .85f)
				{
					// Spawn a wall
					Vector3 pos = new Vector3(x - width / 2f, DGroundPos, y - height / 2f);
					Wall = Instantiate(this.wall, pos, Quaternion.identity, transform);
					Wall.GetComponent<Renderer>().material.color = new Color(0.5f, 0.5f, 0.5f);
				}
				else if (!playerSpawned) // Should we spawn a player?
				{
					player.transform.position = new Vector3(0f, DGroundPos, (y - height) + 10);
					playerSpawnPosition = player.transform.position;
					player.transform.rotation = Quaternion.identity;
					player.GetComponent<Renderer>().material.color = Color.cyan;
					playerSpawned = true;
					Restriction.transform.position = playerSpawnPosition;
				}
			}
		}
	}
}