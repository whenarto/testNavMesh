using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	//public int ID;
	public float speed;
	//public Text posText;

	private Rigidbody rb;

	// Start is called before the first frame update
	void Start()
	{
		rb = GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update()
	{

	}

	void FixedUpdate()
	{
		
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");
		Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
		rb.AddForce(movement * speed);

		//posText.text = "Player" + ID.ToString() + " Position:" + ((int)transform.position.x).ToString() + " " + ((int)transform.position.z).ToString();

		// if(Input.GetKeyDown(KeyCode.Space) && GameManager.count > 0)
		// {
		// 	transform.position = new Vector3(transform.position.x, transform.position.y + 10, transform.position.z);
		// 	GameManager.count = GameManager.count - 1;
		// }
	}
}