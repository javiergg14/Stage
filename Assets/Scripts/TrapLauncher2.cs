using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapLauncher2 : MonoBehaviour
{
    public GameObject EnemyBulletPrefab;
    private float lastShoot;
    public Animator animator;

    void Start()
    {
        
    }

    void Update()
    {
        if (Time.time > lastShoot + 1f)
        {
            Vector3 direction = Vector3.up;
            GameObject bullet = Instantiate(EnemyBulletPrefab, transform.position + direction * 0.1f, Quaternion.identity);
            bullet.GetComponent<EnemyBulletController>().SetDirection(direction.normalized);
            lastShoot = Time.time;
        }
        
    }
}
