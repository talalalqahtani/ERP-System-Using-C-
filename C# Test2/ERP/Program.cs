using System;
using System.Collections;
using System.Security.Cryptography.X509Certificates;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Cms;

class Program{
    static string connectionString = "server=localhost;user=root;password=Tt4064__;database=EmployeeDB;";

    static void Main(string[] args){

        while(true){
        Console.WriteLine("1. Add Employee");
        Console.WriteLine("2. Remove Employee");
        Console.WriteLine("3. Edit Employee");
        Console.WriteLine("4. Search Employee");
        Console.WriteLine("5. Show All Employees");
        Console.WriteLine("0. Exit");
        string input = Console.ReadLine();

          switch (input){
            case "1":
                AddEmployee();
                break;
            case "2":
                RemoveEmployee();
                break;
            case "3":
                EditEmployee();  
                break;  
            case"4":
                SearchEmployee();    
                break;
            case"5":
                ShowEmployee();
                break;
            case "0":
                Console.WriteLine("👋 Exiting program. Goodbye!");
                return; // exits the Main method
            default:
                Console.WriteLine("❌ Invalid option, try again.");
                break;
        
            }
        }
    }

    static void AddEmployee(){

         Console.WriteLine("ADD ID:");
         int id = int.Parse(Console.ReadLine());

         Console.WriteLine("ADD Name:");
         string name = Console.ReadLine();

        Console.WriteLine("ADD Department:");
         string dep = Console.ReadLine();

         Console.WriteLine("ADD Phone:");
         string phone = Console.ReadLine();

         Console.WriteLine("ADD Salary:");
         string salary = Console.ReadLine();

         using (MySqlConnection conn = new MySqlConnection(connectionString)){
            conn.Open();
            string sql = "INSERT INTO Employees (ID, Name, Department, Phone, Salary) VALUES (@id, @name, @dep, @phone, @salary)";
            MySqlCommand cmd = new MySqlCommand(sql,conn);
            cmd.Parameters.AddWithValue("@id",id);
            cmd.Parameters.AddWithValue("@name",name);
            cmd.Parameters.AddWithValue("@dep",dep);
            cmd.Parameters.AddWithValue("@phone",phone);
            cmd.Parameters.AddWithValue("@salary",salary);
            cmd.ExecuteNonQuery();

            Console.WriteLine("✅ Employee added successfully.");
         }
    }

    static void RemoveEmployee(){
        
        Console.WriteLine("ADD ID that will removed:");
        int id = int.Parse(Console.ReadLine());

        using (MySqlConnection conn = new MySqlConnection(connectionString)){
            conn.Open();
            string sql = "DELETE FROM Employees WHERE ID = @id";
            MySqlCommand cmd = new MySqlCommand(sql,conn);

            cmd.Parameters.AddWithValue("@id",id);

            int rowsAffected = cmd.ExecuteNonQuery();

        if (rowsAffected > 0)
        {
            Console.WriteLine("🗑️ Employee removed successfully.");
        }
        else
        {
            Console.WriteLine("⚠️ No employee found with that ID.");
        }
        }
    }
    static void EditEmployee(){
        while(true){
            Console.WriteLine("What ID you want update?: (0 to exit)");
            int idu = int.Parse(Console.ReadLine());
            if(idu != 0){
            using (MySqlConnection conn = new MySqlConnection(connectionString)){
                conn.Open();
                string sql = "";
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
            
            Console.WriteLine("What you want to edit \n1. ID\n2. Name \n3. Department\n4. Phone\n5. Salary\n0. Exit");
            string choice = Console.ReadLine();
            switch(choice){
                case"1":
                Console.WriteLine("Enter new ID:");
                int newid = int.Parse(Console.ReadLine());
                cmd.CommandText = "UPDATE Employees SET ID = @id WHERE ID = @idu";
                cmd.Parameters.AddWithValue("@id",newid);
                cmd.Parameters.AddWithValue("@idu", idu);
                cmd.ExecuteNonQuery();
                Console.WriteLine("✅ ID Changed successfully.");
                break;

                case"2":
                Console.WriteLine("Enter new name:");
                string newname = Console.ReadLine();
                cmd.CommandText = "UPDATE Employees SET Name = @name WHERE ID = @idu";
                cmd.Parameters.AddWithValue("@name",newname);
                cmd.Parameters.AddWithValue("@idu", idu);
                cmd.ExecuteNonQuery();
                Console.WriteLine("✅ Name Changed successfully.");
                break;

                case"3":
                Console.WriteLine("Enter new Department:");
                string newdep = Console.ReadLine();
                cmd.CommandText = "UPDATE Employees SET Department = @dep WHERE ID = @idu";
                cmd.Parameters.AddWithValue("@dep",newdep);
                cmd.Parameters.AddWithValue("@idu", idu);
                cmd.ExecuteNonQuery();
                Console.WriteLine("✅ Department Changed successfully.");
                break;
                
                case"4":
                Console.WriteLine("Enter new Phone number:");
                string newphone = Console.ReadLine();
                cmd.CommandText = "UPDATE Employees SET Phone = @phone WHERE ID = @idu";
                cmd.Parameters.AddWithValue("@phone",newphone);
                cmd.Parameters.AddWithValue("@idu",idu);
                cmd.ExecuteNonQuery();
                Console.WriteLine("✅ Phone number Changed successfully.");
                break;

                case"5":
                Console.WriteLine("Enter new Phone number:");
                string newsalary = Console.ReadLine();
                cmd.CommandText = "UPDATE Employees SET Salary = @salary WHERE ID = @idu";
                cmd.Parameters.AddWithValue("@salary",newsalary);
                cmd.Parameters.AddWithValue("@idu",idu);
                cmd.ExecuteNonQuery();
                Console.WriteLine("✅ Salary Changed successfully.");
                break;

                case "0":
                return; 

                default:
                Console.WriteLine("❌ Invalid option, try again.");
                break;
            }
            }
        }
        else return;
        }
    }

