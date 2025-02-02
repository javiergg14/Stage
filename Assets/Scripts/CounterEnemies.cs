using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterEnemies : MonoBehaviour
{

    private float counter = 0;
    private GameObject[] enemies;
    private GameObject wall;
    private bool destroyed;

    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        wall = GameObject.FindGameObjectWithTag("Collider");
    }

    void Update()
    {
        if (counter == enemies.Length && !destroyed)
        {
            Destroy(wall);
            destroyed = true;
            
        }
    }
    public void enemyDies()
    {
        counter++;
    }
}
