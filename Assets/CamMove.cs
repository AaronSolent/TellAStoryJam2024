using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMove : MonoBehaviour
{
	public float minX;
	public float minY;
	public float maxX;
	public float maxY;
    // Update is called once per frame
    void Update()
	{
		Vector3 target = CatPlatformer.cat.transform.position;
		transform.position = new Vector3(Mathf.Clamp(target.x+1f,minX,maxX),Mathf.Clamp(target.y+1.5f,minY,maxY),-10f);
    }
}
