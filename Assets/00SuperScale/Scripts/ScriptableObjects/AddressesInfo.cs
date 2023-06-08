using UnityEngine;

namespace SuperScale.Data
{
    [CreateAssetMenu(fileName = "AddressesInfo", menuName = "SuperScale/Data/Addresses Info")]
    public class AddressesInfo : AbstractInfo
    {
        [SerializeField] private string _badgeAdrsPrefix;
        [SerializeField] private string _characterAdrsPrefix;
        [SerializeField] private string _flagIdAdrsPrefix;
        [SerializeField] private string _dataAdrsPrefix;

        public string BadgeAdrsPrefix => _badgeAdrsPrefix;
        public string CharacterAdrsPrefix => _characterAdrsPrefix;
        public string FlagAdrsPrefix => _flagIdAdrsPrefix;
        public string DataAdrsPrefix => _dataAdrsPrefix;
    }
}
