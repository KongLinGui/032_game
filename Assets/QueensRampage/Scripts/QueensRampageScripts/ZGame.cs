using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using InaneGames;
public class ZGame : BaseGameScript {
	public ZAIPlayer aiPlayer;
	public ZPlayer player;
	private ZBoard m_board;
	public int turn = 0;
	public AudioSource audioSource;
	public AudioClip[] knockDownClips;
	public override void Awake()
	{
		base.Awake();
		m_board = (ZBoard)GameObject.FindObjectOfType(typeof(ZBoard));
		GameObject go = GameObject.Find("TurnsText");
		if(go)
		{
			turnText = go.GetComponent<Text>();
		}
		 go = GameObject.Find ("Player");
		if(go)
		{
			player = go.GetComponent<ZPlayer>();
		}else{
			Debug.LogError("CANT FIND PLAYER");
		}
		 go = GameObject.Find ("AIPlayer");
		if(go)
		{
			aiPlayer = go.GetComponent<ZAIPlayer>();
		}else{
			Debug.LogError("CANT FIND AIPLAYER");
		}
	}
	public override void OnEnable()
	{
		base.OnEnable();
		BaseGameManager.onDamagableRemove+=onDamagableRemove;
	}
	public override void OnDisable()
	{
		base.OnDisable();
		BaseGameManager.onDamagableRemove-=onDamagableRemove;


	}

	public void onDamagableRemove(Damagable dam)
	{
		bool alive = false;
		GameObject go = dam.gameObject;
		BaseGameManager.addPoints(dam.points);



		int r = Random.Range(0,knockDownClips.Length-1);
		if(audioSource)
		{
			audioSource.PlayOneShot(knockDownClips[r]);
		}
		BaseAI[] ais = (BaseAI[])GameObject.FindObjectsOfType(typeof(BaseAI));
		for(int i=0; i<ais.Length; i++)
		{
			if(ais[i].isAlive())
			{
				alive=true;
			}
		}

		if(alive==false)
		{
			BaseGameManager.gameover(true);

		}
		else if(go.name.Equals("Player"))
		{
			Camera.main.transform.parent = null;
			BaseGameManager.gameover(false);
		}else{
			base.pushText(dam.points.ToString(),scoreGT);
		}
	}
	public Text turnText;
	private int turnIndex=1;
	public void changeTurn()
	{
		if(turn==0)
		{
			if(turnText)
				turnText.text = "Turn: " + turnIndex.ToString();
			turnIndex++;
		}
		turn ^=1;
		if(m_board)
		{
			m_board.clear();
		}
		if(aiPlayer)	
			aiPlayer.setTurn(turn==1);
		if(player)
			player.setTurn(turn==0);
	}
}
