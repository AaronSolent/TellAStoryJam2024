using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueController : MonoBehaviour
{
	public static DialogueController main;
	public bool inProgress = false;
	public bool skipFlag = false;
	public int currentLine = 0;
	public string[] activeDialogue;
	public GameObject dialogueBase;
	public TextMeshProUGUI dialogueText;
	
	// Awake is called when the script instance is being loaded.
	protected void Awake()
	{
		main = this;
		dialogueBase.SetActive(false);
	}
	
	// Update is called every frame, if the MonoBehaviour is enabled.
	protected void Update()
	{
		if(inProgress && Input.GetButtonDown("Submit"))
		{
			skipFlag = true;
		}
	}
	
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
	public void StartDialogue(string[] input)
	{
		activeDialogue = input;
		StartCoroutine(ShowDialogue());
	}
    
	public IEnumerator ShowDialogue()
	{
		CatPlatformer.cat.frozen = true;
		inProgress = true;
		currentLine = 0;
		dialogueBase.SetActive(true);
		foreach(string line in activeDialogue)
		{
			dialogueText.text = "";
			bool skip = false;
			foreach (char letter in line.ToCharArray())
			{
				if(letter == '<') skip = true;
				if(letter == '>') skip = false;
				
				dialogueText.text += letter;
				if(!skip) yield return new WaitForSeconds(0.05f);
				if(skipFlag) skip = true;
			}
			skipFlag = false;
			dialogueText.text = line;
			float timeWaited = 0f;
			while(timeWaited < (1.5f + line.Length*0.05f) && !skipFlag)
			{
				yield return new WaitForSeconds(0.05f);
				timeWaited += 0.05f;
			}
			currentLine++;
			skipFlag = false;
		}
		dialogueBase.SetActive(false);
		inProgress = false;
		CatPlatformer.cat.frozen = false;
	}
}
