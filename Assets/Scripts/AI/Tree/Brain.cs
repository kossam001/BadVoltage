using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Brain : MonoBehaviour
{
    public GameObject character;
    private StateMachine controller;

    public Vector3 moveDestination;

    public List<GameObject> enemyTargets;
    public GameObject activeTarget;

    [Tooltip("Maximum distance to a target to be considered in range of combat.")]
    public float combatRange; 

    public NavMeshAgent agent;

    private void Awake()
    {
        controller = GetComponent<StateMachine>();
    }
}
