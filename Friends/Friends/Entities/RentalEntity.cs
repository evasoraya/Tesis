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

    public class RentalEntity : Entity
    {
        

        public RentalEntity(string tableName, string entityName) : base(tableName, entityName)
        {

        }

        public override void loadData()
        {
            Entity entity = (Entity)this;
            List<long> rentals = Database.getInstance().loadNodeType(entity);

            foreach (long rentalId in rentals)
            {
                List<long> list = Database.getInstance().findRentalsByName(rentalId);
                Rental mov = new Rental(rental_id: rentalId, item_id: list[0], customer_id: list[1], staff_id[2]);
                Global.LocalStorage.SaveRental(mov);

            }


        }

        public void Add(Rental mov)
        {

            string rental_date = mov.rental_date;
            long item = mov.item_id;
            long customer = mov.customer_id;
            string return_date = mov.return_date;
            long staff = mov.staff_id;

            string arr = "INSERT INTO Rental (rental_date, item, customer, return_date, staff) VALUES(@rental_date, @item, @customer, @return_date, @staff)";
            
            try
            {

                MySqlConnection conn = Database.getInstance().Connection;

                MySqlCommand cmd = new MySqlCommand(arr, conn);

                var parameter = cmd.CreateParameter();
                parameter.ParameterName = "rental_date";
                parameter.Value = rental_date;
                cmd.Parameters.Add(parameter);

                var parameter2 = cmd.CreateParameter();
                parameter2.ParameterName = "item";
                parameter2.Value = long.Parse(item);
                cmd.Parameters.Add(parameter2);

                var parameter3 = cmd.CreateParameter();
                parameter3.ParameterName = "customer";
                parameter3.Value = long.Parse(customer);
                cmd.Parameters.Add(parameter3);

                var parameter4 = cmd.CreateParameter();
                parameter4.ParameterName = "return_date";
                parameter4.Value = return_date;
                cmd.Parameters.Add(parameter4);

                var parameter5 = cmd.CreateParameter();
                parameter5.ParameterName = "staff";
                parameter5.Value = long.Parse(staff);
                cmd.Parameters.Add(parameter5);


                cmd.ExecuteNonQuery();
                mov.rental_id= Database.getInstance().getLast();

                Global.LocalStorage.SaveRental(mov);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }

        public Rental getRentalByID(int ID)
        {
            /*var results = from node in Global.LocalStorage.Actor_Accessor_Selector()
                          where node.ID == ID
                          select node;
                          */

            foreach (var mov in Global.LocalStorage.Rental_Accessor_Selector())
            {
                if (mov.ID == ID)
                    return mov;

            }
            return null;
        }
        public void RemoveRental(int ID)
        {
            var Rental = getRentalByID(ID);
          
            string arr = "DELETE FROM Rental WHERE id = @ID";

            try
            {

                MySqlConnection conn = Database.getInstance().Connection;

                MySqlCommand cmd = new MySqlCommand(arr, conn);

                var parameter = cmd.CreateParameter();
                parameter.ParameterName = "ID";
                parameter.Value = ID;
                cmd.Parameters.Add(parameter);

                cmd.ExecuteNonQuery();

                Global.LocalStorage.RemoveCell(Rental.CellID);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }
    }

}
