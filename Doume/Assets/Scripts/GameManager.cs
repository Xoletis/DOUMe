using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
   
    public GameObject player;
    private PlayerInventory inventory;
    [SerializeField]
    private int score;

    public AudioClip openDoor;

    public string nameOfScoreScene;
    public string nameOfGameScene;

    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("Il y a plusieurs instance de Game Manager dans la sc?ne");
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        inventory = player.GetComponent<PlayerInventory>();
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == nameOfGameScene)
        {

            if (inventory.IsDead())
            {
                Debug.Log("mort du joueur");
                SceneManager.LoadScene(nameOfScoreScene);
            }
        }
    }

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
        PlayerPrefs.SetInt("Score", score);
    }

    public int GetScore()
    {
        return score;
    }
}
