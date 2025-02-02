using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recorrido : MonoBehaviour
{

    [SerializeField] private float velocidadMovimiento;


    [SerializeField] private Transform[] puntosMovimiento;


    [SerializeField] private float distanciaMinima;

    public float lifes;
    public GameObject particleDiePrefab;

    private Color originalColor;
    private SpriteRenderer spriteRenderer;
    private Color hitColor = Color.red;
    private bool hasSpawnedParticle = false;
    private GameObject player;
    private Health health;
    private int numeroAleatorio;
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
        Girar();
    }

    private void Update()
    {

        if (Vector2.Distance(transform.position, puntosMovimiento[numeroAleatorio].position) < distanciaMinima)
        {
            numeroAleatorio = Random.Range(0, puntosMovimiento.Length);
            Girar();
        }

        if (lifes == 0 && !hasSpawnedParticle)
        {
            Instantiate(particleDiePrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
            counterEnemies.enemyDies();
            hasSpawnedParticle = true;
        }
        else if (lifes > 0 && player != null) // Asegúrate de que player no sea null antes de usarlo
        {
            transform.position = Vector2.MoveTowards(transform.position, puntosMovimiento[numeroAleatorio].position, velocidadMovimiento * Time.deltaTime);
            transform.rotation = transform.rotation * Quaternion.Euler(0f, 0f, 1.0f);
        }
    }
    private void Girar()
    {
        if (transform.position.x < puntosMovimiento[numeroAleatorio].position.x)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
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
