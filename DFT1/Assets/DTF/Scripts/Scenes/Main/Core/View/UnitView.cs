using UnityEngine;
using UnityEditor;

namespace DTF
{
    public class UnitView : MonoBehaviour
    {
        [SerializeField] private Transform _body;
        [SerializeField] private GameObject _weapon;
        [SerializeField] private GameObject[] _powerStar;
        [SerializeField] private Transform _hp;

        private float _angleWeapon;

        private void Awake()
        {
            ShowWeapon(false);
            SetPowerStar(0);
        }

        public void SetDirection(CardDirection direction)
        {
            if (_body != null)
            {
                if (direction == CardDirection.Left)
                    _body.localScale = new Vector3(1, 1, 1);
                else
                    _body.localScale = new Vector3(-1, 1, 1);
            }
        }

        public void SetPowerStar(int powerPlus)
        {
            if (_powerStar != null)
            {
                for (int i = 0; i < _powerStar.Length; i++)
                {
                    _powerStar[i].SetActive(i < powerPlus);
                }
            }
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
            if (_hp != null)
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
}