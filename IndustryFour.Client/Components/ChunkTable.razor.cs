using IndustryFour.Shared.Dtos.Chunk;
using Microsoft.AspNetCore.Components;

namespace IndustryFour.Client.Components
{
	public partial class ChunkTable
	{
		[Parameter]
		public List<ChunkResultDto> Chunks { get; set; }
	}
}
