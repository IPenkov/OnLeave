namespace OnLeave.Models.BusinessEntities
{
    #region [Using Directives]

    using System;
    using System.ComponentModel.DataAnnotations;

    #endregion

    public class Offer
    {
        public int OfferId { get; set; }
        
        [Required(ErrorMessage="Описание липсва", AllowEmptyStrings=false)]
        public string Description { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "dd/MM/yyyy")]
        public Nullable<System.DateTime> StartDate { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "dd/MM/yyyy")]
        public Nullable<System.DateTime> EndDate { get; set; }

        [Required(ErrorMessage="Намаление липсва")]
        [Range(1, 99, ErrorMessage="Намаление от 1 до 99 процента")]
        public Nullable<byte> Discount { get; set; }
    }
}