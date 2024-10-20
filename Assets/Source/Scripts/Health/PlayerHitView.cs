using System.Collections;
using UnityEngine;

public class PlayerHitView : MonoBehaviour
{
    [SerializeField] private Color _targetColor;
    [SerializeField] private SkinnedMeshRenderer _meshRenderer;

    private readonly float _fadeDuration = 0.1f;
    private HealthModel _healthModel;
    private Color _startColor;
    private Material _material;
    private Coroutine _activeColorProcess;

    private void Awake()
    {
        _material = _meshRenderer.material;
        _startColor = _material.color;
    }

    public void Init(HealthModel healthModel)
    {
        _healthModel = healthModel;
        _healthModel.DamageReceived += StartToFadeColor;
    }

    private void StartToFadeColor()
    {
        if(_activeColorProcess != null)
        {
            StopCoroutine(_activeColorProcess);
        }

        _activeColorProcess = StartCoroutine(FadeColor());
    }

    private IEnumerator FadeColor()
    {
        yield return LerpTo(_startColor, _targetColor, _fadeDuration);
        yield return LerpTo(_targetColor, _startColor, _fadeDuration);

        _activeColorProcess = null;
    }

    private IEnumerator LerpTo(Color startColor, Color targetColor, float duration)
    {
        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;

            float progress = timer / duration;
            _material.color = Color.Lerp(startColor, targetColor, progress);

            yield return null;
        }
    }
}