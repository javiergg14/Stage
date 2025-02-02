using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnxietyController : MonoBehaviour
{

    [SerializeField] private float velocidadMovimiento;


    [SerializeField] private Transform[] puntosMovimiento;

    public float lifes;
    public GameObject AnxietyBulletPrefab;
    public GameObject particleDiePrefab;

    [SerializeField] private float distanciaMinima;

    private bool hasSpawnedParticle = false;
    private int numeroAleatorio;
    private float lastShoot;
    private Color originalColor;
    private GameObject player;
    private Health health;
    private Color hitColor = Color.red;
    private SpriteRenderer spriteRenderer;
    public CounterEnemies counterEnemies;


    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;

        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("No se pudo encontrar al jugador en la escena.");
        }

        health = player.GetComponent<Health>();
        if (health == null)
        {
            Debug.LogError("No se pudo encontrar el componente Health en este objeto.");
        }

        numeroAleatorio = Random.Range(0, puntosMovimiento.Length);
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, puntosMovimiento[numeroAleatorio].position) < distanciaMinima)
        {
            numeroAleatorio = Random.Range(0, puntosMovimiento.Length);
        }

        Vector3 direction = player.transform.position - transform.position;

        if (Time.time > lastShoot + 1f)
        {
            if (direction != Vector3.zero)
            {
                GameObject bullet = Instantiate(AnxietyBulletPrefab, transform.position + direction * 0.1f, Quaternion.identity);
                bullet.GetComponent<EnemyBulletController>().SetDirection(direction.normalized);
                lastShoot = Time.time;
            }
        }

        if (lifes == 0 && !hasSpawnedParticle)
        {
            Instantiate(particleDiePrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
            counterEnemies.enemyDies();
            hasSpawnedParticle = true;
        }
        else if (lifes > 0 && player != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, puntosMovimiento[numeroAleatorio].position, velocidadMovimiento * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet") && lifes > 0)
        {
            StartCoroutine(RecoverColor());
            lifes -= 1;
        }

        if (collision.gameObject.CompareTag("Player") && lifes > 0)
        {
            health.decreaseHealth();
            Camera.main.GetComponent<CameraShake>().Shake();
        }
    }

    IEnumerator RecoverColor()
    {
        spriteRenderer.color = hitColor;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = originalColor;
    }
}
