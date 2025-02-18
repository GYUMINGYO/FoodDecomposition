using DG.Tweening;
using GM.GameEventSystem;
using UnityEngine;
using UnityEngine.UI;

namespace GM
{
    public class MenuPanel : MonoBehaviour
    {
        [SerializeField] private GameEventChannelSO openMenuUIEvent;

        [Header("Button")]
        [SerializeField] private Button menuBtn;
        [SerializeField] private Button[] menuBtns;

        [Header("Settings")]
        [SerializeField] private float duration = 0.3f;

        private bool isMenuOpen = false;
        private Vector2[] openPositions = { 
            new Vector2(0, 190), 
            new Vector2(0, 80), 
            new Vector2(0, -30), 
            new Vector2(0, -140), 
            new Vector2(0, -250) };
        private Vector2 closedPosition = new Vector2(0, -490);

        private void Awake()
        {
            menuBtn.onClick.AddListener(ToggleMenu);

            for (int i = 0; i < menuBtns.Length; i++)
            {
                int idx = i;
                menuBtns[i].onClick.AddListener(() => RaiseEvent((MenuType)idx));
            }
        }

        private void ToggleMenu()
        {
            for (int i = 0; i < menuBtns.Length; i++)
            {
                menuBtns[i].transform.DOLocalMoveY(isMenuOpen ? closedPosition.y : openPositions[i].y, duration);
            }

            isMenuOpen = !isMenuOpen;
        }

        private void RaiseEvent(MenuType type)
        {
            MenuUIEvents.OpneMenuEvent.type = type;
            openMenuUIEvent.RaiseEvent(MenuUIEvents.OpneMenuEvent);
        }

        private void OnDestroy()
        {
            menuBtn.onClick.RemoveListener(ToggleMenu);
        }
    }
}
