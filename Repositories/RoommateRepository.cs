using Microsoft.Data.SqlClient;
using Roommate43.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Roommate43.Repositories
{
    public class RoommateRepository : BaseRepository
    {
        //connectioning the base repo
        public RoommateRepository(string connectionString) : base(connectionString) { }

        public List<Roommate> GetAll()
        {
            //SqlConnection = Connection
            using (SqlConnection conn = Connection)
            {
                //opening the connection 
                conn.Open();

                //cmd Object is type SqlCommand = conn.CreateCommand
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    // //This is saying whats in the query 
                    cmd.CommandText = "Select Id, FirstName, LastName, RentPortion, MoveInDate From Roommate ";
                    //this is running the query
                    SqlDataReader reader = cmd.ExecuteReader();

                    //A list to hold the roommates we get back from the database
                    List<Roommate> roommates = new List<Roommate>();

                    while (reader.Read())
                    {
                        //this is creating the idColumnPosition. This is the number associted with column "Id"
                        int idColumnPosition = reader.GetOrdinal("Id");
                        // reading the value of the column 
                        int idValue = reader.GetInt32(idColumnPosition);

                        int firstNameColumnPosition = reader.GetOrdinal("FirstName");
                        string firstNameValue = reader.GetString(firstNameColumnPosition);


                        int lastNameColumnPosition = reader.GetOrdinal("LastName");
                        string lastNameValue = reader.GetString(lastNameColumnPosition);

                        int rentPortionColumnPosition = reader.GetOrdinal("RentPortion"); 
                        int rentPortion = reader.GetInt32(rentPortionColumnPosition);

                        int moveInDateColumnPosition = reader.GetOrdinal("MoveInDate");
                        DateTime moveInDateValue = reader.GetDateTime(moveInDateColumnPosition);

                        Roommate roommate = new Roommate
                        {
                            Id = idValue,
                            Firstname = firstNameValue,
                            Lastname = lastNameValue,
                            RentPortion = rentPortion,
                            MovedInDate = moveInDateValue
                        };

                        roommates.Add(roommate);
                    }
                    // closing the  connection 
                    reader.Close();

                    return roommates;
                }
            }
        }
       
        //public Chore GetById(int id)
        //{
        //    using (SqlConnection conn = Connection)
        //    {
        //        conn.Open();
        //        using(SqlCommand cmd = conn.CreateCommand())
        //        {
        //            cmd.CommandText = "Select FirstName, LastName, RentPortion, MoveInDate, RoomId FROM Roommate 
        //                LEFT JOIN Room on Roommate.RoodId = Room.id
        //                WHERE Id = @Roommate.id";
                        
        //        }
        //    }
        //}
    }
}
