using System.Collections;
using GM.GameEventSystem;
using UnityEngine;
using UnityEngine.UI;

namespace GM.Managers
{
    public class GameManager : MonoBehaviour, IManagerable
    {
        [SerializeField] private GameEventChannelSO _gameCycleChannel;
        [SerializeField] private Slider timeGauge;

        // Playe Tiem
        // TODO : PlayTiem 측정기 만들기
        private double _dayTime = 15;
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
            GameCycleEvents.RestourantStartEvent.open = true;
            _gameCycleChannel.RaiseEvent(GameCycleEvents.RestourantStartEvent);

            while (_currentDayTime <= _dayTime)
            {
                yield return null;
                if (_isStopDayTimer == true) continue;

                _currentDayTime += Time.deltaTime;
                timeGauge.value = (float)(_currentDayTime / _dayTime);
            }

            // Close
            GameCycleEvents.RestourantStartEvent.open = false;
            _gameCycleChannel.RaiseEvent(GameCycleEvents.RestourantStartEvent);
            _isDayTimer = false;
        }
    }
}
