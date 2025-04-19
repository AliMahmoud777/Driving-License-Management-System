using System;
using System.Data;
using DVLD_DataAccess;

namespace DVLD_Business
{
    public class clsApplicationTypesBusiness
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public float Fees { get; set; }

        public clsApplicationTypesBusiness()
        {
            this.ID = -1;
            this.Title = "";
            this.Fees = default;
        }
        private clsApplicationTypesBusiness(int ID, string Title, float Fees)
        {
            this.ID = ID;
            this.Title = Title;
            this.Fees = Fees;
        }

        public static clsApplicationTypesBusiness Find(int ID)
        {
            string Title = "";
            float Fees = default;

            if (clsApplicationTypesDataAccess.GetApplicationTypeByID(ID, ref Title, ref Fees))
            {
                return new clsApplicationTypesBusiness(ID, Title, Fees);
            }
            else
            {
                return null;
            }
        }

        private bool _UpdateApplicationType()
        {
            return clsApplicationTypesDataAccess.UpdateApplicationType(this.ID, this.Title, this.Fees);
        }

        public static DataTable GetAllApplicationTypes()
        {
            return clsApplicationTypesDataAccess.ListApplicationTypes();
        }

        public bool Save()
        {
            return _UpdateApplicationType();
        }
    }
}