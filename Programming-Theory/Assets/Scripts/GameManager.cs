using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        MainMenu,
        Playing,
        GameOver
    }

    public GameState currentState = GameState.MainMenu;

    public Ball jumperBall;
    public Ball boosterBall;

    public TextMeshProUGUI jumperBallScoreText;
    public TextMeshProUGUI boosterBallScoreText;

    public Canvas mainMenuCanvas;
    public Canvas ScoreCanvas;

    private int maxScore = 10;

    private void Start()
    {
        currentState = GameState.MainMenu;
        mainMenuCanvas.gameObject.SetActive(true);
        ScoreCanvas.gameObject.SetActive(false);
    }

    private void Update()
    {
        switch (currentState)
        {
            case GameState.MainMenu:
                break;
            case GameState.Playing:
                CheckScores();
                break;
            case GameState.GameOver:
                break;
        }
    }
    public void StartGame()
    {
        currentState = GameState.Playing;
        mainMenuCanvas.gameObject.SetActive(false);
        ScoreCanvas.gameObject.SetActive(true);
        ResetScores();
    }

    public void CheckScores()
    {
        if (jumperBall.Score >= maxScore || boosterBall.Score >= maxScore)
        {
            currentState = GameState.GameOver;
            HandleGameOver();
        }
    }

    private void ResetScores()
    {
        jumperBall.Score = 0;
        boosterBall.Score = 0;
        UpdateScoreUI();
    }

    private void HandleGameOver()
    {
        ResetScores();
        currentState = GameState.MainMenu;
        mainMenuCanvas.gameObject.SetActive(true);
        ScoreCanvas.gameObject.SetActive(false);
    }

    public void UpdateScoreUI()
    {
        jumperBallScoreText.text = jumperBall.PlayerName + " Score: " + jumperBall.Score.ToString();
        boosterBallScoreText.text = boosterBall.PlayerName + " Score: " + boosterBall.Score.ToString();
    }

    public void BallFellOff(Ball fallenBall)
    {
        if (fallenBall is JumperBall)
        {
            boosterBall.Score++;
        }
        else if (fallenBall is BoosterBall)
        {
            jumperBall.Score++;
        }

        UpdateScoreUI();

        // Reset ball positions and velocities
        ResetBallPositions();
    }

    private void ResetBallPositions()
    {

        if (jumperBall.Score >= boosterBall.Score)
        {
            boosterBall.transform.localScale *= 0.9f;  // Reduce its size by 10%
            jumperBall.transform.localScale *= 1.1f;   // Increase its size by 10%
        }
        else
        {
            jumperBall.transform.localScale *= 0.9f;  // Reduce its size by 10%
            boosterBall.transform.localScale *= 1.1f; // Increase its size by 10%
        }

        jumperBall.transform.position = new Vector3(-2, 4, 0);
        jumperBall.GetComponent<Rigidbody>().velocity = Vector3.zero;

        boosterBall.transform.position = new Vector3(2, 4, 0);
        boosterBall.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
}

