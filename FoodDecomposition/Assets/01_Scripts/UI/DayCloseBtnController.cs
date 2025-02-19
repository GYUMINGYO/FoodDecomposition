using GM.GameEventSystem;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GM
{
    public class DayCloseBtnController : MonoBehaviour
    {
        public List<Button> btnList;

        [SerializeField] private GameEventChannelSO gameCycleChannel;
        [SerializeField] private WarningPanel warningPanel;

        private bool isOpen = false;

        private void Awake()
        {
            gameCycleChannel.AddListener<RestourantCycleEvent>(HandleGameCycle);

            foreach (Button btn in btnList)
            {
                EventTrigger trigger = btn.gameObject.AddComponent<EventTrigger>();

                EventTrigger.Entry entry = new EventTrigger.Entry
                {
                    eventID = EventTriggerType.PointerClick
                };
                entry.callback.AddListener((eventData) => OnButtonClicked());
                trigger.triggers.Add(entry);
            }
        }

        private void OnButtonClicked()
        {
            if (isOpen)
            {
                warningPanel.ShowText("영업중에는 사용할 수 없는 기능입니다.");
            }
        }

        private void HandleGameCycle(RestourantCycleEvent evt)
        {
            isOpen = evt.open;

            foreach (Button btn in btnList)
            {
                btn.interactable = !evt.open;

                TextMeshProUGUI text = btn.GetComponentInChildren<TextMeshProUGUI>();
                if (text != null)
                {
                    Color color = text.color;
                    color.a = evt.open ? 0.5f : 1f;
                    text.color = color;
                }
            }
        }
    }
}
