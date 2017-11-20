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

        public void AddFilm(Film m)
        {
            filmEntity.Add(m);
        }
        public void RemoveFilm(int ID)
        {
            filmEntity.RemoveFilm(ID);
        }

        public void AddActor(Actor a)
        {
            actorEntity.Add(a);
        }
        public void RemoveActor(int ID)
        {
            actorEntity.RemoveActor(ID);
        }

        public void AddCategory(Category c)
        {
           categoryEntity.Add(c);
        }
        public void RemoveCategory(int c)
        {
            categoryEntity.RemoveCategory(c);
        }

        public void AddItem(Item c)
        {
            itemEntity.Add(c);
        }
        public void RemoveItem(int c)
        {
            itemEntity.RemoveItem(c);
        }

        public void AddAddress(Address c)
        {
            addressEntity.Add(c);
        }
        public void RemoveAddress(int c)
        {
            addressEntity.RemoveAddress(c);
        }

        public void AddCity(City c)
        {
            cityEntity.Add(c);
        }
        public void RemoveCity(int c)
        {
            cityEntity.RemoveCity(c);
        }

        public void AddCountry(Country c)
        {
            countryEntity.Add(c);
        }
        public void RemoveCountry(int c)
        {
            countryEntity.RemoveCountry(c);
        }

        public void AddCustomer(Customer c)
        {
            customerEntity.Add(c);
        }
        public void RemoveCustomer(int c)
        {
            customerEntity.RemoveCustomer(c);
        }

        public void AddLanguage(Language c)
        {
            languageEntity.Add(c);
        }
        public void RemoveLanguage(int c)
        {
            languageEntity.RemoveLanguage(c);
        }

        public void AddPayment(Payment c)
        {
            paymentEntity.Add(c);
        }
        public void RemovePayment(int c)
        {
            paymentEntity.RemovePayment(c);
        }

        public void AddRental(Rental c)
        {
            rentalEntity.Add(c);
        }
        public void RemoveRental(int c)
        {
            rentalEntity.RemoveRental(c);
        }

        public void AddStaff(Staff c)
        {
            staffEntity.Add(c);
        }
        public void RemoveStaff(int c)
        {
            staffEntity.RemoveStaff(c);
        }

        public void AddStore(Store c)
        {
            storeEntity.Add(c);
        }
        public void RemoveStore(int c)
        {
            storeEntity.RemoveStore(c);
        }
    }
}
