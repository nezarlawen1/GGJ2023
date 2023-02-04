using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField] private GameObject _healthIndicatorObj;
    [SerializeField] private Vector3 _startPos;
    [SerializeField] private Vector3 _endPos;

    private HealthHandler _healthHandler;

    [SerializeField] private Image _gameOverTextPopup;
    [SerializeField] private float _appearTime;
    private float _timer;
    private Color _color;
    private bool _gameOver;


    public void Setup(HealthHandler system)
    {
        _healthHandler = system;
        _healthHandler.OnHealthChanged += _healthHandler_OnHealthChanged;
        _startPos = _healthIndicatorObj.transform.position;
        _endPos = new Vector3(_startPos.x, _endPos.y, _startPos.z);
        RefreshHealthBar();

        _color = Color.white;
        _color.a = 0;
        _gameOverTextPopup.color = _color;
        _healthHandler.OnDeath += _healthHandler_OnDeath;
    }

    private void _healthHandler_OnDeath(object sender, System.EventArgs e)
    {
        GameOver();
    }

    private void _healthHandler_OnHealthChanged(object sender, System.EventArgs e)
    {
        RefreshHealthBar();
    }

    private void RefreshHealthBar()
    {
        _healthIndicatorObj.transform.position = new Vector3(_healthIndicatorObj.transform.position.x, _startPos.y - (_startPos.y - ((_startPos.y - _endPos.y) * _healthHandler.GetHealthPercent())), _healthIndicatorObj.transform.position.z);
    }

    private void GameOver()
    {
        _gameOver = true;
    }

    private void Update()
    {
        if (_gameOver)
        {
            _timer += Time.deltaTime;
            if (_timer <= _appearTime)
            {
                _color.a = Mathf.Lerp(_color.a, 1, 0.1f);
                _gameOverTextPopup.color = _color;
            }
            else if (_timer > _appearTime && _timer <= _appearTime * 2)
            {
                _color.a = Mathf.Lerp(_color.a, 0, 0.1f);
                _gameOverTextPopup.color = _color;
            }
            else
            {
                _color.a = 0;
                _gameOverTextPopup.color = _color;
                SceneManager.LoadScene("Game");
            }
        }

    }
}
