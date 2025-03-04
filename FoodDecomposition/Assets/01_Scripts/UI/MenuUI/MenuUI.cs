using GM.GameEventSystem;
using UnityEngine;

namespace GM
{
    public class MenuUI : MonoBehaviour
    {
        [SerializeField] private GameEventChannelSO openMenuUIEvent;
        [SerializeField] private MenuType menuType;

        protected bool isOpen = false;

        public virtual void Awake()
        {
            openMenuUIEvent.AddListener<OpneMenuEvent>(HandleMenuUIEvent);
        }

        private void HandleMenuUIEvent(OpneMenuEvent evt)
        {
            if (evt.type == menuType)
            {
                if (isOpen)
                    Close();
                else
                    Open();
            }
            else if (isOpen)
            {
                Close();
            }
        }

        public virtual void Open()
        { }

        public virtual void Close()
        { }
    }
}