using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BalloonGameManager : MonoBehaviour
{
    public GameObject bigBall;
    public GameObject leftBasket;
    public GameObject rightBasket;
    public GameObject[] smallBalls;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI instructionText;
    public TextMeshProUGUI scoreText;
    public GameObject winPanel;
    public OVRInput.Controller leftController = OVRInput.Controller.LTouch;
    public OVRInput.Controller rightController = OVRInput.Controller.RTouch;

    private OculusTossableBalloon bigBallController;
    private int ballsTransferred = 0;
    private float gameTimer = 0f;
    private bool gameStarted = false;
    private bool gameEnded = false;

    void Start()
    {
        bigBallController = bigBall.GetComponent<OculusTossableBalloon>();
        if (winPanel != null)
            winPanel.SetActive(false);

        UpdateInstructionText("Grab and toss the big ball to start!");
        UpdateScoreText();
    }

    void Update()
    {
        // Update game timer if game is active
        if (gameStarted && !gameEnded)
        {
            gameTimer += Time.deltaTime;
            UpdateTimerText();
        }

        // Check if game has started (when big ball is tossed)
        if (!gameStarted && bigBallController != null && bigBallController.IsInAir())
        {
            StartGame();
        }

        // Check if big ball is on the ground and reset instructions
        if (gameStarted && !gameEnded && bigBallController != null && !bigBallController.IsInAir())
        {
            UpdateInstructionText("Toss the big ball again to continue!");
        }

        // Quick restart with controller buttons if game ended
        if (gameEnded && (OVRInput.GetDown(OVRInput.Button.One, leftController) ||
                         OVRInput.GetDown(OVRInput.Button.One, rightController)))
        {
            RestartGame();
        }
    }

    void StartGame()
    {
        gameStarted = true;
        gameTimer = 0f;
        UpdateInstructionText("Transfer small balls while the big ball is in air!");
    }

    public void BallTransferred()
    {
        ballsTransferred++;
        UpdateScoreText();

        // Give haptic feedback
        OVRInput.SetControllerVibration(1.0f, 0.5f, leftController);
        OVRInput.SetControllerVibration(1.0f, 0.5f, rightController);

        if (ballsTransferred >= smallBalls.Length)
        {
            EndGame();
        }
    }

    void EndGame()
    {
        gameEnded = true;
        if (winPanel != null)
            winPanel.SetActive(true);

        UpdateInstructionText("Great job! Press A button to play again");
    }

    public void BalloonTossed()
    {
        // This can be called from the OculusTossableBalloon script
    }

    public void BalloonDropped()
    {
        // This can be called from the OculusTossableBalloon script 
    }

    void UpdateTimerText()
    {
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(gameTimer / 60F);
            int seconds = Mathf.FloorToInt(gameTimer % 60F);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    void UpdateInstructionText(string text)
    {
        if (instructionText != null)
            instructionText.text = text;
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
            scoreText.text = "Balls: " + ballsTransferred + " / " + smallBalls.Length;
    }

    public bool IsBigBallInAir()
    {
        return bigBallController != null && bigBallController.IsInAir();
    }

    public void RestartGame()
    {
        // Reset game state
        gameStarted = false;
        gameEnded = false;
        ballsTransferred = 0;
        gameTimer = 0f;

        // Reset UI
        if (winPanel != null)
            winPanel.SetActive(false);

        UpdateScoreText();
        UpdateTimerText();
        UpdateInstructionText("Grab and toss the big ball to start!");

        // Reset balls
        if (bigBallController != null)
            bigBallController.ResetBalloon();

        // Reset small balls
        foreach (GameObject ball in smallBalls)
        {
            OculusGrabbableSmallBall smallBallScript = ball.GetComponent<OculusGrabbableSmallBall>();
            if (smallBallScript != null)
                smallBallScript.ResetPosition();
        }
    }
}