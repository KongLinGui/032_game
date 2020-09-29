 	using UnityEngine;
using System.Collections;
using InaneGames;
public class ZPlayer : MonoBehaviour {
	public bool myTurn = true;
	private ZPiece m_zPiece;
	private ZBoard m_board;
	private ZGame m_game;
	private bool m_canMove = true;
	public LayerMask mask;
	private bool m_startedGame = false;
	public AudioSource audioSource;
	private bool m_gameover=false;
	public void Awake()
	{
		m_zPiece = gameObject.GetComponent<ZPiece>();
		m_board = (ZBoard)GameObject.FindObjectOfType(typeof(ZBoard));
		m_game = (ZGame)GameObject.FindObjectOfType(typeof(ZGame));
	}
	void Start()
	{
		handleArrows();
	}
	public void OnDisable()
	{
		BaseGameManager.onGameStart -= onGameStart;
		BaseGameManager.onGameOver -= onGameOver;
	}
	public void OnEnable()
	{
		BaseGameManager.onGameStart += onGameStart;
		BaseGameManager.onGameOver += onGameOver;

	}
	public void onGameOver(bool vic)
	{
		m_gameover=true;
	}
	public void onGameStart()
	{
		m_startedGame=true;
	}
	public void setTurn(bool turn)
	{
		myTurn=turn;

		if(m_gameover==false)
		{
			if(arrows)
			{
				arrows.SetActive(turn);
			}

			if(turn)
			{
				handleArrows();
			}
		}
	}
	public GameObject[] arrowGOs;
	public void handleArrows()
	{
		int col = m_zPiece.getCol();
		int row = m_zPiece.getRow();
		for(int i=0; i<4; i++)
		{arrowGOs[i].SetActive(true);}
		if(m_board.canMoveThere(col,row,col+1,row)==false)
		{
			arrowGOs[0].SetActive(false);
		}
		if(m_board.canMoveThere(col,row,col-1,row)==false)
		{
			arrowGOs[1].SetActive(false);
		}
		if(m_board.canMoveThere(col,row,col,row+1)==false)
		{
			arrowGOs[2].SetActive(false);
		}
		if(m_board.canMoveThere(col,row,col,row-1)==false)
		{
			arrowGOs[3].SetActive(false);
		}
	}
	void Update () {
		if(myTurn && m_startedGame && m_gameover==false)
		{
			if(m_canMove)
			{
				rayCaster();
			}
			else{
				if(m_zPiece.isDone())
				{
					m_game.changeTurn();
					myTurn=false;
					m_canMove=true;
				}
			}
		}


	}
	public GameObject arrows;
	public GameObject child;
	public float lookTime = 0.5f;
	void rayCaster()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit rch;
		if(Input.GetMouseButton(0))
		{
			if(Physics.Raycast(ray,out rch,Mathf.Infinity,mask.value))
			{
				ZPart2 part = rch.collider.gameObject.GetComponent<ZPart2>();
				if(part)
				{
					if(m_board.canMoveThere(part,m_zPiece.getCol(),m_zPiece.getRow()))
					{
						if(arrows)
						{
							arrows.SetActive(false);
						}

						m_board.moveHere( m_zPiece.gameObject, part.gameObject);
						iTween.LookTo(child,part.gameObject.transform.position,lookTime);

						if(audioSource)
						{
							audioSource.Play();
						}
						m_canMove=false;
					}else{
						if(audioSource)
						{
							audioSource.PlayOneShot( Resources.Load ("OutOfHits") as AudioClip);
						}
					}
				}
			}
		}
	}
}
