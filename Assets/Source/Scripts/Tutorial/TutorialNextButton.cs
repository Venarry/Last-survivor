using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class TutorialNextButton : MonoBehaviour, ITutorialAction
{
    private Button _button;

    public event Action<ITutorialAction> Happened;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnClick); 
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnClick);
    }

    private void OnClick()
    {
        Happened?.Invoke(this);
    }
}
