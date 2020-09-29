using UnityEngine;
using System.Collections;

public class PatrollerAI : PatrolAI {
	public float rotate = 90f;

	public float rotateVal = 0.25f;
	private Quaternion m_initalRot;
	public override void Start ()
	{
		m_initalRot = transform.rotation;
		base.Start ();
	}
	public void endRot()
	{
		m_initalRot *= Quaternion.AngleAxis(rotate,Vector3.up);
		transform.rotation = m_initalRot;
	}
	public override void done(bool requireMove)
	{
		endRot();
	}
	public override void rotateObject()
	{
	}
}
