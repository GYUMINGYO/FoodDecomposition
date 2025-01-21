using UnityEngine;

namespace GM
{
    public class TestScript : MonoBehaviour
    {
        [SerializeField] private GameObject linePrefab;
        [SerializeField] private GameObject recipeCardPrefab;

        private RectTransform content;
        private GameObject lineObj;
        int cnt = 3;

        private void Awake()
        {
            content = GetComponent<RectTransform>();
        }

        private void Start()
        {
            lineObj = Instantiate(linePrefab, content);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.O))
            {
                if(cnt == 0)
                {
                    lineObj = Instantiate(linePrefab, content);
                    cnt = 3;
                }

                Instantiate(recipeCardPrefab, lineObj.transform);
                cnt--;
            }
        }
    }
}
