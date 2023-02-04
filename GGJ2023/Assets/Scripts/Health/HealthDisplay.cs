using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField] private GameObject _healthIndicatorObj;
    [SerializeField] private Vector3 _startPos;
    [SerializeField] private Vector3 _endPos;

    private HealthHandler _healthHandler;

    public void Setup(HealthHandler system)
    {
        _healthHandler = system;
        _healthHandler.OnHealthChanged += _healthHandler_OnHealthChanged;
        RefreshHealthBar();
    }

    private void _healthHandler_OnHealthChanged(object sender, System.EventArgs e)
    {
        RefreshHealthBar();
    }

    private void RefreshHealthBar()
    {
        _healthIndicatorObj.transform.position = new Vector3(_healthIndicatorObj.transform.position.x, _startPos.y - ((_startPos.y - _endPos.y) * _healthHandler.GetHealthPercent()), _healthIndicatorObj.transform.position.z);
    }
}
