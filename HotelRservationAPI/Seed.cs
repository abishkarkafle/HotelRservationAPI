using HotelRservationAPI.Data;

namespace HotelRservationAPI
{
    public class Seed
    {

        private DataContext Context;
        public Seed( DataContext _context)
        {
            Context = _context;
        }

        public void SeedDataContext()
        {

        }
    }
}
