using GM.Entities;

namespace GM.Staffs
{
    public class Staff : Entity
    {
        // TODO : 직원 스탯
        public bool IsWorking => _isWorking;

        protected bool _isWorking = false;
    }
}
