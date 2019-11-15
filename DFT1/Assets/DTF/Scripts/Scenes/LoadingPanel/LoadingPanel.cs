using System;
using UnityEngine;
using UnityEngine.UI;

namespace DTF.Scenes.Loading
{
    public class LoadingPanel : MonoBehaviour
    {        
        public Action _loadingScreenHasBeenShown;
        public Action _loadingScreenHasBeenHidden;
        public Action<Action> _sceneReadyToShow;

        [SerializeField] private Text _text;
        private string _animateText;
        private float _animateTime = 0;
        private const float animationInterval = -0.2f;

        public bool IsActive { get; private set; }

        private void Update()
        {
            _animateTime -= Time.deltaTime;
            if (_animateTime < animationInterval)
            {
                _animateTime = 0;
                _animateText += ".";
                if (_animateText.Length > 5)
                    _animateText = "";
                _text.text = $"{_animateText}Loading{_animateText}";
            }
        }

        public void Show(bool value, Action callback = null)
        {
            IsActive = value;

            gameObject.SetActive(value);
            _text.gameObject.SetActive(value);

            if (!value)
            {
                _loadingScreenHasBeenHidden?.Invoke();
            }
            else
            {
                _loadingScreenHasBeenShown?.Invoke();
            }
            callback?.Invoke();
        }

        private void OnEnable()
        {
            _sceneReadyToShow?.Invoke(OnSceneReadyToShow);
        }

        private void OnDisable()
        {
            _sceneReadyToShow?.Invoke(OnSceneReadyToShow);
        }

        private void OnSceneReadyToShow()
        {
           // Show(false);
        }
    }
}