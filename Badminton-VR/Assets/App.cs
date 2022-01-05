using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class App : MonoBehaviour
{
    public EHandedness Handedness;

    public TMP_Dropdown HandednessDropdown;

    public GameObject Left_Racquet;
    public GameObject Right_Racquet;

    public XRRayInteractor Left_RayInteractor;
    public XRRayInteractor Right_RayInteractor;

    private void Awake()
    {
        HandednessDropdown.onValueChanged.AddListener(OnHandednessChanged);
        OnHandednessChanged((int) Handedness);
        HandednessDropdown.value = (int) Handedness;
    }

    private void OnHandednessChanged(int id)
    {
        Handedness = (EHandedness)id;

        switch (Handedness)
        {
            case EHandedness.LeftHand:
                Left_Racquet.SetActive(true);
                Left_RayInteractor.enabled = false;

                Right_Racquet.SetActive(false);
                Right_RayInteractor.enabled = true;
                break;

            case EHandedness.RightHand:
                Left_Racquet.SetActive(false);
                Left_RayInteractor.enabled = true;

                Right_Racquet.SetActive(true);
                Right_RayInteractor.enabled = false;
                break;
        }
    }

    public enum EHandedness
    {
        LeftHand,
        RightHand
    }
}
