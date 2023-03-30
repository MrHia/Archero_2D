using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] monsters;
    private void Start()
    {
        monsters = GameObject.FindGameObjectsWithTag("Monster");
        //Monster.OnMonsterDefeated += OnMonsterDefeated;
    }




    private void OnMonsterDefeated(GameObject monster)
    {

        int index = Array.IndexOf(monsters, monster);


        if (index != -1)
        {
            monsters[index] = null;
        }

    }
}
