using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int health;
    public int numOfHearts;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    private bool sceneLoaded = false;
    private bool isInvulnerable = false;

    private void Update()
    {
        if (health > numOfHearts)
        {
            health = numOfHearts;
        }

        if (health == 0 && !sceneLoaded)
        {
            SceneManager.LoadScene("SceneDeath");
            sceneLoaded = true;
        }

        UpdateHeartsUI();
    }

    private void UpdateHeartsUI()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }

            if (i < numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }

    public void decreaseHealth()
    {
        if (!isInvulnerable)
        {
            health -= 1;
            StartCoroutine(InvulnerabilityCooldown());
        }
    }

    private IEnumerator InvulnerabilityCooldown()
    {
        isInvulnerable = true;
        yield return new WaitForSeconds(1f); // 1 second invulnerability
        isInvulnerable = false;
    }

    public void increaseHealth()
    {
        health += 1;
        UpdateHeartsUI();
    }
}
