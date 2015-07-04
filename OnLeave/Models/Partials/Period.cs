using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnLeave.Models
{
    public partial class Period
    {
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString="dd/MM/yyyy")]
        public Nullable<System.DateTime> StartDate
        {
            get
            {
                return this.FromDate;
            }

            set
            {
                this.FromDate = value;
            }
        }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "dd/MM/yyyy")]
        public Nullable<System.DateTime> EndDate
        {
            get
            {
                return this.ToDate;
            }

            set
            {
                this.ToDate = value;
            }
        }
    }
}