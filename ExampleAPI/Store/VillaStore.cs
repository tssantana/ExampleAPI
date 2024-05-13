using ExampleAPI.Models.Dtos;
using System.Runtime.CompilerServices;

namespace ExampleAPI.Store
{
    public class VillaStore
    {
        public static List<VillaDto> villaList = new List<VillaDto>
        {
            new VillaDto {Id =1, Name= "Villa do Mozão"},
            new VillaDto {Id =2, Name= "Villa do Nicklaus"}
        };

        public static IEnumerable<VillaDto> GetVillas()
        {
            return villaList;
        }
    }
}
