using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        Button buttonStart = root.Q<Button>("Start");
        Button buttonStop = root.Q<Button>("Stop");
        Button buttonEsc = root.Q<Button>("Esc");

        buttonStart.clicked += () => StartNewGame();
    }

    private void StartNewGame()
    {
        ScenesManager.instance.LoadScene(ScenesManager.Scene.Level01);
    }
}
