using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatPlatformer : MonoBehaviour
{
	public static CatPlatformer cat;
	public bool frozen = false;
	public bool canFly = false;
	bool onGround = false;
	bool facingRight = true;
	Rigidbody2D rb2d;
	Animator anim;
	SpriteRenderer spr;
    // Start is called before the first frame update
	void Awake()
	{
		cat = this;
		rb2d = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
	    spr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
	{
		if(!frozen)
		{
		    if(Input.GetButtonDown("Jump"))
		    {
		    	if(onGround || canFly)
		    	{
			    	rb2d.velocity = new Vector2(rb2d.velocity.x, 7f);
			    	if(!onGround) anim.SetBool("Flying",true);
		    	}
		    }
			rb2d.velocity = new Vector2(Input.GetAxisRaw("Horizontal")* 7f,rb2d.velocity.y);
		}
		anim.SetBool("Moving",(rb2d.velocity.magnitude > 0.2f));
		anim.SetBool("Air",!onGround);
		if((rb2d.velocity.x > 0.1f && !facingRight)||(rb2d.velocity.x < -0.1f && facingRight))
		{
			Flip();
		}
    }
    
	public void Flip()
	{
		facingRight = !facingRight;
		spr.flipX = !spr.flipX;
	}
    
    
	// Sent each frame where another object is within a trigger collider attached to this object (2D physics only).
	protected void OnTriggerStay2D(Collider2D other)
	{
		if(other.CompareTag("Floor")) 
		{
			onGround = true;
			anim.SetBool("Flying",false);
		}
	}
	
	// Sent when another object leaves a trigger collider attached to this object (2D physics only).
	protected void OnTriggerExit2D(Collider2D other)
	{
		if(other.CompareTag("Floor")) 
		{
			onGround = false;
		}
	}
}
