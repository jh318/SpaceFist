using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightController : MonoBehaviour 
{
	bool cancelWindow;
	GameObject activeHitbox;
	List<GameObject> hitboxes = new List<GameObject>();
	Technique currentTechnique;
	Animator animator;

	void Awake()
	{
		animator = GetComponent<Animator>();
		cancelWindow = false;
		GetHitboxes();

		
	}

	void Update()
	{
		if(cancelWindow)
		{
			FP_Cancel();
		}
	}

	void FP_HitboxActive(string hitbox)
	{
		if(hitboxes != null)
		{
			foreach(GameObject box in hitboxes)
			{
				if(box.name == hitbox)
				{
						box.SetActive(true);
						activeHitbox = box;
						break;
				}
			}
		}	
	}

	void FP_HitboxDeactivate()
	{
		if(activeHitbox != null)
		{
			activeHitbox.SetActive(false);
			activeHitbox = null;
		}
		
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

	void GetHitboxes()
	{
		Component[] hitboxComponents = GetComponentsInChildren(typeof(Hitbox), true);
		
		if(hitboxComponents != null)
		{
			foreach(Hitbox box in hitboxComponents)
			{
				hitboxes.Add(box.gameObject);
			}
		}
		
	}
}
