
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
                Payment mov = new Payment(payment_id: paymentId);
                Global.LocalStorage.SavePayment(mov);
            }
        }
    }
}
