using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    [SerializeField] private Button restartButton;

    public GameObject GameOverPanel;
    public int GameOverCount = 0;

    private void Awake() {
        GameOverPanel.SetActive(false);

        restartButton.onClick.AddListener(() => {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        });
    }

    public void gameOver() {
            GameOverPanel.SetActive(true);
    }

}
