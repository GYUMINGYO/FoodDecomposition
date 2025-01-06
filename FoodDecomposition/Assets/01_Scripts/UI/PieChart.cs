using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace GM
{
    public class PieChart : MonoBehaviour
    {
        // TODO : 이미지로 요리상태 표시
        // TODO : 파이 차트 만들기

        [SerializeField] private Image _pieChartImage;
        [SerializeField] private GameObject _pieVisual;

        private float _pieValue = 0f;
        private bool _isChartFilling = false;

        private void Start()
        {
            _pieVisual.SetActive(false);
            _pieChartImage.fillAmount = 0f;
        }

        public void SetPieChart(int pieCount)
        {
            _pieChartImage.fillAmount = 0f;
            // TODO : 이거 냉장고 애니메이션 추가하면 바꿔야 함
            _pieValue = 1f / pieCount;
            _pieVisual.SetActive(true);
        }

        public void StartFillPieChart(float duration)
        {
            if (_isChartFilling == true)
            {
                return;
            }

            _isChartFilling = true;
            StartCoroutine(FillPieChart(duration));
        }

        public void EndPieChart()
        {
            _pieVisual.SetActive(false);
            _pieChartImage.fillAmount = 0f;
        }

        private IEnumerator FillPieChart(float duration)
        {
            float time = 0f;
            float startValue = _pieChartImage.fillAmount;
            float endValue = _pieChartImage.fillAmount + _pieValue;

            while (time < duration)
            {
                _pieChartImage.fillAmount = Mathf.Lerp(startValue, endValue, time / duration);
                time += Time.deltaTime;
                yield return null;
            }

            _isChartFilling = false;
        }

        private void Update()
        {
            transform.LookAt(Camera.main.transform);
        }
    }
}