    static void SearchEmployee(){
        Console.WriteLine("Search By:\n1.ID\n2.Name\n3.Department\n4.Phone\n5.Salary");
        string choice = Console.ReadLine();
        using (MySqlConnection conn = new MySqlConnection(connectionString)){
            conn.Open();
            string sql="";
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;

          switch(choice){
            case"1":
                Console.WriteLine("Enter Employee ID:");
                int id = int.Parse(Console.ReadLine());
                sql = "SELECT * FROM Employees WHERE ID = @id";
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@id",id);
                MySqlDataReader reader = cmd.ExecuteReader();
                if(reader.HasRows){
                    Console.WriteLine("The Result:");
                    while(reader.Read()){
                        Console.WriteLine($"ID: {reader["ID"]} , Name: {reader["Name"]} , Department {reader["Department"]}, Phone {reader["Phone"]} , Salary {reader["Salary"]}");
                    }
                }
                else{
                    Console.WriteLine("Employee Not Found!");
                }
                reader.Close();
                break;
            case"2":
                Console.WriteLine("Enter Employee name:");
                string name = Console.ReadLine();
                sql = "SELECT * FROM Employees WHERE Name = @name";
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@name",name);
                MySqlDataReader reader2 = cmd.ExecuteReader();
                if(reader2.HasRows){
                    Console.WriteLine("The Result:");
                    while(reader2.Read()){
                        Console.WriteLine($"ID: {reader2["ID"]} , Name: {reader2["Name"]} , Department {reader2["Department"]}, Phone {reader2["Phone"]} , Salary {reader2["Salary"]}");
                    }
                }
                else{
                    Console.WriteLine("Employee Not Found!");
                }
                reader2.Close();
            break;
            case"3":
                Console.WriteLine("Enter Employee Department:");
                string dep = Console.ReadLine();
                sql = "SELECT * FROM Employees WHERE Department = @dep";
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@dep",dep);
                MySqlDataReader reader3 = cmd.ExecuteReader();
                if(reader3.HasRows){
                    Console.WriteLine("The Result:");
                    while(reader3.Read()){
                        Console.WriteLine($"ID: {reader3["ID"]} , Name: {reader3["Name"]} , Department {reader3["Department"]} , Phone {reader3["Phone"]} , Salary {reader3["Salary"]}");
                    }
                }
                else{
                    Console.WriteLine("Employee Not Found!");
                }
                reader3.Close();
            break;

            case"4":
                Console.WriteLine("Enter Phone number:");
                string phone = Console.ReadLine();
                sql = "SELECT * FROM Employees WHERE Phone = @phone";
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@phone",phone);
                MySqlDataReader reader4 = cmd.ExecuteReader();
                if(reader4.HasRows){
                    Console.WriteLine("The Result:");
                    while(reader4.Read()){
                        Console.WriteLine($"ID: {reader4["ID"]} , Name: {reader4["Name"]} , Department {reader4["Department"]}, Phone {reader4["Phone"]} , Salary {reader4["Salary"]}");
                    }
                }
                else{
                    Console.WriteLine("Employee Not Found!");
                }
                reader4.Close();
                break;

            case"5":
                Console.WriteLine("Enter Salery:");
                string salary = Console.ReadLine();
                sql = "SELECT * FROM Employees WHERE Salary = @salary";
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@salary",salary);
                MySqlDataReader reader5 = cmd.ExecuteReader();
                if(reader5.HasRows){
                    Console.WriteLine("The Result:");
                    while(reader5.Read()){
                        Console.WriteLine($"ID: {reader5["ID"]} , Name: {reader5["Name"]} , Department {reader5["Department"]}, Phone {reader5["Phone"]} , Salary {reader5["Salary"]}");
                    }
                }
                else{
                    Console.WriteLine("Employee Not Found!");
                }
                reader5.Close();
                break;

            default:
            break;
        }  
        }
        
    }
    static void ShowEmployee(){
        using (MySqlConnection conn = new MySqlConnection(connectionString)){
            conn.Open();
            string sql = "SELECT * FROM Employees";
            MySqlCommand cmd = new MySqlCommand(sql,conn);
            MySqlDataReader reader = cmd.ExecuteReader();

            while(reader.Read()){
                Console.WriteLine($"ID: {reader["ID"]} , Name: {reader["Name"]} , Department: {reader["Department"]} , Phone {reader["Phone"]} , Salary: {reader["Salary"]}");
            }
        }
        
    }
}