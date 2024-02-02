using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleController : MonoBehaviour
{
	public static BattleController main;
	public GameObject mainStage;
	public GameObject gameOver;
	public bool battleStarted = false;
    // Start is called before the first frame update
	void Awake()
    {
	    main = this;
    }

	public void StartBattle()
	{
		CatPlatformer.cat.frozen = true;
		battleStarted = true;
		mainStage.SetActive(true);
	}		
	public void EndBattle()
	{	
		mainStage.SetActive(false);
		battleStarted = false;
		CatPlatformer.cat.frozen = false;
		DialogueController.main.currentLine++;
	}
	
	public void LoseBattle()
	{
		mainStage.SetActive(false);
		gameOver.SetActive(true);
	}
	
	public void TryAgain()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
