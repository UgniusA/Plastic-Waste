using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{

    [SerializeField] GameObject StartButton;
    [SerializeField] GameObject PlasticWasteImage;

    public void Awake()
    {
        StartButton.SetActive(true);
        PlasticWasteImage.SetActive(true);
    }






}
