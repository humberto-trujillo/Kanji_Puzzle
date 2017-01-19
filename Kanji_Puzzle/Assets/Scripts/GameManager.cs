using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager> 
{
	public int movesLeft = 30;
	public int scoreGoal = 10000;
	public ScreenFader screenFader;
	public Text levelNameText;
	public Text movesLeftText;
	Board m_board;
	bool m_isReadyToBegin = false;
	bool m_isGameOver = false;
	bool m_isWinner = false;
	bool m_isReadyToReload = false;

	public MessageWindow messageWindow;
	public Sprite loseIcon;
	public Sprite winIcon;
	public Sprite goalIcon;

	void Start ()
	{
		m_board = GameObject.FindObjectOfType<Board>().GetComponent<Board>();
		Scene scene = SceneManager.GetActiveScene();
		if(levelNameText != null)
		{
			levelNameText.text = scene.name;
		}
		UpdateMoves();
		StartCoroutine(ExecuteGameLoop());
	}

	public void UpdateMoves()
	{
		if(movesLeftText != null)
		{
			movesLeftText.text = movesLeft.ToString();
		}
	}

	public void BeginGame()
	{
		m_isReadyToBegin = true;
	}

	public void ReloadScece()
	{
		m_isReadyToReload = true;
	}

	IEnumerator ExecuteGameLoop()
	{
		yield return StartCoroutine("StartGameRoutine");
		yield return StartCoroutine("PlayGameRoutine");
		yield return StartCoroutine("EndGameRoutine");
	}

	IEnumerator StartGameRoutine()
	{
		if(messageWindow != null)
		{
			messageWindow.GetComponent<RectXformMover>().MoveOn();
			messageWindow.ShowMessage(goalIcon,"score goal\n" + scoreGoal.ToString(),"start");
		}
		while(!m_isReadyToBegin)
		{
			yield return null;
		}
		if(screenFader != null)
		{
			screenFader.FadeOff();
		}
		yield return new WaitForSeconds(0.5f);
		if(m_board != null)
		{
			m_board.SetupBoard();
		}
	}

	IEnumerator PlayGameRoutine()
	{
		while(!m_isGameOver)
		{
			if(ScoreManager.Instance != null)
			{
				if(ScoreManager.Instance.CurrentScore >= scoreGoal)
				{
					m_isGameOver = true;
					m_isWinner = true;
				}
			}
			if(movesLeft == 0)
			{
				m_isGameOver = true;
				m_isWinner = false;
			}
			yield return null;
		}
	}

	IEnumerator EndGameRoutine()
	{
		m_isReadyToReload = false;

		if(m_isWinner)
		{
			messageWindow.GetComponent<RectXformMover>().MoveOn();
			messageWindow.ShowMessage(loseIcon,"Ganaste!","OK");
		}
		else
		{
			messageWindow.GetComponent<RectXformMover>().MoveOn();
			messageWindow.ShowMessage(loseIcon,"Perdiste!","OK");
		}

		yield return new WaitForSeconds(1f);

		if(screenFader != null)
		{
			screenFader.FadeOn();
		}

		while(!m_isReadyToReload)
		{
			yield return null;
		}
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}
