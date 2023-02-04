using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private Transform FirePoint;
    [SerializeField] private GameObject Projectile;
    [SerializeField] private Transform _target;
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private float _attackRange;
    [SerializeField] private LayerMask _layerToHit;
    [SerializeField] private float _projectileSpeed = 30f;

    [SerializeField] private float _cooldown = 1f;

    private float _timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        _timer = _cooldown;
    }
    private void Update()
    {
        /*if ((_inputManager.ShootPress || _inputManager.ShootHold) && _timer >= _cooldown)
        {
            Shoot();
        }
        else
        {
            _timer += Time.deltaTime;
        }*/
    }

    public void Shoot()
    {
        SetTargetDestination();
        InstantiateProjectile(FirePoint);
        _timer = 0;
        _target.position = Vector3.zero;
    }
    void SetTargetDestination()
    {
        RaycastHit hit;
        Ray ray = new Ray(_mainCamera.transform.position, _mainCamera.transform.forward);
        if (Physics.Raycast(ray, out hit, _attackRange, _layerToHit))
        {
            _target.position = hit.point;
        }
        else
        {
            _target.position = ray.GetPoint(_attackRange);
        }
        FirePoint.LookAt(_target);
    }
    void InstantiateProjectile(Transform firePoint)
    {
        var Proj = Instantiate(Projectile, firePoint.position, firePoint.rotation, firePoint) as GameObject;
        Proj.GetComponent<Rigidbody>().velocity = Proj.transform.forward * _projectileSpeed;
        Destroy(Proj, 0.2f);
    }
}
