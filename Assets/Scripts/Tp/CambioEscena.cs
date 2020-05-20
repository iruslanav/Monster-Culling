using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CambioEscena : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            CargarScene();
        }
    }

    public void CargarScene()
    {
        SceneManager.LoadScene("Mina");

    }
}
