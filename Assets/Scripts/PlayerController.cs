using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public bool thirdPerson = true;
	public float maxTurnSpeed = 500;

	float rotation = 0.0f;

	float m_horizontal = 0.0f;
	public float Horizontal { get { return  m_horizontal; } }
	float m_vertical = 0.0f;
	public float Vertical { get { return m_vertical; } }
	float m_horizontalRaw = 0.0f;
	float m_verticalRaw = 0.0f;


	Vector3 targetDirection;
	Vector3 axis;
	Vector3 forwardToTargetCross;
	Vector3 targetVector;
	

	Animator animator;
	ThirdPersonCamera tpc;

	void Awake()
	{
		animator = GetComponent<Animator>();
	}

	void Start()
	{
		if(thirdPerson)
		{
			tpc = GameObject.FindObjectOfType<ThirdPersonCamera>().GetComponent<ThirdPersonCamera>();
		}
	}


	void Update()
	{
		GetAxes();
		SetAnimator();
		
		if(Input.GetButtonDown("Fire1"))
		{
			animator.SetTrigger("Attack");
		}
		if(Input.GetButtonDown("Fire2"))
		{
			animator.SetTrigger("Attack2");
		}
		


		if(thirdPerson)
		{
			ThirdPersonCamera();
		}
	}

	void GetAxes()
	{
		m_horizontal = Input.GetAxis("Horizontal");
		m_vertical = Input.GetAxis("Vertical");
		m_horizontalRaw = Input.GetAxisRaw("Horizontal");
		m_verticalRaw = Input.GetAxisRaw("Vertical");
	}

	void SetAnimator()
	{
		if(animator != null)
		{
			animator.SetFloat("Horizontal", m_horizontal);
			animator.SetFloat("Vertical", m_vertical);
			animator.SetFloat("Speed", Mathf.Abs(m_horizontal) + Mathf.Abs(m_vertical)); //TODO Find better formula
		}	
	}

	void ThirdPersonCamera()
	{
		if(tpc != null)
		{
			axis = new Vector3(m_horizontalRaw, 0.0f, m_verticalRaw);
			targetDirection = (tpc.cam.transform.forward * axis.z + tpc.cam.transform.right * axis.x);
			forwardToTargetCross = Vector3.Cross(transform.forward, targetDirection);
			rotation = forwardToTargetCross.y;

			if(Vector3.Dot(transform.forward, targetDirection) < 0.0f)
			{
				rotation = Mathf.Sign(rotation);
			}

			targetVector = new Vector3(1.0f, rotation, 1.0f);
			transform.eulerAngles += Vector3.up * rotation * maxTurnSpeed * Time.deltaTime;	
		}		
	}
}
