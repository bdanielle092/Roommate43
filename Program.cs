using Roommate43.Models;
using Roommate43.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Roommate43
{
    class Program
    {
        //  This is the address of the database.
        //  We define it here as a constant since it will never change.
        private const string CONNECTION_STRING = @"server=localhost\SQLExpress;database=Roommates;integrated security=true";

        static void Main(string[] args)
        {
            RoomRepository roomRepo = new RoomRepository(CONNECTION_STRING);
            ChoreRepository choreRepo = new ChoreRepository(CONNECTION_STRING);
            RoommateRepository roommateRepo = new RoommateRepository(CONNECTION_STRING);

            bool runProgram = true;
            while (runProgram)
            {
                string selection = GetMenuSelection();
                
                switch (selection)
                {
                    case ("Show all rooms"):
                        ShowAllRooms(roomRepo);
                        break;

                    case ("Search for room"):
                        ShowOneRoom(roomRepo);
                        break;

                    case ("Add a room"):
                        InsertRoom(roomRepo);
                        break;

                    case ("Update a room"):
                        UpdateARoom(roomRepo);
                        break;
                    case ("Delete a room"):
                        DeleteARoom(roomRepo);
                        break;

                    case ("Show all chores"):
                        ShowAllChores(choreRepo);
                        break;

                    case ("Search for chore"):
                        ShowOneChore(choreRepo);
                        break;

                    case ("Add a chore"):
                        InsertChore(choreRepo);
                        break;

                    case ("Update a chore"):
                        UpdateAChore(choreRepo);
                        break;

                    case ("Delete a chore"):
                        DeleteAChore(choreRepo);
                        break;
                   
                    case ("Show UnassignedChore"):
                        ShowUnassignedChore(choreRepo);
                        break;

                    //case ("Assign chore to roommate"):
                    //    AssignChoreToRoommate(choreRepo);
                    //    break;

                    case ("Find a roommate"):
                        FindARoommate(roommateRepo);
                        break;

                    case ("Add a roommate"):
                        AddARoommate(roommateRepo);
                        break;

                    case ("Exit"):
                        runProgram = false;
                        break;
                }
            }

        }

        //room
        static void ShowAllRooms(RoomRepository roomRepo)
        {
            List<Room> rooms = roomRepo.GetAll();
            foreach (Room r in rooms)
            {
                Console.WriteLine($"[{r.Id}] {r.Name} Max Occ({r.MaxOccupancy})");
            }
            Console.Write("Press any key to continue");
            Console.ReadKey();
        }

        static void ShowOneRoom(RoomRepository roomRepo)
        {
            Console.Write("Room Id: ");
            int id = int.Parse(Console.ReadLine());

            Room room = roomRepo.GetById(id);

            Console.WriteLine($"{room.Id} - {room.Name} Max Occupancy({room.MaxOccupancy})");
            Console.Write("Press any key to continue");
            Console.ReadKey();
        }

        static void InsertRoom(RoomRepository roomRepo)
        {
            Console.Write("Room name: ");
            string name = Console.ReadLine();

            Console.Write("Max occupancy: ");
            int max = int.Parse(Console.ReadLine());

            Room roomToAdd = new Room()
            {
                Name = name,
                MaxOccupancy = max
            };

            roomRepo.Insert(roomToAdd);

            Console.WriteLine($"{roomToAdd.Name} has been added and assigned an Id of {roomToAdd.Id}");
            Console.Write("Press any key to continue");
            Console.ReadKey();
        }

        static void UpdateARoom(RoomRepository roomRepo)
        {
            List<Room> roomOptions = roomRepo.GetAll();
            foreach (Room r in roomOptions)
            {
                Console.WriteLine($"{r.Id} - {r.Name} Max Occupancy({r.MaxOccupancy})");
            }

            Console.Write("Which room would you like to update? ");
            int selectedRoomId = int.Parse(Console.ReadLine());
            Room selectedRoom = roomOptions.FirstOrDefault(r => r.Id == selectedRoomId);

            Console.Write("New Name: ");
            selectedRoom.Name = Console.ReadLine();

            Console.Write("New Max Occupancy: ");
            selectedRoom.MaxOccupancy = int.Parse(Console.ReadLine());

            roomRepo.Update(selectedRoom);

            Console.WriteLine($"Room has been successfully updated");
            Console.Write("Press any key to continue");
            Console.ReadKey();
        }
        static void DeleteARoom(RoomRepository roomRepo)
        {
            List<Room> roomOptions = roomRepo.GetAll();
            foreach (Room r in roomOptions)
            {
                Console.WriteLine($"{r.Id} - {r.Name} Max Occupancy({r.MaxOccupancy})");
            }

            Console.Write("Which room would you like to delete? ");
            int roomToDeleteId = int.Parse(Console.ReadLine());
            roomRepo.Delete(roomToDeleteId);
            Console.WriteLine("Room has been deleted");
            Console.Write("Press any key to continue");
            Console.ReadKey();
        }

        //Chores
        static void ShowAllChores(ChoreRepository choreRepo)
        {
            List<Chore> chores = choreRepo.GetAll();
            foreach (Chore chore in chores)
            {
                Console.WriteLine($"[{chore.Id}] {chore.Name}");
            }
            Console.Write("Press any key to continue");
            Console.ReadKey();
        }

        static void ShowOneChore(ChoreRepository choreRepo)
        {
            Console.Write("Chore Id: ");
            int id = int.Parse(Console.ReadLine());

           Chore chore = choreRepo.GetById(id);

            Console.WriteLine($"{chore.Id} - {chore.Name} ");
            Console.Write("Press any key to continue");
            Console.ReadKey();
        }
        static void InsertChore(ChoreRepository choreRepo)
        {
            Console.Write("Chore name: ");
            string name = Console.ReadLine();

            

            Chore choreToAdd = new Chore()
            {
                Name = name,
               
            };

            choreRepo.Insert(choreToAdd);

            Console.WriteLine($"{choreToAdd.Name} has been added and assigned an Id of {choreToAdd.Id}");
            Console.Write("Press any key to continue");
            Console.ReadKey();
        }

        static void UpdateAChore(ChoreRepository choreRepo)
        {
            List<Chore> choreOptions = choreRepo.GetAll();
            foreach (Chore chore in choreOptions)
            {
                Console.WriteLine($"{chore.Id} - {chore.Name}");
            }
            Console.WriteLine("Which chore would you like to update?");
            int selectedChoreId = int.Parse(Console.ReadLine());
            Chore selectedChore = choreOptions.FirstOrDefault(chore => chore.Id == selectedChoreId);

            Console.WriteLine("New Name");
            selectedChore.Name = Console.ReadLine();

            choreRepo.Update(selectedChore);

            Console.WriteLine($"Chore has been updated");
            Console.Write("Press any key to continue");
            Console.ReadKey();
        }

        static void DeleteAChore(ChoreRepository choreRepo)
        {
            List<Chore> ChoreOptions = choreRepo.GetAll();
            foreach (Chore chore in ChoreOptions)
            {
                Console.WriteLine($"{chore.Id} - {chore.Name}");
            }

            Console.Write("Which chore would you like to delete? ");
            int choreToDeleteId = int.Parse(Console.ReadLine());
            choreRepo.Delete(choreToDeleteId);
            Console.WriteLine("Chore has been deleted");
            Console.Write("Press any key to continue");
            Console.ReadKey();
        }

        static void ShowUnassignedChore(ChoreRepository choreRepo)
        {
            List<Chore> unassignedChore = choreRepo.GetUnassignedChores();
            foreach (Chore chore in unassignedChore)
            {
                Console.WriteLine($"{chore.Name}");
            }
            Console.Write("Press any key to continue");
            Console.ReadKey();

        }

        //static void AssignChoreToRoommate(ChoreRepository choreRepo)
        //{
        //    List<Chore> allChores = choreRepo.GetAll();
        //    foreach (Chore chore in allChores)
        //    {
        //        Console.WriteLine($"{chore.Id} - {chore.Name}");
        //    }
        //    Console.WriteLine("Select a chore");
        //    int choreIdToAdd = int.Parse(Console.ReadLine());
        //    Chore chore1 = choreRepo.GetById(choreIdToAdd);
        //    List<Roommate> allRoommates = roommateRepo.GetAll();
        //    foreach(Roommate roommate in allRoommates)
        //    {
        //        Console.WriteLine($"{roommate.Id} - {roommate.Firstname} - {roommate.Lastname}");
        //    }
        //    Console.WriteLine("Please select a roommate");
        //    int RoommateIdToAdd = int.Parse(Console.ReadLine());
        //    Roommate roommate1 = roommateRepo.GetById(RoommateIdToAdd);
        //    choreRepo.AssignChore(RoommateIdToAdd, choreIdToAdd);
        //    Console.WriteLine($"{roommate1.Firstname} {roommate1.Lastname} has been assign to {chore1.Name}");
        //    Console.WriteLine("Press any key to continue");
        //    Console.ReadKey();
        //}

        //Roommate
        static void FindARoommate(RoommateRepository roommateRepo)
        {
            Console.Write("Roommate Id:");
            int id = int.Parse(Console.ReadLine());

            Roommate roommate = roommateRepo.GetById(id);
            Console.WriteLine($"FirstName:{roommate.Firstname} RentPortion:{roommate.RentPortion} RoomName:{roommate.Room.Name}");
            Console.Write("Press any key to continue");
            Console.ReadKey();
        }

        static void AddARoommate(RoommateRepository roommateRepo)
        {
            Console.Write("FirstName: ");
            string FirstName = Console.ReadLine();

            Console.Write("LastName: ");
            string LastName = Console.ReadLine();

            Console.Write("RentPortion: ");
            int RentPortion = int.Parse(Console.ReadLine());

            Console.Write("MoveInDate: ");
            DateTime MoveInDate = DateTime.Parse(Console.ReadLine());

            Roommate roommateToAdd = new Roommate()
            {
                Firstname = FirstName,
                Lastname = LastName,
                RentPortion = RentPortion,
                MovedInDate = MoveInDate
            };

            RoommateRepo.Insert(roommateToAdd);

            Console.WriteLine($"{roommateToAdd.Firstname} {roommateToAdd.Lastname} is assigned an Id of {roommateToAdd.Id}. Their rent portion is {roommateToAdd.RentPortion} and their move in date is {roommateToAdd.MovedInDate}");
            Console.Write("Press any key to continue");
            Console.ReadKey();


        }

     

        static string GetMenuSelection()
        {
            Console.Clear();

            List<string> options = new List<string>()
        {
            "Show all rooms",
            "Search for room",
            "Add a room",
             "Update a room",
            "Delete a room",
            "Show all chores",
            "Search for chore",
            "Add a chore",
             "Update a chore",
            "Delete a chore",
            "Show UnassignedChore",
            //"Assign chore to roommate",
             "Find a roommate",
             "Add a roommate",
            "Exit"
        };

            for (int i = 0; i < options.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {options[i]}");
            }

            while (true)
            {
                try
                {
                    Console.WriteLine();
                    Console.Write("Select an option > ");

                    string input = Console.ReadLine();
                    int index = int.Parse(input) - 1;
                    return options[index];
                }
                catch (Exception)
                {

                    continue;
                }
            }

        }
    }
}
