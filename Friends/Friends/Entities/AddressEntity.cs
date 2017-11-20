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

    public class AddressEntity : Entity
    {
        

        public AddressEntity(string tableName, string entityName) : base(tableName, entityName)
        {

        }

        public override void loadData()
        {
            Entity entity = (Entity)this;
            List<long> addresses = Database.getInstance().loadNodeType(entity);

            foreach (long addressId in addresses)
            {
              
                Address mov = new Address(address_id: addressId, stores : Database.getInstance().findStoresByAddress(address_id));
                Global.LocalStorage.SaveAddress(mov);

            }


        }

        public void Add(Address mov)
        {

            string address = mov.address;
            string district= mov.district;
            string postal_code = mov.postal_code;
            string phone = mov.phone;

            string arr = "INSERT INTO Address (address, district, postal_code, phone) VALUES(@address, @district, @postal_code, @phone)";
            
            try
            {

                MySqlConnection conn = Database.getInstance().Connection;

                MySqlCommand cmd = new MySqlCommand(arr, conn);

                var parameter = cmd.CreateParameter();
                parameter.ParameterName = "address";
                parameter.Value = address;
                cmd.Parameters.Add(parameter);

                var parameter2 = cmd.CreateParameter();
                parameter2.ParameterName = "district";
                parameter2.Value = district;
                cmd.Parameters.Add(parameter2);

                var parameter3 = cmd.CreateParameter();
                parameter3.ParameterName = "postal_code";
                parameter3.Value = postal_code;
                cmd.Parameters.Add(parameter3);

                var parameter4 = cmd.CreateParameter();
                parameter4.ParameterName = "phone";
                parameter4.Value = phone;
                cmd.Parameters.Add(parameter4);

               


                cmd.ExecuteNonQuery();
                mov.address_id= Database.getInstance().getLast();

                Global.LocalStorage.SaveAddress(mov);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }

        public Address getAddressByID(int ID)
        {
            /*var results = from node in Global.LocalStorage.Actor_Accessor_Selector()
                          where node.ID == ID
                          select node;
                          */

            foreach (var mov in Global.LocalStorage.Address_Accessor_Selector())
            {
                if (mov.ID == ID)
                    return mov;

            }
            return null;
        }
        public void RemoveAddress(int ID)
        {
            var Address = getAddressByID(ID);
          
            string arr = "DELETE FROM Address WHERE id = @ID";

            try
            {

                MySqlConnection conn = Database.getInstance().Connection;

                MySqlCommand cmd = new MySqlCommand(arr, conn);

                var parameter = cmd.CreateParameter();
                parameter.ParameterName = "ID";
                parameter.Value = ID;
                cmd.Parameters.Add(parameter);

                cmd.ExecuteNonQuery();

                Global.LocalStorage.RemoveCell(Address.CellID);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }
    }

}
