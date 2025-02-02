using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    public string sceneToLoad;
    public string playerPosition;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        

        if (collision.CompareTag("Player"))
        {
            SceneManager.LoadScene(sceneToLoad);

            switch (playerPosition)
            {
                case "top":
                    player.GetComponent<Transform>().position = new Vector2(-0.16f, 1.3f);
                    break;
                case "bot":
                    player.GetComponent<Transform>().position = new Vector2(-0.09f, -2.56f);
                    break;
                case "left":
                    player.GetComponent<Transform>().position = new Vector2(-7.00f, 0.5f);
                    break;
                case "right":
                    player.GetComponent<Transform>().position = new Vector2(6.85f, 0.04f);
                    break;
                case "espejoarribaderecha":
                    player.GetComponent<Transform>().position = new Vector2(2.45f, 0.02f);
                    break;
                case "espejoarribaizquierda":
                    player.GetComponent<Transform>().position = new Vector2(-3.3f, 0.02f);
                    break;
                case "espejoabajoderecha":
                    player.GetComponent<Transform>().position = new Vector2(3, -2.71f);
                    break;
                case "espejoabajoizquierda":
                    player.GetComponent<Transform>().position = new Vector2(-3.5f, -2.71f);
                    break;
                case "final":
                    player.GetComponent<Transform>().position = new Vector2(-0.16f, -2.5f);
                    break;
                case "centro":
                    player.GetComponent<Transform>().position = new Vector2(-0.07f, -1.76f);
                    break;
            }
        }
    }
}
