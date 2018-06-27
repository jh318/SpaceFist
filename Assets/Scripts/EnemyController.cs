using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	bool juggled = false;

	Rigidbody body;

	void Awake()
	{
		if(GetComponent<Rigidbody>() != null)
		{
			body = GetComponent<Rigidbody>();
		}
	}

	// IEnumerator JuggledCoroutine()
	// {
	// 	body.
	// }
}
