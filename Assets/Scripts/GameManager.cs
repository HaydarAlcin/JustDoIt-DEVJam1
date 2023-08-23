using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public float time = 100;
    int decimalDigit;

    public TextMeshProUGUI timerText;

    public Transform player;

    public Transform Motivasion;
    AudioSource motivasionAudio;
    public AudioClip[] motiSounds;
    bool isCooldown=false;
    public float cooldownDuration;

    public GameObject winPanel, losePanel;

    private void Start()
    {
        motivasionAudio = Motivasion.gameObject.GetComponent<AudioSource>();
        winPanel.SetActive(false);
        losePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    private void Update()
    {
        TimeSystem();

        if (!isCooldown)
        {
           
            MotivasionSounds();

            
            isCooldown = true;

            
            cooldownDuration = Random.Range(5f, 12f);
            StartCoroutine(ResetCooldown(cooldownDuration));
        }
    }

    public void TimeSystem()
    {
        time -= Time.deltaTime;
        decimalDigit = Mathf.FloorToInt(time);
        timerText.text = decimalDigit.ToString();

        GameOver();
    }

    public void GameOver()
    {
        if (time<=0)
        {
            losePanel.SetActive(true);
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.Confined; // Fareyi ortaya sabitle
            Cursor.visible = true; // Farenin görünürlüðünü kapat

        }
    }

    public void GameOverPlayer()
    {
        losePanel.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.Confined; // Fareyi ortaya sabitle
        Cursor.visible = true; // Farenin görünürlüðünü kapat
    }

    public void WinGame()
    {   
        Time.timeScale = 0f;
        winPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined; // Fareyi ortaya sabitle
        Cursor.visible = true; // Farenin görünürlüðünü kapat
    }

    public void MotivasionSounds()
    {
        Motivasion.position = new Vector3(player.position.x,10f,player.position.z);
        int random = Random.Range(0, motiSounds.Length);
        motivasionAudio.PlayOneShot(motiSounds[random]);
    }

    private IEnumerator ResetCooldown(float duration)
    {
        yield return new WaitForSeconds(duration);

        // Cooldown süresi tamamlandý, bayraðý false yap
        isCooldown = false;
    }



    public void RestartButton()
    {
        SceneManager.LoadScene(2);
    }

    public void HomeButton()
    {
        SceneManager.LoadScene(0);
    }
}
