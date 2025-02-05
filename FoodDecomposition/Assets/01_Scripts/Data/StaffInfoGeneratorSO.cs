using UnityEngine;

namespace GM.Data
{
    public enum LanguageType
    {
        Korean,
        English
    }

    [CreateAssetMenu(fileName = "StaffInfoGenerator", menuName = "SO/Data/StaffInfoGenerator")]
    public class StaffInfoGeneratorSO : ScriptableObject
    {
        public LanguageType LanguageType;
        public NameDataSO NameContainer;
        public PortraitDataSO PortraitContainer;

        public StaffInfo GetRandomStaffInfo()
        {
            StaffInfo staffInfo = new StaffInfo
            {
                Name = $"{NameContainer.GetRandomFirstName()}{NameContainer.GetRandomLastName()}",
                Portrait = PortraitContainer.GetRandomPortrait()
            };

            if (LanguageType == LanguageType.Korean)
            {
                return staffInfo;
            }
            else if (LanguageType == LanguageType.English)
            {
                staffInfo.Name = $"{NameContainer.GetRandomLastName()} {NameContainer.GetRandomFirstName()}";
            }

            return staffInfo;
        }
    }
}
