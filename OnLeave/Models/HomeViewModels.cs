using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using Common.Helpers;
namespace OnLeave.Models
{
    /// <summary>
    /// Search ViewModel
    /// </summary>
    public class SearchViewModel
    {

        /// <summary>
        /// Gets or sets the name to search for.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the city id.
        /// </summary>
        /// <value>
        /// The city id.
        /// </value>
        public int? CityId { get; set; }

        /// <summary>
        /// Gets or sets the utility building type id.
        /// </summary>
        /// <value>
        /// The utility building type id.
        /// </value>
        public int? UtilityBuildingTypeId { get; set; }

        /// <summary>
        /// Gets or sets the minimum prize.
        /// </summary>
        /// <value>
        /// The minimum prize.
        /// </value>
        public decimal? MinAmount { get; set; }

        /// <summary>
        /// Gets or sets the maximum prize.
        /// </summary>
        /// <value>
        /// The maximum prize.
        /// </value>
        public decimal? MaxAmount { get; set; }

        /// <summary>
        /// Gets or sets the rating.
        /// </summary>
        /// <value>
        /// The rating.
        /// </value>
        public int? Rating { get; set; }

        /// <summary>
        /// Gets or sets the top facilities types.
        /// </summary>
        /// <value>
        /// The top facilities types.
        /// </value>
        public Models.FacilityTypeModel[] TopFacilities { get; set; }

        /// <summary>
        /// Gets or sets the facilities types.
        /// </summary>
        /// <value>
        /// The facilities types.
        /// </value>
        public Models.FacilityTypeModel[] Facilities { get; set; }
    }

    /// <summary>
    /// Send Reservation Model
    /// </summary>
    public class SendReservationModel : IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SendReservationModel"/> class.
        /// </summary>
        public SendReservationModel()
        {
            this.StartDate = this.EndDate = System.DateTime.Now.Date;


            // Format the datetime in an inventive way
            // Format the datetime in an inventive way
            var timestamp = DateTime.UtcNow.ToString("ffffHHMMyytssddmm");
            this.PWHJVT = timestamp;            
            byte[] salt = HashHelper.CreateSalt();            
            this.BSRGVS = Convert.ToBase64String(salt);

            var toHash = String.Format("{0}{1}", 
                timestamp,
                HttpContext.Current.Request.UserHostAddress);
            
            // Optionally, represent the hash value as a base64-encoded string, 
            // For example, if you need to display the value or transmit it over a network.
            this.BHVESH = HashHelper.Hash(toHash, this.BSRGVS);            
        }

        /// <summary>
        /// Gets or sets the utility building id.
        /// </summary>
        /// <value>
        /// The utility building id.
        /// </value>
        public int UtilityBuildingId { get; set; }

        /// <summary>
        /// Gets or sets the name of the client.
        /// </summary>
        /// <value>
        /// The name of the client.
        /// </value>
        [Required(ErrorMessage="Име липсва")]        
        public string ClientName { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        [Required(ErrorMessage="Поща липсва", AllowEmptyStrings=false)]
        [EmailAddress(ErrorMessage="Адрес невалиден")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the telephone.
        /// </summary>
        /// <value>
        /// The telephone.
        /// </value>
        [Required(ErrorMessage="Телефон липсва")]
        public string Tel { get; set; }

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        /// <value>
        /// The start date.
        /// </value>
        [Required]
        [DisplayFormat(DataFormatString = "dd/MM/yyyy")]
        public System.DateTime? StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        /// <value>
        /// The end date.
        /// </value>
        [Required]
        [DisplayFormat(DataFormatString = "dd/MM/yyyy")]
        public System.DateTime? EndDate { get; set; }

        /// <summary>
        /// Gets or sets the content of the reservation.
        /// </summary>
        /// <value>
        /// The content of the reservation.
        /// </value>
        [Required(ErrorMessage="Описание липсва", AllowEmptyStrings=false)]        
        public string ReservationDescription { get; set; }

        // ...existing properties

        /// <summary>
        /// Gets or sets the honeypot.
        /// </summary>
        /// <value>
        /// The honeypot.
        /// </value>
        public string SGEGEH { get; set; }

        /// <summary>
        /// Gets or sets the TimeStamp.
        /// </summary>
        /// <value>
        /// The TimeStamp.
        /// </value>
        public string PWHJVT { get; set; }

        /// <summary>
        /// Gets or sets the Salt.
        /// </summary>
        /// <value>
        /// The Salt.
        /// </value>
        public string BSRGVS { get; set; }

        /// <summary>
        /// Gets or sets the Hashed.
        /// </summary>
        /// <value>
        /// The Hashed.
        /// </value>
        public string BHVESH { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var failedValidationResult = new ValidationResult("An error occured");
            var failedResult = new[] { failedValidationResult };

            if (!String.IsNullOrEmpty(this.SGEGEH))
            {
                return failedResult;
            }

            DateTime timestamp;
            if (!DateTime.TryParseExact(this.PWHJVT, "ffffHHMMyytssddmm",
                                        null, DateTimeStyles.None, out timestamp))
            {
                return failedResult;
            }

            //Check timestamp is within the last 20 minutes
            if (DateTime.UtcNow.AddMinutes(-20) > timestamp)
            {
                return failedResult;
            }

            // Remember to use the same format you did when building the hash
            var toHash = String.Format("{0}{1}", this.PWHJVT, HttpContext.Current.Request.UserHostAddress);
            //var toHash = String.Format("H{0}A{1}S{2}H", this.Timestamp, Request.UserHostAddress, this.Id);

            HashAlgorithm hashAlg = new SHA256CryptoServiceProvider();
            // Convert the data to hash to an array of Bytes.
            byte[] bytValue = System.Text.Encoding.UTF8.GetBytes(String.Concat(toHash, this.BSRGVS));

            var hashed = hashAlg.ComputeHash(bytValue);

            //Check the hash and salt have not been tampered with
            // If anything has changed or been altered, the hashes will not match
            if (this.BHVESH.Equals(Convert.ToBase64String(hashed)))
            {
                // Hashes match, all is good
                return new[] { ValidationResult.Success };
            }

            return failedResult;
        }
     
    }
}