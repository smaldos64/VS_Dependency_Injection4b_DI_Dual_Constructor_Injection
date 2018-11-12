using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Eksemplet her er en udbygning af det sidste eksempel. Det vil sige, at der også her er  
// implementeret DI - Dependency Injection (Pattern) med constructor injection.
// Til forskel fra det sidste eksempel er der her implementeret en klasse :
// CustomerService_Constructor_Injection_Data_Dual, der tillader Dependency Injection både "opad" 
// mod "Business logik klasser" (kravene til disse Business logik klasser er, at de implementerer 
// ICustomerLogicAccessinterfacet) såvel som "nedad" mod Data Acess klasser (kravene til disse
// Data Access klasser er, at de implementerer ICustomerDataAccess interfacet). 
// Man kan således nu mikse klasser fra Business logik laget med klasser fra Data Access laget på 
// kryds og på tværs. Dette er vist i eksemplerne i koden. 

namespace Dependency_Injection4_DI_Constructor_Injection
{
    public interface ICustomerLogicAccess
    {
        string ProcessCustomerData(int id);
    }

    public interface ICustomerDataAccess
    {
        string GetCustomerData(int id);
    }

    public class CustomerBusinessLogic : ICustomerLogicAccess
    {
        ICustomerDataAccess _dataAccess;

        public CustomerBusinessLogic(ICustomerDataAccess custDataAccess)
        {
            _dataAccess = custDataAccess;
        }

        public string ProcessCustomerData(int id)
        {
            return _dataAccess.GetCustomerData(id);
        }
    }

    public class CustomerBusinessLogic1 : ICustomerLogicAccess
    {
        ICustomerDataAccess _dataAccess;

        public CustomerBusinessLogic1(ICustomerDataAccess custDataAccess)
        {
            _dataAccess = custDataAccess;
        }

        public string ProcessCustomerData(int id)
        {
            return (_dataAccess.GetCustomerData(id) + " => Ny Customer Logik klasse !!!");
        }
    }

    public class CustomerService
    {
        CustomerBusinessLogic _customerBL;

        public CustomerService()
        {
            _customerBL = new CustomerBusinessLogic(new CustomerDataAccess());
        }

        public string GetCustomerName(int id)
        {
            return _customerBL.ProcessCustomerData(id);
        }
    }

    public class CustomerService_Constructor_Injection_Data
    {
        CustomerBusinessLogic _customerBL;
        ICustomerDataAccess _dataAccess;

        public CustomerService_Constructor_Injection_Data(ICustomerDataAccess custDataAccess)
        {
            _dataAccess = custDataAccess;
            _customerBL = new CustomerBusinessLogic(_dataAccess);
        }

        public string GetCustomerName(int id)
        {
            return _customerBL.ProcessCustomerData(id);
        }
    }

    public class CustomerService_Constructor_Injection_Data_Dual
    {
        ICustomerLogicAccess _customerBL;
        ICustomerDataAccess _dataAccess;

        public CustomerService_Constructor_Injection_Data_Dual(ICustomerDataAccess custDataAccess,
                                                               ICustomerLogicAccess custLogicAccess)
        {
            _dataAccess = custDataAccess;
            _customerBL = custLogicAccess;
        }

        public string GetCustomerName(int id)
        {
            return _customerBL.ProcessCustomerData(id);
        }
    }

    public class CustomerDataAccess : ICustomerDataAccess
    {
        public CustomerDataAccess()
        {
        }

        public string GetCustomerData(int id)
        {
            //get the customer name from the db in real application        
            return "Dummy Customer Name DI - Constructor Injection " + id.ToString();
        }
    }

    public class CustomerDataAccess1 : ICustomerDataAccess
    {
        public CustomerDataAccess1()
        {
        }

        public string GetCustomerData(int id)
        {
            //get the customer name from the db in real application        
            return "Dummy Customer Name DI - Constructor Injection => (Ny Data Logik klasse !!!) " + id.ToString();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //CustomerService CustomerService_Object = new CustomerService();
            //Console.WriteLine(CustomerService_Object.GetCustomerName(10));

            //CustomerService_Constructor_Injection_Data CustomerService_Object_DI = new CustomerService_Constructor_Injection_Data(new CustomerDataAccess());
            //Console.WriteLine(CustomerService_Object_DI.GetCustomerName(12));

            //CustomerService_Constructor_Injection_Data CustomerService_Object_DI1 = new CustomerService_Constructor_Injection_Data(new CustomerDataAccess1());
            //Console.WriteLine(CustomerService_Object_DI1.GetCustomerName(14));

            //CustomerBusinessLogic CustomerBusinessLogic_Object = new CustomerBusinessLogic(new CustomerDataAccess());
            //Console.WriteLine(CustomerBusinessLogic_Object.ProcessCustomerData(16) + " => Direkte oprettelse af CustomerBusinessLogic objekt");

            //CustomerBusinessLogic CustomerBusinessLogic_Object1 = new CustomerBusinessLogic(new CustomerDataAccess1());
            //Console.WriteLine(CustomerBusinessLogic_Object1.ProcessCustomerData(18) + " => Direkte oprettelse af CustomerBusinessLogic objekt");

            CustomerDataAccess CustomerDataAccess_object = new CustomerDataAccess();
            CustomerService_Constructor_Injection_Data_Dual CustomerService_Constructor_Injection_Data_Dual_object =
                new CustomerService_Constructor_Injection_Data_Dual(CustomerDataAccess_object, new CustomerBusinessLogic(CustomerDataAccess_object));
            Console.WriteLine(CustomerService_Constructor_Injection_Data_Dual_object.GetCustomerName(10));

            CustomerDataAccess1 CustomerDataAccess1_object = new CustomerDataAccess1();
            CustomerService_Constructor_Injection_Data_Dual CustomerService_Constructor_Injection_Data_Dual1_object =
                new CustomerService_Constructor_Injection_Data_Dual(CustomerDataAccess1_object, new CustomerBusinessLogic(CustomerDataAccess1_object));
            Console.WriteLine(CustomerService_Constructor_Injection_Data_Dual1_object.GetCustomerName(10));

            CustomerService_Constructor_Injection_Data_Dual CustomerService_Constructor_Injection_Data_Dual2_object =
                new CustomerService_Constructor_Injection_Data_Dual(CustomerDataAccess_object, new CustomerBusinessLogic1(CustomerDataAccess_object));
            Console.WriteLine(CustomerService_Constructor_Injection_Data_Dual2_object.GetCustomerName(10));

            CustomerService_Constructor_Injection_Data_Dual CustomerService_Constructor_Injection_Data_Dual3_object =
                new CustomerService_Constructor_Injection_Data_Dual(CustomerDataAccess1_object, new CustomerBusinessLogic1(CustomerDataAccess1_object));
            Console.WriteLine(CustomerService_Constructor_Injection_Data_Dual3_object.GetCustomerName(10));

            Console.ReadLine();
        }
    }
}
