namespace Async_Inn.Models.Interfaces
{
    public interface IRoom
    {
        Task<Room> Create(Room room);

        // GET All
        Task<List<Room>> GetRooms();

        // GET Room By Id

        Task<Room> GetRoom(int roomId);

        // Update
        Task<Room> UpdateRoom(int id, Room room);

        // Delete 

        Task Delete(int id);
    }
}
