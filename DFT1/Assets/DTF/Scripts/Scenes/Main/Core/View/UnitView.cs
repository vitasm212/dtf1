using UnityEngine;
using UnityEditor;

public class UnitView : MonoBehaviour
{
    [SerializeField] private GameObject _weapon;
    [SerializeField] private Transform _hp;

    private float _angleWeapon;

    private void Start()
    {
        ShowWeapon(false);
    }

    private void Update()
    {
        if (_weapon != null && _weapon.activeSelf)
        {
            _angleWeapon += Time.deltaTime;
            _weapon.transform.localRotation = Quaternion.Euler(0, 0, -_angleWeapon * 360);
        }
    }

    public void UpdateHp(float value)
    {
        if(_hp != null)
        {
            _hp.localScale = new Vector3(value, 1, 1);
        }
    }

    public void ShowWeapon(bool value)
    {
        _angleWeapon = 0;
        if (_weapon != null)
            _weapon.SetActive(value);
    }
}