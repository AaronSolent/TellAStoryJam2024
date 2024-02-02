using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
	public bool inRange = false;
	public bool activated = false;
	public bool playOnStart = false;
	public GameObject alert;
	public string[] lines;
    // Start is called before the first frame update
    void Start()
    {
	    if(alert)alert.SetActive(false);
	    if(playOnStart) Interact(); 
    }

    // Update is called once per frame
    void Update()
    {
	    if(inRange && Input.GetButtonDown("Submit") && !DialogueController.main.inProgress && !activated)
	    {
	    	Interact();
	    }
    }
    
	public void Interact()
	{	
		DialogueController.main.StartDialogue(lines);
		activated = true;
	}
	
	// Sent when another object enters a trigger collider attached to this object (2D physics only).
	protected void OnTriggerEnter2D(Collider2D other)
	{
		if(other.CompareTag("Player"))
		{
			inRange=true;
			if(alert)alert.SetActive(true);
		}
	}
	
	// Sent when another object leaves a trigger collider attached to this object (2D physics only).
	protected void OnTriggerExit2D(Collider2D other)
	{	
		if(other.CompareTag("Player"))
		{
			inRange=false;
			if(alert)alert.SetActive(false);
		}
	}
	
}
