using MySql.Data.MySqlClient;
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

        public void Add(Staff dir)
        {

            string name = dir.first_name;
            string last = dir.last_name;
            string email = dir.email;
            string arr = "INSERT INTO Staff (first_name,last_name,email) VALUES(@Name,@last,@email)";

            try
            {

                MySqlConnection conn = Database.getInstance().Connection;

                MySqlCommand cmd = new MySqlCommand(arr, conn);

                var parameter = cmd.CreateParameter();
                parameter.ParameterName = "Name";
                parameter.Value = name;
                cmd.Parameters.Add(parameter);

                var parameter1 = cmd.CreateParameter();
                parameter1.ParameterName = "last";
                parameter1.Value = last;
                cmd.Parameters.Add(parameter1);

                var parameter2 = cmd.CreateParameter();
                parameter2.ParameterName = "email";
                parameter2.Value = email;
                cmd.Parameters.Add(parameter2);

                cmd.ExecuteNonQuery();
                dir.staff_id = Database.getInstance().getLast();

                Global.LocalStorage.SaveStaff(dir);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }


        public Staff getStaffByID(int ID)
        {
            /*var results = from node in Global.LocalStorage.Actor_Accessor_Selector()
                          where node.ID == ID
                          select node;
                          */

            foreach (var staff in Global.LocalStorage.Staff_Accessor_Selector())
            {
                if (staff.staff_id == ID)
                    return staff;

            }
            return null;
        }
        public void RemoveStaff(int ID)
        {
            var staff = getStaffByID(ID);

            string arr = "DELETE FROM Staff WHERE staff_id = @ID";

            try
            {

                MySqlConnection conn = Database.getInstance().Connection;

                MySqlCommand cmd = new MySqlCommand(arr, conn);

                var parameter = cmd.CreateParameter();
                parameter.ParameterName = "ID";
                parameter.Value = ID;
                cmd.Parameters.Add(parameter);

                cmd.ExecuteNonQuery();

                Global.LocalStorage.RemoveCell(staff.CellID);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }
    }

}
