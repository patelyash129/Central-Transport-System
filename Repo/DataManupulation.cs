using Models2.DB;
using Models2.Employee;
using Models2.User;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Web.DynamicData;
using Tech.Frameworks;
using Tech.Frameworks.Helpers;
namespace Repo
{
    public class DataManupulation
    {
        DBHelperSQL sql,sql1;

        public DataManupulation()
        {
            sql = new DBHelperSQL("Data Source=LAPTOP-JQ4Q11VB\\SQLEXPRESS;initial catalog=CTS;integrated security=True;Persist Security Info=True;");
            sql1 = new DBHelperSQL("Data Source=LAPTOP-JQ4Q11VB\\SQLEXPRESS;initial catalog=React;integrated security=True;Persist Security Info=True;");

        }
        public string AddData(Employee employee)
        {
            int depid = getdepid(employee.Department.DepartmentName,employee.Department.DepartmentDesignation);
            Employee emp=new Employee()
            {
                Name = employee.Name,
                Address = employee.Address,
                Age = employee.Age,
                Email = employee.Email,
                Salary = employee.Salary,
                Number= employee.Number, 
                DepartmentId=depid   
           };
            
            DBActions dBActions = new DBActions(sql);
            try
            {
               var x= dBActions.InsertWithoutPrimaryKey<Employee>(emp);
               return "successfully send data";
            }
            catch (Exception ex)
            {
                return "Exception occur in betwwen send"+ex;
            }
        }
        public string AddTruckreq(string str,TruckRequest tq)
        {
            string k = str.ToString().Trim('"');

            var m = sql.GetDataTable("Select * From UserTable Where U_email='" + k + "';");
            int l = Con(m, "E_Id");
            TruckRequest Trq = new TruckRequest()
            {
                EmployeeId = l,
                Venue = tq.Venue,
                PhoneNumber = tq.PhoneNumber,
                Purpose = tq.Purpose,
                Date = tq.Date,
                Time = tq.Time,
                RequestFor = tq.RequestFor,
                Res_Id = 0,
                AllotId=0,
                Status = tq.Status
               
            };

            DBActions dBActions = new DBActions(sql);
            try
            {
                var x = sql.ExecuteQuery("INSERT INTO TruckRequest([E_Id],[venue],[Phone number],[Purpose],[Date],[Time],[Request For],[Res_Id],[Allot_Id],[Status]) VALUES ("+Trq.EmployeeId+",'" + Trq.Venue + "'," + Trq.PhoneNumber + ",'" + Trq.Purpose + "','" + Trq.Date + "','" + Trq.Time + "','" + Trq.RequestFor + "'," + Trq.Res_Id + "," + Trq.AllotId + ",'" + Trq.Status + "');");
                return "successfully send data";
            }
            catch (Exception ex)
            {
                return "Exception occur in betwwen send" + ex;
            }
        }

