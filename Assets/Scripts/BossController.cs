using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
	SpriteRenderer spr;
	public GameObject bullet;
	public int health = 20;
	public float moveInterval = 0.1f;
	public float fireInterval = 0.1f;
	
	// Awake is called when the script instance is being loaded.
	protected void Awake()
	{
		spr = GetComponent<SpriteRenderer>();
	}
    // Start is called before the first frame update
    void Start()
    {
	    StartCoroutine(FlipAnim());
	    StartCoroutine(Fight());
	    StartCoroutine(Move());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
	public IEnumerator Fight()
	{
		yield return new WaitForSeconds(2f);
		while (health != 0)
		{
			yield return new WaitForSeconds(fireInterval);
			GameObject proj = GameObject.Instantiate(bullet,transform.position,Quaternion.identity);
			proj.transform.Rotate(0f,0f,Random.Range(0f,360f));
			proj.transform.parent = transform.parent;
		}
	}
	public IEnumerator Move()
	{
		while(health > 0)
		{	
			while(transform.position.y < 3f)
			{
				transform.Translate(0f,0.02f,0f);
				yield return new WaitForSeconds(moveInterval);
			}
			while(transform.position.y > -3f)
			{
				transform.Translate(0f,-0.02f,0f);
				yield return new WaitForSeconds(moveInterval);
			}
		}
	}
	
	public IEnumerator FlipAnim()
	{
		while(gameObject)
		{
			spr.flipX = !spr.flipX;
			yield return new WaitForSeconds(0.5f);
		}
	}
	public IEnumerator Damage()
	{
		health--;
		spr.color = new Color(0.2f,0f,0f);
		transform.localScale*=0.99f;
		yield return new WaitForSeconds(fireInterval);
		spr.color = Color.black;
		if(health == 0) StartCoroutine(Death());
	}
	private IEnumerator Death()
	{
		CatBattler.cat.invuln = true;
		for (int i = 0; i < 100; i++)
		{
			transform.Translate(new Vector3(Random.Range(-0.05f,0.05f),0.08f,0f));
			yield return new WaitForSeconds(0.05f);
		}
		BattleController.main.EndBattle();
		Destroy(this.gameObject);
	}
	// Sent when another object enters a trigger collider attached to this object (2D physics only).
	protected void OnTriggerEnter2D(Collider2D other)
	{
		if(other.CompareTag("PlayerProj"))
		{
			Destroy(other.gameObject);
			StartCoroutine(Damage()); 
		}
	}
}
