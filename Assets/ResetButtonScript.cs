using UnityEngine;
using UnityEngine.UI;
using System;


public class ResetGameButtonScript : MonoBehaviour
{
    public Button button;
    public event Action ButtonIsPressed;
    public GameManager gameManager;
    public GameObject endMenu;

    private void Start()
    {
        button.onClick.AddListener(ClickButton);
        ButtonIsPressed += LoadNewGame;
    }

    private void ClickButton()
    {
        Debug.Log("Поле пересобрано");
        ButtonIsPressed?.Invoke();
    }

    public void LoadNewGame()
    {
        endMenu.SetActive(false);
        gameManager.StartResetGame();
    }
}
