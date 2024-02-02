using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatBattler : MonoBehaviour
{
	public static CatBattler cat;
	public GameObject bullet;
	float xInp;
	float yInp;
	public Transform healthbar;
	Rigidbody2D rb2d;
	SpriteRenderer spr;
	bool cooldown = false;
	public bool invuln = false;
	public float speed = 1f;
	public int health = 20;
	int maxHealth;

	// Awake is called when the script instance is being loaded.
	protected void Awake()
	{
		rb2d = GetComponent<Rigidbody2D>();
		spr = GetComponent<SpriteRenderer>();
		maxHealth = health;
		cat = this;
	}
    // Update is called once per frame
    void Update()
    {
	    xInp = Input.GetAxisRaw("Horizontal");
	    yInp = Input.GetAxisRaw("Vertical");
	    if(Input.GetButton("Jump") && !cooldown)
	    {
	    	FireBullet();
	    	StartCoroutine(Cooldown());
	    }
    }
    
	void FireBullet()
	{
		GameObject proj = GameObject.Instantiate(bullet,transform.position,Quaternion.identity);
		proj.transform.parent = transform.parent;
	}
    
	// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
	protected void FixedUpdate()
	{
		rb2d.velocity = new Vector2(xInp*speed,yInp*speed);
	}
	public IEnumerator Cooldown()
	{
		cooldown = true;
		yield return new WaitForSeconds(0.2f);
		cooldown = false;
	}
	
	public IEnumerator Damage()
	{
		health--;
		spr.color = Color.red;
		healthbar.localScale = new Vector3((float)health/(float)maxHealth,1f,1f);
		if(health <=0) BattleController.main.LoseBattle();
		yield return new WaitForSeconds(0.1f);
		spr.color = Color.white;
	}
	
	// Sent when another object enters a trigger collider attached to this object (2D physics only).
	protected void OnTriggerEnter2D(Collider2D other)
	{
		if(other.CompareTag("EnemyProj"))
		{
			Destroy(other.gameObject);
			if(!invuln)StartCoroutine(Damage());
		}
	}
}
