using System;
using System.Data;
using DVLD_DataAccess;

namespace DVLD_Business
{
    public class clsTestTypesBusiness
    {
        public enum enTestType { Vision = 1, Written = 2, Street = 3 };

        public enTestType ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public float Fees { get; set; }

        public clsTestTypesBusiness()
        {
            this.ID = enTestType.Vision;
            this.Title = "";
            this.Description = "";
            this.Fees = default;
        }
        private clsTestTypesBusiness(enTestType ID, string Title, string Description, float Fees)
        {
            this.ID = ID;
            this.Title = Title;
            this.Description = Description;
            this.Fees = Fees;
        }

        public static clsTestTypesBusiness Find(enTestType ID)
        {
            string Title = "", Description = "";
            float Fees = default;

            if (clsTestTypesDataAccess.GetTestTypeByID((int)ID, ref Title, ref Description, ref Fees))
            {
                return new clsTestTypesBusiness(ID, Title, Description, Fees);
            }
            else
            {
                return null;
            }
        }

        private bool _UpdateTestType()
        {
            return clsTestTypesDataAccess.UpdateTestType((int)this.ID, this.Title, this.Description, this.Fees);
        }

        public static DataTable GetAllTestTypes()
        {
            return clsTestTypesDataAccess.ListTestTypes();
        }

        public bool Save()
        {
            return _UpdateTestType();
        }
    }
}