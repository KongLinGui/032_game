using UnityEngine;
using System.Collections;
using UnityEngine.UI;
namespace InaneGames
{
	public class MenuHandler : MonoBehaviour {
		private GameObject m_pausePanel;
		private GameObject m_playPanel;
		private GameObject m_initPanel;
		private GameObject m_resultsPanel;
		public bool ismaster=true;
		public void Awake()
		{
			Animator[] animators = gameObject.GetComponentsInChildren<Animator>(true);
			for(int i=0; i<animators.Length;i++)
			{
				animators[i].gameObject.SetActive(true);
			}

			m_pausePanel = GameObject.Find ("PausePanel");
			m_playPanel = GameObject.Find ("PlayPanel");
			m_resultsPanel = GameObject.Find ("ResultsPanel");


		}
		public IEnumerator Start()
		{

			yield return new WaitForEndOfFrame();
			if(ismaster)
				{
				if(m_pausePanel)
				{
					m_pausePanel.SetActive(false);
				}
				if(m_playPanel)
				{
					m_playPanel.SetActive(false);
				}
				if(m_resultsPanel)
				{
					m_resultsPanel.SetActive(false);
				}
			}
		}
		public void mainMenu()
		{		
			Time.timeScale=1;

			Application.LoadLevel(0);
		}
		public void reload()
		{			
			Time.timeScale=1;

			Application.LoadLevel(Application.loadedLevel);
		}
		public void nextLevel()
		{
			Time.timeScale=1;

			Application.LoadLevel(Application.loadedLevel+1);

		}
		public void resumeGame()
		{				
			Time.timeScale=1;
			Constants.fadeInFadeOut(m_playPanel,m_pausePanel);

		}

		public void startGame()
		{
//				Debug.Log ("startGame");
			BaseGameManager.startGame();
		}
	}
}