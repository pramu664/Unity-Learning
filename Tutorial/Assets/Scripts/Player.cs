using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerController))]
public class Player : MonoBehaviour {
	public float moveSpeed;
	PlayerController controller;
	Camera viewCamera;

	void Start () {
		controller = GetComponent<PlayerController> ();
		viewCamera = Camera.main; 
	}
	
	void Update () {
		Vector3 input = new Vector3 (Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));	
		Vector3 direction = input.normalized;
		Vector3 moveVelocity = moveSpeed * direction;
		controller.Move (moveVelocity);

		Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);
		Plane groundPlane = new Plane (Vector3.up, Vector3.zero);
		float rayDistance;
		if (groundPlane.Raycast (ray, out rayDistance)) {
			Vector3 point = ray.GetPoint (rayDistance);
			Debug.DrawLine (ray.origin, point, Color.red);
			controller.LookAt (point);
		}
	}
}
