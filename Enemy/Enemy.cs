using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterStats))]
public class Enemy : MonoBehaviour
{
    PlayerManager playerManager;
    CharacterStats myStats;
    
    void Start()
    {
        playerManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
        myStats = GetComponent<CharacterStats>();
    }  
}
