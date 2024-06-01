using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace homework_10_Upcasting_Downcasting.Classes
{
    public class Hotel
    {
        private Room[] rooms = [];
        public string Name { get; }
        public Hotel(string name)
        {
            Name = name;
            while (!HelperStaticClass.NameValidation(name))
            {
                HelperStaticClass.ColoringText("The name can not be empty. Name your room correctly");
                name = Console.ReadLine();
                Name = name;
            }
        }

        public void AddRoom(params Room[] room)
        {
            foreach (Room r in room)
            {
                Array.Resize(ref rooms, rooms.Length + 1);
                rooms[rooms.Length - 1] = r;
                Console.WriteLine($"Room with name {r.Name} was added to our hotel\n");
            }

        }

        public void ShowMyRooms()
        {
            foreach (Room room in rooms)
            {
                room.ShowInfo();
            }
            Console.WriteLine();
        }

        public void MakeReservation(int? id)
        {
            try
            {
                bool isFound = false;

                if (id == null)
                {
                    throw new NullReferenceException("ID can not be null");
                }


                foreach (Room room in rooms)
                {
                    if (room.Id == id)
                    {
                        if (room.IsAvailable)
                        {
                            room.IsAvailable = false;
                            Console.WriteLine($"Congratulations, You have booked the room with ID - {room.Id}");
                            return;
                        }
                        else throw new NotAvailableException($"The room by ID - {room.Id} is already booked");
                    }
                }
                throw new NullReferenceException($"Room by ID - {id} was not found");



            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine(ex.Message);
            }

            catch (NotAvailableException ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }
    }
}
