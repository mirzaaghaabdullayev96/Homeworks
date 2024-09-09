namespace Api_intro_hw_33.DTOs.BookDTOs
{
    public record BookGetDto(int id, string name, decimal costPrice, decimal salePrice, int genreId, string genreName );
}
