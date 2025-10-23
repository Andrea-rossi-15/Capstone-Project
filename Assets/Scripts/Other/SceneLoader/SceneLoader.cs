using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
        Time.timeScale = 1;
    }
    public void ActivateMenu(GameObject menu)
    {
        menu.SetActive(true);
    }

    public void DeactivateMenu(GameObject menu)
    {
        menu.SetActive(false);
    }
}
