using UnityEngine;

public class MiniMap : MonoBehaviour
{
	[SerializeField] Transform player;
	[SerializeField] GameObject miniMapBorder;
	[SerializeField] float borderColorTimer = 5f;
	Vector3 newPosition;
	float timeSinceChange = 0f;
	private void Start()
	{
		newPosition.y = transform.position.y;
		newPosition.z = player.transform.position.z + 20f;
		transform.position = newPosition;
		transform.rotation = Quaternion.Euler(90f,player.eulerAngles.y,0f);
		InvokeRepeating("ColorChange",timeSinceChange,borderColorTimer);
	}
	void ColorChange()
	{
		//timeSinceChange += Time.deltaTime;
		//if (timeSinceChange >= borderColorTimer)
		//{
			miniMapBorder.GetComponent<UnityEngine.UI.Image>().color = new Color(Random.value, Random.value, Random.value);
			//timeSinceChange = 0f;
		//}
	}
	//Player follow
	//private void LateUpdate()
	//{
	//	newPosition = player.transform.position;
	//	newPosition.y = transform.position.y;
	//	transform.position = newPosition;

	//  transform.rotation = Quaternion.Euler(90f,player.eulerAngles.y,0f);
	//}
}