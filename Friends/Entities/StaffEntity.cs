
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Trinity;
using Trinity.Storage;

namespace Friends.Entities
{

    public class StaffEntity : Entity
    {


        public StaffEntity(string tableName, string entityName) : base(tableName, entityName)
        {

        }

        public override void loadData()
        {
            Entity entity = (Entity)this;
            List<long> staffs = Database.getInstance().loadNodeType(entity);

            foreach (long staffId in staffs)
            {
                Staff dir = new Staff(staff_id: staffId);
                Global.LocalStorage.SaveStaff(dir);
            }
        }
    }
}
