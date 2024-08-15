using UnityEngine;
using UnityEngine.UI;

public class UpgradesShopTrigger : MonoBehaviour
{
    private const float TimeToOpenShop = 2f;

    [SerializeField] private Image _triggerArea;

    private UpgradesShop _upgradesShop;
    private bool _inTrigger;
    private float _timeInTrigger;

    private bool TimerIsReach => _timeInTrigger >= TimeToOpenShop;

    public void Init(UpgradesShop upgradesShop)
    {
        _upgradesShop = upgradesShop;
        RefreshAreaView();
    }

    private void Update()
    {
        if (_inTrigger == false || TimerIsReach == true)
            return;

        _timeInTrigger += Time.deltaTime;
        RefreshAreaView();

        if (TimerIsReach == true)
        {
            _upgradesShop.Show();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Player _))
        {
            _inTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Player _))
        {
            _inTrigger = false;
            _timeInTrigger = 0;
            RefreshAreaView();
        }
    }

    private void RefreshAreaView()
    {
        _triggerArea.fillAmount = _timeInTrigger / TimeToOpenShop;
    }
}
