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
    private Button backButton;


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

        backButton = UI.Q<Button>("BackButton");
        backButton.clicked += OnBackButtonClicked;
    }
    // Carrega a cena do jogo
    private void OnPlayButtonClicked()
    {
        SceneManager.LoadScene("SampleScene");
    }
    // Abre a tela de créditos
    private void OnCreditsButtonClicked()
    {
        // Abre o menu de Créditos
        //UI.Q<VisualElement>("MainMenu").visible = false;
        //UI.Q<VisualElement>("CreditsMenu").visible = true;
        UI.Q<VisualElement>("MainMenu").style.display = DisplayStyle.None;
        UI.Q<VisualElement>("CreditsMenu").style.display = DisplayStyle.Flex;
    }
    // Sai do jogo
    private void OnQuitButtonClicked()
    {
        Debug.Log("Sair do Jogo");
        Application.Quit();
    }
    // Fecha a tela de créditos
    private void OnBackButtonClicked()
    {
        UI.Q<VisualElement>("CreditsMenu").style.display = DisplayStyle.None;
        UI.Q<VisualElement>("MainMenu").style.display = DisplayStyle.Flex;        
    }
}
