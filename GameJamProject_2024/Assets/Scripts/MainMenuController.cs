using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;


public class MainMenuController : MonoBehaviour
{
    private VisualElement UI;

    private Button playButton;
    private Button credisButton;
    private Button quitButton;

    private void Awake()
    {
        UI = GetComponent<UIDocument>().rootVisualElement;
    }

    private void OnEnable()
    {
        playButton = UI.Q<Button>("PlayButton");
        playButton.clicked += OnPlayButtonClicked;

        credisButton = UI.Q<Button>("CreditsButton");
        credisButton.clicked += OnCreditsButtonClicked;

        quitButton = UI.Q<Button>("QuitButton");
        quitButton.clicked += OnQuitButtonClicked;
    }
    // Carrega a cena do jogo
    private void OnPlayButtonClicked()
    {
        SceneManager.LoadScene(1);
    }

    private void OnCreditsButtonClicked()
    {
        // Abre o menu de Créditos
    }

    private void OnQuitButtonClicked()
    {
        Application.Quit();
    }
}
