using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum TeamColour
{
    RED,
    GREEN,
    BLUE
}

public class Gameplay : MonoBehaviour
{
    private static Gameplay instance;
    public static Gameplay Instance { get { return instance; } }

    [Header("Initialization")]
    [SerializeField] public List<Material> colours;

    public TeamColour playerTeamColour;
    public GameObject player;

    [SerializeField] private GameObject characterPrefab;

    [SerializeField] private Transform spawnCenter;
    [SerializeField] private float spawnRadius;

    [SerializeField] private int redTeamSize;
    [SerializeField] private int blueTeamSize;
    [SerializeField] private int greenTeamSize;

    [SerializeField] private Transform redTeam;
    [SerializeField] private Transform greenTeam;
    [SerializeField] private Transform blueTeam;

    [Header("Score UI")]
    [SerializeField] private Slider redTeamScore;
    [SerializeField] private Slider greenTeamScore;
    [SerializeField] private Slider blueTeamScore;

    [Header("Health UI")]
    [SerializeField] private CharacterData playerData;
    [SerializeField] private Slider redHealth;
    [SerializeField] private Slider greenHealth;
    [SerializeField] private Slider blueHealth;

    public Dictionary<TeamColour, Dictionary<int, GameObject>> teams;

    private int characterCount = 0;

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this.gameObject);

        else
            instance = this;

        playerTeamColour = GameManager.Instance.playerTeam;

        redTeamSize = GameManager.Instance.redTeamSize;
        greenTeamSize = GameManager.Instance.greenTeamSize;
        blueTeamSize = GameManager.Instance.blueTeamSize;

        teams = new Dictionary<TeamColour, Dictionary<int, GameObject>>();
        
        for (int i = 0; i < 3; i++)
            teams.Add((TeamColour)i, new Dictionary<int, GameObject>());

        for (int i = 0; i < redTeamSize + greenTeamSize + blueTeamSize; i++)
        {
            Vector3 spawnLocation = spawnCenter.position + new Vector3(Random.Range(-spawnRadius, spawnRadius), 0.0f, Random.Range(-spawnRadius, spawnRadius));

            GameObject spawnedCharacter = Instantiate(characterPrefab, spawnLocation, Quaternion.Euler(0.0f,0.0f, 0.0f));
        }
    }

    public List<GameObject> GetEnemies(TeamColour colour)
    {
        List<GameObject> enemies = new List<GameObject>();
        List<TeamColour> teamKeys = new List<TeamColour>(teams.Keys);

        foreach (TeamColour col in teamKeys)
        {
            if (col != colour)
            {
                enemies.AddRange(teams[col].Values);
            }
        }

        return enemies;
    }

    private void Update()
    {
        UpdateScore();
        UpdateHealth();
    }

    private void UpdateScore()
    {
        int totalCharacters = redTeamSize + blueTeamSize + greenTeamSize + 1; // +player

        float redTotal = (float)teams[TeamColour.RED].Count / (float)totalCharacters;
        float greenTotal = (float)teams[TeamColour.GREEN].Count / (float)(totalCharacters) + redTotal;
        float blueTotal = (float)teams[TeamColour.BLUE].Count / (float)(totalCharacters) + greenTotal;

        redTeamScore.value = redTotal;
        greenTeamScore.value = greenTotal;
        blueTeamScore.value = blueTotal;

        for (int i = 0; i < 3; i++)
        {
            CheckWin((TeamColour)i);
        }
    }

    private void UpdateHealth()
    {

    }

    public TeamColour GetColourToTeam(Color colour)
    {
        if (Mathf.Max(colour.r, colour.g, colour.b) % colour.r == 0)
            return TeamColour.RED;

        else if (Mathf.Max(colour.r, colour.g, colour.b) % colour.g == 0)
            return TeamColour.GREEN;

        else
            return TeamColour.BLUE;
    }

    public Color GetTeamToColour(TeamColour teamColour)
    {
        switch(teamColour)
        {
            case TeamColour.RED:
                return Color.red;
            case TeamColour.GREEN:
                return Color.green;
            default:
                return Color.blue;
        }
    }

    public void CheckWin(TeamColour colour)
    {
        if (teams[colour].Count >= redTeamSize + blueTeamSize + greenTeamSize + 1)
        {
            if (playerTeamColour == colour)
                GameManager.Instance.win = true;

            UIManager.Instance.LoadScene("GameOver");
        }
    }
}
