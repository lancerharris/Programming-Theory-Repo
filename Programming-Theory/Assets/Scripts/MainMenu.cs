using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class MainMenu : MonoBehaviour
{
  [Header("UI Components")]
  public TMP_InputField jumperBallNameInput;
  public TMP_InputField boosterBallNameInput;
  public TMP_Dropdown jumperBallColorDropdown;
  public TMP_Dropdown boosterBallColorDropdown;
  public Button startGameButton;

  [Header("Ball Colors")]
  public Color[] jumperBallColors; // You can set this array in the Unity Editor with the desired colors.
  public Color[] boosterBallColors; // You can set this array in the Unity Editor with the desired colors.

  private GameManager gameManager;

  private void Start()
  {
    gameManager = FindObjectOfType<GameManager>();

    startGameButton.onClick.AddListener(StartGame);

    PopulateColorDropdowns();
  }

  private void Update()
  {
    // Set Ball Colors
    gameManager.jumperBall.BallColor = jumperBallColors[jumperBallColorDropdown.value];
    gameManager.boosterBall.BallColor = boosterBallColors[boosterBallColorDropdown.value];

    gameManager.jumperBall.GetComponent<Renderer>().material.color = gameManager.jumperBall.BallColor;
    gameManager.boosterBall.GetComponent<Renderer>().material.color = gameManager.boosterBall.BallColor;
  }

  private void PopulateColorDropdowns()
  {
    jumperBallColorDropdown.ClearOptions();
    boosterBallColorDropdown.ClearOptions();

    List<string> jumperColorNames = new List<string>();
    List<string> boosterColorNames = new List<string>();
    foreach (Color color in jumperBallColors)
    {
      jumperColorNames.Add(ColorUtility.ToHtmlStringRGB(color));
    }
    foreach (Color color in boosterBallColors)
    {
      boosterColorNames.Add(ColorUtility.ToHtmlStringRGB(color));
    }

    jumperBallColorDropdown.AddOptions(jumperColorNames);
    boosterBallColorDropdown.AddOptions(boosterColorNames);
  }

  private void StartGame()
  {
    // Set Ball Names
    gameManager.jumperBall.PlayerName = jumperBallNameInput.text;
    gameManager.boosterBall.PlayerName = boosterBallNameInput.text;

    // Start the game in the GameManager
    gameManager.StartGame();

    // Hide the main menu (you could also disable it or switch to another scene)
    this.gameObject.SetActive(false);
  }
}
