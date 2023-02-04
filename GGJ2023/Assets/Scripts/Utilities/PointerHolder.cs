using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointerHolder : MonoBehaviour
{
    [SerializeField] private List<PortalPointer> _pointers = new List<PortalPointer>();
    private bool _activated;

    [SerializeField] private Image _textPopup;
    [SerializeField] private float _appearTime;
    private float _timer;
    private bool _donePopup;
    private Color _color;

    // Start is called before the first frame update
    void Start()
    {
        _color = _textPopup.color;
        _color.a = 0;
        _textPopup.color = _color;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_activated)
        {
            foreach (var item in _pointers)
            {
                if (item.IsSpawned)
                {
                    _activated = true;
                    break;
                }
            }
        }

        if (_activated && !_donePopup)
        {
            _timer += Time.deltaTime;
            if (_timer <= _appearTime)
            {
                _color.a = Mathf.Lerp(_color.a, 1, 0.1f);
                _textPopup.color = _color;
            }
            else if (_timer > _appearTime && _timer <= _appearTime * 2)
            {
                _color.a = Mathf.Lerp(_color.a, 0, 0.1f);
                _textPopup.color = _color;
            }
            else
            {
                _color.a = 0;
                _textPopup.color = _color;
                _donePopup = true;
            }
        }
    }
}
