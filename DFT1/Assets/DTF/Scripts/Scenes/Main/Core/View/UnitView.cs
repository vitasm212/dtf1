using UnityEngine;
using UnityEditor;

public class UnitView : MonoBehaviour
{
    [SerializeField] private GameObject _weapon;
    private float _angleWeapon;

    private void Update()
    {
        if (_weapon != null && _weapon.activeSelf)
        {
            _angleWeapon += Time.deltaTime;
            _weapon.transform.localRotation = Quaternion.Euler(0, 0, -_angleWeapon * 360);
        }
    }

    public void ShowWeapon(bool value)
    {
        _angleWeapon = 0;
        if (_weapon != null)
            _weapon.SetActive(value);
    }
}