﻿using MySql.Data.MySqlClient;
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

    public class CustomerEntity : Entity
    {
        

        public CustomerEntity(string tableName, string entityName) : base(tableName, entityName)
        {

        }

        public override void loadData()
        {
            Entity entity = (Entity)this;
            List<long> customers = Database.getInstance().loadNodeType(entity);

            foreach (long customerId in customers)
            {
               
                Customer mov = new Customer(customer_id: customerId, rents: Database.getInstance().findRentByCustomer(customerId));
                Global.LocalStorage.SaveCustomer(mov);

            }


        }

        public void Add(Customer mov)
        {

            long store_id = mov.store_id;
            string name = mov.first_name;
            string last = mov.last_name;
            string email = mov.email;

            

            string arr = "INSERT INTO Customer ( store_id, first_name,last_name,email) VALUES(@store_id, @name, @last_name, @email)";
            
            try
            {

                MySqlConnection conn = Database.getInstance().Connection;

                MySqlCommand cmd = new MySqlCommand(arr, conn);

                var parameter = cmd.CreateParameter();
                parameter.ParameterName = "store_id";
                parameter.Value = address_id;
                cmd.Parameters.Add(parameter);

                var parameter2 = cmd.CreateParameter();
                parameter2.ParameterName = "name";
                parameter2.Value = name;
                cmd.Parameters.Add(parameter2);

                var parameter3 = cmd.CreateParameter();
                parameter3.ParameterName = "last_name";
                parameter3.Value = last_name;
                cmd.Parameters.Add(parameter3);

                var parameter3 = cmd.CreateParameter();
                parameter3.ParameterName = "email";
                parameter3.Value = email;
                cmd.Parameters.Add(parameter3);



                cmd.ExecuteNonQuery();
                mov.Customer_id = Database.getInstance().getLast();

                Global.LocalStorage.SaveCustomer(mov);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }

        public Customer getCustomerByID(int ID)
        {
            /*var results = from node in Global.LocalStorage.Actor_Accessor_Selector()
                          where node.ID == ID
                          select node;
                          */

            foreach (var mov in Global.LocalStorage.Customer_Accessor_Selector())
            {
                if (mov.ID == ID)
                    return mov;

            }
            return null;
        }
        public void RemoveCustomer(int ID)
        {
            var Customer = getCustomerByID(ID);
          
            string arr = "DELETE FROM Customer WHERE id = @ID";

            try
            {

                MySqlConnection conn = Database.getInstance().Connection;

                MySqlCommand cmd = new MySqlCommand(arr, conn);

                var parameter = cmd.CreateParameter();
                parameter.ParameterName = "ID";
                parameter.Value = ID;
                cmd.Parameters.Add(parameter);

                cmd.ExecuteNonQuery();

                Global.LocalStorage.RemoveCell(Customer.CellID);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }
    }

}
