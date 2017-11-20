
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

    public class PaymentEntity : Entity
    {
        

        public PaymentEntity(string tableName, string entityName) : base(tableName, entityName)
        {

        }

        public override void loadData()
        {
            Entity entity = (Entity)this;
            List<long> payments = Database.getInstance().loadNodeType(entity);

            foreach (long paymentId in payments)
            {
                List<long> list = Database.getInstance().findPaymentsById(paymentId);
                Payment mov = new Payment(payment_id: paymentId, customer_id: list[0], rental_id: list[1], staff_id: list[2]);
                Global.LocalStorage.SavePayment(mov);

            }


        }

        public void Add(Payment mov)
        {

            long customer = mov.customer_id;
            long staff = mov.staff_id;
            long rental = mov.rental_id;
            long amount = mov.amount;

            string arr = "INSERT INTO Payment (customer_id, staff_id,rental_id, amount) VALUES(@customer, @staff, @rental, @amount)";
            
            try
            {

                NpgsqlConnection conn = Database.getInstance().Connection;

                NpgsqlCommand cmd = new NpgsqlCommand(arr, conn);

                var parameter = cmd.CreateParameter();
                parameter.ParameterName = "customer_id";
                parameter.Value = customer;
                cmd.Parameters.Add(parameter);

                var parameter2 = cmd.CreateParameter();
                parameter2.ParameterName = "staff_id";
                parameter2.Value = staff;
                cmd.Parameters.Add(parameter2);

                var parameter3 = cmd.CreateParameter();
                parameter3.ParameterName = "rental_id";
                parameter3.Value = rental;
                cmd.Parameters.Add(parameter3);

                var parameter4 = cmd.CreateParameter();
                parameter4.ParameterName = "amount";
                parameter4.Value = amount;
                cmd.Parameters.Add(parameter4);

               


                cmd.ExecuteNonQuery();
                mov.payment_id= Database.getInstance().getLast();

                Global.LocalStorage.SavePayment(mov);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }

        public Payment getPaymentByID(int ID)
        {
            /*var results = from node in Global.LocalStorage.Actor_Accessor_Selector()
                          where node.ID == ID
                          select node;
                          */

            foreach (var mov in Global.LocalStorage.Payment_Accessor_Selector())
            {
                if (mov.payment_id == ID)
                    return mov;

            }
            return null;
        }
        public void RemovePayment(int ID)
        {
            var Payment = getPaymentByID(ID);
          
            string arr = "DELETE FROM Payment WHERE id = @ID";

            try
            {

                NpgsqlConnection conn = Database.getInstance().Connection;

                NpgsqlCommand cmd = new NpgsqlCommand(arr, conn);

                var parameter = cmd.CreateParameter();
                parameter.ParameterName = "ID";
                parameter.Value = ID;
                cmd.Parameters.Add(parameter);

                cmd.ExecuteNonQuery();

                Global.LocalStorage.RemoveCell(Payment.CellID);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }
    }

}
