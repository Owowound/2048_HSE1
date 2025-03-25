using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField]
        private GameManager gameManager;

    void Update()
    {
        KeyboardInput();
    }

    private void KeyboardInput()
    {
        var keyboard = Keyboard.current;
        if (keyboard == null)
        {
            return;
        }

        if (keyboard.sKey.wasPressedThisFrame || keyboard.downArrowKey.wasPressedThisFrame)
        {
            Debug.Log("Движение вниз");
            gameManager.Move(Direction.Down);
        }
        if (keyboard.aKey.wasPressedThisFrame || keyboard.leftArrowKey.wasPressedThisFrame)
        {
            Debug.Log("Движение влева");
            gameManager.Move(Direction.Left);
        }
        if (keyboard.dKey.wasPressedThisFrame || keyboard.rightArrowKey.wasPressedThisFrame)
        {
            Debug.Log("Движение вправо");
            gameManager.Move(Direction.Right);
        }
        if (keyboard.wKey.wasPressedThisFrame || keyboard.upArrowKey.wasPressedThisFrame)
        {
            Debug.Log("Движение вверх");
            gameManager.Move(Direction.Up);
        }
    }
}