        public int getdepid(string x,string y)
        {
            x = x.ToUpper();
            y = y.ToUpper();
           var lines= sql.GetDataTable("Select Id From Department Where Name='"+x+"' AND Designation='"+y+"';");
            int id=0;
            foreach (DataRow row in lines.Rows)
            {
                id = Convert.ToInt32(row["Id"]);
            }
            return id;
        }
        public String AddDepartment(Department department)
        {
            Department d= new Department()
            {
                DepartmentName=department.DepartmentName.ToUpper(),
                DepartmentDesignation=department.DepartmentDesignation.ToUpper(),
                DepartmentLocation=department.DepartmentLocation.ToUpper()
            };
            DBActions dBActions = new DBActions(sql);
            try
            {
                var x = dBActions.InsertWithoutPrimaryKey<Department>(d);
                return "Successfully Added Department";
            }
            catch (Exception ex)
            {
                return "Fail to send the data"+ex;
            }
        }
        public String AddRequest(string str,Request req)
        {
            string k = str.ToString().Trim('"');

            var m = sql.GetDataTable("Select * From UserTable Where U_email='"+k+"';");
            int x = Con(m, "E_Id");
            Request request = new Request() { 
            SingleOrReturn=req.SingleOrReturn,
            Destination=req.Destination,
            Date=req.Date,
            DestinationCity=req.DestinationCity,
            DestinationState=req.DestinationState,
            NumberOfDays=req.NumberOfDays,
            Source=req.Source,
            SourceCity=req.SourceCity,
            SourceState=req.SourceState,
            Purpose=req.Purpose,
            E_Id=x,
            Allot_Id=req.Allot_Id,
            Res_Id=req.Res_Id,
            StartTime=req.StartTime,
            EndTime=req.EndTime,
            Status=req.Status
            };
            DBActions dBActions = new DBActions(sql);
            try
            {
                string query = "INSERT INTO Request ([SingleOrReturn], [T_destination], [T_date], [D_city], [D_state], [NoOfdays], [T_source], [S_city], [S_State], [Purpose], [E_Id], [Allot_Id], [Res_Id], [S_time], [D_time], [Staus]) VALUES ('"+ request.SingleOrReturn + "','"+ request.Destination + "','"+ request.Date + "','"+ request.DestinationCity+"','"+ request.DestinationState + "',"+ request.NumberOfDays + ",'"+ request.Source + "','"+ request.SourceCity + "','"+ request.SourceState + "','"+ request.Purpose + "',"+ request.E_Id + ","+ 0 + ","+ 0 + ",'"+ request.StartTime + "','"+ request.EndTime + "','"+ request.Status + "');";


                var y =sql.ExecuteQuery(query);
                //   var y = dBActions.InsertWithoutPrimaryKey<Request>(request);
                return "Successfully Added request data";
            }
            catch (Exception ex)
            {
                return "Fail to send the data" + ex;
            }
        }
        public String LoginReq(Loginmodel x)
        {
            int i= 0,n=0;
            string e="";
            var m = sql.GetDataTable("Select * From UserTable Where U_email='"+x.Email.ToString()+"' AND U_Password='"+x.Password.ToString()+"';");
            foreach(DataRow k in m.Rows)
            {
                n = Convert.ToInt32(k["U_Id"]);
                e = k["U_email"].ToString();
            }
            if (e!="")
            {
                i = 1;
            }
            if(i==1)
            {

                return e;
            }
            else
            {
                return "";
            }
        }
        public string Signupadd(Signup x)
        {
            try
            {
                DBActions dBActions = new DBActions(sql);
                var m = dBActions.InsertWithoutPrimaryKey<Signup>(x);
                return "Successfully added data to database";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public List<Request> GetAllHistoryData()
        {
            var re = sql.GetDataTable("Select * From Request;");
            List<Request> list = ConvertDataTableToList(re);
            return list;
        }
        public List<Request> GetAllData()
        {
            var re = sql.GetDataTable("Select * From Request Where Allot_Id='0' AND Res_Id='0';");
            List<Request> list =ConvertDataTableToList(re);
            return list;
        }
        public List<Request> ConvertDataTableToList(DataTable dataTable)
        {
            var studentList = new List<Request>();

            foreach (DataRow row in dataTable.Rows)
            {
                var student = new Request()
                {
                    Id = Convert.ToInt64(row["Req_id"]),
                    SingleOrReturn = row["SingleOrReturn"].ToString(),
                    Destination = row["T_destination"].ToString(),
                    Date = row["T_date"].ToString(),
                    DestinationCity = row["D_city"].ToString(),
                    DestinationState = row["D_state"].ToString(),
                    NumberOfDays = Convert.ToInt32(row["NoOfdays"]),
                    Source = row["T_source"].ToString(),
                    SourceCity = row["S_city"].ToString(),
                    SourceState = row["S_State"].ToString(),
                    Purpose = row["Purpose"].ToString(),
                    E_Id = Convert.ToInt32(row["E_Id"]),
                    Allot_Id = Convert.ToInt32(row["Allot_Id"]),
                    Res_Id = Convert.ToInt32(row["Res_Id"]),
                    StartTime = row["S_time"].ToString(),
                    EndTime = row["D_time"].ToString(),
                    Status = row["Staus"].ToString()

                };

                studentList.Add(student);
            }

            return studentList;
        }
        public List<Vehicle> GetVehicle(String date,string For) {
            var x = new List<Vehicle>();
            int count = 0;

            if (For == "Car")
            {
                var y = sql.GetDataTable("Select * From vehicle Where V_type='CAR';");
                var m = new List<int>();
                var z = sql.GetDataTable("Select * From Allotment Where date='" + date + "';");
                foreach (DataRow row in y.Rows)
                {
                    var vehicle = new Vehicle()
                    {
                        V_Id = Convert.ToInt32(row["V_Id"]),
                        V_Name = row["V_Name"].ToString(),
                        V_type = row["V_type"].ToString(),
                        V_number = row["V_number"].ToString(),
                        V_color = row["V_color"].ToString()

                    };
                    foreach (DataRow innerrow in z.Rows)
                    {
                        if (vehicle.V_Id == Convert.ToInt32(innerrow["V_Id"]))
                        {
                            count = 1;
                        }
                    }

                    if (count == 1)
                    {
                        count = 0;
                    }
                    else
                    {
                        x.Add(vehicle);
                    }

                }
            }
            else {
                var y = sql.GetDataTable("Select * From vehicle Where V_type='Truck';");
                var m = new List<int>();
                var z = sql.GetDataTable("Select * From TruckAllotment Where date='" + date + "';");
                foreach (DataRow row in y.Rows)
                {
                    var vehicle = new Vehicle()
                    {
                        V_Id = Convert.ToInt32(row["V_Id"]),
                        V_Name = row["V_Name"].ToString(),
                        V_type = row["V_type"].ToString(),
                        V_number = row["V_number"].ToString(),
                        V_color = row["V_color"].ToString()
                    };
                          foreach (DataRow innerrow in z.Rows)
                    {
                        if (vehicle.V_Id == Convert.ToInt32(innerrow["V_Id"]))
                        {
                            count = 1;
                        }
                    }

                    if (count == 1)
                    {
                        count = 0;
                    }
                    else
                    {
                        x.Add(vehicle);
                    }
              
                }

            }

           
            return x;
        }
        public string AllotVeh(Allotment all)
        {
            DBActions dbaction=new DBActions(sql);
            
            try
            {
                Respone r = new Respone()
                {
                    Req_id = all.R_Id,
                    V_id = all.V_Id,
                    Status = "Alloted",
                    Reason = "Available"
                };
                var type = sql.GetDataTable("Select V_type From vehicle Where V_Id='" + r.V_id + "';");
                string name = SCon(type, "V_type").Trim();
                if (name == "CAR")
                {
                    //  var y = dbaction.InsertWithoutPrimaryKey<Respone>(r);
                    var y = sql.ExecuteQuery("Insert into Respone(Req_id,V_id,Status,Reason) Values("+r.Req_id+","+r.V_id+",'Alloted','Available');");
                var Res = sql.GetDataTable("Select Res_Id From Respone Where Req_id='"+r.Req_id+"' AND V_id='"+r.V_id+"';");
                int Res_id = Con(Res,"Res_Id");
                all.Res_id = Res_id;
              
                    // var x = dbaction.InsertWithoutPrimaryKey<Allotment>(all);
                    var x = sql.ExecuteQuery("INSERT INTO Allotment (R_Id, V_Id, date, destination, source, D_time, S_time, Res_id) VALUES (" + r.Req_id + ", " + r.V_id + ", '" + all.Date + "', '" + all.Destination + "', '" + all.Source + "', '" + all.D_time + "', '" + all.S_time + "', " + all.Res_id + ");");
                    var Allot = sql.GetDataTable("Select Allot_Id From Allotment Where R_id='" + all.R_Id + "' AND V_Id='" + all.V_Id + "';");
                    int Allot_id = Con(Allot, "Allot_Id");

                    var m = sql.ExecuteQuery("UPDATE Request SET Res_Id ='" + Res_id + "',Allot_Id ='" + Allot_id + "',Staus = 'Approved' WHERE Req_id = '" + r.Req_id + "';");
                    return "Successfully send  CAR data to DB";

                }
                else if(name=="Truck")
                {
                    var y = sql.ExecuteQuery("Insert into Respone(Req_id,V_id,Status,Reason) Values(" + r.Req_id + "," + r.V_id + ",'Alloted','Available');");
                    var Res = sql.GetDataTable("Select Res_Id From Respone Where Req_id='" + r.Req_id + "' AND V_id='" + r.V_id + "';");
                    int Res_id = Con(Res, "Res_Id");
                    all.Res_id = Res_id;

                    // var x = dbaction.InsertWithoutPrimaryKey<Allotment>(all);
                    var x = sql.ExecuteQuery("INSERT INTO TruckAllotment (R_Id, V_Id, date, Venue, Time,  Res_id) VALUES (" + r.Req_id + ", " + r.V_id + ", '" + all.Date + "', '" + all.Destination +  "', '" + all.D_time + "', " + all.Res_id + ");");
                    var Allot = sql.GetDataTable("Select Allot_Id From TruckAllotment Where R_id='" + all.R_Id + "' AND V_Id='" + all.V_Id + "';");
                    int Allot_id = Con(Allot, "Allot_Id");

                    var m = sql.ExecuteQuery("UPDATE TruckRequest SET Res_Id ='" + Res_id + "',Allot_Id ='" + Allot_id + "',Status = 'Approved' WHERE Id = '" + r.Req_id + "';");
                    return "Successfully send Truck data to DB";

                }
                else
                {
                    return "Failed to send data to DB";

                }
            }
            catch(Exception e)
            {
                return "Failed due to " + e;
            }
        }

        public string Rej(Allotment all)
        {
            DBActions dbaction = new DBActions(sql);

            try
            {
                Respone r = new Respone()
                {
                    Req_id = all.R_Id,
                    V_id = 20002414,
                    Status = "Not Alloted",
                    Reason = all.Source
                };
                if (all.Destination == "Truck")
                {
                    var y = sql.ExecuteQuery("Insert into Respone(Req_id,V_id,Status,Reason) Values(" + r.Req_id + "," + r.V_id + ",'Not Alloted','" + r.Reason + "');");
                    var Res = sql.GetDataTable("Select Res_Id From Respone Where Req_id='" + r.Req_id + "' AND V_id='" + r.V_id + "';");
                    int Res_id = Con(Res, "Res_Id");
                    //all.Res_id = Res_id;
                    // var x = dbaction.InsertWithoutPrimaryKey<Allotment>(all);
                    //  var x = sql.ExecuteQuery("INSERT INTO Allotment (R_Id, V_Id, date, destination, source, D_time, S_time, Res_id) VALUES (" + r.Req_id + ", " + r.V_id + ", '" + all.Date + "', '" + all.Destination + "', '" + all.Source + "', '" + all.D_time + "', '" + all.S_time + "', " + all.Res_id + ");");
                    // var Allot = sql.GetDataTable("Select Allot_Id From Allotment Where R_id='" + all.R_Id + "' AND V_Id='" + all.R_Id + "';");
                    int Allot_id = 0;

                    var m = sql.ExecuteQuery("UPDATE TruckRequest SET Res_Id ='" + Res_id + "',Allot_Id ='" + Allot_id + "',Status = 'Not Approved' WHERE Id = '" + r.Req_id + "';");
                }
                else
                {


                    //  var y = dbaction.InsertWithoutPrimaryKey<Respone>(r);
                    var y = sql.ExecuteQuery("Insert into Respone(Req_id,V_id,Status,Reason) Values(" + r.Req_id + "," + r.V_id + ",'Not Alloted','" + r.Reason + "');");
                    var Res = sql.GetDataTable("Select Res_Id From Respone Where Req_id='" + r.Req_id + "' AND V_id='" + r.V_id + "';");
                    int Res_id = Con(Res, "Res_Id");
                    //all.Res_id = Res_id;
                    // var x = dbaction.InsertWithoutPrimaryKey<Allotment>(all);
                    //  var x = sql.ExecuteQuery("INSERT INTO Allotment (R_Id, V_Id, date, destination, source, D_time, S_time, Res_id) VALUES (" + r.Req_id + ", " + r.V_id + ", '" + all.Date + "', '" + all.Destination + "', '" + all.Source + "', '" + all.D_time + "', '" + all.S_time + "', " + all.Res_id + ");");
                    // var Allot = sql.GetDataTable("Select Allot_Id From Allotment Where R_id='" + all.R_Id + "' AND V_Id='" + all.R_Id + "';");
                    int Allot_id = 0;

                    var m = sql.ExecuteQuery("UPDATE Request SET Res_Id ='" + Res_id + "',Allot_Id ='" + Allot_id + "',Staus = 'Not Approved' WHERE Req_id = '" + r.Req_id + "';");
                }
                return "Successfully send data to DB";
            }
            catch (Exception e)
            {
                return "Failed due to " + e;
            }
        }

        public int Con(DataTable dataTable, string str)
        {
            int Id = 0;
            foreach (DataRow row in dataTable.Rows)
            {


                Id = Convert.ToInt32(row[str]);

            }
            return Id;
        }
        public string SCon(DataTable dataTable, string str)
            {
                string name = "";
                foreach (DataRow row in dataTable.Rows)
                {


                    name =row[str].ToString();

                }
                return name;
            }
        
        public string AddVeh(Vehicle v)
        {
            DBActions dbaction = new DBActions(sql);
            try
            {
                string query = "INSERT INTO vehicle ([V_Name],[V_type],[V_number],[V_color]) VALUES ('"+v.V_Name+"','"+v.V_type+"','"+v.V_number+"','"+v.V_color+"');";


                // Adding parameters to the SqlCommand object to protect against SQL injection
              

                // Execute the query and get the number of affected rows
               var x = sql.ExecuteQuery(query);
              //   var x = dbaction.InsertWithoutPrimaryKey<Vehicle>(v);
                //   var x = sql.ExecuteQuery("INSERT INTO vehicle (V_Name, V_type, V_number, V_color) VALUES (" + v.V_Name+","+v.V_type+"," + v.V_number + "," + v.V_color + ");");
                return "Successfully send data to DB";
            }
            catch (Exception e)
            {
                return "Failed due to " + e;
            }
        }
        public Employee profile(string x)
        {
            try
            {
              Employee empdet= new Employee();
                string k = x.ToString().Trim('"');
                var Empid = sql.GetDataTable("Select * From UserTable Where U_email='" + k + "';");
                int id = Con(Empid, "E_Id");
                var emp = sql.GetDataTable("Select * From Employee Where Id='" + id + "';");
                foreach (DataRow row in emp.Rows)
                {
                    empdet.Id = Convert.ToInt32(row["Id"]);
                        empdet.Name = row["Name"].ToString();
                        empdet.Address = row["Address"].ToString();
                        empdet.Age = row["Age"].ToString();
                        empdet.Number = row["number"].ToString();
                        empdet.Email = row["Email"].ToString();
                        empdet.Salary =Convert.ToDouble(row["Salary"]);
                        empdet.DepartmentId = Convert.ToInt32(row["D_Id"]);
                }
                var department = sql.GetDataTable("Select * From Department Where Id='"+empdet.DepartmentId+"';");
                foreach (DataRow row in department.Rows)
                {
                    empdet.Department = new Department();
                    empdet.Department.DepartmentName = row["Name"].ToString();
                    empdet.Department.DepartmentDesignation = row["Designation"].ToString();
                    empdet.Department.DepartmentLocation = row["Location"].ToString();
                  
                }
                return empdet;
            }
            catch (Exception e)
            { 
                Employee employee = new Employee();
                return employee;
            }
        }
        public List<Request> reqdata(string x)
        {
            try
            {
                Request empdet = new Request();
                string k = x.ToString().Trim('"');
                var Empid = sql.GetDataTable("Select * From UserTable Where U_email='" + k + "';");
                int id = Con(Empid, "E_Id");
                var emp = sql.GetDataTable("Select * From Request Where E_Id='" + id + "';");
                List<Request> list = new List<Request>();
                foreach (DataRow row in emp.Rows)
                {
                    Request student = new Request()
                    {
                        Id = Convert.ToInt64(row["Req_id"]),
                        SingleOrReturn = row["SingleOrReturn"].ToString(),
                        Destination = row["T_destination"].ToString(),
                        Date = row["T_date"].ToString(),
                        DestinationCity = row["D_city"].ToString(),
                        DestinationState = row["D_state"].ToString(),
                        NumberOfDays = Convert.ToInt32(row["NoOfdays"]),
                        Source = row["T_source"].ToString(),
                        SourceCity = row["S_city"].ToString(),
                        SourceState = row["S_State"].ToString(),
                        Purpose = row["Purpose"].ToString(),
                        E_Id = Convert.ToInt32(row["E_Id"]),
                        Allot_Id = Convert.ToInt32(row["Allot_Id"]),
                        Res_Id = Convert.ToInt32(row["Res_Id"]),
                        StartTime = row["S_time"].ToString(),
                        EndTime = row["D_time"].ToString(),
                        Status = row["Staus"].ToString()

                    };
                    if (student.Allot_Id == 0 && student.Res_Id == 0)
                    {
                        list.Add(student);
                    }
                }

                return list;
            }
            catch (Exception e)
            {
                List<Request> employee = new List<Request>();
                return employee;
            }
        }
        public List<Request> reqapprovedata(string x)
        {
            try
            {
                Request empdet = new Request();
                string k = x.ToString().Trim('"');
                var Empid = sql.GetDataTable("Select * From UserTable Where U_email='" + k + "';");
                int id = Con(Empid, "E_Id");
                var emp = sql.GetDataTable("Select * From Request Where E_Id='" + id + "';");
                List<Request> list = new List<Request>();
                foreach (DataRow row in emp.Rows)
                {
                    Request student = new Request()
                    {
                        Id = Convert.ToInt64(row["Req_id"]),
                        SingleOrReturn = row["SingleOrReturn"].ToString(),
                        Destination = row["T_destination"].ToString(),
                        Date = row["T_date"].ToString(),
                        DestinationCity = row["D_city"].ToString(),
                        DestinationState = row["D_state"].ToString(),
                        NumberOfDays = Convert.ToInt32(row["NoOfdays"]),
                        Source = row["T_source"].ToString(),
                        SourceCity = row["S_city"].ToString(),
                        SourceState = row["S_State"].ToString(),
                        Purpose = row["Purpose"].ToString(),
                        E_Id = Convert.ToInt32(row["E_Id"]),
                        Allot_Id = Convert.ToInt32(row["Allot_Id"]),
                        Res_Id = Convert.ToInt32(row["Res_Id"]),
                        StartTime = row["S_time"].ToString(),
                        EndTime = row["D_time"].ToString(),
                        Status = row["Staus"].ToString()

                    };
                    if (student.Allot_Id != 0 && student.Res_Id != 0)
                    {
                        list.Add(student);
                    }
                }

                return list;
            }
            catch (Exception e)
            {
                List<Request> employee = new List<Request>();
                return employee;
            }
        }



        public List<Request> reqrejectdata(string x)
        {
            try
            {
                Request empdet = new Request();
                string k = x.ToString().Trim('"');
                var Empid = sql.GetDataTable("Select * From UserTable Where U_email='" + k + "';");
                int id = Con(Empid, "E_Id");
                var emp = sql.GetDataTable("Select * From Request Where E_Id='" + id + "';");
                List<Request> list = new List<Request>();
                foreach (DataRow row in emp.Rows)
                {
                    Request student = new Request()
                    {
                        Id = Convert.ToInt64(row["Req_id"]),
                        SingleOrReturn = row["SingleOrReturn"].ToString(),
                        Destination = row["T_destination"].ToString(),
                        Date = row["T_date"].ToString(),
                        DestinationCity = row["D_city"].ToString(),
                        DestinationState = row["D_state"].ToString(),
                        NumberOfDays = Convert.ToInt32(row["NoOfdays"]),
                        Source = row["T_source"].ToString(),
                        SourceCity = row["S_city"].ToString(),
                        SourceState = row["S_State"].ToString(),
                        Purpose = row["Purpose"].ToString(),
                        E_Id = Convert.ToInt32(row["E_Id"]),
                        Allot_Id = Convert.ToInt32(row["Allot_Id"]),
                        Res_Id = Convert.ToInt32(row["Res_Id"]),
                        StartTime = row["S_time"].ToString(),
                        EndTime = row["D_time"].ToString(),
                        Status = row["Staus"].ToString()

                    };
                    if (student.Allot_Id == 0 && student.Res_Id != 0)
                    {
                        list.Add(student);
                    }
                }

                return list;
            }
            catch (Exception e)
            {
                List<Request> employee = new List<Request>();
                return employee;
            }
        }

        public List<TruckRequest> reqdatatruck(string x)
        {
            try
            {
                string k = x.ToString().Trim('"');
                var Empid = sql.GetDataTable("Select * From UserTable Where U_email='" + k + "';");
                int id = Con(Empid, "E_Id");
                var emp = sql.GetDataTable("Select * From TruckRequest Where E_Id='" + id + "';");
                List<TruckRequest> list = new List<TruckRequest>();

                foreach (DataRow row in emp.Rows)
                {
                    var truckRequest = new TruckRequest()
                    {
                        id = Convert.ToInt32(row["Id"]),
                        EmployeeId = Convert.ToDouble(row["E_Id"]),
                        Venue = row["venue"].ToString(),
                        PhoneNumber = Convert.ToDouble(row["Phone number"]),
                        Purpose = row["Purpose"].ToString(),
                        Date = row["Date"].ToString(),
                        Time = row["Time"].ToString(),
                        RequestFor = row["Request For"].ToString(),
                        Res_Id = row["Res_Id"] != DBNull.Value ? Convert.ToInt32(row["Res_Id"]) : (int?)null,
                        AllotId = row["Allot_Id"] != DBNull.Value ? Convert.ToInt32(row["Allot_Id"]) : (int?)null,
                        Status = row["Status"].ToString()
                    };
                    if (truckRequest.AllotId == 0 && truckRequest.Res_Id == 0)
                    {
                        list.Add(truckRequest);
                    }
                }
                return list;
            }
            catch (Exception e)
            {
                List<TruckRequest> employee = new List<TruckRequest>();
                return employee;
            }
        }

        public List<TruckRequest> reqapprovedatatruck(string x)
        {
            try
            {
                string k = x.ToString().Trim('"');
                var Empid = sql.GetDataTable("Select * From UserTable Where U_email='" + k + "';");
                int id = Con(Empid, "E_Id");
                var emp = sql.GetDataTable("Select * From TruckRequest Where E_Id='" + id + "';");
                List<TruckRequest> list = new List<TruckRequest>();

                foreach (DataRow row in emp.Rows)
                {
                    var truckRequest = new TruckRequest()
                    {
                        id = Convert.ToInt32(row["Id"]),
                        EmployeeId = Convert.ToDouble(row["E_Id"]),
                        Venue = row["venue"].ToString(),
                        PhoneNumber = Convert.ToDouble(row["Phone number"]),
                        Purpose = row["Purpose"].ToString(),
                        Date = row["Date"].ToString(),
                        Time = row["Time"].ToString(),
                        RequestFor = row["Request For"].ToString(),
                        Res_Id = row["Res_Id"] != DBNull.Value ? Convert.ToInt32(row["Res_Id"]) : (int?)null,
                        AllotId = row["Allot_Id"] != DBNull.Value ? Convert.ToInt32(row["Allot_Id"]) : (int?)null,
                        Status = row["Status"].ToString()
                    };
                    if (truckRequest.AllotId !=0 && truckRequest.Res_Id != 0)
                    {
                        list.Add(truckRequest);
                    }
                }
                return list;
            }
            catch (Exception e)
            {
                List<TruckRequest> employee = new List<TruckRequest>();
                return employee;
            }
        }



        public List<TruckRequest> reqrejectdatatruck(string x)
        {
            try
            {
                string k = x.ToString().Trim('"');
                var Empid = sql.GetDataTable("Select * From UserTable Where U_email='" + k + "';");
                int id = Con(Empid, "E_Id");
                var emp = sql.GetDataTable("Select * From TruckRequest Where E_Id='" + id + "';");
                List<TruckRequest> list = new List<TruckRequest>();

                foreach (DataRow row in emp.Rows)
                {
                    var truckRequest = new TruckRequest()
                    {
                        id = Convert.ToInt32(row["Id"]),
                        EmployeeId = Convert.ToDouble(row["E_Id"]),
                        Venue = row["venue"].ToString(),
                        PhoneNumber = Convert.ToDouble(row["Phone number"]),
                        Purpose = row["Purpose"].ToString(),
                        Date = row["Date"].ToString(),
                        Time = row["Time"].ToString(),
                        RequestFor = row["Request For"].ToString(),
                        Res_Id = row["Res_Id"] != DBNull.Value ? Convert.ToInt32(row["Res_Id"]) : (int?)null,
                        AllotId = row["Allot_Id"] != DBNull.Value ? Convert.ToInt32(row["Allot_Id"]) : (int?)null,
                        Status = row["Status"].ToString()
                    };
                    if (truckRequest.AllotId == 0 && truckRequest.Res_Id != 0)
                    {
                        list.Add(truckRequest);
                    }
                }
                return list;
            }
            catch (Exception e)
            {
                List<TruckRequest> employee = new List<TruckRequest>();
                return employee;
            }
        }

        public List<TruckRequest> ShowAllTruck()
        {
            var k = sql.GetDataTable("Select * from TruckRequest Where Allot_Id='0' AND Res_Id='0' ;");
            List<TruckRequest> m = ConvertTruckTableToList(k);
            return m;
        }
        public List<TruckRequest> ConvertTruckTableToList(DataTable dataTable)
        {
            var truckRequestList = new List<TruckRequest>();

            foreach (DataRow row in dataTable.Rows)
            {
                var truckRequest = new TruckRequest()
                {
                    id = Convert.ToInt32(row["Id"]),
                    EmployeeId = Convert.ToDouble(row["E_Id"]),
                    Venue = row["venue"].ToString(),
                    PhoneNumber = Convert.ToDouble(row["Phone number"]),
                    Purpose = row["Purpose"].ToString(),
                    Date = row["Date"].ToString(),
                    Time = row["Time"].ToString(),
                    RequestFor = row["Request For"].ToString(),
                    Res_Id = row["Res_Id"] != DBNull.Value ? Convert.ToInt32(row["Res_Id"]) : (int?)null,
                    AllotId = row["Allot_Id"] != DBNull.Value ? Convert.ToInt32(row["Allot_Id"]) : (int?)null,
                    Status = row["Status"].ToString()
                };
                truckRequestList.Add(truckRequest);
            }

            return truckRequestList;
        }



        public List<TruckRequest> reqAdminrejectdatatruck()
        {
            try
            {
                var emp = sql.GetDataTable("Select * From TruckRequest;");
                List<TruckRequest> list = new List<TruckRequest>();

                foreach (DataRow row in emp.Rows)
                {
                    var truckRequest = new TruckRequest()
                    {
                        id = Convert.ToInt32(row["Id"]),
                        EmployeeId = Convert.ToDouble(row["E_Id"]),
                        Venue = row["venue"].ToString(),
                        PhoneNumber = Convert.ToDouble(row["Phone number"]),
                        Purpose = row["Purpose"].ToString(),
                        Date = row["Date"].ToString(),
                        Time = row["Time"].ToString(),
                        RequestFor = row["Request For"].ToString(),
                        Res_Id = row["Res_Id"] != DBNull.Value ? Convert.ToInt32(row["Res_Id"]) : (int?)null,
                        AllotId = row["Allot_Id"] != DBNull.Value ? Convert.ToInt32(row["Allot_Id"]) : (int?)null,
                        Status = row["Status"].ToString()
                    };
                    if (truckRequest.AllotId == 0 && truckRequest.Res_Id != 0)
                    {
                        list.Add(truckRequest);
                    }
                }
                return list;
            }
            catch (Exception e)
            {
                List<TruckRequest> employee = new List<TruckRequest>();
                return employee;
            }
        }



        public List<Request> reqAdminrejectdata()
        {
            try
            {
                Request empdet = new Request();
                var emp = sql.GetDataTable("Select * From Request;");
                List<Request> list = new List<Request>();
                foreach (DataRow row in emp.Rows)
                {
                    Request student = new Request()
                    {
                        Id = Convert.ToInt64(row["Req_id"]),
                        SingleOrReturn = row["SingleOrReturn"].ToString(),
                        Destination = row["T_destination"].ToString(),
                        Date = row["T_date"].ToString(),
                        DestinationCity = row["D_city"].ToString(),
                        DestinationState = row["D_state"].ToString(),
                        NumberOfDays = Convert.ToInt32(row["NoOfdays"]),
                        Source = row["T_source"].ToString(),
                        SourceCity = row["S_city"].ToString(),
                        SourceState = row["S_State"].ToString(),
                        Purpose = row["Purpose"].ToString(),
                        E_Id = Convert.ToInt32(row["E_Id"]),
                        Allot_Id = Convert.ToInt32(row["Allot_Id"]),
                        Res_Id = Convert.ToInt32(row["Res_Id"]),
                        StartTime = row["S_time"].ToString(),
                        EndTime = row["D_time"].ToString(),
                        Status = row["Staus"].ToString()

                    };
                    if (student.Allot_Id == 0 && student.Res_Id != 0)
                    {
                        list.Add(student);
                    }
                }

                return list;
            }
            catch (Exception e)
            {
                List<Request> employee = new List<Request>();
                return employee;
            }
        }



        public List<TruckRequest> reqAdminapprovedatatruck()
        {
            try
            {
                var emp = sql.GetDataTable("Select * From TruckRequest;");
                List<TruckRequest> list = new List<TruckRequest>();

                foreach (DataRow row in emp.Rows)
                {
                    var truckRequest = new TruckRequest()
                    {
                        id = Convert.ToInt32(row["Id"]),
                        EmployeeId = Convert.ToDouble(row["E_Id"]),
                        Venue = row["venue"].ToString(),
                        PhoneNumber = Convert.ToDouble(row["Phone number"]),
                        Purpose = row["Purpose"].ToString(),
                        Date = row["Date"].ToString(),
                        Time = row["Time"].ToString(),
                        RequestFor = row["Request For"].ToString(),
                        Res_Id = row["Res_Id"] != DBNull.Value ? Convert.ToInt32(row["Res_Id"]) : (int?)null,
                        AllotId = row["Allot_Id"] != DBNull.Value ? Convert.ToInt32(row["Allot_Id"]) : (int?)null,
                        Status = row["Status"].ToString()
                    };
                    if (truckRequest.AllotId != 0 && truckRequest.Res_Id != 0)
                    {
                        list.Add(truckRequest);
                    }
                }
                return list;
            }
            catch (Exception e)
            {
                List<TruckRequest> employee = new List<TruckRequest>();
                return employee;
            }
        }

        public List<Request> reqAdminapprovedata()
        {
            try
            {
                Request empdet = new Request();
              var emp = sql.GetDataTable("Select * From Request;");
                List<Request> list = new List<Request>();
                foreach (DataRow row in emp.Rows)
                {
                    Request student = new Request()
                    {
                        Id = Convert.ToInt64(row["Req_id"]),
                        SingleOrReturn = row["SingleOrReturn"].ToString(),
                        Destination = row["T_destination"].ToString(),
                        Date = row["T_date"].ToString(),
                        DestinationCity = row["D_city"].ToString(),
                        DestinationState = row["D_state"].ToString(),
                        NumberOfDays = Convert.ToInt32(row["NoOfdays"]),
                        Source = row["T_source"].ToString(),
                        SourceCity = row["S_city"].ToString(),
                        SourceState = row["S_State"].ToString(),
                        Purpose = row["Purpose"].ToString(),
                        E_Id = Convert.ToInt32(row["E_Id"]),
                        Allot_Id = Convert.ToInt32(row["Allot_Id"]),
                        Res_Id = Convert.ToInt32(row["Res_Id"]),
                        StartTime = row["S_time"].ToString(),
                        EndTime = row["D_time"].ToString(),
                        Status = row["Staus"].ToString()

                    };
                    if (student.Allot_Id != 0 && student.Res_Id != 0)
                    {
                        list.Add(student);
                    }
                }

                return list;
            }
            catch (Exception e)
            {
                List<Request> employee = new List<Request>();
                return employee;
            }
        }




        public List<ReactEmployee> GetAllEmp()
        {
            try
            {
                var j = sql1.GetDataTable("select * From Employee;");
                List<ReactEmployee> k = Converttoemp(j);
                return k;
            }
            catch(Exception e)
            {
                return null;
            }
        }

        public List<ReactEmployee> Converttoemp(DataTable dt)
        {
            List<ReactEmployee> l = new List<ReactEmployee>();
            foreach (DataRow row in dt.Rows)
            {
                ReactEmployee emp = new ReactEmployee()
                {
                    Name = row["Name"].ToString(),
                    Address = row["Address"].ToString(),
                    Url = row["URL"].ToString(),
                };
                l.Add(emp);
            }
            return l;
        }
    }
}
