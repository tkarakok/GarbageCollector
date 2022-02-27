using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UiManager : Singleton<UiManager>
{
    [Header("Panel")]
    public GameObject mainMneuPanel;
    public GameObject inGamePanel;
    public GameObject gameOverPanel;
    public GameObject endGamePanel;
    [Header("Main Menu")]
    public Text mainMenuTotalCoinText;
    public Text mainMenuLevelText;
    [Header("In Game")]
    public Text inGameCoinText;
    public Text inGameCurrentLevelText;
    public Text inGameNextLevelText;
    [Header("EndGame")]
    public GameObject[] recyclerSprites;
    public GameObject targetSprite;
    public Text endGameCoinText;


    private GameObject _currentPanel;

    private void Start()
    {
        MainMenuUIUpdate();
        _currentPanel = mainMneuPanel;
    }

    public void PanelChange(GameObject openPanel)
    {
        _currentPanel.SetActive(false);
        openPanel.SetActive(true);
        _currentPanel = openPanel;

    }

    #region Buttons
    public void StartButton()
    {
        StateManager.Instance.State = State.InGame;
        PanelChange(inGamePanel);
        InGameCoinUpdate();
        InGameLevelUpdate();
        GameManager.Instance.animator.SetBool("Run",true);
    }
    public void RestartButton()
    {
        LevelManager.Instance.ChangeLevel("LEVEL " + LevelManager.Instance.CurrentLevel);
    }
    public void NextLevelButton()
    {
        LevelManager.Instance.ChangeLevel("LEVEL " + LevelManager.Instance.GetLevelName());

    }
    #endregion




    #region UIUpdate
    public void MainMenuUIUpdate()
    {
        mainMenuTotalCoinText.text = PlayerPrefs.GetInt("Total").ToString();
        mainMenuLevelText.text = "LEVEL " + (LevelManager.Instance.CurrentLevel).ToString();
    }
    // In Game
    public void InGameCoinUpdate()
    {
        inGameCoinText.text = GameManager.Instance.RecycleGarbage.ToString();
    }
    public void InGameLevelUpdate()
    {
        inGameCurrentLevelText.text = LevelManager.Instance.CurrentLevel.ToString();
        inGameNextLevelText.text = (LevelManager.Instance.CurrentLevel + 1).ToString();
    }
    public void EndGame()
    {
        PanelChange(endGamePanel);
        StartCoroutine(Claim());
        PlayerPrefs.SetInt("Total", PlayerPrefs.GetInt("Total") + GameManager.Instance.RecycleGarbage);
        endGameCoinText.text = PlayerPrefs.GetInt("Total").ToString();
    }
    #endregion

    IEnumerator Claim()
    {
        for (int i = 0; i < recyclerSprites.Length; i++)
        {
            recyclerSprites[i].transform.DOMove(targetSprite.transform.position, .25f).OnComplete(() => recyclerSprites[i].gameObject.SetActive(false));
            yield return new WaitForSeconds(.05f);
        }
    }
}
