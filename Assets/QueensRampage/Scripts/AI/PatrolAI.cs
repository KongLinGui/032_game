using UnityEngine;
using System.Collections;

public class PatrolAI : BaseAI {
	private int m_dir = 1;
	public override void Start()
	{
		base.Start();
		int row = m_piece.getRow();
		int col = m_piece.getCol();
		Vector3 vec = transform.forward;
		vec.Normalize();

		if(Mathf.Abs(vec.z)>0.1f)
		{
			if(vec.z>0.1f)
			{
				m_dir=1;	
			}
			if(vec.z<0.1f)
			{
				m_dir=-1;
			}
		}
		if(Mathf.Abs(vec.x)>0.1f)
		{
			if(vec.x>0.1f)
			{
				m_dir=1;	
			}
			if(vec.x<0.1f)
			{
				m_dir=-1;
			}
		}


		if(Mathf.Abs(vec.z)>0.1f)
		{
			GameObject go = m_board.getGameObject(col,row+m_dir);
			if(go==null)
			{
				m_dir*=-1;
			}
		}
		if(Mathf.Abs(vec.x)>0.1f)
		{
			GameObject go = m_board.getGameObject(col+m_dir,row);
			if(go==null)
			{
				m_dir*=-1;
			}
		}


	}
	public GameObject getGameObject(int colOffset,int rowOffset,int col, int row)
	{
		GameObject go = null;
		int curRow = m_piece.getRow()+rowOffset;
		int curCol = m_piece.getCol() + colOffset;
		if(m_board.checkSpots(curCol,curRow,col,row))
		{
			 go = m_board.getGameObject(col,row);

		}
		return go;
	}

	public float rotateX = 0.5f;
	public override bool think()
	{
		bool rc=false;
		int row = m_piece.getRow();
		int col = m_piece.getCol();

		Vector3 vec = transform.rotation * new Vector3(0,0,-1);
		vec.Normalize();

		if(Mathf.Abs(vec.z)>0.1f)
		{

				GameObject go = getGameObject(0,0,col,row+m_dir);
				GameObject go2 =getGameObject(0,m_dir,col,row+(m_dir*2));
				if(go)
				{
					m_board.moveHere(gameObject,go);
					rc=true;
				}
				if(go2==null)
				{
				rotateObject();

				}

			rc=true;
		}
		
		if(Mathf.Abs(vec.x)>0.1f)
		{
			GameObject go = getGameObject(0,0,col+m_dir,row);
			GameObject go2 = getGameObject(m_dir,0,col+(m_dir*2),row);

			if(go)
			{

				m_board.moveHere(gameObject,go);
				rc=true;
			}
			if(go2==null)
			{
				rotateObject();
			}
			rc=true;
		}
		
		return rc;
	}

	public virtual void rotateObject()
	{
		iTween.RotateBy(child.gameObject,new Vector3(0,rotateX,0),1f);
		m_dir*=-1;
	}
}
