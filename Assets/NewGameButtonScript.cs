using UnityEngine;
using UnityEngine.UI;
using System;


public class NewGameButtonScript : MonoBehaviour
{
    public Button button;
    public event Action ButtonIsPressed;
    public GameManager gameManager;

    private void Start()
    {
        button.onClick.AddListener(ClickButton);
        ButtonIsPressed += LoadNewGame;
    }

    private void ClickButton()
    {
        Debug.Log("Начата новая игра");
        ButtonIsPressed?.Invoke();
    }

    public void LoadNewGame()
    {
        gameManager.StartNewGame();
    }
}
