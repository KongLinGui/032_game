using UnityEngine;
using System.Collections;

public class TurnerAI : ZombieAI {
	public float rotate = 90f;

	private Quaternion m_initalRot;
	public override void Start ()
	{
		m_initalRot = transform.rotation;
		base.Start ();
	}
	public void endRot()
	{
		m_initalRot *= Quaternion.AngleAxis(180,Vector3.up);
		transform.rotation = m_initalRot;
	}
	public override void done(bool requireMove)
	{
		if(requireMove==false)
		{
			iTween.RotateBy(gameObject,iTween.Hash("amount",new Vector3(0,.5f,0),
		                             "time",.5f,
		                             "oncomplete","endRot",
	                            	 "oncompletetarget",gameObject));
		}
	}
}
