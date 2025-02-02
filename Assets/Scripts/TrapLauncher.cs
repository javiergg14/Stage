using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapLauncher : MonoBehaviour
{
    public GameObject EnemyBulletPrefab;
    private float lastShoot;
    public Animator animator;

    void Start()
    {
        
    }

    void Update()
    {
        if (Time.time > lastShoot + 2f)
        {
            Vector3 direction = Vector3.down;
            GameObject bullet = Instantiate(EnemyBulletPrefab, transform.position + direction * 0.1f, Quaternion.identity);
            bullet.GetComponent<EnemyBulletController>().SetDirection(direction.normalized);
            lastShoot = Time.time;
        }
        
    }
}
