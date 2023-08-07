
using Microsoft.AspNetCore.Http.HttpResults;
using TestApi.Dto;
using TestApi.File;
using TestApi.Helper;
using TestApi.Models;
using TestApi.Services.IServices;

namespace TestApi.Services
{
    public class Repository<T> : IRepository<T> where T : class
    {
        //******

        private List<Reservation> reservationList = new List<Reservation>();

        public Repository()
        {
            ReservationFile reservationFile = new ReservationFile();
            var file = reservationFile.ReadFileContent();

        }


        public async Task<ResultModel<T>> AddReservation(ReservationDTO reservationDTO)
        {
            ResultModel<T> result = new ResultModel<T>();

            try
            {
                Reservation newReservation = new Reservation
                {
                    Name = reservationDTO.Name,
                    StartLocation = reservationDTO.StartLocation,
                    EndLocation = reservationDTO.EndLocation
                };

                if (newReservation == null)
                {
                    result.SetResultProperties(false, null, new List<object> { "New reservation item cannot be empty!" });
                    return result;
                }

                reservationList.Add(newReservation);

                result.SetResultProperties(true, newReservation);
                return result;
            }
            catch (Exception ex)
            {
                result.SetResultProperties(false, null, new List<object> { ex.Message });
                return result;
            }
        }

        public Task<ResultModel<T>> DeleteReservation(int id)
        {
            try
            {
                ResultModel<T> resultModel = new ResultModel<T>();
                var reservationForDelete = reservationList.FirstOrDefault(x => x.Id == id);
                if (reservationForDelete == null)
                {
                    resultModel.SetResultProperties(false, null, new List<object> { "New reservation item cannot be empty!" });
                    return resultModel;
                }

                reservationList.Remove(reservationForDelete);
                return Task.CompletedTask;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Task<(IEnumerable<Reservation>, ResultModel<T>)> GetAllReservation()
        {
            try
            {
                IEnumerable<Reservation> reservations = reservationList;
                //public Task<ResultModel<IEnumerable<Reservation>>> GetAllReservation()
                //if (reservations.Count()==0)
                //{

                //    result.IsSuccess = false;
                //    result.Message = "dsvolj";
                //    return
                //}

                return Task.FromResult(reservations);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Task<(Reservation, ResultModel<T>)> GetReservationById(int id)
        {
            try
            {
                var reservation = reservationList.FirstOrDefault(x => x.Id == id);
                if (reservation == null)
                {
                    throw new ApplicationException($"Reservation with id {id} not found.");
                }
                return Task.FromResult(reservation);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Task<ResultModel<T>> UpdateReservation(int id, ReservationDTO reservationDTO)
        {
            try
            {
                var reservation = reservationList.Find(x => x.Id == id);
                if (reservation == null)
                {
                    throw new ApplicationException($"Reservation with id {id} not found.");

                }
                reservation.Name = reservationDTO.Name;
                reservation.StartLocation = reservationDTO.StartLocation;
                reservation.EndLocation = reservationDTO.EndLocation;
                return Task.CompletedTask;
            }
            catch (Exception)
            {

                throw;
            }
        }



    }
}
