using UnityEngine;

namespace GM.Entities
{
    public class UnitDescriptionUI : MonoBehaviour, IEntityComponent
    {
        // TODO : 폐기 예정
        private Entity _entity;

        [SerializeField] private Canvas _uiDescriptionCanvas;
        [SerializeField] private Vector2 _offSet;

        public void Initialize(Entity entity)
        {
            _entity = entity;
            _uiDescriptionCanvas.enabled = false;
        }

        public void ActiveUI(bool isActive)
        {
            _uiDescriptionCanvas.enabled = isActive;
        }

        private void LateUpdate()
        {
            transform.LookAt(Camera.main.transform);

            /* Vector3 camPos = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z - 10);
            transform.position = camPos;
            transform.rotation = Camera.main.transform.rotation;

            Vector3 entityPos = Vector3.zero;
            entityPos.x = _entity.transform.position.x - (_offSet.x * -1);
            entityPos.y = _entity.transform.position.y - _offSet.y;
            entityPos.z = transform.position.z;

            transform.localPosition = entityPos; */
        }
    }
}
