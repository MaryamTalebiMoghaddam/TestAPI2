using TestApi.Dto;
using TestApi.Models;

namespace TestApi.Services.IServices
{
    public interface IRepository<T> where T : class
    {
        Task<(IEnumerable<Reservation>, ResultModel<T>)> GetAllReservation();
        Task<(Reservation, ResultModel<T>)> GetReservationById(int id);
        Task<ResultModel<T>> DeleteReservation(int id);
        Task<ResultModel<T>> AddReservation(ReservationDTO reservationDTO);
        Task<ResultModel<T>> UpdateReservation(int id,ReservationDTO reservationDTO); 

    }
}
