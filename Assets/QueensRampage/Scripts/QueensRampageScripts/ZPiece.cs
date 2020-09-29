using UnityEngine;
using System.Collections;
using InaneGames;

public class ZPiece : MonoBehaviour {
	public int m_row=0;
	public int m_col=0;
	private ZBoard m_board;
	private Damagable m_damagable;
	private ZGame m_game;
	private Animator m_animator;
	private bool m_done;
	void Awake()
	{
		m_animator = gameObject.GetComponentInChildren<Animator>();
		m_board = (ZBoard)GameObject.FindObjectOfType(typeof(ZBoard));
		m_damagable = gameObject.GetComponent<Damagable>();
		if(m_board)
		{
			m_board.findClosetSpot(transform.position,ref m_col,ref m_row);
		}
	}
	public void DoneTeleporting()
	{
		if(m_board)
		{

			m_board.findClosetSpot(transform.position,ref m_col,ref m_row);
			m_board.teleportToCell(gameObject.layer,m_col,m_row);
		}
	}
	public void killSelf()
	{
		if(m_animator)
		{
			m_animator.SetBool("Dead",true);
		}
		if(m_damagable)
		{
			m_damagable.killSelf();
		
		}	
	}
	public void setDone(bool done)
	{
		m_done=done;
	}
	public void setPos(int c, int r)
	{
		m_row = r;
		m_col = c;
	}
	public int getRow()
	{
		return m_row;
	}
	public int getCol()
	{
		return m_col;
	}
	public bool isDone()
	{
		return m_done;
	}

	/*
	public bool checkSpots(int c1, int r1,
	                       int c2, int r2)
	{
		string nom1 = "ConnectorC" + c1 + "R" + r1 +"ToC" +  c2 +"R"+r2;
		string nom2 = "ConnectorC" + c2 + "R" + r2 +"ToC" +  c1 +"R"+r1;

		GameObject g0 = GameObject.Find (nom1);
		GameObject g1 = GameObject.Find (nom2);

		return g0 || g1;
	}

	public bool canMoveThere(ZPart2 part)
	{
		bool move = false;
		int newRow = part.row;
		int newCol = part.cell;

		int rowDif = Mathf.Abs(newRow - m_row);
		int colDif = Mathf.Abs(newCol - m_col);

		if(newRow!=m_row && rowDif==1 && colDif==0)
		{
			if(checkSpots(m_col,m_row,m_col,newRow))
			{
				m_row = newRow;
				move=true;
			}
		}
		else if(newCol!=m_col && colDif==1 && rowDif==0)
		{
			if(checkSpots(m_col,m_row,newCol,m_row))
			{
				m_col = newCol;
				move=true;
			}

		}
		return move;
	}*/


}
