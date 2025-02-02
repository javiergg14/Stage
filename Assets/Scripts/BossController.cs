using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossController : MonoBehaviour
{
    public float lifes;
    public GameObject particleDiePrefab;

    private bool hasSpawnedParticle = false;
    private Color originalColor;
    private SpriteRenderer spriteRenderer;
    private Color hitColor = Color.red;
    private GameObject[] trapLaunchers;
    private float tiempo;

    // Start is called before the first frame update
    void Start()
    {
        trapLaunchers = GameObject.FindGameObjectsWithTag("TrapLauncher");
        spriteRenderer = GetComponent<SpriteRenderer>();
        particleDiePrefab.transform.localScale *= 5;
        originalColor = spriteRenderer.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (lifes == 0 && !hasSpawnedParticle)
        {
            Instantiate(particleDiePrefab, transform.position, Quaternion.identity);
            spriteRenderer.enabled = false;
            foreach (var trapLauncher in trapLaunchers)
            {
                Destroy(trapLauncher);
            }
            tiempo = Time.time;
            hasSpawnedParticle = true;
        }
        if (hasSpawnedParticle)
        {
            finishGame();
        }
    }

    private void finishGame()
    {
        if (Time.time - tiempo > 1f)
        {
            SceneManager.LoadScene("SceneEscenFinal");
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet") && lifes > 0)
        {
            StartCoroutine(RecoverColor());
            lifes -= 1;
        }
    }

    IEnumerator RecoverColor()
    {
        spriteRenderer.color = hitColor;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = originalColor;
    }
}
