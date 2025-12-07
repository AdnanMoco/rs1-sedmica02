namespace MojaApp.API.Controllers.Dtos
{
    public record StudentGetbyIdResponse
        (
        int StudentId,
        string Ime,
        string Prezime,
        string SlikaStudenta,
        StudentGetbyIdResponseOpstina? Opstina
        );

    public record StudentGetbyIdResponseOpstina
        (
        string Naziv,
        string Ptt

        );
    
}
