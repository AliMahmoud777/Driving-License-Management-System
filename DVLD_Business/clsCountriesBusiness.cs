using System;
using System.Data;
using DVLD_DataAccess;

namespace DVLD_Business
{
    public class clsCountriesBusiness
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public clsCountriesBusiness()
        {
            this.ID = -1;
            this.Name = "";
        }
        private clsCountriesBusiness(int ID, string Name)
        {
            this.ID = ID;
            this.Name = Name;
        }

        public static clsCountriesBusiness Find(int ID)
        {
            string Name = "";

            if (clsCountriesDataAccess.GetCountryByID(ID, ref Name))
            {
                return new clsCountriesBusiness(ID, Name);
            }
            else
            {
                return null;
            }
        }

        public static clsCountriesBusiness FindByName(string Name)
        {
            int ID = -1;

            if (clsCountriesDataAccess.GetCountryByName(Name, ref ID))
            {
                return new clsCountriesBusiness(ID, Name);
            }
            else
            {
                return null;
            }
        }

        public static DataTable ListCountries()
        {
            return clsCountriesDataAccess.GetAllCountries();
        }
    }
}