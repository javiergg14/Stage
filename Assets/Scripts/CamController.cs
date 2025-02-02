using System.Collections;
using UnityEngine;

public class CamController : MonoBehaviour
{
    public float speed;
    public float lifes;
    public Animator animator;
    public GameObject particleDiePrefab;

    private bool hasSpawnedParticle = false;
    private Color originalColor;
    private SpriteRenderer spriteRenderer;
    private Color hitColor = Color.red;
    private GameObject player;
    private Health health;
    public CounterEnemies counterEnemies;

    void Start()
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
    }

    void Update()
    {
        if (lifes == 0 && !hasSpawnedParticle)
        {
            Instantiate(particleDiePrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
            counterEnemies.enemyDies();
            hasSpawnedParticle = true;
        }
        else if (lifes > 0 && player != null) // Asegúrate de que player no sea null antes de usarlo
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            Animation();
        }
    }

    private void Animation()
    {
        animator.SetBool("running", true);
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
