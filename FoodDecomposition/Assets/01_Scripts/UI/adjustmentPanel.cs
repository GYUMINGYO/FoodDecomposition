using DG.Tweening;
using GM.GameEventSystem;
using GM.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GM
{
    public class adjustmentPanel : MonoBehaviour
    {
        [SerializeField] private GameEventChannelSO gameCycleChannel;
        [SerializeField] private CanvasGroup group;

        [Header("Text")]
        [SerializeField] private TextMeshProUGUI dayText;
        [SerializeField] private TextMeshProUGUI revanueText;
        [SerializeField] private TextMeshProUGUI materialCostText;
        [SerializeField] private TextMeshProUGUI personalCostText;
        [SerializeField] private TextMeshProUGUI totalProfitText;

        [Header("Color")]
        [SerializeField] private Color plusColor;
        [SerializeField] private Color minusColor;

        [Header("Button")]
        [SerializeField] private Transform nextDayBtnOnCloseTrm;

        private EventSystem eventSystem;

        private int dayCnt = 0;
        private bool isOpen = false;

        private void Awake()
        {
            eventSystem = EventSystem.current;

            gameCycleChannel.AddListener<RestourantCycleEvent>(HandleEndDay);
        }

        private void HandleEndDay(RestourantCycleEvent evt)
        {
            if (evt.open) return;

            dayCnt++;
            Open();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && isOpen)
            {
                if (!eventSystem.IsPointerOverGameObject())
                {
                    Close();
                }
            }
        }

        private void Open()
        {
            isOpen = true;

            DataManager dataManager = ManagerHub.Instance.GetManager<DataManager>();
            {//test
                float personalCost = ManagerHub.Instance.GetManager<StaffManager>().GetStaffCount() * 10;
                dataManager.AddPersonalCost(personalCost);
            }
            float totalProfit = dataManager.DayMoney - dataManager.MaterialCost - dataManager.PersonalCost;
            dayText.text = $"{dayCnt}일차";
            revanueText.text = $"{dataManager.DayMoney}";
            materialCostText.text = $"{dataManager.MaterialCost}";
            personalCostText.text = $"{dataManager.PersonalCost}";
            totalProfitText.text = $"{totalProfit}";
            totalProfitText.color = totalProfit > 0 ? plusColor : minusColor;
            dataManager.DailyCostsClear();

            group.alpha = 1;
            transform.DOLocalMoveY(0, 0.5f)
                .OnComplete(() => group.blocksRaycasts = true);
        }

        public void Close()
        {
            isOpen = false;

            transform.DOLocalMoveY(-1000, 0.5f)
                .OnComplete(() =>
                {
                    group.alpha = 0;
                    group.blocksRaycasts = false;
                });
            nextDayBtnOnCloseTrm.DOLocalMoveX(670, 0.3f);
        }

        public void NextDay()
        {
            nextDayBtnOnCloseTrm.DOLocalMoveX(910, 0.3f);
            ManagerHub.Instance.GetManager<GameManager>().RestourantOpen();
        }
    }
}
