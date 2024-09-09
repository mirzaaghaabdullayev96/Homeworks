namespace Api_intro_hw_33.DTOs.BookDTOs
{
    public record BookUpdateDto(string name, decimal costPrice, decimal salePrice, int genreId);
}
