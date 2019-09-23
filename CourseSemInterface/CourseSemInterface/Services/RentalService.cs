using System;
using CourseSemInterface.Entities;

namespace CourseSemInterface.Services
{
    class RentalService
    {
        public double  PricePerHour { get; private set; }                                    //prop
        public double PricePerDay { get; private set; }

        private ITaxService _taxService;
        public RentalService(double pricePerHour, double pricePerDay, ITaxService taxService)                       //construtor
        {
            PricePerHour = pricePerHour;
            PricePerDay = pricePerDay;
            _taxService = taxService;
        }

        public void ProcessInvoice(CarRental carRental)                                    //método
        {
            TimeSpan duration = carRental.Finish.Subtract(carRental.Start);

            double basicpayment = 0.0;
            if (duration.TotalHours <= 12.0)
            {
                basicpayment = PricePerHour * Math.Ceiling(duration.TotalHours);
            }
            else
            {
                basicpayment = PricePerDay * Math.Ceiling(duration.TotalDays);
            }

            double tax = _taxService.Tax(basicpayment);

            carRental.Invoice = new Invoice(basicpayment, tax);
        }

    }
}
