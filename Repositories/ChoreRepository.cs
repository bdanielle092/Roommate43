﻿using Microsoft.Data.SqlClient;
using Roommate43.Models;
using System.Collections.Generic;

namespace Roommate43.Repositories
{
    public class ChoreRepository : BaseRepository
    {
        //Connecting the base Repo
        public ChoreRepository(string connectionString) : base(connectionString) { }

        //getting a list of all the chores
        public List<Chore> GetAll()
        {
            //SqlConnection = Connection Creating a Connection variable 
            using (SqlConnection conn = Connection)
            {
                //opening the connection 
                conn.Open();

                //cmd Object is type SqlCommand = conn.CreateCommand Creating a Command variable 
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    //Selecting what we want from the database
                    //This is saying whats in the query
                    cmd.CommandText = "Select Id, Name From Chore ";

                    //this is sending the query out 
                    SqlDataReader reader = cmd.ExecuteReader();

                    //A list to hold the chores we get back from the database
                    List<Chore> chores = new List<Chore>();

                    //while there is info to read continue
                    while (reader.Read())
                    {
                        //this is creating the idColumnPosition. This is the number associted with column "Id"
                        int idColumnPosition = reader.GetOrdinal("Id");
                        // reading the value of the column 
                        int idValue = reader.GetInt32(idColumnPosition);

                        //this is creating the nameColumnPosition. This is the number associted with column "Name"
                        int nameColumnPosition = reader.GetOrdinal("Name");
                        // reading the value of the column
                        string nameValue = reader.GetString(nameColumnPosition);

                        //creating a chore object 
                        Chore chore = new Chore
                        {
                            Id = idValue,
                            Name = nameValue
                        };

                        //add the chore
                        chores.Add(chore);
                    }
                    // closing the connection 
                    reader.Close();
                    //returning the list of chores
                    return chores;
                }
            }
        }

        //returns a  single chore by id 
        public Chore GetById(int id)
        {
            //SqlConnectin = Connection 
            using(SqlConnection conn = Connection)
            {
                //opeing the connectin 
                conn.Open();
                //cmd Object is type SqlCommand = conn.CreateCommand
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    
                    //This is saying whats in the query and we are getting the chore by id
                    cmd.CommandText = "Select Name From Chore Where Id = @id";
                    //adds the id value
                    cmd.Parameters.AddWithValue("@id", id);
                    //this is sending the query out 
                    SqlDataReader reader = cmd.ExecuteReader();
                   
                    // chore is null
                    Chore chore = null;
                    //if we only except a single row back from the database we don't need a while loop
                    if (reader.Read())
                    {
                        chore = new Chore
                        {
                            Id = id,
                            Name = reader.GetString(reader.GetOrdinal("Name"))
                        };
                    }
                    //closing the connectin 
                    reader.Close();

                    //returning the chore
                    return chore;
                }
            }
        }

        //adding a room 
        public void Insert(Chore chore)
        {
            //SqlConnectin = Connection.
            using (SqlConnection conn = Connection)
            {
                //opening connection 
                conn.Open();
                //cmd Object is type SqlCommand = conn.CreateCommand. 
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    //we are inserting a chore to the database. The database will give it an id and the value will be name
                    cmd.CommandText = @"INSERT INTO Chore (Name)
                       OUTPUT INSERTED.Id
                       VALUES (@name)";
                    //adds the name value
                    cmd.Parameters.AddWithValue("@name", chore.Name);
                    //run the query and adds a column for the new id ?
                    int id = (int)cmd.ExecuteScalar();

                    chore.Id = id;
                }
            }
        }

        //updates a chore
        public void Update(Chore chore)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE Chore
                                        Set Name = @name
                                    WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@name", chore.Name);
                    cmd.Parameters.AddWithValue("id", chore.Id);

                    //execute this SQL code to update a row
                    cmd.ExecuteNonQuery();
                }
            }
        }

        //delete a chore
        public void Delete(int id)
        {
            using(SqlConnection conn = Connection)
            {
                conn.Open();
                //you must first break the child connection before you can delete the chore, otherwise it will break 
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM RoommateChore WHERE choreId = @choreId";
                    cmd.Parameters.AddWithValue("@choreId", id);
                    cmd.ExecuteNonQuery();
                }
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM Chore WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        //show unassign chores
        public List<Chore> GetUnassignedChores()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"select chore.Id, Name FROM Chore
                       LEFT JOIN RoommateChore on Chore.Id = RoommateChore.ChoreId
                       LEFT JOIN Roommate on Roommate.Id = RoommateChore.RoommateId
                       WHERE RoommateChore.Id IS NULL";
                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Chore> chores = new List<Chore>();

                    while(reader.Read())
                    {
                        int idColumnPositioin = reader.GetOrdinal("Id");
                        int idValue = reader.GetInt32(idColumnPositioin);

                        int nameColumnPosition = reader.GetOrdinal("Name");
                        string nameValue = reader.GetString(nameColumnPosition);

                        Chore chore = new Chore
                        {
                            Id = idValue,
                            Name = nameValue
                        };

                        chores.Add(chore);
                    }

                    reader.Close();
                    return chores;
                }
            }
        }

        //assigns a chore
        public void AssignChore(int roommateId, int choreId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO RoommateChore(RoommateId, ChoreId)
                                      VALUES(@Roommate, @Chore)";
                    //adds the name value
                    cmd.Parameters.AddWithValue("@Roommate", roommateId);
                    cmd.Parameters.AddWithValue("@Chore", choreId);
                    //runs the query
                    cmd.ExecuteScalar();
                }
            }
        }

    }
}
