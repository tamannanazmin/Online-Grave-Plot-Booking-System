using GraveBooking.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace GraveBooking.Services.Data
{
    public class SecurityDAO
    {
       private string connection = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\EDC-SEIP\Desktop\SD Project copies\Muniya1\GraveBooking\App_Data\GraveBooking.mdf;Integrated Security=True";
                                                                                         
        public void createUser(User user)
        {
            string name, email, phone, password;
            name = user.userName;
            email = user.email;
            phone = user.phone;
            password = user.password;

           
            using (SqlConnection sqlcon = new SqlConnection(connection))
            {
                string sqlquery = "insert into Users(userName,email,phone,password) values ('"+name+ "','" + email + "','" + phone + "','" + name + "')";
                using (SqlCommand sqlcom = new SqlCommand(sqlquery, sqlcon))
                {
                    sqlcon.Open();
                    sqlcom.ExecuteNonQuery();
                }
            }
        }
        public void updateUser(User user,int uid)
        {
            string name, email, phone, password;
            name = user.userName;
            email = user.email;
            phone = user.phone;
            password = user.password;


            using (SqlConnection sqlcon = new SqlConnection(connection))
            {
                string sqlquery = ("UPDATE Users SET userName = '" + name + "',email = '"+email+"',phone ='" +phone+ "' where userId = '"+uid+"'");


                using (SqlCommand sqlcom = new SqlCommand(sqlquery, sqlcon))
                {
                    sqlcon.Open();
                    sqlcom.ExecuteNonQuery();
                }
            }
        }
        public void updateAdmin(admin admin, int uid)
        {
            string name, email, phone, password;
            name = admin.adminName;
            email = admin.adminEmail;
            phone = admin.adminPhone;
            password = admin.adminPassword;


            using (SqlConnection sqlcon = new SqlConnection(connection))
            {
                string sqlquery = ("UPDATE admin SET adminName = '" + name + "',adminEmail = '" + email + "',adminPhone ='" + phone + "' where adminId = '" + uid + "'");


                using (SqlCommand sqlcom = new SqlCommand(sqlquery, sqlcon))
                {
                    sqlcon.Open();
                    sqlcom.ExecuteNonQuery();
                }
            }
        }


        public void createAdmin(admin admin)
        {
            string adminName, adminEmail, adminPhone, adminPassword;
            adminName = admin.adminName;
            adminEmail = admin.adminEmail;
            adminPhone = admin.adminPhone;
            adminPassword = admin.adminPassword;
            using (SqlConnection sqlcon = new SqlConnection(connection))
            {   
                string sqlquery = "insert into admin(adminName,adminEmail,adminPhone,adminPassword) values ('" + adminName + "','" + adminEmail + "','" + adminPhone + "','" + adminPassword + "')";
                using (SqlCommand sqlcom = new SqlCommand(sqlquery, sqlcon))
                {
                    sqlcon.Open();
                    sqlcom.ExecuteNonQuery();
                }
            }
        }

        internal bool IfAlreadyAnOwner(int uid)
        {
            bool success = false;
            using (SqlConnection sqlcon = new SqlConnection(connection))
            {
                string sqlquery = "SELECT * FROM owner WHERE userId='" + uid + "'";
                using (SqlCommand sqlcom = new SqlCommand(sqlquery, sqlcon))
                {
                    try
                    {
                        sqlcon.Open();
                        SqlDataReader reader = sqlcom.ExecuteReader();
                        if (reader.HasRows)
                        {
                            success = true;
                        }
                        else
                        {
                            success = false;
                        }
                        reader.Close();
                    }
                    catch (Exception e)
                    {
                        ;
                    }
                }

            }
            return success;
        }

        public void addOwner(owner ow)
        {
            //ow.adminId = 1;
            //ow.userId = 1;
            using (SqlConnection sqlcon = new SqlConnection(connection))
            {
                
                string sqlquery = "insert into owner(fullName,gender,fatherName,motherName,profession,phone,email,address,photo,nid,dateOfBirth,plotId,totalPrice,bankName,branchName,branchAddress,dd,adminId,userId,graveyardId) values ('" + ow.fullName + "','" + ow.gender + "','" + ow.fatherName + "','" + ow.motherName + "','" + ow.profession + "','" + ow.phone + "','" + ow.email + "','" + ow.address + "','" + ow.photo + "','" + ow.nid + "','" + ow.dateOfBirth + "','" + ow.plotId + "','" + ow.totalPrice + "','" + ow.bankName + "','" + ow.branchName + "','" + ow.branchAddress + "','" + ow.dd+ "','" + ow.adminId +"','"+ ow.userId + "','" +ow.graveyardId + "')";
                using (SqlCommand sqlcom = new SqlCommand(sqlquery, sqlcon))
                {
                    sqlcon.Open();
                    sqlcom.ExecuteNonQuery();
                }
            }
        }


        public void addBooking(Booking b)
        {
            //ow.adminId = 1;
            //ow.userId = 1;
            using (SqlConnection sqlcon = new SqlConnection(connection))
            {

                string sqlquery = "insert into Booking(OwnerId,GraveyardId,GravePlotId,Date,userId) values ('" + b.OwnerId + "','" + b.GraveyardId + "','" + b.GravePlotId +"','" + b.Date + "','" + b.userId + "')";
                using (SqlCommand sqlcom = new SqlCommand(sqlquery, sqlcon))
                {
                    sqlcon.Open();
                    sqlcom.ExecuteNonQuery();
                }
            }
        }
        public string GraveyardName(int gid)
        {
            string name = "";

            using (SqlConnection con = new SqlConnection(connection))
            {
                SqlCommand cmd = new SqlCommand("SELECT graveyardName FROM graveDescription where graveyardId='" + gid + "'", con);
                cmd.CommandType = CommandType.Text;
                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    name = Convert.ToString(rdr["graveyardName"]);

                }
            }
            return name;
        }

        public List<Booking> fetchBookingInfo(int uId)
        {
            List<Booking> bookingViewList = new List<Booking>();
            int gid;
            string gn;
            using (SqlConnection con = new SqlConnection(connection))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Booking where userId='" + uId.ToString() + "'", con);
                cmd.CommandType = CommandType.Text;
                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var b = new Booking();
     
                    gid = Convert.ToInt32(rdr["GraveyardId"]);
                    gn = GraveyardName(gid);
                    b.OwnerId = gn.ToString();
                    b.GraveyardId = Convert.ToInt32(rdr["GraveyardId"]);
                    b.GravePlotId = Convert.ToInt32(rdr["GravePlotId"]);
                    b.Date = rdr["Date"].ToString();
                    bookingViewList.Add(b);
                }
            }
            return bookingViewList;
        }

        public int getPlotCount(int GraveyardId)
        {
            int plotCount = 0;

            using (SqlConnection con = new SqlConnection(connection))
            {
                //SqlCommand cmd = new SqlCommand("SELECT loginCount FROM admin where adminPhone='" + adminPhone + "'", con);
                SqlCommand cmd = new SqlCommand("SELECT COUNT(GravePlotId) as x FROM Booking WHERE GraveyardId = '" + GraveyardId + "'",con);
                cmd.CommandType = CommandType.Text;
                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    plotCount = Convert.ToInt32(rdr["x"]);

                }
            }
            return plotCount;
        }

        public int getAvailableNoOfPlots(int graveyardId)
        {
            //This actually stores no of plots
            int available = 0;

            using (SqlConnection con = new SqlConnection(connection))
            {
                SqlCommand cmd = new SqlCommand("SELECT numberOfPlots FROM graveDescription where graveyardId='" + graveyardId + "'", con);
                cmd.CommandType = CommandType.Text;
                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {

                    available = Convert.ToInt32(rdr["numberOfPlots"]);

                }
            }
            return available;
        }
        public void updateAvailablePlots(int gId,int cnt,int available)
        {
            //This available actually stores no of plots
            int x = 0;
            x = available - cnt;
            using (SqlConnection sqlcon = new SqlConnection(connection))
            {
                //string sqlquery = "insert into graveDescription(graveyardName,authorName,numberOfPlots,plotPrice,contact,availablePlot,location,posterImage,adminId) values ('" + grave.graveyardName + "','" + grave.authorName + "','" + grave.numberOfPlots + "','" + grave.plotPrice + "','" + grave.contact + "','" + grave.availablePlot + "','" + grave.location + "','" + grave.posterImage + "','" + grave.adminId + "')";
                string sqlquery = ("UPDATE graveDescription SET availablePlot = '" +x+ "' WHERE graveyardId = '" + gId + "'");
                using (SqlCommand sqlcom = new SqlCommand(sqlquery, sqlcon))
                {
                    sqlcon.Open();
                    sqlcom.ExecuteNonQuery();
                }
            }
        }
        public void updatePlotNoInBooking(int pid)
        {
            int gid = -1;
            using (SqlConnection sqlcon = new SqlConnection(connection))
            { 
                string sqlquery = ("UPDATE Booking SET GravePlotId = '" + pid + "' WHERE GravePlotId = '" + gid + "'");
                using (SqlCommand sqlcom = new SqlCommand(sqlquery, sqlcon))
                {
                    sqlcon.Open();
                    sqlcom.ExecuteNonQuery();
                }
            }
        }
        public void updatePlotNoInOwner(int pid)
        {
            int gid = -1;
            using (SqlConnection sqlcon = new SqlConnection(connection))
            {
                string sqlquery = ("UPDATE owner SET plotId = '" + pid + "' WHERE plotId = '" + gid + "'");
                using (SqlCommand sqlcom = new SqlCommand(sqlquery, sqlcon))
                {
                    sqlcon.Open();
                    sqlcom.ExecuteNonQuery();
                }
            }
        }


        public List<string> fetchBookedPlots(int gid)
        {
            //List<Temp> plotList = new List<Temp>();
            //List<Temp> TotalplotList = new List<Temp>();
            //actually unbooked plots are fetched here
             var plotList = new List<string>();
            var TotalplotList = new List<string>();

            using (SqlConnection con = new SqlConnection(connection))
            {
                SqlCommand cmd = new SqlCommand("SELECT GravePlotId FROM Booking where GraveyardId = '" + gid + "'", con);
                cmd.CommandType = CommandType.Text;
                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    plotList.Add(Convert.ToString(rdr["GravePlotId"]));

                }
                con.Close();
                
                int total = 0;
                SqlCommand cmd1 = new SqlCommand("SELECT numberOfPlots FROM graveDescription where graveyardId = '" + gid + "'", con);
                cmd1.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader rdr1 = cmd1.ExecuteReader();
                while (rdr1.Read())
                {
                    total = Convert.ToInt32(rdr1["numberOfPlots"]);

                }
                con.Close();

                for (int i=0;i<total;i++)
                {
                    int temp = i + 1;
                    string temp2 = Convert.ToString(temp);
                    TotalplotList.Add(temp2);
                }

                foreach(var number in plotList)
                {
                    TotalplotList.Remove(number);
                }
            }
            return TotalplotList;
        }

        public void addUnbookedPlots(List<string> l)
        {

            using (SqlConnection sqlcon = new SqlConnection(connection))
            {
                foreach (var p in l)
                {
                    sqlcon.Close();
                    string sqlquery = "insert into Temp(PlotNo)values('" + p + "')";
                    using (SqlCommand sqlcom = new SqlCommand(sqlquery, sqlcon))
                    {
                        sqlcon.Open();
                        sqlcom.ExecuteNonQuery();
                    }
                }
            }
        }
      
        public List<Temp> showUnbookedPlots()
        {
            List<Temp> plotList = new List<Temp>();

            using (SqlConnection sqlcon = new SqlConnection(connection))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Temp", sqlcon);
                cmd.CommandType = CommandType.Text;
                sqlcon.Open();

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var plot = new Temp();
                    plot.PlotNo = Convert.ToString(rdr["PlotNo"]);
                    plotList.Add(plot);

                }

            }
            return plotList;
        }

        public void DeleteTempData()
        {
            using (SqlConnection sqlcon = new SqlConnection(connection))
            {

                string sqlquery = "DELETE FROM TEMP;";
                using (SqlCommand sqlcom = new SqlCommand(sqlquery, sqlcon))
                {
                    sqlcon.Open();
                    sqlcom.ExecuteNonQuery();
                }
            }
        }

        public int getPlotPrice(int gid)
        {
            int grid = 0;
           
            using (SqlConnection con = new SqlConnection(connection))
            {
                SqlCommand cmd = new SqlCommand("SELECT plotPrice FROM graveDescription where graveyardId='" + gid + "'", con);
                cmd.CommandType = CommandType.Text;
                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    grid = Convert.ToInt32(rdr["plotPrice"]);

                }
            }
            return grid;
        }
        public void addContact(Contact contact)
        {
            using (SqlConnection sqlcon = new SqlConnection(connection))
            {
                string sqlquery = "insert into contact(name1,contact1,feedback1) values ('" + contact.name1 + "','" + contact.contact1 + "','" + contact.feedback1 + "')";
                using (SqlCommand sqlcom = new SqlCommand(sqlquery, sqlcon))
                {
                    sqlcon.Open();
                    sqlcom.ExecuteNonQuery();
                }
            }
        }

        public void addgraveDescription(graveDescription grave)
        {  
            using (SqlConnection sqlcon = new SqlConnection(connection))
            {
                string sqlquery = "insert into graveDescription(graveyardName,authorName,numberOfPlots,plotPrice,contact,availablePlot,location,posterImage,adminId) values ('" + grave.graveyardName + "','" + grave.authorName + "','" + grave.numberOfPlots + "','" + grave.plotPrice + "','" + grave.contact + "','" + grave.availablePlot + "','" + grave.location + "','" + grave.posterImage + "','" + grave.adminId + "')";
                using (SqlCommand sqlcom = new SqlCommand(sqlquery, sqlcon))
                {
                    sqlcon.Open();
                    sqlcom.ExecuteNonQuery();
                }
            }
        }
        public List<graveDescription> fetchgrave()
        {
            List<graveDescription> graveList = new List<graveDescription>();

            using (SqlConnection con = new SqlConnection(connection))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM graveDescription", con);
                cmd.CommandType = CommandType.Text;
                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var grave = new graveDescription();

                    grave.graveyardId = Convert.ToInt32(rdr["graveyardId"]);
                    grave.graveyardName = rdr["graveyardName"].ToString();
                    grave.authorName = rdr["authorName"].ToString();
                    grave.numberOfPlots = Convert.ToInt32(rdr["numberOfPlots"]);
                    grave.plotPrice = Convert.ToInt32(rdr["plotPrice"]);
                    grave.contact = rdr["contact"].ToString();
                    grave.availablePlot = Convert.ToInt32(rdr["availablePlot"]);
                    grave.location = rdr["location"].ToString();
                    grave.posterImage = rdr["posterImage"].ToString();
                    grave.adminId= Convert.ToInt32(rdr["adminId"]);
                    graveList.Add(grave);
                    
                }
            }
            return graveList;
        }
        

       
        public List<graveDescription> fetchgrave(String searchKey)
        {
            List<graveDescription> graveList = new List<graveDescription>();

            using (SqlConnection con = new SqlConnection(connection))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM graveDescription where graveyardName LIKE '" + searchKey + "' OR authorName LIKE '" + searchKey + "'", con);
                cmd.CommandType = CommandType.Text;
                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var grave = new graveDescription();

                    grave.graveyardId = Convert.ToInt32(rdr["graveyardId"]);
                    grave.graveyardName = rdr["graveyardName"].ToString();
                    grave.numberOfPlots = Convert.ToInt32(rdr["numberOfPlots"]);
                    grave.plotPrice = Convert.ToInt32(rdr["plotPrice"]);
                    grave.contact = rdr["contact"].ToString();
                    grave.availablePlot = Convert.ToInt32(rdr["availablePlot"]);
                    grave.posterImage = rdr["posterImage"].ToString();
                    grave.location = rdr["location"].ToString();
                    grave.adminId = Convert.ToInt32(rdr["adminId"]);
                    graveList.Add(grave);
                }
            }
            return graveList;
        }
        public List<graveDescription> fetchgrave(int graveyardId)
        {
            List<graveDescription> graveList = new List<graveDescription>();

            using (SqlConnection con = new SqlConnection(connection))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM graveDescription where graveyardId = '" + graveyardId + "'", con);
                cmd.CommandType = CommandType.Text;
                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var grave = new graveDescription();

                    grave.graveyardId = Convert.ToInt32(rdr["graveyardId"]);
                    grave.graveyardName = rdr["graveyardName"].ToString();
                    grave.numberOfPlots = Convert.ToInt32(rdr["numberOfPlots"]);
                    grave.plotPrice = Convert.ToInt32(rdr["plotPrice"]);
                    grave.contact = rdr["contact"].ToString();
                    grave.availablePlot = Convert.ToInt32(rdr["availablePlot"]);
                    grave.posterImage = rdr["posterImage"].ToString();
                    grave.location = rdr["location"].ToString();
                    grave.adminId = Convert.ToInt32(rdr["adminId"]);
                    graveList.Add(grave);
                }
            }
            return graveList;
        }
        public List<graveDescription> fetchgraveAsPerAdminId(int adminId)
        {
            List<graveDescription> graveList = new List<graveDescription>();

            using (SqlConnection con = new SqlConnection(connection))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM graveDescription where adminId = '" + adminId + "'", con);
                cmd.CommandType = CommandType.Text;
                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var grave = new graveDescription();

                    grave.graveyardId = Convert.ToInt32(rdr["graveyardId"]);
                    grave.graveyardName = rdr["graveyardName"].ToString();
                    grave.numberOfPlots = Convert.ToInt32(rdr["numberOfPlots"]);
                    grave.plotPrice = Convert.ToInt32(rdr["plotPrice"]);
                    grave.contact = rdr["contact"].ToString();
                    grave.availablePlot = Convert.ToInt32(rdr["availablePlot"]);
                    grave.posterImage = rdr["posterImage"].ToString();
                    grave.location = rdr["location"].ToString();
                    grave.adminId = Convert.ToInt32(rdr["adminId"]);
                    graveList.Add(grave);
                }
            }
            return graveList;
        }
        public List<owner> fetchOwnerInfo()
        {
            List<owner> ownerViewList = new List<owner>();

            using (SqlConnection con = new SqlConnection(connection))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM owner", con);
                cmd.CommandType = CommandType.Text;
                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var ow = new owner();
                    ow.ownerId = Convert.ToInt32(rdr["ownerId"]);
                    ow.fullName = rdr["fullName"].ToString();
                    ow.gender = rdr["gender"].ToString();
                    ow.fatherName = rdr["fatherName"].ToString();
                    ow.motherName = rdr["motherName"].ToString();
                    ow.profession = rdr["profession"].ToString();
                    ow.phone = rdr["phone"].ToString();
                    ow.email = rdr["email"].ToString();
                    ow.address = rdr["address"].ToString();
                    ow.photo = rdr["photo"].ToString();
                    ow.nid = rdr["nid"].ToString();
                    ow.dateOfBirth = rdr["dateOfBirth"].ToString();
                    ow.plotId = Convert.ToInt32(rdr["plotId"]);
                    ow.totalPrice = Convert.ToInt32(rdr["totalPrice"]);
                    ow.bankName = rdr["bankName"].ToString();
                    ow.branchName = rdr["branchName"].ToString();
                    ow.branchAddress = rdr["branchAddress"].ToString();
                    ow.dd = rdr["dd"].ToString();
                    ow.adminId = Convert.ToInt32(rdr["adminId"]);

                    ownerViewList.Add(ow);
                }
            }
            return ownerViewList;
        }

        public List<owner> fetchOwnerInfoAsPerUserId(int uid)
        {
            List<owner> ownerViewList = new List<owner>();

            using (SqlConnection con = new SqlConnection(connection))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM owner where userId='" + uid.ToString() + "'", con);
                cmd.CommandType = CommandType.Text;
                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var ow = new owner();
                    ow.ownerId = Convert.ToInt32(rdr["ownerId"]);
                    ow.fullName = rdr["fullName"].ToString();
                    ow.gender = rdr["gender"].ToString();
                    ow.fatherName = rdr["fatherName"].ToString();
                    ow.motherName = rdr["motherName"].ToString();
                    ow.profession = rdr["profession"].ToString();
                    ow.phone = rdr["phone"].ToString();
                    ow.email = rdr["email"].ToString();
                    ow.address = rdr["address"].ToString();
                    ow.photo = rdr["photo"].ToString();
                    ow.nid = rdr["nid"].ToString();
                    ow.dateOfBirth = rdr["dateOfBirth"].ToString();
                    ow.plotId = Convert.ToInt32(rdr["plotId"]);
                    ow.totalPrice = Convert.ToInt32(rdr["totalPrice"]);
                    ow.bankName = rdr["bankName"].ToString();
                    ow.branchName = rdr["branchName"].ToString();
                    ow.branchAddress = rdr["branchAddress"].ToString();
                    ow.dd = rdr["dd"].ToString();
                    ow.adminId = Convert.ToInt32(rdr["adminId"]);

                    ownerViewList.Add(ow);
                }
            }
            return ownerViewList;
        }

        public string getOwnerName(int uid)
        {
            string name="";

            using (SqlConnection con = new SqlConnection(connection))
            {
                SqlCommand cmd = new SqlCommand("SELECT fullName FROM owner where userId='" + uid + "'", con);
                cmd.CommandType = CommandType.Text;
                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    name = Convert.ToString(rdr["fullName"]);

                }
            }      
            return name;
        }
        public string getOwnerGender(int uid)
        {
            string name = "";

            using (SqlConnection con = new SqlConnection(connection))
            {
                SqlCommand cmd = new SqlCommand("SELECT gender FROM owner where userId='" + uid + "'", con);
                cmd.CommandType = CommandType.Text;
                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    name = Convert.ToString(rdr["gender"]);

                }
            }
            return name;
        }
        public string getOwnerFatherName(int uid)
        {
            string name = "";

            using (SqlConnection con = new SqlConnection(connection))
            {
                SqlCommand cmd = new SqlCommand("SELECT fatherName FROM owner where userId='" + uid + "'", con);
                cmd.CommandType = CommandType.Text;
                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    name = Convert.ToString(rdr["fatherName"]);

                }
            }
            return name;
        }
        public string getOwnerMotherName(int uid)
        {
            string name = "";

            using (SqlConnection con = new SqlConnection(connection))
            {
                SqlCommand cmd = new SqlCommand("SELECT motherName FROM owner where userId='" + uid + "'", con);
                cmd.CommandType = CommandType.Text;
                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    name = Convert.ToString(rdr["motherName"]);

                }
            }
            return name;
        }
        public string getOwnerProfession(int uid)
        {
            string name = "";

            using (SqlConnection con = new SqlConnection(connection))
            {
                SqlCommand cmd = new SqlCommand("SELECT profession FROM owner where userId='" + uid + "'", con);
                cmd.CommandType = CommandType.Text;
                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    name = Convert.ToString(rdr["profession"]);

                }
            }
            return name;
        }
        public string getOwnerPhone(int uid)
        {
            string name = "";

            using (SqlConnection con = new SqlConnection(connection))
            {
                SqlCommand cmd = new SqlCommand("SELECT phone FROM owner where userId='" + uid + "'", con);
                cmd.CommandType = CommandType.Text;
                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    name = Convert.ToString(rdr["phone"]);

                }
            }
            return name;
        }
        public string getOwnerEmail(int uid)
        {
            string name = "";

            using (SqlConnection con = new SqlConnection(connection))
            {
                SqlCommand cmd = new SqlCommand("SELECT email FROM owner where userId='" + uid + "'", con);
                cmd.CommandType = CommandType.Text;
                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    name = Convert.ToString(rdr["email"]);

                }
            }
            return name;
        }
        public string getOwnerAddress(int uid)
        {
            string name = "";

            using (SqlConnection con = new SqlConnection(connection))
            {
                SqlCommand cmd = new SqlCommand("SELECT address FROM owner where userId='" + uid + "'", con);
                cmd.CommandType = CommandType.Text;
                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    name = Convert.ToString(rdr["address"]);

                }
            }
            return name;
        }
        public string getOwnerNid(int uid)
        {
            string name = "";

            using (SqlConnection con = new SqlConnection(connection))
            {
                SqlCommand cmd = new SqlCommand("SELECT nid FROM owner where userId='" + uid + "'", con);
                cmd.CommandType = CommandType.Text;
                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    name = Convert.ToString(rdr["nid"]);

                }
            }
            return name;
        }
        public string getOwnerDateOfBirth(int uid)
        {
            string name = "";

            using (SqlConnection con = new SqlConnection(connection))
            {
                SqlCommand cmd = new SqlCommand("SELECT dateOfBirth FROM owner where userId='" + uid + "'", con);
                cmd.CommandType = CommandType.Text;
                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    name = Convert.ToString(rdr["dateOfBirth"]);

                }
            }
            return name;
        }

        public List<owner> fetchOwnerInfo(int adminId)
        {
            List<owner> ownerViewList = new List<owner>();
            int gid = 0;
            string gn = null;
            using (SqlConnection con = new SqlConnection(connection))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM owner where adminId='" + adminId.ToString() + "'", con);
                cmd.CommandType = CommandType.Text;
                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var ow = new owner();
                    ow.ownerId = Convert.ToInt32(rdr["ownerId"]);
                    ow.fullName = rdr["fullName"].ToString();
                    ow.gender = rdr["gender"].ToString();
                    ow.fatherName = rdr["fatherName"].ToString();
                    ow.motherName = rdr["motherName"].ToString();
                    ow.profession = rdr["profession"].ToString();
                    ow.phone = rdr["phone"].ToString();
                    ow.email = rdr["email"].ToString();
                    ow.address = rdr["address"].ToString();
                    gid = Convert.ToInt32(rdr["graveyardId"]);
                    gn = GraveyardName(gid);
                    ow.photo = gn.ToString();
                    ow.nid = rdr["nid"].ToString();
                    ow.dateOfBirth = rdr["dateOfBirth"].ToString();
                    ow.plotId = Convert.ToInt32(rdr["plotId"]);
                    ow.totalPrice = Convert.ToInt32(rdr["totalPrice"]);
                    ow.bankName = rdr["bankName"].ToString();
                    ow.branchName = rdr["branchName"].ToString();
                    ow.branchAddress = rdr["branchAddress"].ToString();
                    ow.dd = rdr["dd"].ToString();
                    ow.adminId = Convert.ToInt32(rdr["adminId"]);

                    ownerViewList.Add(ow);
                }
            }
            return ownerViewList;
        }
        public List<owner> fetchOwnerInfo(String searchKey)
        {
            List<owner> ownerViewList = new List<owner>();

            using (SqlConnection con = new SqlConnection(connection))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM owner where fullName LIKE '" + searchKey +"'", con);
                cmd.CommandType = CommandType.Text;
                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var ow = new owner();

                    ow.ownerId = Convert.ToInt32(rdr["ownerId"]);
                    ow.fullName = rdr["fullName"].ToString();
                    ow.gender = rdr["gender"].ToString();
                    ow.fatherName = rdr["fatherName"].ToString();
                    ow.motherName = rdr["motherName"].ToString();
                    ow.profession = rdr["profession"].ToString();
                    ow.phone = rdr["phone"].ToString();
                    ow.email = rdr["email"].ToString();
                    ow.address = rdr["address"].ToString();
                    ow.photo = rdr["photo"].ToString();
                    ow.nid = rdr["nid"].ToString();
                    ow.dateOfBirth = rdr["dateOfBirth"].ToString();
                    ow.plotId = Convert.ToInt32(rdr["plotId"]);
                    ow.totalPrice = Convert.ToInt32(rdr["totalPrice"]);
                    ow.bankName = rdr["bankName"].ToString();
                    ow.branchName = rdr["branchName"].ToString();
                    ow.branchAddress = rdr["branchAddress"].ToString();
                    ow.dd = rdr["dd"].ToString();
                    ow.adminId = Convert.ToInt32(rdr["adminId"]);
                    ownerViewList.Add(ow);
                }
            }
            return ownerViewList;
        }
        public List<User> fetchUser()
        {
            List<User> userList = new List<User>();

            using (SqlConnection con = new SqlConnection(connection))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Users", con);
                cmd.CommandType = CommandType.Text;
                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var user = new User();

                    user.userId = Convert.ToInt32(rdr["userId"]);
                    user.userName = rdr["userName"].ToString();
                    user.email = rdr["email"].ToString();
                    user.phone = rdr["phone"].ToString();                
                    userList.Add(user);
                }
            }
            return userList;
        }
        public List<admin> fetchAdmin()
        {
            List<admin> adminList = new List<admin>();

            using (SqlConnection con = new SqlConnection(connection))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM admin", con);
                cmd.CommandType = CommandType.Text;
                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var admin = new admin();

                    admin.adminId = Convert.ToInt32(rdr["adminId"]);
                    admin.adminName = rdr["adminName"].ToString();
                    admin.adminEmail = rdr["adminEmail"].ToString();
                    admin.adminPhone = rdr["adminPhone"].ToString();
                    
                    adminList.Add(admin);
                }
            }
            return adminList;
        }
        public int loginCount(String adminPhone)
        {
            int loginCount=0;

            using (SqlConnection con = new SqlConnection(connection))
            {
                SqlCommand cmd = new SqlCommand("SELECT loginCount FROM admin where adminPhone='"+ adminPhone+"'", con);
                cmd.CommandType = CommandType.Text;
                con.Open();
               
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                   

                    loginCount = Convert.ToInt32(rdr["loginCount"]);
                   
                }
            }
            return loginCount;
        }
        
        public int getAdminId(String adminPhone,String adminPassword)
        {
            int adminId = 0;
            using (SqlConnection con = new SqlConnection(connection))
            {
                SqlCommand cmd = new SqlCommand("SELECT adminId FROM admin where adminPhone='" + adminPhone + "'" + "and adminPassword='" + adminPassword + "'", con);
                cmd.CommandType = CommandType.Text;
                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    adminId = Convert.ToInt32(rdr["adminId"]);

                }
            }
            return adminId;
        }
        public int getAdminIdFromGrave(int graveyardId)
        {
            int adminId = 0;
            using (SqlConnection con = new SqlConnection(connection))
            {
                SqlCommand cmd = new SqlCommand("SELECT adminId FROM graveDescription where graveyardId='" + graveyardId + "'", con);
                cmd.CommandType = CommandType.Text;
                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    adminId = Convert.ToInt32(rdr["adminId"]);

                }
            }
            return adminId;
        }
        public int getUserId(String userPhone, String userPassword)
        {
            int userId = 0;
            using (SqlConnection con = new SqlConnection(connection))
            {
                SqlCommand cmd = new SqlCommand("SELECT userId FROM Users where phone='" + userPhone + "'" + "and password='" + userPassword + "'", con);
                cmd.CommandType = CommandType.Text;
                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    userId = Convert.ToInt32(rdr["userId"]);

                }
            }
            return userId;
        }
        public void updateCount(String adminPhone, int count)
        {
            int loginCount = 0;


            using (SqlConnection con = new SqlConnection(connection))
            {
                SqlCommand cmd = new SqlCommand("UPDATE admin set loginCount="+count+"WHERE adminPhone ='"+adminPhone+"'", con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                cmd.ExecuteNonQuery();

            }
            
        }
        public List<admin> fetchAdmin(int adminId)
        {
            List<admin> adminList = new List<admin>();

            using (SqlConnection con = new SqlConnection(connection))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM admin where adminId='" + adminId + "'", con);
                cmd.CommandType = CommandType.Text;
                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var admin = new admin();

                    admin.adminId = Convert.ToInt32(rdr["adminId"]);
                    admin.adminName = rdr["adminName"].ToString();
                    admin.adminEmail = rdr["adminEmail"].ToString();
                    admin.adminPhone = rdr["adminPhone"].ToString(); 
                    adminList.Add(admin);
                }
            }
            return adminList;
        }
        public List<admin> fetchAdmin(String searchKey)
        {
            List<admin> adminList = new List<admin>();

            using (SqlConnection con = new SqlConnection(connection))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM admin where adminName LIKE '" + searchKey + "'", con);
                cmd.CommandType = CommandType.Text;
                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var admin = new admin();

                    admin.adminId = Convert.ToInt32(rdr["adminId"]);
                    admin.adminName = rdr["adminName"].ToString();
                    admin.adminEmail = rdr["adminEmail"].ToString();
                    admin.adminPhone = rdr["adminPhone"].ToString();
                    adminList.Add(admin);
                }
            }
            return adminList;
        }
        internal bool FindByAdmin(admin admin)
        {
            bool success = false;
            using (SqlConnection sqlcon = new SqlConnection(connection))
            {
                string sqlquery = "SELECT * FROM admin WHERE adminPhone='" + admin.adminPhone + "' AND adminPassword='" + admin.adminPassword + "'";
                using (SqlCommand sqlcom = new SqlCommand(sqlquery, sqlcon))
                {
                    try
                    {
                        sqlcon.Open();
                        SqlDataReader reader = sqlcom.ExecuteReader();
                        if (reader.HasRows)
                        {
                            success = true;
                        }
                        else
                        {
                            success = false;
                        }
                        reader.Close();
                    }
                    catch (Exception e)
                    {
                        ;
                    }
                }

            }
            return success;
        }
        public List<User> fetchUser(int userId)
        {
            List<User> userList = new List<User>();

            using (SqlConnection con = new SqlConnection(connection))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Users where userId='"+userId+"'", con);
                cmd.CommandType = CommandType.Text;
                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var user = new User();

                    user.userId = Convert.ToInt32(rdr["userId"]);
                    user.userName = rdr["userName"].ToString();
                    user.email = rdr["email"].ToString();
                    user.phone = rdr["phone"].ToString();
                    userList.Add(user);
                }
            }
            return userList;
        }
        internal bool FindByUser(User user)
        {
            bool success = false;
            using (SqlConnection sqlcon = new SqlConnection(connection))
            {
                string sqlquery = "SELECT * FROM Users WHERE phone='"+user.phone+"' AND password='"+user.password+"'";
                using (SqlCommand sqlcom = new SqlCommand(sqlquery, sqlcon))
                {
                    try 
                    { 
                    sqlcon.Open();
                    SqlDataReader reader = sqlcom.ExecuteReader();
                    if(reader.HasRows)
                    {
                        success = true;
                    }
                    else
                    {
                        success = false;
                    }
                    reader.Close();
                    }
                    catch(Exception e)
                    {
                        ;
                    }
                }

            }
            return success;
        }
    }
}