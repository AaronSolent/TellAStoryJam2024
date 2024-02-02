using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class FinaleScene : MonoBehaviour
{
	public int startFight;
	public int endGame;
	public TextMeshPro life;
	public CanvasGroup winUI;
	public SpriteRenderer human;
	public Sprite humanHappy;
	public SpriteRenderer catDead;
	public SpriteRenderer catAlive;
	void Start()
	{
		StartCoroutine(WaitForBattle());
	}

	public IEnumerator WaitForBattle()
	{
		while(DialogueController.main.currentLine != startFight)
		{
			yield return new WaitForSeconds(0.05f);
		}
		BattleController.main.StartBattle();
		while(DialogueController.main.currentLine != startFight + 1)
		{
			yield return new WaitForSeconds(0.05f);
		}
		GetComponent<NPCController>().Interact();
		yield return new WaitForSeconds(0.5f);
		life.gameObject.SetActive(true);
		life.text = "9";
		life.color = Color.green;
		yield return new WaitForSeconds(1f);
		life.color = Color.red;
		life.text = "8";
		yield return new WaitForSeconds(0.3f);
		life.color = Color.green;
		float x = 0f;
		catDead.flipX = false;
		while(x < 1f)
		{
			x += 0.01f;
			catAlive.color = new Color(1f,1f,1f,x);
			catDead.color = new Color(1f,1f,1f,1f-x);
			yield return new WaitForSeconds(0.02f);
		}
		life.gameObject.SetActive(false);
		while(DialogueController.main.currentLine != 3)
		{
			yield return new WaitForSeconds(0.05f);
		}
		human.sprite = humanHappy;
		while(DialogueController.main.currentLine != endGame)
		{
			yield return new WaitForSeconds(0.05f);
		}
		x= 0f;
		winUI.interactable = true;
		winUI.blocksRaycasts = true;
		while(x < 1f)
		{
			x += 0.01f;
			winUI.alpha = x;
			yield return new WaitForSeconds(0.02f);
		}
	}
	
	public void ExitGame()
	{
		Application.Quit();
		SceneManager.LoadScene(0);
	}
	
    // Update is called once per frame
    void Update()
    {
        
    }
}
