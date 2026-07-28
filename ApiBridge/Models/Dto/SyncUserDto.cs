
namespace ApiBridge.Models.Dto
{
    public class SyncUserDto

    {
        public string? External_id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Payment_status { get; set; }
        public SyncUserDto()
        {
        }
    }
}

