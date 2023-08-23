using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameTextManager : MonoBehaviour
{
    public void StartButton()
    {
        SceneManager.LoadScene(2);
    }
}
