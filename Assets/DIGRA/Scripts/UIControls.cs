using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIControls : MonoBehaviour
{
    public float waitTime = 3;
    public Button[] Buttons;
    private void Start()
    {
        StartCoroutine(loadButtons(waitTime));
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Proceed()
    {
        SceneManager.LoadScene(sceneBuildIndex: 1);
    }

    public void Quit()
    {
        Application.Quit();
    }

    IEnumerator loadButtons(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        foreach (var button in Buttons)
        {
            button.GetComponent<Button>().interactable = true;
        }
        
    }
}
