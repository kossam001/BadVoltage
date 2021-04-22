using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterData : MonoBehaviour
{
    public int id;
    public int health = 100;
    public bool isAttacking = false;

    public float maxCharge;
    public float charge;

    private void Awake()
    {
        charge = 500;
    }
}
