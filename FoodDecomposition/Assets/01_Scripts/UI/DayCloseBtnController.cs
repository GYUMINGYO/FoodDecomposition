using GM.GameEventSystem;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GM
{
    public class DayCloseBtnController : MonoBehaviour
    {
        public List<Button> btnList;
        [SerializeField] private GameEventChannelSO gameCycleChannel;

        private void Awake()
        {
            gameCycleChannel.AddListener<RestourantCycleEvent>(HandleGameCycle);
        }

        private void HandleGameCycle(RestourantCycleEvent evt)
        {
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
