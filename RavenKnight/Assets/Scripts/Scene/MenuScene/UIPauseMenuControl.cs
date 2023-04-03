using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIPauseMenuControl : MonoBehaviour
{
    [SerializeField] private KeyCode pauseButton = KeyCode.Escape;
    [SerializeField] private GameObject pauseMenu;

    private void Update()
    {
        if (Input.GetKeyDown(pauseButton))
        {
            if (SceneManager.GetActiveScene().buildIndex != (int)EnumScenes.MenuScene)
            {
                if (pauseMenu.activeSelf)
                {
                    pauseMenu.SetActive(false);
                }
                else
                {
                    pauseMenu.SetActive(true);
                }
            }
        }
    }
}
