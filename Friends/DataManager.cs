using Friends.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Friends
{
    public class DataManager
    {

        ActorEntity actorEntity;
        CategoryEntity categoryEntity;
        FilmEntity filmEntity;
        ItemEntity itemEntity;
        AddressEntity addressEntity;
        CityEntity cityEntity;
        CountryEntity countryEntity;
        CustomerEntity customerEntity;
        LanguageEntity languageEntity;
        PaymentEntity paymentEntity;
        RentalEntity rentalEntity;
        StaffEntity staffEntity;
        StoreEntity storeEntity;

        

        public DataManager()
        {
            actorEntity = new ActorEntity("actor", "Actor");//
            actorEntity.loadData();
          
            categoryEntity = new CategoryEntity("category", "Category");//
            categoryEntity.loadData();

            itemEntity = new ItemEntity("item", "Item");//
            itemEntity.loadData();

            filmEntity = new FilmEntity("film", "Film");//
            filmEntity.loadData();

            addressEntity = new AddressEntity("address", "Address");//
            addressEntity.loadData();

            cityEntity = new CityEntity("city", "City");//
            cityEntity.loadData();

            countryEntity = new CountryEntity("country", "Country");//
            countryEntity.loadData();

            customerEntity = new CustomerEntity("customer", "Customer");//
            customerEntity.loadData();

            languageEntity = new LanguageEntity("language", "Language");//
            languageEntity.loadData();

            paymentEntity = new PaymentEntity("payment", "Payment");
            paymentEntity.loadData();
     
            rentalEntity = new RentalEntity("rental", "Rental");
            rentalEntity.loadData();

            staffEntity = new StaffEntity("staff", "Staff");
            staffEntity.loadData();

            storeEntity = new StoreEntity("store", "Store");
            storeEntity.loadData();




        }

        private static DataManager _instance;

        public static DataManager getInstance()
        {
            if (_instance == null)
                _instance = new DataManager();

            return _instance;
        }
        
    }
}
