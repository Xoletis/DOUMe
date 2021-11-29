using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{

    [SerializeField]
    private int score;
    private int highscore;

    public Text scoreText;
    public Text highscoreText;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        score = GameManager.instance.GetScore();
        highscore = PlayerPrefs.GetInt("Highscore");

        if (score > highscore)
        {
            highscore = score;
            PlayerPrefs.SetInt("Highscore", score);
        }

        scoreText.text = "Score : " + score.ToString();
        highscoreText.text = "Highscore : " + highscore.ToString();
    }
}
