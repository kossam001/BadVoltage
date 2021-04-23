using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Team
{
    Ally,
    Enemy
}

public class StageManager : MonoBehaviour
{
    private static StageManager instance;
    public static StageManager Instance { get { return instance; } }

    public GameObject playerCharacter;
    public Dictionary<Team, Dictionary<int, GameObject>> teamTable;

    public bool stageClear;
    public bool stageFail;
    public int enemyRemaining = 0;

    [SerializeField] private List<SpawnPoint> spawnPoints;
    [SerializeField] private List<GameObject> enemyTemplates;

    private int characterCount = 0;
    [SerializeField] private int currentWave = 0;
    [SerializeField] private int enemyToSpawn = 0;

    [SerializeField] private float goalWaveTimer;
    [SerializeField] private float currentWaveTimer = 10;

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this.gameObject);
        
        else
            instance = this;

        CreateTeams();
    }

    public void CreateTeams()
    {
        // Set the teams
        teamTable = new Dictionary<Team, Dictionary<int, GameObject>>();
        teamTable[Team.Ally] = new Dictionary<int, GameObject>();
        teamTable[Team.Enemy] = new Dictionary<int, GameObject>();

        // Setup the player character
        CharacterData playerData = playerCharacter.GetComponentInChildren<CharacterData>();
        
        SetupCharacter(playerData.gameObject, Team.Ally);
    }

    public List<GameObject> GetEnemies(Team team)
    {
        List<GameObject> enemyList = new List<GameObject>();
        List<Team> teamKeys = new List<Team>(teamTable.Keys);

        foreach (Team key in teamKeys)
        {
            if (key != team)
            {
                enemyList.AddRange(teamTable[key].Values);
            }
        }
       
        return enemyList;
    }

    public List<GameObject> GetFriends(Team team)
    {
        List<GameObject> friendList = new List<GameObject>();
        List<Team> teamKeys = new List<Team>(teamTable.Keys);

        foreach (Team key in teamKeys)
        {
            if (key == team)
            {
                friendList.AddRange(teamTable[key].Values);
            }
        }

        return friendList;
    }

    public void RemoveFromTeam(CharacterData character)
    {
        teamTable[character.currentTeam].Remove(character.id);
    }

    // Manage Waves
    public void SpawnWave()
    {
        if (currentWaveTimer <= 0)
        {
            currentWave++;
            enemyRemaining += currentWave * 2;
            enemyToSpawn = currentWave * 2;
            currentWaveTimer = goalWaveTimer;
        }
        else if (enemyToSpawn > 0)
        {
            if (IsInvoking(nameof(SpawnCharacter))) return;

            Invoke(nameof(SpawnCharacter), Random.Range(0.1f, 10.0f));
        }
        else if (enemyToSpawn <= 0)
        {
            currentWaveTimer -= Time.deltaTime;
        }
    }

    public void SpawnCharacter()
    {
        GameObject spawnedEnemy = spawnPoints[Random.Range(0, spawnPoints.Count)].SpawnObject(enemyTemplates[Random.Range(0, enemyTemplates.Count)]);

        if (spawnedEnemy != null)
        {
            CharacterData enemyData = spawnedEnemy.GetComponent<CharacterData>();

            enemyData.id = characterCount++;
            enemyData.currentTeam = Team.Enemy;

            teamTable[Team.Enemy].Add(enemyData.id, spawnedEnemy);
            enemyToSpawn--;
        }
        else
        {
            if (IsInvoking(nameof(SpawnCharacter))) return;

            Invoke(nameof(SpawnCharacter), Random.Range(0.1f, 10.0f));
        }
    }

    public void SetupCharacter(GameObject character, Team team)
    {
        // Set the characters
        CharacterData characterData;

        characterData = character.GetComponent<CharacterData>();

        characterData.id = characterCount;
        characterData.currentTeam = team;

        teamTable[team][characterData.id] = character;

        characterCount++;
    }

    private void Update()
    {
        SpawnWave();
    }
}
