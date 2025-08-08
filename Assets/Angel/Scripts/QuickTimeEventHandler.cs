using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class QuickTimeEventHandler : MonoBehaviour
{
    [SerializeField] InputAction quickTimeEventAction;
    [SerializeField] List<GameObject> enemies;
    [SerializeField] AudioSource audioSrc;
    [SerializeField] List<AudioClip> clipList;

    private void OnEnable()
    {
        quickTimeEventAction = InputSystem.actions.FindAction("QuickTimeEvent");
        quickTimeEventAction.performed += OnQuickTimeEvent;

    }
    private void OnDisable()
    {
        quickTimeEventAction = InputSystem.actions.FindAction("QuickTimeEvent");
        quickTimeEventAction.performed -= OnQuickTimeEvent;
    }

    private void OnQuickTimeEvent(InputAction.CallbackContext context)
    {
        enemies = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));

        // Which control triggered this?
        var control = context.control;

        if (control is KeyControl keyControl)
        {
            if (keyControl.keyCode == Key.Digit1) 
            {
                Debug.Log("1 has been Pressed");
                LoopCheck(enemies, 1);
            }
            if (keyControl.keyCode == Key.Digit2) 
            {
                Debug.Log("2 has been Pressed");

                LoopCheck(enemies, 2);
            }
            if (keyControl.keyCode == Key.Digit3) 
            {
                Debug.Log("3 has been Pressed");

                LoopCheck(enemies, 3);
            }
            if (keyControl.keyCode == Key.Digit4) 
            {
                Debug.Log("4 has been Pressed");

                LoopCheck(enemies, 4);
            }
            if (keyControl.keyCode == Key.Digit5) 
            {
                Debug.Log("5 has been Pressed");

                LoopCheck(enemies, 5);
            }
            if (keyControl.keyCode == Key.Digit6) 
            {
                Debug.Log("6 has been Pressed");

                LoopCheck(enemies, 6);
            }
        }
    }
    void LoopCheck(List<GameObject> enemies, int num)
    {
        foreach (var e in enemies)
        {
            audioSrc.clip = clipList[num - 1];
            audioSrc.Play();
            e.GetComponent<EnemyManager>().CheckInput(num);
        }
    }
}
