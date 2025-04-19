using System;
using System.Data;
using DVLD_DataAccess;

namespace DVLD_Business
{
    public class clsLicenseClassesBusiness
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte MinimumAllowedAge { get; set; }
        public byte DefaultValidityPeriod { get; set; }
        public float ClassFees { get; set; }

        public clsLicenseClassesBusiness()
        {
            this.ID = -1;
            this.Name = "";
            this.Description = "";
            this.MinimumAllowedAge = 0;
            this.DefaultValidityPeriod = 0;
            this.ClassFees = default;
        }

        private clsLicenseClassesBusiness(int ID, string Name, string Description, byte MinimumAllowedAge,
            byte DefaultValidityPeriod, float ClassFees)
        {
            this.ID = ID;
            this.Name = Name;
            this.Description = Description;
            this.MinimumAllowedAge = MinimumAllowedAge;
            this.DefaultValidityPeriod = DefaultValidityPeriod;
            this.ClassFees = ClassFees;
        }

        public static clsLicenseClassesBusiness Find(int ID)
        {
            string Name = "", Description = "";
            byte MinimumAllowedAge = 0, DefaultValidityPeriod = 0;
            float ClassFees = default;

            if(clsLicenseClassesDataAccess.GetLicenseClassByID(ID, ref Name, ref Description,
                ref MinimumAllowedAge, ref DefaultValidityPeriod, ref ClassFees))
            {
                return new clsLicenseClassesBusiness(ID, Name, Description, MinimumAllowedAge, 
                    DefaultValidityPeriod, ClassFees);
            }
            else
            {
                return null;
            }
        }

        public static clsLicenseClassesBusiness FindByName(string Name)
        {
            string Description = "";
            int ID = -1;
            byte MinimumAllowedAge = 0, DefaultValidityPeriod = 0;
            float ClassFees = default;

            if (clsLicenseClassesDataAccess.GetLicenseClassByName(ref ID, Name, ref Description,
                ref MinimumAllowedAge, ref DefaultValidityPeriod, ref ClassFees))
            {
                return new clsLicenseClassesBusiness(ID, Name, Description, MinimumAllowedAge,
                    DefaultValidityPeriod, ClassFees);
            }
            else
            {
                return null;
            }
        }

        public static DataTable ListLicenseClasses()
        {
            return clsLicenseClassesDataAccess.GetAllLicenseClasses();
        }
    }
}