using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightController : MonoBehaviour 
{
	public GameObject hitbox;

	bool cancelWindow;
	Technique currentTechnique;

	Animator animator;

	void Awake()
	{
		animator = GetComponent<Animator>();
		hitbox.SetActive(false);
		cancelWindow = false;
	}

	void Update()
	{
		if(cancelWindow)
		{
			FP_Cancel();
		}
	}

	void FP_HitboxActive()
	{
		hitbox.SetActive(true);
	}

	void FP_HitboxDeactivate()
	{
		hitbox.SetActive(false);
	}

	void FP_CancelWindowActive()
	{
		cancelWindow = true;
	}

	void FP_CancelWindowDeactivate()
	{
		cancelWindow = false;
	}

	void FP_Cancel()
	{
		if(Input.GetButtonDown("Fire1") && animator.GetBool("IsAttacking"))
			animator.SetTrigger("Cancel");
	}

	void FP_AttackName(Technique tech)
	{
		currentTechnique = tech;
	}

	void OnTriggerEnter(Collider c)
	{
		if(c.gameObject.GetComponent<DamageController>() && c.gameObject != this.gameObject)
		{
			Debug.Log("Hitbox got something");

			c.gameObject.GetComponent<DamageController>().health -= currentTechnique.damage;
			SpecialPropertiesCheck(currentTechnique, c.gameObject);

			Debug.Log(c.gameObject.GetComponent<DamageController>().health);
			
		}
	}

	void SpecialPropertiesCheck(Technique tech, GameObject target)
	{
		if(target.GetComponent<Rigidbody>())
		{
			if (tech.launch)
			{
				target.GetComponent<Rigidbody>().velocity = Vector3.zero;
				target.GetComponent<Rigidbody>().AddForce(transform.forward * tech.force, ForceMode.Impulse);
			}
			else if(tech.juggle)
			{
				target.GetComponent<Rigidbody>().velocity = Vector3.zero;
				target.GetComponent<Rigidbody>().AddForce(Vector3.up * tech.force, ForceMode.Impulse);
			}
			else if(tech.dizzy)
			{
				//TODO: Implement dizzy
			}
		}
	
	}

}
