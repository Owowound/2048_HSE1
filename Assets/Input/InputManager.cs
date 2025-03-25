using System.IO.Pipes;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class InputManager : MonoBehaviour
{
    [SerializeField]
        private GameManager gameManager;

    private float minSwipeDistance = 50f;
    private Vector2 swipeStartPos;
    private bool isSwiping;

    void Update()
    {
        KeyboardInput();
        MouseInput();
        TouchInput();
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

    private void MouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            swipeStartPos = Input.mousePosition;
            isSwiping = true;
        }

        if (Input.GetMouseButtonUp(0) && isSwiping)
        {
            Vector2 swipeEndPos = Input.mousePosition;
            ManageSwipe(swipeEndPos);
            isSwiping = false;
        }
    }

    private void TouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == UnityEngine.TouchPhase.Began)
            {
                swipeStartPos = touch.position;
                isSwiping = true;
            }
            else if (isSwiping)
            {
                Vector2 swipeEndPos = touch.position;
                ManageSwipe(swipeEndPos);
                isSwiping = false;
            }
        }
    }

    private void ManageSwipe(Vector2 swipeEndPos)
    {
        Vector2 direction = swipeEndPos - swipeStartPos;

        if (direction.magnitude < minSwipeDistance)
        {
            return;
        }

        Debug.Log("Обработка свайпа по экрану");

        direction.Normalize();
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if (direction.x < 0)
            {
                Debug.Log("Движение влево");
                gameManager.Move(Direction.Left);
            }
            else
            {
                Debug.Log("Движение вправо");
                gameManager.Move(Direction.Right);
            }
        }
        else
        {
            if (direction.y < 0)
            {
                Debug.Log("Движение вниз");
                gameManager.Move(Direction.Down);
            }
            else
            {
                Debug.Log("Движение вверх");
                gameManager.Move(Direction.Up);
            }
        }
    }
}
