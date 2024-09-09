namespace Api_intro_hw_33.DTOs.BookDTOs
{
    public record BookCreateDto(string name, decimal costPrice, decimal salePrice, int genreId);

}
