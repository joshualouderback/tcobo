using UnityEngine;
using System.Collections;

public class PortraitRotation : MonoBehaviour 
{
	SpriteRenderer spriteRenderer;

	void Start()
	{
		spriteRenderer = this.GetComponent<SpriteRenderer>();
	}

	void Update()
	{

	}

	/*
	public Transform Left;
	public Transform Right;
	public Transform Center;

	[Space]

	public float RotationTime = 1.0f;
	public float DelayTime = 0.25f;

	SpriteRenderer spriteRenderer;
	ActionSequence seq;

	void Start()
	{
		spriteRenderer = this.GetComponent<SpriteRenderer>();
	}



	// Simple settors for actions
	void SetPosition(Vector3 value) { this.transform.position = value; }
	void SetColor(Color color){ spriteRenderer.color = color; }
	void SetToLeft() { this.transform.position = Left; }
	void SetToRight() { this.transform.position = Right; }

	//----- Left Rotations -----//

	void RotateLeftToCenter()
	{
		ActionGroup grp = new ActionGroup(seq);
		new ActionPropertyVec3(grp, this.transform.position, Center, RotationTime, EasesAndCurves.Eases.Linear, SetPosition);
		new ActionPropertyColor(grp, spriteRenderer.color, new Color(1, 1, 1, 1.0f), RotationTime, EasesAndCurves.Eases.Linear, SetColor);
		new ActionDelay(seq, DelayTime);
		// Check if we are still held, if we are restart
	}

	void RotateLeftFromCenter()
	{
		ActionGroup grp = new ActionGroup(seq);
		new ActionPropertyVec3(grp, this.transform.position, Left, 1, EasesAndCurves.Eases.Linear, SetPosition);
		new ActionPropertyColor(grp, spriteRenderer.color, new Color(1, 1, 1, 0.5f), RotationTime, EasesAndCurves.Eases.Linear, SetColor);
		new ActionDelay(seq, DelayTime);
	}

	void RotateLeftToRight()
	{
		new ActionPropertyColor(seq, spriteRenderer.color, new Color(1, 1, 1, 0.0f), RotationTime * 0.5f, EasesAndCurves.Eases.Linear, SetColor);
		new ActionCall(seq, SetToRight);
		new ActionPropertyColor(seq, spriteRenderer.color, new Color(1, 1, 1, 0.5f), RotationTime * 0.5f, EasesAndCurves.Eases.Linear, SetColor);
		new ActionDelay(seq, DelayTime);
	}
		
	//----- Right Rotations -----//

	void RotateRight()
	{

	}
	*/
}
