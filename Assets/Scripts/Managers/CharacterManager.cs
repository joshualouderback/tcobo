using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class CharacterPortrait 
{
	public CharacterPortrait(Sprite image)
	{
		portrait = image;
		taken = false;
	}

	public Sprite portrait;
	[HideInInspector]
	public bool taken;
}

public class SelectPlayerEvent : CustomEvent
{
	public SelectPlayerEvent(int selection)
	{
		index = selection;
	}

	public int index;
}

public class CharacterManager : MonoBehaviour {

	private List<CharacterPortrait> AvailableCharacters_ = new List<CharacterPortrait>();

	public int NumberOfCharacters 
	{
		get { return AvailableCharacters_.Count; }
	}

	public int FindNextAvailableOnLeft(int index, SpriteRenderer spriteRenderer)
	{
		do
		{
			if(--index < 0)
				index = AvailableCharacters_.Count - 1;
		}
		while(AvailableCharacters_[index].taken);
		spriteRenderer.sprite = AvailableCharacters_[index].portrait;
		return index;
	}

	public int FindNextAvailableOnRight(int index, SpriteRenderer spriteRenderer)
	{
		while(AvailableCharacters_[index = ++index % AvailableCharacters_.Count].taken);
		spriteRenderer.sprite = AvailableCharacters_[index].portrait;
		return index;
	}

	public bool TakeSelection(int index)
	{
		// If already taken, deny
		if(AvailableCharacters_[index].taken)
			return false;
		else // Otherwise accept
		{
			AvailableCharacters_[index].taken = true;
			return true;
		}
	}

	public void UntakeSelection(int index)
	{
		AvailableCharacters_[index].taken = false;
	}

		
	// Use this for initialization
	void OnEnable () 
	{
		// Load all of the sprites within the resource folder
		Sprite[] portraits =  Resources.LoadAll<Sprite>("CharacterPortraits");	
		foreach(Sprite portrait in portraits)
		{
			AvailableCharacters_.Add(new CharacterPortrait(portrait));
		}
	}
	
}
