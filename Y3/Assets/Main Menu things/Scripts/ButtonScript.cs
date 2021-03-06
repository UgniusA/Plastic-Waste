using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class ButtonScript : MonoBehaviour
{

    [SerializeField] GameObject StartButton;
    [SerializeField] GameObject PlasticWasteImage;
    [SerializeField] GameObject MainCamera;
    [SerializeField] GameObject CameraMovement;
    [SerializeField] GameObject Buttons;
    [SerializeField] GameObject play;
    [SerializeField] GameObject settings;
    [SerializeField] GameObject quit;
    [SerializeField] GameObject SettingsMenu;
    [SerializeField] GameObject LastCameraMovement;
    [SerializeField] GameObject Resolution;
    [SerializeField] GameObject Audio;
    [SerializeField] GameObject Graphics;
    [SerializeField] GameObject FullScreen;
    [SerializeField] GameObject Back;
    [SerializeField] GameObject Camera;
    [SerializeField] GameObject postprocessing;


    public void Awake()
    {
        StartButton.SetActive(true);
        PlasticWasteImage.SetActive(true);
        MainCamera.SetActive(true);
        Buttons.SetActive(false);
    }

    public void nMouseDrag()
    {
        StartCoroutine(ClicktoStartButton());
    }

    public IEnumerator ClicktoStartButton()
    {
        StartButton.SetActive(false);
        PlasticWasteImage.SetActive(false);
        MainCamera.SetActive(false);
        CameraMovement.SetActive(true);
        yield return new WaitForSeconds(2f);
        Buttons.SetActive(true);
    }

    public void ButtonsClicked()
    {
        Buttons.SetActive(false);
        StartCoroutine(gameSettings());
    }

    public IEnumerator gameSettings()
    {
        yield return new WaitForSeconds(0.5f);
        play.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        settings.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        quit.SetActive(true);
    }


    public void settingMenu()
    {
        settings.SetActive(false);
        play.SetActive(false);
        quit.SetActive(false);
        SettingsMenu.SetActive(true);
    }

    public void startGame()
    {
        StartCoroutine(StartGameAnimation());
    }

    public IEnumerator StartGameAnimation()
    {
        MainCamera.SetActive(false);
        CameraMovement.SetActive(false);
        LastCameraMovement.SetActive(true);
        settings.SetActive(false);
        play.SetActive(false);
        quit.SetActive(false);
        SettingsMenu.SetActive(false);
        yield return new WaitForSeconds(9.5f);
        Camera.SetActive(false);
        postprocessing.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ResolutionButton()
    {
        Resolution.SetActive(true);
    }

    public void VolumeButton()
    {
        Audio.SetActive(true);
    }

    public void GraphicsButton()
    {
        Graphics.SetActive(true);
    }
    public void doExitGame()
    {
        Debug.Log("quit");
        Application.Quit();
    }

    public void FullScreenButton()
    {
        FullScreen.SetActive(true);
    }

    public void BackButton()
    {
        StartCoroutine(BackButtons());
    }

    public IEnumerator BackButtons()
    {
        SettingsMenu.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        play.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        settings.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        quit.SetActive(true);
    }
}
