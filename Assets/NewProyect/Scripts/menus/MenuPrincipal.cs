using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    public string sceneName;

    private void Start()
    {
        // Si es necesario, puedes cargar aquí cualquier inicialización
    }

    public void cargarEscenaJuego()
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            if (SceneExists(sceneName))
            {
                // Guarda la escena actual antes de cargar la nueva
                PlayerPrefs.SetString("PreviousScene", SceneManager.GetActiveScene().name);
                PlayerPrefs.Save();

                SceneManager.LoadScene(sceneName);
            }
            else
            {
                Debug.LogError("La escena '" + sceneName + "' no está incluida en las configuraciones del build.");
            }
        }
        else
        {
            Debug.LogError("El nombre de la escena no está definido.");
        }
    }

    private bool SceneExists(string name)
    {
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);
            if (sceneName == name)
            {
                return true;
            }
        }
        return false;
    }

    public void Retry()
    {
        // Obtiene la escena anterior guardada
        string previousScene = PlayerPrefs.GetString("PreviousScene", "");

        if (!string.IsNullOrEmpty(previousScene))
        {
            SceneManager.LoadScene(previousScene);
        }
        else
        {
            Debug.LogError("No se encontró la escena anterior. Asegúrate de que se haya guardado antes de cargar el menú de Game Over.");
        }
    }

   

    public void salirJuego()
    {
        Application.Quit();
    }
}
