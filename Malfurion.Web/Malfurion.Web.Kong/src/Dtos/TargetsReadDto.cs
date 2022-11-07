namespace Malfurion.Web.Kong.Dtos;

internal class TargetsReadDto
{
    public int Total { get; set; }
    public List<TargetReadDto> Data { get; set; } = new List<TargetReadDto>();
}