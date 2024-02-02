using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HeavenCam : MonoBehaviour
{
	public GameObject floor;
	public SpriteRenderer screenFade;
	public SpriteRenderer catWinged;
	public SpriteRenderer catWingless;
    // Start is called before the first frame update
    void Start()
    {
	    StartCoroutine(WaitForFloor());
    }

	public IEnumerator WaitForFloor()
	{
		while(DialogueController.main.currentLine != 11)
		{
			yield return new WaitForSeconds(0.05f);
		}
		
		floor.SetActive(false);
		yield return new WaitForSeconds(4f);
		float x = 0f;
		while(x < 1f)
		{
			x += 0.025f;
			yield return new WaitForSeconds(0.05f);
			catWinged.color =  new Color(1f,1f,1f,x);
		}
		yield return new WaitForSeconds(2f);
		x = 0f;
		while(x < 1f)
		{
			x += 0.025f;
			yield return new WaitForSeconds(0.05f);
			screenFade.color =  new Color(1f,1f,1f,x);
		}
		SceneManager.LoadScene(2);
		
	}
}
