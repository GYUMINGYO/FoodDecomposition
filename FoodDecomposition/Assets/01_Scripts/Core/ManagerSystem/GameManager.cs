using GM.GameEventSystem;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace GM.Managers
{
    public class GameManager : MonoBehaviour, IManagerable
    {
        [SerializeField] private GameEventChannelSO _gameCycleChannel;
        [SerializeField] private Slider timeGauge;
        [SerializeField] private Transform dayLight;

        private double _dayTime = 120;
        private double _currentDayTime = 0;

        private bool _isDayTimer;
        private bool _isStopDayTimer;
        private bool _isStopCustomer;

        public void Initialized()
        {
            _currentDayTime = 0;
            _isDayTimer = false;
            _isStopDayTimer = false;
            _isStopCustomer = false;
        }

        public void Clear()
        {
        }

        private void Start()
        {
            StartDayTimer();
        }

        public double GetDayTime() => _currentDayTime;

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
            _isStopCustomer = false;
            _currentDayTime = 0;
            GameCycleEvents.RestourantCycleEvent.open = true;
            _gameCycleChannel.RaiseEvent(GameCycleEvents.RestourantCycleEvent);

            while (_currentDayTime <= _dayTime)
            {
                yield return null;
                if (_isStopDayTimer == true) continue;

                _currentDayTime += Time.deltaTime;

                float duration = (float)(_currentDayTime / _dayTime);
                timeGauge.value = duration;
                dayLight.localRotation = Quaternion.Euler(Mathf.Lerp(-155, -375, duration), -30, 0);

                if (duration > 0.8f && !_isStopCustomer)
                {
                    // 마감시간(손님 생성 멈춤)
                    _gameCycleChannel.RaiseEvent(GameCycleEvents.RestourantClosingTimeEvent);
                    _isStopCustomer = true;
                }
            }

            yield return new WaitUntil(() => ManagerHub.Instance.GetManager<DataManager>().CustomerCnt == 0);
            // Close(실질적 레스토랑 영업 종료)
            GameCycleEvents.RestourantCycleEvent.open = false;
            _gameCycleChannel.RaiseEvent(GameCycleEvents.RestourantCycleEvent);
            _isDayTimer = false;
        }
    }
}
