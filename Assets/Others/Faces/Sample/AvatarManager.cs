using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AvatarManager : MonoBehaviour {
	public Image cbody;
	public Image cface;
	public Image chair;
	public Image ckit;
	public Sprite[] body;
	public Sprite[] face;
	public Sprite[] hair;
	public Sprite[] kit;
	public Color[] background;

	public int iHair = 0;
	public int iBody = 0;
	public int iFace = 0;
	public int iKit = 0;
	public int iBgColor = 0;
	public Avatar avatar =new Avatar();
	public GameObject panel;
	// Use this for initialization
	void Start () {

		//Wait For Avatar Data from server


	}
	
	public void RandomizeCharacter(){
		iBody = Random.Range(0, body.Length);
		iBgColor = Random.Range(0, background.Length);
		iFace = Random.Range(0, face.Length);
		iHair = Random.Range(0, hair.Length);
		iKit = Random.Range(0, kit.Length);
		UpdateAvatar();
	}
	
    public void SetAvatar()
    {
		avatar.bgColor = iBgColor.ToString();
		avatar.face = iFace.ToString();
		avatar.hair = iHair.ToString();
		avatar.body = iBody.ToString();
		avatar.kit = iKit.ToString();
	}

	public void SetAvatar(Avatar avatar)
    {
		this.avatar = avatar;
		GetAvatar();
    }
    public void UpdateAvatar()
    {
		chair.sprite = hair[iHair];
		cbody.sprite = body[iBody];
		cface.sprite = face[iFace];
		ckit.sprite = kit[iKit];
		panel.GetComponent<RawImage>().color = background[iBgColor];
	}

	//Updates the parameters of the avatar then shows the changes
    public void GetAvatar()
	{
		iBgColor =int.Parse(avatar.bgColor);
		iFace =int.Parse(avatar.face);
		iHair =int.Parse(avatar.hair);
		iBody =int.Parse(avatar.body);
		iKit = int.Parse(avatar.kit);

		UpdateAvatar();
	}



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

		iBgColor = (++iBgColor) % background.Length;
		panel.GetComponent<RawImage>().color = background[iBgColor];

	}
	public void PreviousBgColor()
	{
		--iBgColor;
		if (iBgColor < 0)
		{
			iBgColor = background.Length - 1;
		}
		panel.GetComponent<RawImage>().color = background[iBgColor];
	}



}

