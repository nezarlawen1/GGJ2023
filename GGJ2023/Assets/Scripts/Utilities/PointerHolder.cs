using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PointerHolder : MonoBehaviour
{
    [SerializeField] private List<PortalPointer> _pointers = new List<PortalPointer>();
    [SerializeField] private PortalSpawnerManager _spawnerManager;
    private bool _activatedPortal;
    private bool _activatedWin;

    [SerializeField] private Image _portalsTextPopup, _winTextPopup;

    [SerializeField] private float _appearTime;
    [SerializeField] private float _appearTimeWin;
    private float _timer;
    private bool _donePopupPortal;
    private bool _donePopupWin;
    private Color _color;
    private Color _color2;

    // Start is called before the first frame update
    void Start()
    {
        _color = Color.white;
        _color2 = Color.white;
        _color.a = 0;
        _color2.a = 0;
        _portalsTextPopup.color = _color;
        _winTextPopup.color = _color2;
    }

    // Update is called once per frame
    void Update()
    {
        PortalsText();
        WinText();
    }

    private void PortalsText()
    {
        if (!_activatedPortal)
        {
            foreach (var item in _pointers)
            {
                if (item.IsSpawned)
                {
                    _activatedPortal = true;
                    break;
                }
            }
        }

        if (_activatedPortal && !_donePopupPortal)
        {
            _timer += Time.deltaTime;
            if (_timer <= _appearTime)
            {
                _color.a = Mathf.Lerp(_color.a, 1, 0.1f);
                _portalsTextPopup.color = _color;
            }
            else if (_timer > _appearTime && _timer <= _appearTime * 2)
            {
                _color.a = Mathf.Lerp(_color.a, 0, 0.1f);
                _portalsTextPopup.color = _color;
            }
            else
            {
                _color.a = 0;
                _portalsTextPopup.color = _color;
                _donePopupPortal = true;
            }
        }
    }    
    
    private void WinText()
    {
        if (!_activatedWin)
        {
            int counter = 0;
            for (int i = 0; i < _spawnerManager.spawnPortalList.Count; i++)
            {
                if (_spawnerManager.spawnPortalList[i].newPortal == null && _pointers[i].IsSpawned)
                {
                    counter++;
                }
            }

            if (counter == _spawnerManager.spawnPortalList.Count)
            {
                _timer = 0;
                _activatedWin = true;
            }
        }

        if (_activatedWin && !_donePopupWin)
        {
            _timer += Time.deltaTime;
            if (_timer <= _appearTimeWin)
            {
                _color2.a = Mathf.Lerp(_color2.a, 1, 0.1f);
                _winTextPopup.color = _color2;
            }
            else if (_timer > _appearTimeWin && _timer <= _appearTimeWin * 2)
            {
                _color2.a = Mathf.Lerp(_color2.a, 0, 0.1f);
                _winTextPopup.color = _color2;
            }
            else
            {
                _color2.a = 0;
                _winTextPopup.color = _color2;
                _donePopupWin = true;
                SceneManager.LoadScene(0);
            }
        }
    }
}
