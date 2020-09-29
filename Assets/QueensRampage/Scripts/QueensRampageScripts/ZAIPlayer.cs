using UnityEngine;
using System.Collections;

public class ZAIPlayer : MonoBehaviour {
	private BaseAI[] m_ais;
	private bool m_myTurn = false;
	private ZGame m_game;
	private bool m_requiresExecute=true;
	public AudioSource audioSource;
	void Start () {
		m_ais = gameObject.GetComponentsInChildren<BaseAI>();
		m_game = (ZGame)GameObject.FindObjectOfType(typeof(ZGame));
		if(audioSource==null)
		{
			audioSource = gameObject.AddComponent<AudioSource>();
			audioSource.clip = Resources.Load ("throw") as AudioClip;
			audioSource.pitch = 2f;	
		}

	}
	public void setTurn(bool turn)
	{
		m_myTurn=turn;

	}
	void Update () {

		if(m_myTurn)
		{
			if(m_requiresExecute)
			{
				if(audioSource)
				{
					audioSource.Play();
				}
				for(int i=0; i<m_ais.Length; i++)
				{
					if(m_ais[i])
					{
						m_ais[i].execute();
					}
				}
				m_requiresExecute=false;
			}
			else{
				checkForDone();
			}
		}
	}
	void checkForDone()
	{
		bool done=true;
		for(int i=0; i<m_ais.Length; i++)
		{
			if(m_ais[i] && m_ais[i].isDone()==false)
			{
				done=false;
			}
		}

		if(done)
		{
//			Debug.Log ("checkForDone" + done );
			m_requiresExecute=true;
			m_game.changeTurn();
			m_myTurn = false;
		}

	
	}
}
