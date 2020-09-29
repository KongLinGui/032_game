using UnityEngine;
using System.Collections;
using InaneGames;
public class BaseAI : MonoBehaviour {
	protected ZBoard m_board;
	protected ZPiece m_playerPiece;
	protected ZPiece m_piece;
	protected bool m_myTurn = false;
	protected Damagable m_damagable;
	protected bool m_done=false;
	public Transform child;

	void Awake()
	{

		m_board = (ZBoard)GameObject.FindObjectOfType(typeof(ZBoard));
		m_piece = gameObject.GetComponent<ZPiece>();
		m_damagable = gameObject.GetComponent<Damagable>();
		GameObject playerGO = GameObject.FindWithTag("Player");
		if(playerGO){
			m_playerPiece = playerGO.GetComponent<ZPiece>();
		}
	}
	public virtual void Start()
	{

	}
	public bool isDone()
	{
		return m_piece.isDone();
	}

	public bool execute()
	{
		bool requireMove = false;
		m_myTurn = true;
		if(m_playerPiece && m_myTurn && m_damagable.isAlive())
		{
			requireMove = think();
			done(requireMove);
			m_myTurn=false;
		}
		return requireMove;
	}
	public virtual void done(bool requireMove)
	{
	}
	public virtual bool think()
	{
		return false;
	}
	public bool isAlive()
	{
		return m_damagable.isAlive();
	}
}
