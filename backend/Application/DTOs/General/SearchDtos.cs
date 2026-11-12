using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.General;

public class SearchHotelsDto
{
    [Required( ErrorMessage = "Город обязателен для поиска!" )]
    [StringLength( 100, MinimumLength = 2, ErrorMessage = "Название города должно быть от 2 до 100 символов!" )]
    public string City { get; set; } = string.Empty;
}

public class SearchRoomsDto
{
    [Required( ErrorMessage = "Необходимо указать дату заезда!" )]
    public DateOnly Arrival { get; set; }

    [Required( ErrorMessage = "Необходимо указать дату выезда!" )]
    public DateOnly Departure { get; set; }
}