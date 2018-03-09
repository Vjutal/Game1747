using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
	GameObject cameraTarget;
	public float rotateSpeed;
	public float offsetDistance;
	public float offsetHeight;
	public float smoothing;
	float rotate;
	bool following = true;
	Vector3 offset;
	Vector3 targetLastPosition;

	void Start()
	{
		cameraTarget = GameObject.FindGameObjectWithTag("Player");

		targetLastPosition =  new Vector3(
                cameraTarget.transform.position.x,
                cameraTarget.transform.position.y + offsetHeight,
                cameraTarget.transform.position.z - offsetDistance);

        Debug.Log(targetLastPosition.ToString());

		offset =  new Vector3(
                cameraTarget.transform.position.x,
                cameraTarget.transform.position.y + offsetHeight,
                cameraTarget.transform.position.z - offsetDistance);

        Debug.Log(offset.ToString());

    }

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.F))
		{
            following = !following;
			//if(following)
			//{
			//	following = false;
			//} 
			//else
			//{
			//	following = true;
			//}
		}

		if(Input.GetKey(KeyCode.Q))
		{
			rotate = -1;
		} 
		else if(Input.GetKey(KeyCode.E))
		{
			rotate = 1;
		} 
		else
		{
			rotate = 0;
		}

		if(following)
		{
			offset = 
                Quaternion.AngleAxis(rotate * rotateSpeed, Vector3.up) * offset;

            Debug.Log(offset.ToString());

			transform.position =
                cameraTarget.transform.position + offset; 

			transform.position = new Vector3(
                Mathf.Lerp(targetLastPosition.x, cameraTarget.transform.position.x + offset.x, smoothing * Time.deltaTime), 
				Mathf.Lerp(targetLastPosition.y, cameraTarget.transform.position.y + offset.y, smoothing * Time.deltaTime), 
				Mathf.Lerp(targetLastPosition.z, cameraTarget.transform.position.z + offset.z, smoothing * Time.deltaTime));
		} 
		else
		{
            transform.position = targetLastPosition; 
		}
		transform.LookAt(cameraTarget.transform.position);
	}

	void LateUpdate()
	{
		targetLastPosition = transform.position;
	}
}