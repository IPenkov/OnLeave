using Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace OnLeave.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }
    }

    public class ManageUserViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "E-mail адрес")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Парола")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [Display(Name = "E-mail address")]
        [EmailAddress]
        public string Email { get; set; }
    }

    public class ResetPasswordModel
    {
        [Required]
        [Display(Name = "New Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "New password and confirmation does not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string ReturnToken { get; set; }

        [Required]
        [EmailAddress]        
        public string Email { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [StringLength(50, ErrorMessage = "Максимална дължина от 50 символа")]
        [Display(Name = "Име")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Фамилията е задължителна")]
        [StringLength(50, ErrorMessage = "Максимална дължина от 50 символа")]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [Required]
        [StringLength(50, ErrorMessage="Максимална дължина от 50 символа")]
        [EmailAddress]
        [Display(Name="E-Mail")]
        public string Email { get; set; }

        [StringLength(50, ErrorMessage="Максимална дължина от 50 символа")]
        [Display(Name="Телефон")]
        public string Telephone { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Минимална дължина на {0} {2}.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Парола")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Потвърди парола")]
        [Compare("Password", ErrorMessage = "Паролите не съвпадат.")]
        public string ConfirmPassword { get; set; }
    }

    /// <summary>
    /// Edit Account View Model
    /// </summary>
    public class EditViewModel
    {
        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        [Required]
        [StringLength(50, ErrorMessage = "First name exceed 50 characters")]
        [Display(Name = "Име")]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        [Required]
        [StringLength(50, ErrorMessage = "Last name exceed 50 characters")]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the telephone.
        /// </summary>
        /// <value>
        /// The telephone.
        /// </value>
        [StringLength(50, ErrorMessage = "Telephone exceed 50 characters")]
        [Display(Name = "Телефон")]
        public string Telephone { get; set; }
    }

    public class ChangePasswordModel
    {
        [Display(Name = "Парола")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = " {0}та трабва да бъде поне {2} символа.", MinimumLength = 6)]
        public string Password { get; set; }

        [Display(Name = "Потвърди парола")]
        [DataType(DataType.Password)]        
        [Compare("Password", ErrorMessage = "Паролите не съвпадат.")]
        public string ConfirmPassword { get; set; }
    }

    /// <summary>
    /// Add hotle, house etc. model
    /// </summary>
    public class UtilityBuildingModel
    {
        public UtilityBuildingModel()
        {
            this.PhotoIds = new int[0];
        }
        /// <summary>
        /// Hotel's id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [Display(Name = "Име")]
        [Required(AllowEmptyStrings=false, ErrorMessage="Име липсва")]
        [StringLength(50, ErrorMessage="Максимален брой {1} символи")]
        public string Name { get; set; }

        [Display(Name = "Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Name missing")]
        [StringLength(50, ErrorMessage = "Maximum {1} characters")]
        public string NameEN { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        [Display(Name = "Описание")]
        [Required(AllowEmptyStrings=false, ErrorMessage="Липсва описание")]
        [StringLength(255, ErrorMessage="Максимален брой {1} символи")]
        public string Description { get; set; }

        [Display(Name = "Description")]        
        [StringLength(255, ErrorMessage = "Maximum {1} characters")]
        public string DescriptionEN { get; set; }

        [Display(Name="Тип")]
        [Required(ErrorMessage="Тип сграда липсва")]
        public int UtilityBuildingTypeId { get; set; }

        [Display(Name = "Адрес")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Адрес липсва")]
        [StringLength(255, ErrorMessage = "Максимален брой {1} символи")]
        public string Address { get; set; }

        [Display(Name="Address")]
        [StringLength(255, ErrorMessage = "Maximum {1} characters")]
        public string AddressEN { get; set; }

        [Display(Name = "Лице за контакт")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Лице за контакт липсва")]
        [StringLength(255, ErrorMessage = "Максимален брой {1} символи")]
        public string ContactPerson { get; set; }

        [Display(Name="Contact Person")]
        [StringLength(255, ErrorMessage = "Maximum {1} characters ")]
        public string ContactPersonEN { get; set; }

        [Display(Name="Телефон")]
        [Required(AllowEmptyStrings = false, ErrorMessage="Телефон липсва")]
        [StringLength(50, ErrorMessage = "Максимален брой {1} символи")]
        public string PhoneNumber { get; set; }

        [Display(Name="Град")]
        [Required(ErrorMessage = "Град липсва")]
        public int? CityId { get; set; }

        [Display(Name="Кагегория")]
        public byte Rating { get; set; }

        /// <summary>
        /// Gets or sets the latitude.
        /// </summary>
        /// <value>
        /// The latitude.
        /// </value>
        public decimal Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude.
        /// </summary>
        /// <value>
        /// The longitude.
        /// </value>
        public decimal Longitude { get; set; }
        

        [FileSize(10240)]
        [FileTypes("jpg,jpeg,png")]
        public HttpPostedFileBase File { get; set; }

        /// <summary>
        /// Gets or sets the photo ids.
        /// </summary>
        /// <value>
        /// The photo ids.
        /// </value>
        public int[] PhotoIds { get; set; }

        /// <summary>
        /// Gets or sets the facilities.
        /// </summary>
        /// <value>
        /// The facilities.
        /// </value>
        public FacilityTypeModel[] Facilities { get; set; }

        /// <summary>
        /// Gets or sets the rooms.
        /// </summary>
        /// <value>
        /// The rooms.
        /// </value>
        public RoomModel[] Rooms { get; set; }

        public List<Period> Periods { get; set; }
    }

    /// <summary>
    /// Room Model
    /// </summary>
    public class RoomModel
    {
        /// <summary>
        /// Gets or sets the room type id.
        /// </summary>
        /// <value>
        /// The room type id.
        /// </value>
        public int RoomTypeId { get; set; }

        /// <summary>
        /// Gets or sets the name of the room type.
        /// </summary>
        /// <value>
        /// The name of the room type.
        /// </value>
        public string RoomTypeName { get; set; }

        /// <summary>
        /// Gets or sets the total number.
        /// </summary>
        /// <value>
        /// The total number.
        /// </value>
        public short TotalNumber { get; set; }
    }

    /// <summary>
    /// Facility Type Model
    /// </summary>
    public class FacilityTypeModel
    {
        /// <summary>
        /// Gets or sets the facility type id.
        /// </summary>
        /// <value>
        /// The facility type id.
        /// </value>
        public int FacilityTypeId { get; set; }

        /// <summary>
        /// Gets or sets the name of the facility type.
        /// </summary>
        /// <value>
        /// The name of the facility type.
        /// </value>
        public string FacilityTypeName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="FacilityTypeModel"/> is selected.
        /// </summary>
        /// <value>
        ///   <c>true</c> if selected; otherwise, <c>false</c>.
        /// </value>
        public bool Selected { get; set; }
    }

    /// <summary>
    /// City model
    /// </summary>
    public class CityModel
    {
        public CityModel()
        {
            // Bourgas coordinates by default
            this.longitude = 27.4717M;
            this.latitude = 42.4953M;
        }

        /// <summary>
        /// Gets or sets the city id.
        /// </summary>
        /// <value>
        /// The city identifier.
        /// </value>
        public int CityId { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [Required(ErrorMessage = "Име липсва")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the latitude.
        /// </summary>
        /// <value>
        /// The latitude.
        /// </value>
        public decimal latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude.
        /// </summary>
        /// <value>
        /// The longitude.
        /// </value>
        public decimal longitude { get; set; }        
    }
}

