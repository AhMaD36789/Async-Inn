namespace Async_Inn.Models.Interfaces
{
    public interface IHotel
    {
        Task<Hotel> Create(Hotel hotel);

        // GET All
        Task<List<HotelDTO>> GetHotels();

        // GET Hotel By Id

        Task<HotelDTO> GetHotel(int HotelId);

        // Update
        Task<Hotel> UpdateHotel(int id, Hotel hotel);

        // Delete 

        Task Delete(int id);

    }
}
