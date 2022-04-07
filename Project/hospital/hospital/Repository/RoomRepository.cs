using Model;
using System.Collections.Generic;
using System;

namespace Repository
{
   public class RoomRepository
   {
      public bool Create(Model.Room room)
      {
         throw new NotImplementedException();
      }
      
      public Room FindRoomById(String id)
      {
         throw new NotImplementedException();
      }
      
      public List<Room> FindAll()
      {
         throw new NotImplementedException();
      }
      
      public bool UpdateById(String id, Model.Room room)
      {
         throw new NotImplementedException();
      }
      
      public bool DeleteById(String id)
      {
         throw new NotImplementedException();
      }
      
      public System.Collections.Generic.List<Room> room;
      
      
      public System.Collections.Generic.List<Room> Room
      {
         get
         {
            if (room == null)
               room = new System.Collections.Generic.List<Room>();
            return room;
         }
         set
         {
            RemoveAllRoom();
            if (value != null)
            {
               foreach (Model.Room oRoom in value)
                  AddRoom(oRoom);
            }
         }
      }
      
      
      public void AddRoom(Model.Room newRoom)
      {
         if (newRoom == null)
            return;
         if (this.room == null)
            this.room = new System.Collections.Generic.List<Room>();
         if (!this.room.Contains(newRoom))
            this.room.Add(newRoom);
      }
      
      
      public void RemoveRoom(Model.Room oldRoom)
      {
         if (oldRoom == null)
            return;
         if (this.room != null)
            if (this.room.Contains(oldRoom))
               this.room.Remove(oldRoom);
      }
      
      
      public void RemoveAllRoom()
      {
         if (room != null)
            room.Clear();
      }
      public FileHandler.RoomFileHandler roomFileHandler;
   
   }
}