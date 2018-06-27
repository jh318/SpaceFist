﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour 
{
	public Camera cam;
	public GameObject pivot;
	public GameObject target;
	public Vector3 startPosition = new Vector3(0.0f, 2.0f, -10.0f);
	public float followSpeed = 5.0f;

	bool cursorLock = false;
	float m_MouseX = 0.0f;
	float m_MouseY = 0.0f;
	bool active = true;
	
	void Start()
	{
		active = true;

		cam.transform.parent = pivot.transform;
		target.transform.parent = target.transform;

		pivot.transform.localPosition = Vector3.zero;
		cam.transform.localPosition = Vector3.zero;
		cam.transform.localPosition = startPosition;
	}

	void Update()
	{
		m_MouseX = Input.GetAxis("Mouse X");
		m_MouseY = Input.GetAxis("Mouse Y");
		
		LockCursor();
	}

	void LateUpdate()
	{
		transform.position = Vector3.Lerp(transform.position, target.transform.position, followSpeed * Time.deltaTime);
		pivot.transform.eulerAngles += new Vector3(-m_MouseY, m_MouseX, 0.0f);
	}

	void LockCursor()
	{
		if(Input.GetKeyDown(KeyCode.C))
		{
			if(!cursorLock){
				Cursor.lockState = CursorLockMode.Locked;
				Debug.Log("Cursor Lock");
				cursorLock = true;
			}
			else
			{
				Cursor.lockState = CursorLockMode.None;
				cursorLock = false;
				Debug.Log("Cursor Unlocked");
			}
			
		}
	}
}
