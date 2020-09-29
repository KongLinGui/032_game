using UnityEngine;
using System.Collections;

using System.Collections.Generic;
public class Bridge : MonoBehaviour {
	public GameObject go1;
	public GameObject go2;
	public GameObject getClosestParts(ZPart2[] parts)
	{
		float d1 = Mathf.Infinity;
		Debug.Log ("getClosestParts:" + parts.Length) ;

		GameObject part=null;
		for(int i=0; i<parts.Length; i++)
		{
			float d0 = (parts[i].transform.position - transform.position).magnitude;
			if(d0<d1)
			{
				d1 = d0;
				part = parts[i].gameObject;
			}
		}
		return part;
	}
	void OnDrawGizmosSelected() {
		Gizmos.color = Color.cyan;
		if(go1 && go2)
			Gizmos.DrawLine( go1.transform.position,go2.transform.position);
	}

	public void getClosestParts()
	{
		ZPart2[] parts = (ZPart2[])GameObject.FindObjectsOfType(typeof(ZPart2));
		go1 = getClosestParts(parts);
		int n =0;
		ZPart2[]  parts2 = new ZPart2[parts.Length-1];
		for(int i=0; i<parts.Length; i++)
		{
			if(parts[i].gameObject!=go1)
			{
				parts2[n] = parts[i];
				n++;
			}
		}
		go2 = getClosestParts(parts2);

	}

	public void updateName()
	{
		getClosestParts();
		gameObject.name = "Connector"+go1.name + "To" + go2.name;
	}

}
