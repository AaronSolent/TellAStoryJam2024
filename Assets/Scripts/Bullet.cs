using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	public float speed =1f;
	// Update is called once per frame
	// Awake is called when the script instance is being loaded.
	protected void Awake()
	{
		StartCoroutine(TimeOut());
	}
	void FixedUpdate()
    {
	    transform.Translate(transform.right*(0.1f*speed),Space.Self);
    }
    
	IEnumerator TimeOut()
	{
		yield return new WaitForSeconds(5f);
		Destroy(this.gameObject);
	}
}
