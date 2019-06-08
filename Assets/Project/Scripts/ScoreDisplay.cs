using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI livesText;
    private GameSession gameSession;

    void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
    }

    void Update()
    {
        if (scoreText && livesText)
        {
            scoreText.text = gameSession.GetScore().ToString();
            livesText.text = gameSession.GetLives().ToString();
        }
    }
}
