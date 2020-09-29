using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ZBoard : MonoBehaviour {
	public int nomCells = 6;
	public int nomRows = 6;
	public float cellOffset = 10;
	private Dictionary<string,GameObject> m_dictionary = new Dictionary<string,GameObject>();
	public float moveTime = .5f;
	private List<ZPiece> m_pieces = new List<ZPiece>();

	public void Awake()
	{

		ZPart2[] parts = (ZPart2[])gameObject.GetComponentsInChildren<ZPart2>();
		for(int i=0; i<parts.Length; i++)
		{
			m_dictionary.Add(parts[i].gameObject.name,parts[i].gameObject);
		}
	}
	public void generateBoard ()
	{
		for(int i=0;i<transform.childCount; i++)
		{
			GameObject go = transform.GetChild(i).gameObject;
			if(go!=gameObject)
			{
				DestroyImmediate(go);
			}
		}

		generateIslands();
		generateVerticalConnectors();
		generateHorizontalConnectors();
	}
	public void clear()
	{
		m_pieces.Clear();

	}
	public void findClosetSpot(Vector3 pos,
	                           ref int col,ref int row)
	{
		float d0 = Mathf.Infinity;
		ZPart2 part = null;
		ZPart2[] parts = (ZPart2[])GameObject.FindObjectsOfType(typeof(ZPart2));
		for(int i=0; i<parts.Length; i++)
		{
			float d1 = (parts[i].transform.position - pos).magnitude;

			if(d1<d0)
			{
				part = parts[i];
				d0 = d1;
			}
		}


		if(part)
		{
			col = part.col;
			row = part.row;
		}
	}

	public bool checkSpots(int c1, int r1,
	                       int c2, int r2)
	{
		string nom1 = "ConnectorC" + c1 + "R" + r1 +"ToC" +  c2 +"R"+r2;
		string nom2 = "ConnectorC" + c2 + "R" + r2 +"ToC" +  c1 +"R"+r1;

		GameObject g0 = GameObject.Find (nom1);
		GameObject g1 = GameObject.Find (nom2);
		
		return g0 || g1;
	}
	public GameObject getGameObject(int playerCol,int playerRow)
	{
		GameObject part = null;
		string key = "C" + playerCol+ "R"+playerRow;
		if(m_dictionary.ContainsKey(key)){
			part = m_dictionary[key];
		}
		return part;
	}
	public void teleportToCell(int layer,int col, int row)
	{
		GameObject go =getGameObject(col,row);
		ZPart2 part = go.GetComponent<ZPart2>();
		if(part)
		{
			ZPiece[] pieces = (ZPiece[])GameObject.FindObjectsOfType(typeof(ZPiece));
			for(int i=0; i<pieces.Length; i++)
			{
				if(pieces[i].getCol()==col && 
				   pieces[i].getRow()==row && 
				   pieces[i].gameObject.layer!=layer)
				{
					pieces[i].killSelf();
				}
			}
		}
	}

	public void killCell(GameObject go)
	{

		for(int i=0; i<m_pieces.Count; i++)
		{
			if(m_pieces[i] && m_pieces[i].gameObject.layer!=go.layer)
			{
				m_pieces[i].killSelf();
			}
		}
		if(go)
		{
			go.SendMessage("setDone",true,SendMessageOptions.DontRequireReceiver);
		}
	}
	public iTween.EaseType easeType;
	public void moveHere(GameObject go, GameObject targetPos)
	{
		ZPiece piece = go.GetComponent<ZPiece>();
		ZPart2 part = targetPos.GetComponent<ZPart2>();
		if(part.killZone)
		{
			ZPiece[] pieces = (ZPiece[])GameObject.FindObjectsOfType(typeof(ZPiece));
			for(int i=0; i<pieces.Length; i++)
			{
				if(pieces[i].getCol()==part.col && pieces[i].getRow()==part.row)
				{
					m_pieces.Add(pieces[i]);
				}
			}
		}


		if(go && targetPos)
		{
			go.SendMessage("setDone",false,SendMessageOptions.DontRequireReceiver);
			iTween.MoveTo(go,iTween.Hash("position",targetPos.transform.position,
			                             "time",moveTime,
			                             "oncomplete","killCell",
			                             "oncompleteparams",go,
			                             "easeType",easeType,
			                             "oncompletetarget",gameObject));
			piece.setPos( part.col,part.row);
		}
	}
	public bool canMoveThere(int newCol,
	                         int newRow,
	                         int rCol,
	                         int rRow)
	{
		bool move = false;
		int row = rRow;
		int col = rCol;
		int rowDif = Mathf.Abs(newRow - row);
		int colDif = Mathf.Abs(newCol - col);

		

		if(newRow!=row && rowDif==1 && colDif==0)
		{
			if(checkSpots(col,row,col,newRow))
			{
				move=true;
			}
		}
		else if(newCol!=col && colDif==1 && rowDif==0)
		{
			if(checkSpots(col,row,newCol,row))
			{
				move=true;
			}


		}

		return move;
	}

	public bool canMoveThere(ZPart2 part,
	                          int rCol,
	                          int rRow)
	{

		return canMoveThere(part.col,part.row,rCol,rRow);
	}


	void generateHorizontalConnectors()
	{
		Vector3 pos = Vector3.zero;
		pos.x = cellOffset;
		pos.y-=0.2f;
		for(int i=0; i<nomRows; i++)
		{
			for(int j=0; j<nomCells-1; j++)
			{
				GameObject obj = Resources.Load ("Bridge2") as GameObject;
				GameObject go = (GameObject)Instantiate(Resources.Load ("Bridge2") as GameObject,
				                                        pos,
				                                        obj.transform.rotation);
				Bridge b = go.AddComponent<Bridge>();
				if(b)
				{
					b.go1 = GameObject.Find("C"+j+"R"+i);
					b.go2 = GameObject.Find("C"+(j+1)+"R"+i);
				}
				go.name = "ConnectorC" + j + "R" + i +"ToC" +  (j+1) +"R"+(i);
				go.transform.parent =  transform;
				pos.x+=cellOffset;
			}
			pos.z+=cellOffset;
			pos.x=cellOffset;
		}
	}
	void generateVerticalConnectors()
	{
		Vector3 pos = Vector3.zero;
		pos.z = cellOffset;
		pos.y-=0.2f;
		for(int i=0; i<nomRows-1; i++)
		{
			for(int j=0; j<nomCells; j++)
			{
				GameObject go = (GameObject)Instantiate(Resources.Load ("Bridge") as GameObject,
				                                        pos,
				                                        Quaternion.identity);
				go.name = "ConnectorC" + j + "R" + i +"ToC" +  j +"R"+(i+1);
				Bridge b = go.AddComponent<Bridge>();
				if(b)
				{
					b.go1 = GameObject.Find("C"+j+"R"+i);
					b.go2 = GameObject.Find("C"+j+"R"+(i+1));
				}
				go.transform.parent =  transform;
				pos.x+=cellOffset;
			}
			pos.z+=cellOffset;
			pos.x=0;
		}
	}
	void generateIslands()
	{
		Vector3 pos = Vector3.zero;

		for(int i=0; i<nomRows; i++)
		{
			for(int j=0; j<nomCells; j++)
			{
				GameObject go = (GameObject)Instantiate(Resources.Load ("Island") as GameObject,
				                                        pos,
				                                        Quaternion.identity);
				go.name = "C" + j + "R"+i;
				ZPart2 part = go.AddComponent<ZPart2>();
				if(part)
				{
					part.init(j,i);
				}
				go.transform.parent =  transform;
				pos.x+=cellOffset;
			}
			pos.z+=cellOffset;
			pos.x=0;
		}
	}
}
