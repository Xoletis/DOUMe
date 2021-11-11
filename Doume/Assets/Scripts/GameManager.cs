using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
   
    public GameObject player;
    private PlayerInventory inventory;
    [SerializeField]
    private int score;

    public string nameOfScoreScene;
    public string nameOfGameScene;

    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.LogError("Il y a plusieurs instance de Game Manger dans la scéne");
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
            if (Input.GetKeyDown(KeyCode.P))
            {
                inventory.HurtPlayer(20);
            }

            if (inventory.IsDead())
            {
                Debug.Log("mort du joueur");
                SceneManager.LoadScene(nameOfScoreScene);
            }

            if (Input.GetKeyDown(KeyCode.O))
            {
                SceneManager.LoadScene(nameOfScoreScene);
            }

            if (Input.GetKeyDown(KeyCode.M))
            {
                score += 20;
            }
        }
    }

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
    }

    public int GetScore()
    {
        return score;
    }
}
