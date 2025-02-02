using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class restart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(GameObject.FindGameObjectWithTag("Player"));
        Destroy(GameObject.FindGameObjectWithTag("Canvas"));
        Destroy(GameObject.FindGameObjectWithTag("Music"));


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && SceneManager.GetActiveScene().name != "SceneEscenFinal")
        {
            SceneManager.LoadScene("Scene1");
        }
    }
}
