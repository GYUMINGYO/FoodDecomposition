using GM.Managers;
using System.Collections;
using TMPro;
using UnityEngine;

namespace GM
{
    public class PreferencePanel : MonoBehaviour
    {
        [SerializeField] private string[] nameList;

        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI valueText;
        [SerializeField] private Transform gaugeFill;

        [SerializeField] private float duration = 0.5f;

        private void Start()
        {
            gaugeFill.localPosition = new Vector3(-203, 0, 0);
            nameText.text = nameList[0];
            valueText.text = $"0/{(int)ManagerHub.Instance.GetManager<PreferenceManager>().TargetPreference}";

            StartCoroutine(UpdateGaugeUI());
        }

        private IEnumerator UpdateGaugeUI()
        {
            float gaugePref = 0;
            PreferenceManager manager = ManagerHub.Instance.GetManager<PreferenceManager>();

            while (true)
            {
                yield return new WaitUntil(() => gaugePref != manager.TotalPreference);
                float totalPref = manager.TotalPreference;
                float targetPref = manager.TargetPreference;

                float currentTime = 0;
                while (currentTime < duration)
                {
                    currentTime += Time.deltaTime;
                    gaugePref = Mathf.Lerp(gaugePref, totalPref, currentTime);

                    if ((int)gaugePref >= (int)targetPref)
                    {
                        gaugePref = 0;
                        manager.PreferenceLevelUp();

                        valueText.text = $"{0}/{(int)manager.TargetPreference}";
                        gaugeFill.localPosition = new Vector3(-203, 0, 0);

                        if (manager.Level <= nameList.Length)
                            nameText.text = nameList[manager.Level - 1];

                        break;
                    }

                    valueText.text = $"{(int)gaugePref}/{(int)targetPref}";
                    gaugeFill.localPosition = new Vector3(-203 + (203 * gaugePref / targetPref), 0, 0);

                    yield return null;
                }
                valueText.text = $"{(int)totalPref}/{(int)targetPref}";
            }
        }
    }
}
