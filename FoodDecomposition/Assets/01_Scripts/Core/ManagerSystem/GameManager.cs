using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace GM.Managers
{
    public class GameManager : MonoBehaviour, IManagerable
    {
        [SerializeField] private Slider timeGauge;

        // TODO : 이벤트를 게임이벤트로 처리해도 괜찮을 듯
        public event Action OnRestaurantOpen;
        public event Action OnRestaurantClose;

        // Playe Tiem
        // TODO : PlayTiem 측정기 만들기
        private double _dayTime = 120;
        private double _currentDayTime = 0;

        private bool _isDayTimer;
        private bool _isStopDayTimer;

        public void Initialized()
        {
            _currentDayTime = 0;
            _isDayTimer = false;
            _isStopDayTimer = false;
        }

        public void Clear()
        {
        }

        private void Start()
        {
            StartDayTimer();
        }

        private void Update()
        {
            //! Test
            if (Input.GetKeyDown(KeyCode.H))
            {
                _isStopDayTimer = !_isStopDayTimer;
            }
        }

        public double GetDayTime() => _currentDayTime;
        public void SetDayTime()
        {
            // TODO : DayTime 세팅 / 세팅을 해야 할지 의문?
        }

        public void StopTimer() => _isStopDayTimer = true;
        public void PlayTimer() => _isStopDayTimer = false;

        public void StartDayTimer()
        {
            if (_isDayTimer == true) return;
            StartCoroutine(DayTimer());
        }

        private IEnumerator DayTimer()
        {
            _isDayTimer = true;
            _currentDayTime = 0;
            OnRestaurantOpen?.Invoke();

            while (_currentDayTime <= _dayTime)
            {
                yield return null;
                if (_isStopDayTimer == true) continue;

                _currentDayTime += Time.deltaTime;
                timeGauge.value = (float)(_currentDayTime / _dayTime);
            }

            OnRestaurantClose?.Invoke();
            _isDayTimer = false;
        }
    }
}
