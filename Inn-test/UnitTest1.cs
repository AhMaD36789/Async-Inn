using Async_Inn.Models.Services;
using Async_Inn.Models;
using AsyncInnTest;

namespace AsyncInnTest
{
    public class UnitTest1 : Mock
    {

        // room and amenity
        [Fact]
        public async void AddAmenityToRoom()
        {
            var room = await CreateAndSaveRoom();

            var amenity = await CreateAndSaveAmenity();

            var roomAmenities = new RoomService(_db);

            await roomAmenities.AddAmenityToRoom(room.ID, amenity.ID);

            var result = await roomAmenities.GetRoom(room.ID);

            Assert.Contains(result.Amenities, x => x.ID == amenity.ID);
        }

        [Fact]
        public async void RemoveAmenityFromRoom()
        {
            var room = await CreateAndSaveRoom();

            var amenity = await CreateAndSaveAmenity();

            var roomAmenities = new RoomService(_db);

            await roomAmenities.AddAmenityToRoom(room.ID, amenity.ID);

            await roomAmenities.RemoveAmenityFromoRoom(room.ID, amenity.ID);

            var result = await roomAmenities.GetRoom(room.ID);

            Assert.DoesNotContain(result.Amenities, x => x.ID == amenity.ID);
        }

        [Fact]
        public async Task HotelService_Should_Add_Hotel_Room()
        {
            // Arrange
            var room = await CreateAndSaveRoom();
            var hotel = await CreateAndSaveHotel();

            var hotelRoom = new HotelRoomDTO()
            {
                HotelID = hotel.ID,
                RoomNumber = 10,
                Rate = 5.5M,
                PetFriendly = true,
                RoomID = room.ID
            };

            var hotelRoomService = new HotelRoomService(_db, _room);

            // Act
            await hotelRoomService.Create(hotelRoom, hotel.ID);

            var hotelService = new HotelService(_db);


            // Assert
            var result = await hotelService.GetHotel(hotel.ID);
            Assert.Contains(result.HotelRooms, x => x.RoomID == room.ID && x.HotelID == hotel.ID);
        }

        [Fact]
        public async void UpdateANewHotelRoom()
        {

            var room = await CreateAndSaveRoom();

            var hotel = await CreateAndSaveHotel();

            var hotelRoom = new HotelRoomDTO()
            {
                HotelID = hotel.ID,
                RoomNumber = 10,
                Rate = 5.5M,
                PetFriendly = true,
                RoomID = room.ID
            };

            var hotelRoomService = new HotelRoomService(_db, _room);

            await hotelRoomService.Create(hotelRoom, hotel.ID);

            var hotelService = new HotelService(_db);


            var result = await hotelService.GetHotel(hotel.ID);

            Assert.Contains(result.HotelRooms, x => x.RoomID == room.ID);

            Assert.Contains(result.HotelRooms, x => x.HotelID == hotel.ID);

            var room2 = await CreateAndSaveRoom2();

            var newHotelRoom = new HotelRoomDTO()
            {
                HotelID = hotel.ID,
                RoomNumber = 10,
                Rate = 9.5M,
                PetFriendly = false,
                RoomID = room2.ID
            };

            await hotelRoomService.UpdateHotelRoom(hotel.ID, newHotelRoom.RoomNumber, newHotelRoom);

            var result2 = await hotelService.GetHotel(hotel.ID);

            Assert.Contains(result2.HotelRooms, x => x.HotelID == hotel.ID);

            Assert.Contains(result2.HotelRooms, x => x.RoomID == room2.ID);

            Assert.DoesNotContain(result2.HotelRooms, x => x.RoomID == room.ID);
        }

        [Fact]
        public async void RemoveHotelRooms()
        {
            var room = await CreateAndSaveRoom();

            var hotel = await CreateAndSaveHotel();

            var hotelRoom = new HotelRoomDTO()
            {
                HotelID = hotel.ID,
                RoomNumber = 10,
                Rate = 5.5M,
                PetFriendly = true,
                RoomID = room.ID
            };

            var hotelRoomService = new HotelRoomService(_db, _room);

            await hotelRoomService.Create(hotelRoom, hotel.ID);

            var hotelService = new HotelService(_db);


            var result = await hotelService.GetHotel(hotel.ID);

            Assert.Contains(result.HotelRooms, x => x.RoomID == room.ID);

            Assert.Contains(result.HotelRooms, x => x.HotelID == hotel.ID);

            await hotelRoomService.Delete(hotel.ID, hotelRoom.RoomNumber);
            result = await hotelService.GetHotel(hotel.ID);
            Assert.DoesNotContain(result.HotelRooms, x => x.RoomID == room.ID);

            Assert.DoesNotContain(result.HotelRooms, x => x.HotelID == hotel.ID);
        }

        [Fact]
        public async void AddHotelAndRemoveIt()
        {
            var hotel = await CreateAndSaveHotel();
            var hotel2 = await CreateAndSaveHotel2();

            var hotelService = new HotelService(_db);

            var result = await hotelService.GetHotels();

            Assert.Contains(result, x => x.ID == hotel.ID);
            Assert.Contains(result, x => x.ID == hotel2.ID);

            await hotelService.Delete(hotel.ID);

            result = await hotelService.GetHotels();

            Assert.DoesNotContain(result, x => x.ID == hotel.ID);
            Assert.Contains(result, x => x.ID == hotel2.ID);

        }
    }
}