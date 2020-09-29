using UnityEngine;
using UnityEngine.UI;
using System.Collections;
namespace InaneGames {
	/// <summary>
	/// Base result state.
	/// </summary>
	public class BaseResultState : BaseMenuState 
	{
		private GameObject m_resultsPanel;
		private GameObject m_playPanel;
		private Text m_resultsText;
		private AudioSource m_audioSource;
		public void Awake()
		{
			m_resultsPanel = GameObject.Find ("ResultsPanel");
			m_playPanel = GameObject.Find ("PlayPanel");
			m_resultsText = Misc.getText("ResultsText");
			m_audioSource=	gameObject.AddComponent<AudioSource>();
			m_audioSource.loop=true;
		}


		public void OnEnable()
		{
			BaseGameManager.onGameOver += onGameOver;
		}
		
		public void OnDisable()
		{
			BaseGameManager.onGameOver -= onGameOver;
		}
		
		public void onGameOver(bool vic)
		{				
			BaseGameScript bs = (BaseGameScript)GameObject.FindObjectOfType(typeof(BaseGameScript));


			if(m_resultsText)
			{
				m_resultsText.text = bs.getResultsAsString();
			}	
			Constants.fadeInFadeOut(m_resultsPanel,m_playPanel);

			if(vic==false)
			{
				if(m_audioSource)
				{
					m_audioSource.PlayOneShot(Resources.Load("DefeatTune") as AudioClip);
				}
				Destroy(GameObject.Find("NextButton"));
			}else{
				if(m_audioSource)
				{
					m_audioSource.PlayOneShot(Resources.Load("VictoryTune") as AudioClip);
				}
				FMG.Constants.setMaxLevel(Application.loadedLevel+1);
			}
		}
		
	}
}
