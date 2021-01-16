using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomSample : MonoBehaviour {
	public Image cbody;
	public Image cface;
	public Image chair;
	public Image ckit;
	public Sprite[] body;
	public Sprite[] face;
	public Sprite[] hair;
	public Sprite[] kit;
	public Color[] background;
	private Camera cam;


	// Use this for initialization
	void Start () {
		cam = GameObject.Find("Main Camera").GetComponent<Camera>();
		RandomizeCharacter();
	}
	
	public void RandomizeCharacter(){
//		cbody.sprite = body[0];
		cbody.sprite = body[Random.Range(0,body.Length)];
		cface.sprite = face[Random.Range(0,face.Length)];
		chair.sprite = hair[Random.Range(0,hair.Length)];
		ckit.sprite = kit[Random.Range(0,kit.Length)];

		cam.backgroundColor = background[Random.Range(0,background.Length)];
	}
	public int iHair = 0;
	public int iBody = 0;
	public int iFace = 0;
	public int iKit = 0;
	public int iBackColor= 0;

	//Change Hair

	public void NextHair(){

		iHair = (++iHair) % hair.Length;
		chair.sprite = hair[iHair];

	}
	public void PreviousHair()
	{
		--iHair;
		if(iHair<0)
        {
			iHair = hair.Length - 1;
		}
		chair.sprite = hair[iHair];
	}

	//Change Body

	public void NextBody()
	{

		iBody = (++iBody) % body.Length;

		cbody.sprite = body[iBody];

	}
	public void PreviousBody()
	{
		--iBody;
		if (iBody < 0)
		{
			iBody = body.Length - 1;
		}
		cbody.sprite = body[iBody];
	}

	//Change Face
	public void NextFace()
	{

		iFace = (++iFace) % face.Length;

		cface.sprite = face[iFace];

	}
	public void PreviousFace()
	{
		--iFace;
		if (iFace < 0)
		{
			iFace = face.Length - 1;
		}
		cface.sprite = face[iFace];
	}
	//Change Kit
	public void NextKit()
	{

		iKit = (++iKit) % kit.Length;

		ckit.sprite = kit[iKit];

	}
	public void PreviousKit()
	{
		--iKit;
		if (iKit < 0)
		{
			iKit = kit.Length - 1;
		}
		ckit.sprite = kit[iKit];
	}
	//Change Background color
	public void NextBgColor()
	{

		iBackColor = (++iBackColor) % background.Length;
		cam.backgroundColor = background[iBackColor];

	}
	public void PreviousBgColor()
	{
		--iBackColor;
		if (iBackColor < 0)
		{
			iBackColor = background.Length - 1;
		}
		cam.backgroundColor = background[iBackColor];
	}

}

