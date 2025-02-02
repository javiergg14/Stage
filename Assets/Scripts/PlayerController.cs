using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject BulletPrefab;
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Canvas canvas;
    public Health health;

    private float lastShoot;
    private Vector2 movement;
    public Animator animator;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(canvas);
    }

    void Update()
    {
        ProcessInputs();
    }

    private void FixedUpdate()
    {
        Movement();
        Shoot();
    }

    private void ProcessInputs()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        Animation();
        
    }

    private void Movement()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    private void Shoot()
    {
        Vector3 direction = Vector3.zero;

        if (Time.time > lastShoot + 0.3f)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                direction += Vector3.up;
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                direction += Vector3.down;
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                direction += Vector3.right;
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                direction += Vector3.left;
            }

            if (direction != Vector3.zero)
            {
                GameObject bullet = Instantiate(BulletPrefab, transform.position + direction * 0.1f, Quaternion.identity);
                bullet.GetComponent<BulletController>().SetDirection(direction.normalized);
                lastShoot = Time.time;
            }
        }
    }

    private void Animation()
    {
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            health.decreaseHealth();
        }
    }
}
