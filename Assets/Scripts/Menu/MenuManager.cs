using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject Panel;
    

    private void Start()
    {
        Panel.SetActive(false);
        
    }
    public void ExitPanelButton()
    {
        Panel.SetActive(false);
    }

    public void PanelButton()
    {
        Panel.SetActive(true);
    }

    public void StartButton()
    {
        SceneManager.LoadScene(1);
    }
}
