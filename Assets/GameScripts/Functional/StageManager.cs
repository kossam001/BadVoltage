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

    [SerializeField] private List<SpawnPoint> spawnPoints;

    private int characterCount = 0;
    private int currentWave = 0;

    [SerializeField] private float goalWaveTimer;
    private float currentWaveTimer;

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
    public void SpawnCharacters()
    {
        SpawnCharacter();
    }

    public void SpawnCharacter()
    {

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
}
