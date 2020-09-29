using UnityEngine;
using System.Collections;
using InaneGames;
public class ZombieAI : BaseAI {


	
	public override bool think()
	{
		bool rc=false;
		int row = m_piece.getRow();
		int col = m_piece.getCol();
		int playerRow = m_playerPiece.getRow();
		int playerCol = m_playerPiece.getCol();

		Vector3 vec = transform.rotation * new Vector3(0,0,-1);
		vec.Normalize();


		if(Mathf.Abs(vec.z)>0.1f)
		{
			if(col==playerCol)
			{
				float val = playerRow - (row - vec.z);
				Debug.Log ("ValZ" + val);
				if(playerRow==row-vec.z)
				{
					GameObject go = m_board.getGameObject(playerCol,playerRow);
					if(go)
					{
						m_board.moveHere(gameObject,go);
						rc=true;
					}
				}
			}
		}
		else if(Mathf.Abs(vec.x)>0.1f)
		{
			if(playerRow==row)
			{
				float val = playerCol - (col - vec.x);
				Debug.Log ("ValX" + val);
				if(playerCol==col-vec.x)
				{
					GameObject go = m_board.getGameObject(playerCol,playerRow);
					if(go)
					{
						m_board.moveHere(gameObject,go);
						rc=true;
					}
				}
			}
		}
		if(rc==false)
		{
			m_piece.setDone(true);
		}

		return rc;
	}
}
