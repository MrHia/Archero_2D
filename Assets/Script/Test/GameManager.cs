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

        Enemy_bot.OnMonsterDefeated += onMonsterDefeated;
    }
    Transform GetClosestEnemy(Transform[] enemies)
    {
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (Transform t in enemies)
        {
            float dist = Vector3.Distance(t.position, currentPos);
            if (dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }
        return tMin;
    }



    private void onMonsterDefeated(GameObject monster)
    {

        int index = Array.IndexOf(monsters, monster);


        if (index != -1)
        {
            monsters[index] = null;
        }

    }
}
