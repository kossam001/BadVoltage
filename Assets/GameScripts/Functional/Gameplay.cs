using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Team
{
    Friend,
    Foe
}

public class Gameplay : MonoBehaviour
{
    private static Gameplay instance;
    public static Gameplay Instance { get { return instance; } }

    [Header("Initialization")]
    [SerializeField] public List<Material> colours;

    public Team playerTeamColour;
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

    [Header("Health UI")]
    [SerializeField] private CharacterData playerData;

    public Dictionary<Team, Dictionary<int, GameObject>> teams;

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

        teams = new Dictionary<Team, Dictionary<int, GameObject>>();
        
        for (int i = 0; i < 3; i++)
            teams.Add((Team)i, new Dictionary<int, GameObject>());

        for (int i = 0; i < redTeamSize + greenTeamSize + blueTeamSize; i++)
        {
            Vector3 spawnLocation = spawnCenter.position + new Vector3(Random.Range(-spawnRadius, spawnRadius), 0.0f, Random.Range(-spawnRadius, spawnRadius));

            GameObject spawnedCharacter = Instantiate(characterPrefab, spawnLocation, Quaternion.Euler(0.0f,0.0f, 0.0f));
        }
    }

    public List<GameObject> GetEnemies(Team team)
    {
        List<GameObject> enemies = new List<GameObject>();
        List<Team> teamKeys = new List<Team>(teams.Keys);

        foreach (Team teamKey in teamKeys)
        {
            if (teamKey != team)
            {
                enemies.AddRange(teams[teamKey].Values);
            }
        }

        return enemies;
    }
}
