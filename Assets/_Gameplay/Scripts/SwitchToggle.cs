using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchToggle : MonoBehaviour
{
    [SerializeField] RectTransform uiHandle;

    Toggle toggle;

    Vector2 handlePosition;

    private void Awake()
    {
        toggle = GetComponent<Toggle>();
        handlePosition = uiHandle.anchoredPosition;
        toggle.onValueChanged.AddListener(OnSwitch);
        if (toggle.isOn)
        {
            OnSwitch(true);
        }
    }

    public void OnSwitch(bool on)
    {
        uiHandle.anchoredPosition = on ? handlePosition * -1f : handlePosition;

    }

    private void OnDestroy()
    {
        toggle.onValueChanged.AddListener(OnSwitch);
    }
}
