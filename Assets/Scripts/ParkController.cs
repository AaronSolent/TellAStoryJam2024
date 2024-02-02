using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ParkController : MonoBehaviour
{
	public int initialWait;
	public int startFight;
	public int loadScene;
	public SpriteRenderer human;
	public Sprite humanHappy;
    // Start is called before the first frame update
	void Start()
	{
		StartCoroutine(WaitForBattle());
	}

	public IEnumerator WaitForBattle()
	{
		while(DialogueController.main.currentLine != initialWait)
		{
			yield return new WaitForSeconds(0.05f);
		}
		if(loadScene == -1) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
		while(DialogueController.main.currentLine != startFight)
		{
			yield return new WaitForSeconds(0.05f);
		}
		BattleController.main.StartBattle();
		while(DialogueController.main.currentLine != startFight + 1)
		{
			yield return new WaitForSeconds(0.05f);
		}
		human.sprite = humanHappy;
		GetComponent<NPCController>().Interact();
		while(DialogueController.main.currentLine != loadScene)
		{
			yield return new WaitForSeconds(0.05f);
		}
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
